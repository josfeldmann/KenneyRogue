using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinDashAbility : Ability
{
    public float speed = 5;
    public float maxDashDistance = 5f;

    public override void SkillDown() {
        if (!onCooldown) {
            SetOnCooldown();

            Vector3 target = player.GetMousePosition();
            Vector3 direction = target - player.transform.position;
            if ( Vector3.Distance(player.transform.position, target) > maxDashDistance) {
                target = player.transform.position + ((direction).normalized * maxDashDistance);
            }
            RaycastHit2D hit = Physics2D.Raycast(player.transform.position, (direction).normalized, direction.magnitude, player.impassableLayers);
            if (hit) {
                target = hit.point;
            }

            player.statemachine.ChangeState(new GoblinDashState(target, speed, 5));
            
        }
    }

  
}

public class GoblinDashState : State<RoguelikePlayer> {

    public Vector3 target;
    public float speed = 5;

    public GoblinDashState(Vector3 tar, float speed, float timeOutTime) {
        target = tar;
        this.speed = speed;
    }

    public override void Enter(StateMachine<RoguelikePlayer> obj) {
        obj.target.inCustomMovement = true;
        obj.target.MakeInvincibleAndImpassable();
    }


    public override void Update(StateMachine<RoguelikePlayer> obj) {
        if (obj.target.transform.position != target) {
            obj.target.transform.position = Vector3.MoveTowards(obj.target.transform.position, target, speed * Time.deltaTime);
        } else {
            obj.ChangeState(new RoguelikePlayerMoveState());
        }

        obj.target.AimRightHand();
        obj.target.AbilityWeaponFireCheck();
    }

    public override void Exit(StateMachine<RoguelikePlayer> obj) {
        obj.target.inCustomMovement = false;
        obj.target.MakeNotInvicible();
    }

}
