using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectileWeapon : Weapon
{

    public float minShotTime =  0.25f;
    public List<ProjectilePoint> projectilePoints;
    public float time = 0;
    public AudioSource shootSFX;


    public override void FireDown() {
        ShootLogic();
    }

    public override void FireHeld() {
        ShootLogic();
    }

    public void ShootLogic() {
        if (Time.time > time) {
            Vector3 vec = player.cam.ScreenToWorldPoint(Input.mousePosition);
            foreach (ProjectilePoint p in projectilePoints) {
                p.Fire(vec);
            }
            time = Time.time + minShotTime;
            shootSFX.Play();
        }
    }

}
