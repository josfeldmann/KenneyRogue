using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/ItemObject")]
public class ItemObject : TrackedObject {
    public string itemKey = "";
    public string itemName;
    public Sprite itemSprite;
    public List<StatPair> itemStats = new List<StatPair>();
    public string itemDescription;
    public int goldPrice = 100;

    public override string GetKey() {
        return itemKey;
    }

    public override string GetName() {
        return itemName;
    }

    public string GetDescription() {
        return itemDescription;
    }
}


