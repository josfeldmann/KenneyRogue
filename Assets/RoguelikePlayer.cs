using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StatUnit : Unit {
    [Header("StatInfo")]
    [HideInInspector] public StatWrapper currentStats = new StatWrapper();
    public StatWrapper baseStats = new StatWrapper();
    public StatWrapper bonusStats = new StatWrapper();


    public virtual void CalculateStats() {
        currentStats = baseStats + bonusStats;
        currentStats.maxHP += currentStats.strength * 10;
        currentStats.maxMana += currentStats.intelligence * 10;
        currentStats.magicAmp += currentStats.intelligence;
        currentStats.manaRegen += currentStats.intelligence;
        currentStats.attackSpeed += currentStats.agility;
        currentStats.moveSpeed += currentStats.agility;
        currentStats.attackDamage += (currentStats.agility + currentStats.strength) / 4;
    }
}

public class RoguelikePlayer : StatUnit {



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
    public List<ItemObject> items = new List<ItemObject>();

    [Header("CharacterController")]
    public StateMachine<RoguelikePlayer> statemachine;
    public Camera cam;
    public SpriteRenderer sRenderer;
    public InputManager manager;
    public Transform rightHand;

    public LayerMask enemyTargettingLayer;

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


    
    public float walkSpeed = 5f;
    public Transform abilitySlotTransform;
    public Transitioner transitioner;
    public HealthManager healthManager;
    public GameObject pausedPrompt, deathPrompt;

    [HideInInspector] public bool setup = false;

    public void Setup() {
        canWin = true;
        deathPrompt.gameObject.SetActive(false);
        pausedPrompt.gameObject.SetActive(false);
        transitioner.IntroTransition();
        healthManager.UpdateHP();
        
        statemachine = new StateMachine<RoguelikePlayer>(new RoguelikePlayerMoveState(), this);
        onTakeDamage += healthManager.UpdateHP;
        onTakeDamage += FlashForDamage;
        onDeath += Die;
        setup = true;
        SetWeapon(weaponObject);
        SetAbilities();

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

        foreach (AbilityObject a in abilityObjects) {
            Ability ab = Instantiate(a.abilityPrefab, abilityGroupingingobject);
            abilities.Add(ab);
            ab.transform.localPosition = Vector3.zero;
            ab.transform.localEulerAngles = Vector3.zero;
            ab.CalculateCooldown();
            AbilityButton button = Instantiate(abilityButtonprefab, buttonGroupingObject);
            ab.Set(button, a, enemyTargettingLayer);
            button.SetAbility(ab);
            
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

   

    public void FlashForDamage() {
        canBeHurt = false;
        StartCoroutine(DamageFlash());
    }


    private IEnumerator DamageFlash() {

        canBeHurt = false;

        for (int i = 0; i < 3; i++) {
            yield return new WaitForSeconds(0.15f);
            sRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            sRenderer.enabled = true;
        }
        canBeHurt = true;

    }

    public void Die() {
        currentHP = 0;
        canPause = false;
        deathPrompt.SetActive(true);
        pausedPrompt.SetActive(false);
        GameManager.UnPause();
        canBeHurt = false;
        statemachine.ChangeState(new RoguelikePlayerDoNothingState());
      
        StartCoroutine(Dienumerator());
    }

    IEnumerator Dienumerator() {

        yield return null;

    }


    public bool canWin = true;

 

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.layer == Layers.hurtPlayer) {
            if (canBeHurt) {
                pushBack = -(other.contacts[0].point - new Vector2(transform.position.x, transform.position.y)).normalized * lavaPushBack;
                TakeDamage(1);
                print("takeDamage");
            }
        }
    }


    public void AimRightHand() {




        Vector3 mousePoint = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePoint.z = 0;

        Vector3 dir = (rightHand.position - mousePoint).normalized;
        if (weapon && weapon.flipCorrect) {

            if (dir.x > 0) {
                weapon.spriteRenderer.flipY = true;
            }
            if (dir.x < 0) {
                weapon.spriteRenderer.flipY = false;
            }
        }
        rightHand.right = -dir;
        abilityGroupingingobject.right = -(abilityGroupingingobject.position - mousePoint).normalized;


    }

}


public class RoguelikePlayerMoveState : State<RoguelikePlayer> {

    public override void Update(StateMachine<RoguelikePlayer> obj) {
        if (obj.target.manager.horizontal != 0 || obj.target.manager.vertical != 0) {
            obj.target.rb.velocity = new Vector2(obj.target.manager.horizontal, obj.target.manager.vertical) * obj.target.walkSpeed;
        } else {
            obj.target.rb.velocity = Vector2.zero;
        }

        obj.target.rb.velocity += obj.target.pushBack;
        obj.target.DecreasePushBack();

        obj.target.AimRightHand();

        if (obj.target.weapon != null) {
            if (obj.target.manager.firePressed) {
                obj.target.weapon.FireDown();
            } else if (obj.target.manager.fireheld) {
                obj.target.weapon.FireHeld();
            } else if (obj.target.manager.fireReleased) {
                obj.target.weapon.FireUp();
            }
        }

        if (obj.target.abilities.Count >= 1) {
            if (obj.target.manager.skill1Down) {
                obj.target.abilities[0].SkillDown();
            } else if (obj.target.manager.skill1Held) {
                obj.target.abilities[0].SkillHeld();
            } else if (obj.target.manager.skill1Up) {
                obj.target.abilities[0].SkillUp();
            }
        }

        if (obj.target.abilities.Count >= 2) {
            if (obj.target.manager.skill2Down) {
                obj.target.abilities[1].SkillDown();
            } else if (obj.target.manager.skill2Held) {
                obj.target.abilities[1].SkillHeld();
            } else if (obj.target.manager.skill2Up) {
                obj.target.abilities[1].SkillUp();
            }
        }

        if (obj.target.abilities.Count >= 3) {
            if (obj.target.manager.skill3Down) {
                obj.target.abilities[2].SkillDown();
            } else if (obj.target.manager.skill3Held) {
                obj.target.abilities[2].SkillHeld();
            } else if (obj.target.manager.skill3Up) {
                obj.target.abilities[2].SkillUp();
            }
        }





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

