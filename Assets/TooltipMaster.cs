using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TooltipMaster : MonoBehaviour
{
    public static TooltipMaster current;

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

    public void HideAllSections() {
        titleSection.SetActive(false);
        statSection.SetActive(false);
        descriptionSection.SetActive(false);
    }

    internal void SetItem(ItemObject itemObject, ItemIcon itemIcon) {
        gameObject.SetActive(true);
        HideAllSections();
        titleSection.SetActive(true);
        itemImage.sprite = itemObject.itemSprite;
        titleText.SetText(itemObject.GetName());
        transform.position = itemIcon.transform.position + new Vector3(0,80, 0);
        rect.anchoredPosition = new Vector2(Mathf.Min(rect.anchoredPosition.x, (rect.rect.width/-2) ), rect.anchoredPosition.y);
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

    public void ShowAbilityButtonToolTip(AbilityButton button) {
        gameObject.SetActive(true);
        HideAllSections();
        titleSection.SetActive(true);
        descriptionSection.SetActive(true);
        transform.position = button.transform.position + new Vector3(0, 100, 0);
        if (button.isWeapon) {
            titleText.SetText(button.weapon.weaponObject.GetName());
            itemImage.sprite = button.weapon.weaponObject.weaponSprite;
            descriptionSetter.SetText(button.weapon.GetDescription());
        } else {
            titleText.SetText(button.ability.abilityObject.GetName());
            itemImage.sprite = button.ability.abilityObject.abilitySprite;
            descriptionSetter.SetText(button.ability.GetDescription());
        }

    }
}
