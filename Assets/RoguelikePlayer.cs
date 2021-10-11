using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class StatUnit : Unit {
    [Header("StatInfo")]
    [HideInInspector] public StatWrapper currentStats = new StatWrapper();
    public StatWrapper baseStats = new StatWrapper();
    public StatWrapper bonusStats = new StatWrapper();
    public VoidDelegate onChangeStats;
    public List<ItemObject> items = new List<ItemObject>();
    public int gold = 0, xp = 0;
    public VoidDelegate goldXPChanged;
    public virtual void CalculateStats() {
        CalculateStats(false);
    }

    private void CalculateStats(bool hp) {
        currentStats = baseStats + bonusStats;
        foreach (ItemObject item in items) {
            currentStats.AddItemStats(item);
        }
        currentStats.maxHP += currentStats.strength * 10;
        currentStats.maxMana += currentStats.intelligence * 10;
        currentStats.manaRegen += currentStats.intelligence;
        currentStats.attackSpeed += currentStats.agility;
        currentStats.moveSpeed += currentStats.agility;
        currentStats.attackDamage += (currentStats.agility + currentStats.strength) / 4;
        currentStats.spellAmp += currentStats.intelligence;
        maxHp = currentStats.maxHP;
        if (hp) currentHP = maxHp;
        if (onChangeStats != null) onChangeStats.Invoke();
        
    }

    public void CalculateWithHP() {
        CalculateStats(true);
    }

    public bool CanSpendGold(int goldAmount) {
        if (gold >= goldAmount) {
            return true;
        }
        return false;
    }

    public bool SpendGold(int goldAmount) {
        if (gold >= goldAmount) {
            gold -= goldAmount;
            goldXPChanged.Invoke();
            return true;
        }
        return false;
    }

}

public class RoguelikePlayer : StatUnit {


    public bool inCustomMovement = false;
    
    public RaceObject raceObject;
    [Header("Weapon")]
    public Transform buttonGroupingObject;
    public Transform abilityGroupingingobject;
    public WeaponObject weaponObject;
    public AbilityButton weaponButton;
    public Weapon weapon;
    
    
    [Header("Abilities")]
    public List<AbilityObject> abilityObjects = new List<AbilityObject>();
    public AbilityButton abilityButtonprefab;
    public List<AbilityButton> buttons = new List<AbilityButton>();
    public List<Ability> abilities;

    [Header("Items")]
    public Transform itemTransform;
    public ItemIcon itemIconPrefab;
    public List<ItemIcon> itemIcons;

    [Header("CharacterController")]
    public StateMachine<RoguelikePlayer> statemachine;
    public float baseMoveSpeed = 5f;
    public SpriteRenderer sRenderer;
    public InputManager manager;
    public Transform rightHand;
    public Collider2D bodyColiider;
    public LayerMask enemyTargettingLayer;
    public LayerMask impassableLayers;

    public VoidDelegate onGoldXpChanged;

    [Header("UI")]
    public GameObject hotBar, itemBar;

    internal void SetPlayerNextScene(string toGoTo) {


        if (!canWin) return;
        StartCoroutine(LevelPhaseOut(toGoTo));

    }


    public override void CalculateStats() {
        base.CalculateStats();
        foreach (Ability a in abilities) {
            a.CalculateCooldown();
        }
    }


    public IEnumerator LevelPhaseOut(string toGoTo) {



        yield return new WaitForSeconds(1f);
        transitioner.EndTransition();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(toGoTo);
    }

    internal Vector3 GetMousePosition() {
        Vector3 vec = MapCam.current.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        vec.z = 0;
        return vec;
    }

    public Transform abilitySlotTransform;
    public Transitioner transitioner;
    public HealthManager healthManager;
    public GameObject pausedPrompt, deathPrompt;

    [HideInInspector] public bool setup = false;

    public void Setup() {
        canWin = true;
        deathPrompt.gameObject.SetActive(false);
        pausedPrompt.gameObject.SetActive(false);
        //transitioner.IntroTransition();
        healthManager.UpdateHP(0);
        
        statemachine = new StateMachine<RoguelikePlayer>(new RoguelikePlayerMoveState(), this);
        onTakeDamage += healthManager.UpdateHP;
       // onTakeDamage += FlashForDamage;
        onDeath += Die;
        setup = true;
        SetWeapon(weaponObject);
        SetAbilities();
        SetItems();
        SetRace(raceObject);
        CalculateWithHP();
        sRenderer.enabled = true;
        sRenderer.sprite = raceObject.raceSprite;
        hotBar.gameObject.SetActive(true);
        itemBar.gameObject.SetActive(true);
    }

    

    public void SetRace(RaceObject race) {
        raceObject = race;
        baseStats = raceObject.racialStats;
        sRenderer.sprite = race.raceSprite;
    }

    public void SetWeapon(WeaponObject weaponO) {
        if (weapon) {
            Destroy(weapon.gameObject);
        }
        weapon = Instantiate(weaponO.weaponPrefab, rightHand);
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localEulerAngles = Vector3.zero;
        weapon.weaponObject = weaponObject;
        weapon.SetTargetLayer(enemyTargettingLayer);
        weapon.Set(weaponButton, weaponO, this);
        weaponButton.SetWeapon(weapon);
    }

    public void SetAbilities() {
        foreach (AbilityButton a in buttons) {
            Destroy(a.gameObject);
        }
        foreach (Ability a in abilities) {
            Destroy(a.gameObject);
        }

        abilities = new List<Ability>();
        int amt = 0;
        foreach (AbilityObject a in abilityObjects) {
            Ability ab = Instantiate(a.abilityPrefab, abilityGroupingingobject);
            AbilityButton button = Instantiate(abilityButtonprefab, buttonGroupingObject);
            ab.Set(button, a, enemyTargettingLayer);
            ab.Setup();
            abilities.Add(ab);
            ab.player = this;
            ab.transform.localPosition = Vector3.zero;
            ab.transform.localEulerAngles = Vector3.zero;
            ab.CalculateCooldown();
            button.SetAbility(ab);
            if (amt == 0) {

            } else if (amt == 1) {

            } else if (amt == 2) {

            }
            amt++;
            
        }
    }

    public void SetItems() {
        foreach (ItemIcon itemIcon in itemIcons) {
            itemIcon.gameObject.SetActive(false);
        }
        if (items.Count <= 0) {
            itemTransform.gameObject.SetActive(false);
        } else {
            itemTransform.gameObject.SetActive(true) ;
        }
        for (int i = itemIcons.Count; i < items.Count; i++) {
            ItemIcon iIcon = Instantiate(itemIconPrefab, itemTransform);
            itemIcons.Add(iIcon);
        }
        for (int i = 0; i < items.Count; i++) {
            itemIcons[i].gameObject.SetActive(true);
            itemIcons[i].Set(items[i]);
        }
    }


    public void ResetLevel() {
        StopAllCoroutines();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    bool canPause = true;

    private void Update() {
        if (!setup) {
            return;
        }
        if (canPause) {
            if (manager.pauseButtonDown) {
                if (GameManager.paused) {
                    pausedPrompt.gameObject.SetActive(false);
                    GameManager.UnPause();
                } else {
                    GameManager.Pause();
                    pausedPrompt.gameObject.SetActive(true);
                }
            }
        }
        if (manager.resetButtonDown) {
            ResetLevel();
        }

        if (GameManager.paused) return;
        statemachine.Update();
    }

    public float pushBackDecreaseSpeed = 5f;
    [HideInInspector] public Vector2 pushBack;
    public float lavaPushBack = 10f;
    public void DecreasePushBack() {

        if (pushBack != Vector2.zero) {
            pushBack = Vector2.MoveTowards(pushBack, Vector2.zero, Time.deltaTime * pushBackDecreaseSpeed);
        }

    }

  

    public int currentNumberOfWeapons = 0;

   

    public void FlashForDamage(float amt) {
        canTakeDamage = false;
        StartCoroutine(DamageFlash());
    }


    private IEnumerator DamageFlash() {

        canTakeDamage = false;

        for (int i = 0; i < 3; i++) {
            yield return new WaitForSeconds(0.15f);
            sRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            sRenderer.enabled = true;
        }
        canTakeDamage = true;

    }

    public void Die() {
        currentHP = 0;
        canPause = false;
        deathPrompt.SetActive(true);
        pausedPrompt.SetActive(false);
        GameManager.UnPause();
        canTakeDamage = false;
        statemachine.ChangeState(new RoguelikePlayerDoNothingState());
      
        StartCoroutine(Dienumerator());
    }

    IEnumerator Dienumerator() {

        yield return null;

    }


    public bool canWin = true;

 

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.layer == Layers.hurtPlayer) {
            if (canTakeDamage) {
               // pushBack = -(other.contacts[0].point - new Vector2(transform.position.x, transform.position.y)).normalized * lavaPushBack;
                TakeDamage(1);
                print("takeDamage");
            }
        }
    }


    public void AimRightHand() {
        Vector3 mousePoint = MapCam.current.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePoint.z = 0;

        Vector3 dir = (rightHand.position - mousePoint).normalized;
        if (weapon && weapon.flipCorrect) {

            if (mousePoint.x <= transform.position.x) {
                weapon.spriteRenderer.flipY = true;
            } else  {
                weapon.spriteRenderer.flipY = false;
            }
        }
        rightHand.right = -dir;
        abilityGroupingingobject.right = -(abilityGroupingingobject.position - mousePoint).normalized;
    }

    public void AbilityWeaponFireCheck() {
        if (weapon != null) {
            if (manager.firePressed) {
                weapon.FireDown();
            } else if (manager.fireheld) {
                weapon.FireHeld();
            } else if (manager.fireReleased) {
                weapon.FireUp();
            }
        }

        if (abilities.Count >= 1) {
            if (manager.skill1Down) {
                abilities[0].SkillDown();
            } else if (manager.skill1Held) {
                abilities[0].SkillHeld();
            } else if (manager.skill1Up) {
                abilities[0].SkillUp();
            }
        }

        if (abilities.Count >= 2) {
            if (manager.skill2Down) {
                abilities[1].SkillDown();
            } else if (manager.skill2Held) {
                abilities[1].SkillHeld();
            } else if (manager.skill2Up) {
                abilities[1].SkillUp();
            }
        }

        if (abilities.Count >= 3) {
            if (manager.skill3Down) {
                abilities[2].SkillDown();
            } else if (manager.skill3Held) {
                abilities[2].SkillHeld();
            } else if (manager.skill3Up) {
                abilities[2].SkillUp();
            }
        }
    }



    public void MakeInvincibleAndImpassable() {
        bodyColiider.enabled = false;
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        canTakeDamage = false;
    }

    public void MakeNotInvicible() {
        bodyColiider.enabled = true;
        rb.isKinematic = false;
        rb.velocity = Vector3.zero;
        canTakeDamage = true;
    }

    internal void AddItem(ItemObject item) {
        items.Add(item);
        SetItems();
        CalculateStats();
    }



    public void DisablePlayerWithoutUI() {
        sRenderer.enabled = false;
        if (weapon) weapon.gameObject.SetActive(false);
        hotBar.gameObject.SetActive(false);
        itemBar.gameObject.SetActive(false);
        deathPrompt.gameObject.SetActive(false);
        pausedPrompt.gameObject.SetActive(false);
        statemachine = new StateMachine<RoguelikePlayer>(new RoguelikePlayerDisabledState(), this);
    }

    public void Reenable() {
        weapon.gameObject.SetActive(true);
        sRenderer.enabled = true;
        statemachine = new StateMachine<RoguelikePlayer>(new RoguelikePlayerMoveState(), this);
    }


    public void DisablePlayerWithUI() {
        if (weapon) weapon.gameObject.SetActive(false);
        sRenderer.enabled = false;
        statemachine = new StateMachine<RoguelikePlayer>(new RoguelikePlayerDisabledState(), this);
    }
}


public class RoguelikePlayerMoveState : State<RoguelikePlayer> {

    public override void Update(StateMachine<RoguelikePlayer> obj) {
        if (obj.target.manager.horizontal != 0 || obj.target.manager.vertical != 0) {
            obj.target.rb.velocity = new Vector2(obj.target.manager.horizontal, obj.target.manager.vertical) * obj.target.baseMoveSpeed * (obj.target.currentStats.moveSpeed/100f);
        } else {
            obj.target.rb.velocity = Vector2.zero;
        }

        obj.target.rb.velocity += obj.target.pushBack;
        obj.target.DecreasePushBack();

        obj.target.AimRightHand();
        obj.target.AbilityWeaponFireCheck();
        
    }


   

}

public class RoguelikePlayerDisabledState : State<RoguelikePlayer> {
    public override void Enter(StateMachine<RoguelikePlayer> obj) {
        obj.target.rb.velocity = new Vector2();
    }
}

public class RoguelikePlayerDoNothingState : State<RoguelikePlayer> {
    public override void Update(StateMachine<RoguelikePlayer> obj) {
        obj.target.rb.velocity = obj.target.pushBack;
        obj.target.DecreasePushBack();
        if (obj.target.sRenderer.transform.parent == obj.target.transform && obj.target.pushBack == Vector2.zero) {
            obj.target.sRenderer.transform.SetParent(obj.target.transform.parent);
        }
    }
}

