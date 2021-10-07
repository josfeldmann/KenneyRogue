using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    public StatDatabase database;
    public TextMeshProUGUI goldText, xpText;
    public StatUnit attachedunit;
    public StatTextIcon agility, intelligence, strength, attackDamage, attackSpeed, movespeed, spellAmp, cooldownReduction;

    private void Awake() {
        database.Init();
        SetGoldXP();
        SetStats();
        attachedunit.onChangeStats += SetStats;
        attachedunit.goldXPChanged += SetGoldXP;
    }

    public void SetGoldXP() {
        goldText.text = ((int)attachedunit.gold).ToString();
        xpText.text = ((int)attachedunit.xp).ToString();

    }

    public void SetStats() {
        agility.Set(attachedunit.currentStats.agility);
        strength.Set(attachedunit.currentStats.strength);
        intelligence.Set(attachedunit.currentStats.intelligence);
        attackDamage.Set(attachedunit.currentStats.attackDamage);
        attackSpeed.Set(attachedunit.currentStats.attackSpeed);
        cooldownReduction.Set(attachedunit.currentStats.coolDownReduction);
        movespeed.Set(attachedunit.currentStats.moveSpeed);
        spellAmp.Set(attachedunit.currentStats.spellAmp);
    }

}
