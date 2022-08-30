using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTurretAbility : Ability
{
    public Turret turretPrefab;

    public override void SkillDown() {
        if (!onCooldown) {
            SetOnCooldown();
            Turret turret = Instantiate(turretPrefab, GetMouseClickPosition(), Quaternion.identity);
        }
    }
}
