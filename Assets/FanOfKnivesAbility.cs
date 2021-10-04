using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanOfKnivesAbility : Ability
{

    public List<ProjectilePoint> point;
    public List<int> levelToSwords = new List<int>();
    public List<int> levelToDamage = new List<int>() { 100, 200, 300 };
    public float swordspeed = 10f;
    public override void Setup() {
        foreach (ProjectilePoint p in point) {
            p.SetMask(enemyLayerMask);
        }
    }

    public override void SkillDown() {
        if (!onCooldown) {
            SetOnCooldown();
            for (int i = 0; i < levelToSwords[level-1]; i++) {
                point[i].SetDamage(levelToDamage[level - 1] * (player.currentStats.spellAmp / 100));
                point[i].SetSpeed(10);
                point[i].Fire(GetMouseClickPosition());
            }

        }
    }

    public override void CalculateCooldown() {
        cooldown = 1;
    }
}
