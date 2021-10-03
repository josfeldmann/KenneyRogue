using System.Collections;
using UnityEngine;


[CreateAssetMenu(menuName = "Character/RaceObject")]
public class RaceObject : TrackedObject {

    public string raceKey;
    public string raceName;
    public Sprite raceSprite;
 
    public override string GetName() {
        return raceName;
    }

    public override string GetKey() {
        return raceKey;
    }

    public StatWrapper racialStats;
    public AbilityObject racialAbility;
    
}

