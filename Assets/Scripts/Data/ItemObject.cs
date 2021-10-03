using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/ItemObject")]
public class ItemObject : TrackedObject {
    public string itemKey = "";
    public string itemName;
    public Sprite itemSprite;
    List<StatPair> itemStats = new List<StatPair>();

    public override string GetKey() {
        return itemKey;
    }

    public override string GetName() {
        return itemName;
    }
}


