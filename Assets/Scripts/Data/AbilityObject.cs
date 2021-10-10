
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


