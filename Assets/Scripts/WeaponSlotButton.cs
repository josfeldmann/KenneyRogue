using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlotButton : MonoBehaviour
{
    public Image weaponImage;
    public Image hotKey;
    public Weapon weapon;
    public void Set(Weapon w, int hotKeyi) {
        weapon = w;
        weaponImage.sprite = weapon.sprite;
        char c = hotKeyi.ToString()[0];
        print(c);
        hotKey.sprite = GameDatabase.charSprite[c];
        hotKey.rectTransform.sizeDelta = new Vector2(hotKey.sprite.rect.width / 32 * 100, hotKey.sprite.rect.height / 32 * 100);
    }
}
