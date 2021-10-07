using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldWeapon : Weapon
{

    public ShieldProjectile shieldProjectile;

    private float currentTrail = 0;
    public bool showingtrail = false;
    public float trailAmount;
    private void Awake() {
        trailAmount = shieldProjectile.trail.time;
        shieldProjectile.weapon = this;
        shieldProjectile.trail.time = currentTrail;
    }

    public override void SetTargetLayer(LayerMask mask) {
        shieldProjectile.targetMask = mask;
    }

    private void Update() {
        if (!showingtrail && currentTrail > 0) {
            currentTrail -= Time.deltaTime;
            if (currentTrail < 0) {
                currentTrail = 0;
            }
            shieldProjectile.trail.time = currentTrail;
        }
    }

    public override void FireDown() {
        
        if (!shieldProjectile.active) {
            showingtrail = true;
            currentTrail = trailAmount;
            shieldProjectile.trail.time = currentTrail;
            shieldProjectile.transform.SetParent(null);
            shieldProjectile.target = player.GetMousePosition();
            shieldProjectile.active = true;
            shieldProjectile.goingToTarget = true;
            shieldProjectile.toReturnTo = this.transform;
            shieldProjectile.speed = shieldProjectile.baseSpeed * (player.currentStats.attackSpeed / 100);
            shieldProjectile.damage = player.currentStats.attackDamage;
            shieldProjectile.units = new List<Unit>();

        }

    }

    public void ReturnShield(ShieldProjectile shieldProjectile) {
        shieldProjectile.active = false;
        shieldProjectile.transform.SetParent(transform);
        shieldProjectile.transform.localPosition = Vector3.zero;
        shieldProjectile.transform.localEulerAngles = Vector3.zero;
        showingtrail = false;
        
    }
}
