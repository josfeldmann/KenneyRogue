using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteHealthBar : MonoBehaviour
{
    public StatUnit attachedUnit;

    public SpriteRenderer sprite;

    private void Awake() {
        SetHealthBar();
        attachedUnit.onTakeDamage += SetHealthBar;
        attachedUnit.onChangeStats += SetHealthBar;
    }

   

    public void SetHealthBar() {
        float amt = attachedUnit.currentHP / attachedUnit.maxHp;
        sprite.transform.localScale = new Vector3(amt, 1, 1);
        if (amt == 0) gameObject.SetActive(false);
        else gameObject.SetActive(true);
        if (amt <= 0.25f) {
            sprite.color = Colors.red;
        } else if (amt <= 0.5f) {
            sprite.color = Colors.yellow;
        } else {
            sprite.color = Colors.green;
        }
    }

    public void OnTakeDamage() {
        SetHealthBar();
    }

}
