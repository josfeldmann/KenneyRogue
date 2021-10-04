using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectileWeapon : Weapon
{

    public float regularTimeBetweenAttacks = 1;
    public List<ProjectilePoint> projectilePoints;
    public float time = 0;
    public AudioSource shootSFX;

    public override void SetTargetLayer(LayerMask mask) {
        foreach (ProjectilePoint p in projectilePoints) {
            p.SetMask(mask);
        }
    }

    public override void FireDown() {
        ShootLogic();
    }

    public override void FireHeld() {
        ShootLogic();
    }

    public override string GetDescription() {
        return "Projectile Weapon";
    }



    public virtual void ShootLogic() {
        if (Time.time > time) {
            Vector3 vec = player.GetMousePosition();
            foreach (ProjectilePoint p in projectilePoints) {
                p.SetDamage(player.currentStats.attackDamage);
                p.Fire(vec);
            }
            time = Time.time + regularTimeBetweenAttacks/ (player.currentStats.attackSpeed/100);
            shootSFX.Play();
        }
    }

    

}

