using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemObject item;
    public bool sellObject;
    public SpriteRenderer itemSprite;
    public TextMeshPro text;

    private void Awake() {
        if (item != null) {
            Set(item);
        }
    }

    public void Set(ItemObject item) {
        this.item = item;
        itemSprite.sprite = item.itemSprite;
        if (sellObject) {
            text.gameObject.SetActive(true);
            text.SetText(item.goldPrice.ToString());
        } else {
            text.gameObject.SetActive(false);
        }
    }

    public void OnMouseEnter() {
        TooltipMaster.current.SetItemWorld(item, transform);
    }

    public void OnMouseExit() {
        TooltipMaster.current.CloseToolTip();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == Layers.player) {
            RoguelikePlayer player = collision.gameObject.GetComponent<RoguelikePlayer>();
            if (player == null) return;
            if (sellObject) {
                if (player.CanSpendGold(item.goldPrice)) {
                    player.SpendGold(item.goldPrice);
                    player.AddItem(item);
                } else {
                    
                }
            } else {
                player.AddItem(item);
                Destroy(gameObject);
            }
        }
    }
}
