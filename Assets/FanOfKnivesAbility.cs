using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanOfKnivesAbility : Ability
{

    public List<ProjectilePoint> point;
    public List<int> levelToSwords = new List<int>();

    public override void Setup() {
        foreach (ProjectilePoint p in point) {
            p.SetMask(enemyLayerMask);
        }
    }

    public override void SkillDown() {
        if (!onCooldown) {
            SetOnCooldown();
            for (int i = 0; i < levelToSwords[level-1]; i++) {
                point[i].Fire(GetMouseClickPosition());
            }

        }
    }
}
