using System.Collections;
using System.Collections.Generic;

public class FireBallAbility : Ability
{

    public Projectile fireBall;
    public float[] levelDamage = new float[3] { 200, 350, 500 };
    public override void SkillDown() {
        
        if (!onCooldown) {
            Projectile p = Instantiate(fireBall, transform.position, transform.rotation);
            p.damage = levelDamage[level - 1] * (player.currentStats.spellAmp / 100);
            p.Init(enemyLayerMask);
            SetOnCooldown();
        }
    }

    public override string GetDescription() {
        return "Shoots a Fireball for [50/100/150] damage";
    }
}
