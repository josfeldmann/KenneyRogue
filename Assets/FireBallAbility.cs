using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    
}
