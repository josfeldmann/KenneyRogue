using System;
using UnityEngine;

public static class Colors {

    public static Color32 green = new Color32(104,142,38,255);
    public static Color32 red = new Color32(230, 72, 46, 255);
    public static Color32 yellow = new Color32(244, 180, 27, 255);
    public static Color32 blue = new Color32(0, 107, 166, 255);
    public static Color32 brown = new Color32(71, 45, 60, 255);
    public static Color32 lightbrown = new Color32(122, 68, 74, 255);
    public static Color32 lighterbrown = new Color32(191, 121, 88, 255);
    public static Color32 white = new Color32(207, 198, 184, 255);


}

public class Ability : MonoBehaviour {

    [HideInInspector]public RoguelikePlayer player;
    [HideInInspector]public LayerMask enemyLayerMask;
    [HideInInspector]public AbilityButton button;
    [HideInInspector]public AbilityObject abilityObject;
    [HideInInspector]public bool onCooldown = false;
    [HideInInspector]public float cooldown;
    [HideInInspector]public float currentCooldownAmount = 0;
    [HideInInspector]public int level = 1;

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

    public Vector3 GetMouseClickPosition() {
        
        return player.GetMousePosition();
    }

    public virtual string GetDescription() {
        return "Description";
    }
}
