using System.Collections;
using System.Collections.Generic;

public class FireBallAbility : Ability
{

    public Projectile fireBall;

    public override void SkillDown() {
        
        if (!onCooldown) {
            Projectile p = Instantiate(fireBall, transform.position, transform.rotation);
            p.Init(enemyLayerMask);
            SetOnCooldown();
        }
    }

    public override string GetDescription() {
        return "Shoots a Fireball for [50/100/150] damage";
    }
}
