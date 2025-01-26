using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SimpleDescriptionBox : MonoBehaviour {

    public Image image;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;

    public void SetAbility(AbilityObject ability) {
        image.sprite = ability.abilitySprite;
        nameText.text = ability.abilityName;
        descriptionText.text = ability.abilityPrefab.GetDescription();
    }

    public void SetRace(RaceObject race) {
        image.sprite = race.raceSprite;
        nameText.text = race.raceName;
        descriptionText.text = race.raceDescription;
    }

    public void SetWeapon(WeaponObject  weapon) {

        image.sprite=weapon.weaponSprite;
        nameText.text = weapon.weaponName;
        descriptionText.text = weapon.weaponPrefab.GetDescription();


    }

}




