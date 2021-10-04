using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowEnemy : StatUnit
{
    public LayerMask targettingLayer;
    public GameObject crossBowAimer;
    public ProjectilePoint shootPoint;
    public StateMachine<CrossbowEnemy> stateMachine;
    public Target target;
    public Vector2 idleWaitTime = new Vector2(1, 4);
    public float attackWaitTime = 1f;

    private void Awake() {
        shootPoint.SetMask(targettingLayer);
        stateMachine = new StateMachine<CrossbowEnemy>(new CrossBowEnemyIdle(), this);
    }

    public void AimAtTarget() {
        crossBowAimer.transform.right = (target.transform.position - transform.position).normalized;
    }

    private void Update() {

        if (target == null) {
            target = Target.GetPlayer();
            return;
        }
        stateMachine.Update();
    }
}


public class CrossBowEnemyIdle : State<CrossbowEnemy> {

    public float AttackTime;

    public override void Enter(StateMachine<CrossbowEnemy> obj) {
        AttackTime = Time.time + Random.Range(obj.target.idleWaitTime.x, obj.target.idleWaitTime.y);
    }

    public override void Update(StateMachine<CrossbowEnemy> obj) {
        obj.target.AimAtTarget();
        if (AttackTime < Time.time) {
            obj.ChangeState(new CrossbowEnemyAttack());
        }
    }
}

public class CrossbowEnemyAttack : State<CrossbowEnemy> {

    float attackTime;

    public override void Enter(StateMachine<CrossbowEnemy> obj) {
        attackTime = Time.time +  obj.target.attackWaitTime;
    }

    public override void Update(StateMachine<CrossbowEnemy> obj) {
        if (Time.time > attackTime) {
            obj.target.shootPoint.Fire(obj.target.target.transform.position);
            obj.ChangeState(new CrossBowEnemyIdle());
        }
    }
}
