using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class TooltipMaster : MonoBehaviour
{
    public static float toolTipy = 100;
    public static TooltipMaster current;
    public static float topLeftCornery = 100;
    public RectTransform canvasRect;
    [Header("Sections")]
    public GameObject titleSection;
    public GameObject statSection;
    public GameObject descriptionSection;
    private RectTransform rect;

    [Header("Title")]
    public TextMeshProUGUI titleText;
    public Image itemImage;

    [Header("Stat")]
    public TextMeshProUGUI statText;
    public List<TextMeshProUGUI> statTexts;

    [Header("Description")]
    public TMPSizeSetter descriptionSetter;

    private void Awake() {
        current = this;
        rect = GetComponent<RectTransform>();
        gameObject.SetActive(false);
        
    }
    public ContentSizeFitter fitter;
    public void SetAnchorBottomLeft() {
        rect.anchorMin = new Vector2(0, 0);
        rect.anchorMax = new Vector2(0, 0);
        rect.pivot = new Vector2(0.5f, 0);
     

    }

    public void SetAnchorTopLeft() {
        rect.anchorMin = new Vector2(0, 1);
        rect.anchorMax = new Vector2(0, 1);
        rect.pivot = new Vector2(0.5f, 1);
    }

    public void PositionInTopLeftCorner() {
        SetAnchorTopLeft();
        rect.anchoredPosition = new Vector2(rect.rect.width / 2, 0);
    }


    public void HideAllSections() {
        titleSection.SetActive(false);
        statSection.SetActive(false);
        descriptionSection.SetActive(false);
    }

    public void ClampTooltip() {
        rect.anchoredPosition = new Vector2(Mathf.Clamp(rect.anchoredPosition.x, (rect.rect.width / 2), canvasRect.rect.width - (rect.rect.width / 2)), rect.anchoredPosition.y);
    }


    #region POSITIONING
    public void SetItemWorld(ItemObject itemObject, Transform t) {
        SetAnchorBottomLeft();
        SetItem(itemObject);
        transform.position = Camera.main.WorldToScreenPoint(t.position) + new Vector3(0, 80, 0);
        ClampTooltip();
    }

    public void SetHexNode(HexNode hexNode) {

        gameObject.SetActive(true);
        HideAllSections();
        titleSection.SetActive(true);
        descriptionSection.SetActive(true);
        itemImage.sprite = hexNode.spriteRenderer.sprite;
        titleText.SetText(MapDatabase.tileDict[hexNode.associatedHex.tileType].GetName());
        descriptionSetter.SetText(MapDatabase.tileDict[hexNode.associatedHex.tileType].GetDescription());
        // transform.position = Camera.main.WorldToScreenPoint(hexNode.transform.position) + new Vector3(0, 80, 0);
        // ClampTooltip();

        PositionInTopLeftCorner();
    }


    public void SetStat(StatEnum statEnum, StatTextIcon statTextIcon) {
        SetAnchorBottomLeft();
        SetStat(statEnum);
        transform.position = new Vector3(statTextIcon.transform.position.x, toolTipy, 0);
        rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, toolTipy);
        ClampTooltip();
    }

    public void SetItemIconUI(ItemObject itemObject, ItemIcon itemIcon) {
        SetAnchorBottomLeft();
        SetItem(itemObject);
        transform.position = itemIcon.transform.position + new Vector3(0, 80, 0);
        ClampTooltip();
    }


    public void ShowAbilityButtonToolTip(AbilityButton button) {
        SetAnchorBottomLeft();
        gameObject.SetActive(true);
        HideAllSections();
        titleSection.SetActive(true);
        descriptionSection.SetActive(true);
        transform.position = new Vector3(button.transform.position.x, toolTipy);
        rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, toolTipy);
        if (button.isWeapon) {
            titleText.SetText(button.weapon.weaponObject.GetName());
            itemImage.sprite = button.weapon.weaponObject.weaponSprite;
            descriptionSetter.SetText(button.weapon.GetDescription());
        } else {
            titleText.SetText(button.ability.abilityObject.GetName());
            itemImage.sprite = button.ability.abilityObject.abilitySprite;
            descriptionSetter.SetText(button.ability.GetDescription());
        }
        ClampTooltip();
    }

    #endregion


    internal void SetItem(ItemObject itemObject) {
        gameObject.SetActive(true);
        HideAllSections();
        titleSection.SetActive(true);
        itemImage.sprite = itemObject.itemSprite;
        titleText.SetText(itemObject.GetName());
        statSection.SetActive(true);
        ShowItemStats(itemObject);
    }

    internal void CloseToolTip() {
        gameObject.SetActive(false);
    }

    public void ShowItemStats(ItemObject item) {
        foreach (TextMeshProUGUI text in statTexts) {
            text.gameObject.SetActive(false);
        }
        for (int i = statTexts.Count; i < item.itemStats.Count; i++) {
            TextMeshProUGUI t = Instantiate(statText, statSection.transform);
            statTexts.Add(t);
        }
        for (int i = 0; i < item.itemStats.Count; i++) {
            statTexts[i].gameObject.SetActive(true);
            statTexts[i].SetText(item.itemStats[i].ToString());
        }
    }


   


    public void SetStat(StatEnum stat) {
        gameObject.SetActive(true);
        HideAllSections();
        titleSection.SetActive(true);
        descriptionSection.SetActive(true);

        StatGroup s = StatDatabase.stats[stat];

        titleText.text = s.GetName();
        itemImage.sprite = s.sprite;
        descriptionSetter.SetText(s.GetDescription());

    }
}
