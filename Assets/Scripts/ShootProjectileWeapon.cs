using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectileWeapon : Weapon
{
    public float minShotTime =  0.25f;
    public Transform projectilePoint;
    public Projectile projectile;
    public float time = 0;


    public override void FireDown() {
        ShootLogic();
    }

    public override void FireHeld() {
        ShootLogic();
    }

    public void ShootLogic() {
        if (Time.time > time) {
            Projectile p = Instantiate(projectile, projectilePoint.position, projectilePoint.rotation);
            p.Init();
            time = Time.time + minShotTime;
        }
    }

}
