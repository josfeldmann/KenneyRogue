using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoguelikeSetup : MonoBehaviour
{
    public RaceObject race;
    public WeaponObject weapon;
    public List<AbilityObject> abilities;
    public RoguelikePlayer player;

    public void Awake() {
        player.raceObject = race;
        player.weaponObject = weapon;
        player.abilityObjects = abilities;
        player.Setup();
    }

}
