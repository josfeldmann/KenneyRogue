using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image itemImage;
    public ItemObject itemObject;

    public void OnPointerEnter(PointerEventData eventData) {
        TooltipMaster.current.SetItemIconUI(itemObject, this);
    }

    public void OnPointerExit(PointerEventData eventData) {
        TooltipMaster.current.CloseToolTip();
    }

    public void Set(ItemObject item) {
        itemObject = item;
        itemImage.sprite = item.itemSprite;
    }
}
