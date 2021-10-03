using UnityEngine;

[CreateAssetMenu(menuName = "Character/WeaponObject")]
public class WeaponObject : TrackedObject {
    public string weaponKey;
    public string weaponName;
    public Sprite weaponSprite;
    public Weapon weaponPrefab;
    public override string GetKey() {
        return weaponKey;
    }
    public override string GetName() {
        return weaponName;
    }
}

