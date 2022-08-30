using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowEnemy : StatUnit
{
    public float baseMoveSpeed = 5;
    public LayerMask targettingLayer;
    public GameObject crossBowAimer;
    public ProjectilePoint shootPoint;
    public StateMachine<CrossbowEnemy> stateMachine;
    public Target target;
    public Vector2 idleWaitTime = new Vector2(1, 4);
    public float attackWaitTime = 1f;
    public float shrinkTime = 0.25f;
    public float shotSpeed = 10f;

    public float minTargetDistance = 5;
    public float maxTargetDistance = 15;


    private void Awake() {
        CalculateWithHP();
        shootPoint.SetMask(targettingLayer);
        stateMachine = new StateMachine<CrossbowEnemy>(new CrossBowEnemyIdle(), this);
        onDeath += ArcherDie;
        shootPoint.SetDamage(currentStats.attackDamage);
    }

    public void AimAtTarget() {
        crossBowAimer.transform.right = (target.transform.position - transform.position).normalized;
    }

    private void Update() {

        if (target == null) {
            GetTarget();
            return;
        }
        stateMachine.Update();
    }

    public void ArcherDie() {
        stateMachine.ChangeState(new CrossBowEnemyDie());
    }

    public void GetTarget() {
        target = Target.GetClosestPlayer(transform);
    }

    public Vector3 GetDirection() {
        // float roll = Random.Range(0f, 1f);
        if (target == null) return new Vector3();


        float dis = Vector3.Distance(target.transform.position, transform.position);

        if (dis < minTargetDistance) {
            return (transform.position - target.transform.position).normalized;
        } else if (dis > maxTargetDistance) {
            return (target.transform.position - transform.position).normalized;
        } else {
            return Vector3.zero;
        }

        /* if (roll < 0.33f) {
             return (transform.position - target.transform.position).normalized;
         } else if (roll < 0.66f) {
             return (target.transform.position - transform.position).normalized;
         } else {
             return new Vector3(Random.Range(-1, 1), Random.Range(-1, 1)).normalized;
         } */

    }

}


public class CrossBowEnemyIdle : State<CrossbowEnemy> {

    public float AttackTime;

    Vector3 walkDirection;
    float moveSpeed;

    public override void Enter(StateMachine<CrossbowEnemy> obj) {
        obj.target.GetTarget();
        AttackTime = Time.time + Random.Range(obj.target.idleWaitTime.x, obj.target.idleWaitTime.y);
        moveSpeed = obj.target.baseMoveSpeed * (obj.target.currentStats.moveSpeed / 100f);
        walkDirection = obj.target.GetDirection();

    }

    public override void Update(StateMachine<CrossbowEnemy> obj) {
        obj.target.AimAtTarget();
        obj.target.rb.velocity = walkDirection * moveSpeed;
        if (AttackTime < Time.time) {
            obj.target.rb.velocity = Vector2.zero;
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
        obj.target.AimAtTarget();
        if (Time.time > attackTime) {
            obj.target.shootPoint.Fire(obj.target.target.transform.position);
            obj.ChangeState(new CrossBowEnemyIdle());
        }
    }
}



public class CrossBowEnemyDie : State<CrossbowEnemy> {
    public float timer = 0;
    public override void Enter(StateMachine<CrossbowEnemy> obj) { }
    public override void Update(StateMachine<CrossbowEnemy> obj) {
        if (timer > obj.target.shrinkTime) {
            GameObject.Destroy(obj.target.gameObject);
            return;
        }
        obj.target.transform.localScale = new Vector3((obj.target.shrinkTime - timer) / obj.target.shrinkTime, (obj.target.shrinkTime - timer) / obj.target.shrinkTime, 1);
        timer += Time.deltaTime;
    }
}
