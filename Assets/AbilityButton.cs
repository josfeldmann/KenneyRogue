using UnityEngine;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour {

    public Image abilityImage;
    public Image radialFill;

    public bool isWeapon = false;
    public Ability ability;
    public Weapon weapon;

    public void SetAbility(Ability a) {
        ability = a;
        abilityImage.sprite = a.abilityObject.abilitySprite;
        gameObject.name = a.abilityObject.GetName();
        isWeapon = false;
        radialFill.enabled = true;
    }

    public void SetWeapon(Weapon w) {
        weapon = w;
        isWeapon = true;
        gameObject.name = w.weaponObject.GetName();
        abilityImage.sprite = w.weaponObject.weaponSprite;
        radialFill.enabled = false;
    }

}

