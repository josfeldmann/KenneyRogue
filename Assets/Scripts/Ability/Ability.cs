using UnityEngine;

public class Ability : MonoBehaviour {

    [HideInInspector]public RoguelikePlayer player;
    [HideInInspector]public LayerMask enemyLayerMask;
    [HideInInspector]public AbilityButton button;
    [HideInInspector]public AbilityObject abilityObject;
    [HideInInspector]public bool onCooldown = false;
    [HideInInspector]public float cooldown;
    [HideInInspector]public float currentCooldownAmount = 0;

    public void Set(AbilityButton button, AbilityObject aObject, LayerMask mask) {
        this.button = button;
        this.abilityObject = aObject;
        onCooldown = false;
        currentCooldownAmount = 0;
        button.radialFill.fillAmount = 0;
        gameObject.name = aObject.GetName();
        enemyLayerMask = mask;
    }

    public void SetCoolDown() {
        
    }

    public void SetOnCooldown() {
        currentCooldownAmount = 0;
        button.radialFill.fillAmount = 1;
        onCooldown = true;
    }

    public virtual void Setup() {

    }

    public virtual void SkillDown() {
        print(gameObject.name + " Down");
    }

    public virtual void SkillHeld() {
        print(gameObject.name + " Held");
    }

    public virtual void SkillUp() {
        print(gameObject.name + " Up");
    }

    public virtual void CalculateCooldown() {
        cooldown = 5;
    }

    private void Update() {
        if (onCooldown) {
            currentCooldownAmount += Time.deltaTime;
            button.radialFill.fillAmount = (1 - (currentCooldownAmount / cooldown));
            if (currentCooldownAmount >= cooldown) {
                onCooldown = false;
                button.radialFill.fillAmount = 0;
            } 
        }
    }
}
