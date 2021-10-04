using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public StatUnit AttachedUnit;

    public Image fillImage;
    public TextMeshProUGUI healthText;

    private void Awake() {
        AttachedUnit.onTakeDamage += TakeDamage;
        AttachedUnit.onChangeStats += SetOverallHealth;
        SetOverallHealth();
    }

    public void SetOverallHealth() {
        fillImage.fillAmount = AttachedUnit.currentHP / AttachedUnit.maxHp;
        healthText.SetText(((int)AttachedUnit.currentHP) + "/" + ((int)AttachedUnit.maxHp));
    }

    public void TakeDamage() {
        fillImage.fillAmount = AttachedUnit.currentHP / AttachedUnit.maxHp;
        healthText.SetText(((int)AttachedUnit.currentHP) + "/" + ((int)AttachedUnit.maxHp));
    }
}
