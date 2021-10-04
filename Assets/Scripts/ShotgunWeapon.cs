using UnityEngine;

public class ShotgunWeapon : ShootProjectileWeapon {

    public float[] damagePerLevel = new float[3] { 0.33f, 0.66f, 1 };
    public float shotSpeed = 10f;
    public override void ShootLogic() {
        if (Time.time > time) {
            Vector3 vec = player.GetMousePosition();
            foreach (ProjectilePoint p in projectilePoints) {
                p.SetDamage(player.currentStats.attackDamage * damagePerLevel[level - 1]);
                p.SetSpeed(shotSpeed);
                p.Fire(vec);
            }
            time = Time.time + regularTimeBetweenAttacks / (player.currentStats.attackSpeed / 100);
            shootSFX.Play();
        }
    }

}

