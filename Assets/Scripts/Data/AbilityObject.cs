
using UnityEngine;

[CreateAssetMenu( menuName = "Character/AbilityObject" )]
public class AbilityObject : TrackedObject {
    public bool inRandomPool = true;
    public string abilityKey = "";
    public string abilityName = "";
    public Sprite abilitySprite;
    public Ability abilityPrefab;
    

    public override string GetKey() {
        return abilityKey;
    }

    public override string GetName() {
        return abilityName;
    }

    
}


[CreateAssetMenu(menuName = "Character/AbilityUpgrade")]
public class AbilityUpgrade : TrackedObject {

    public string upgradeKey = "";
    public string upgradeName = "";

    public override string GetName() {
        return upgradeName;
    }

    public override string GetKey() {
        return upgradeKey;
    }
}


