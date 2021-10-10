using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class RunSelectFigure :HoverBehaviour
{
    public UnityEvent OnClickEvent;
    public ModeSelector modeSelector;
    public TextMeshPro titleText;
    public SpriteRenderer sprite;
    public SpriteRenderer abilityOne, abilityTwo, weaponSprite;
    [HideInInspector]public RaceObject race;
    [HideInInspector]public WeaponObject weaponObject;
    [HideInInspector]public List<AbilityObject> abilities = new List<AbilityObject>();



    public override void OnHoverEnter() {
        modeSelector.Hover(this);
    }

    public override void OnHoverExit() {
        modeSelector.HoverExit(this);
    }

    public override void OnClick() {
        OnClickEvent.Invoke();
    }

    public void SetVisual() {
        sprite.sprite = race.raceSprite;
        abilityOne.sprite = abilities[0].abilitySprite;
        abilityTwo.sprite = abilities[1].abilitySprite;
        weaponSprite.sprite = weaponObject.weaponSprite;
    }
}
