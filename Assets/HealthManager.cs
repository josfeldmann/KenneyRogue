using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{

    public Sprite emptyHeart, halfHeart, fullHeart;
    public Image imageprefab;
    public Transform groupingTransform;
    public List<Image> hearts = new List<Image>();
    public Unit attachedUnit;


    public void UpdateHP() {


        if (attachedUnit.maxHp % 2 == 1) {
            Debug.LogError("MaxHP should be divisible by 2");
        }

        for (int i = 0; i < attachedUnit.maxHp / 2; i++) {
            if (hearts.Count <= i ) {
                hearts.Add(Instantiate(imageprefab, groupingTransform));
            }
        }


        int val = (int)attachedUnit.currentHP;

        for (int i = 0; i < hearts.Count; i++) {
            if (val >= 2) {
                hearts[i].sprite = fullHeart;
            } else if (val == 1) {
                hearts[i].sprite = halfHeart;
            } else if (val <= 0) {
                hearts[i].sprite = emptyHeart;
            }
            val -= 2;
        }





    }

}
