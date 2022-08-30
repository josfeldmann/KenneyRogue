using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpriteHealthBar : MonoBehaviour
{
    public TextMeshPro damageText;
    public float damageTimer = 0;
    float dmgAmount = 0;
    public StatUnit attachedUnit;
    public SpriteRenderer sprite;

    private void Awake() {
        SetHealthBar();
        attachedUnit.onTakeDamage += TakeDamageUpdate;
        attachedUnit.onChangeStats += SetHealthBar;
        damageText.gameObject.SetActive(false);
    }

    public void TakeDamageUpdate(float dam) {
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
        AddDamageText(dam);
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

    public void AddDamageText(float amt) {
        dmgAmount += amt;
        damageTimer = 1;
        damageText.gameObject.SetActive(true);
        damageText.SetText(((int)dmgAmount).ToString());
    }

    public void OnTakeDamage() {
        SetHealthBar();
    }

    private void Update() {
        if (damageText.gameObject.activeInHierarchy) {
            damageTimer -= Time.deltaTime;
            if (damageTimer < 0) {
                damageText.gameObject.SetActive(false);
                dmgAmount = 0;
                damageTimer = 0;
            }
        }
    }

}
