using System;
using System.Collections.Generic;
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

    public List<Vector3> GetSpotsAroundPoint(Vector3 point, int number, float angleOffset, float radius) {
        List<Vector3> vec = new List<Vector3>();

        Vector2 startingpos = new Vector2((float)Math.Cos(angleOffset * Math.PI / 180), (float)Math.Sin(angleOffset * Math.PI / 180));

        for (int i = 0; i < number; i++) {

            float angle = angleOffset + (i * 360f / number);

            Vector3 v = new Vector3((float)Math.Cos(angle  * Math.PI / 180), (float)Math.Sin(angle * Math.PI / 180), 0).normalized * radius;
            v += point;
            vec.Add(v);


        }


        return vec;
    }



    public virtual string GetDescription() {
        return "Description";
    }
}
