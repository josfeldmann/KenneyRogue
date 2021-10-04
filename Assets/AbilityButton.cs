using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public Image abilityImage;
    public Image radialFill;

    public bool isWeapon = false;
    public Ability ability;
    public Weapon weapon;

    public List<Image> levelImageIndicators;

    public void OnPointerEnter(PointerEventData eventData) {
       // Debug.Break();
        TooltipMaster.current.ShowAbilityButtonToolTip(this);
    }

    public void OnPointerExit(PointerEventData eventData) {
        TooltipMaster.current.CloseToolTip();
    }

    public void SetAbility(Ability a) {
        ability = a;
        abilityImage.sprite = a.abilityObject.abilitySprite;
        gameObject.name = a.abilityObject.GetName();
        isWeapon = false;
        radialFill.enabled = true;
        SetLevel();
        levelImageIndicators[0].gameObject.transform.parent.gameObject.SetActive(true);

    }

    public void SetLevel() {
        for (int i = 0; i < levelImageIndicators.Count; i++) {
            if (i < ability.level) {
                levelImageIndicators[i].color = Colors.yellow;
            } else {
                levelImageIndicators[i].color = Colors.lightbrown;
            }
        }
    }

    public void SetWeapon(Weapon w) {
        weapon = w;
        isWeapon = true;
        gameObject.name = w.weaponObject.GetName();
        abilityImage.sprite = w.weaponObject.weaponSprite;
        radialFill.enabled = false;
        levelImageIndicators[0].gameObject.transform.parent.gameObject.SetActive(false);
    }

}

