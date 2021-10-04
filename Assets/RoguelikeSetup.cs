using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoguelikeSetup : MonoBehaviour
{
    public RaceObject race;
    public WeaponObject weapon;
    public List<AbilityObject> abilities;
    public List<ItemObject> itemObjects;
    public RoguelikePlayer player;

    public void Awake() {
        player.raceObject = race;
        player.weaponObject = weapon;
        List<AbilityObject> abilitiesToAdd = new List<AbilityObject>(abilities);
        abilitiesToAdd.Insert(0, race.racialAbility);
        player.items = new List<ItemObject>(itemObjects); 
        player.abilityObjects = abilitiesToAdd;
        player.Setup();
    }

}
