using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportingEnemy : StatUnit
{

    public StateMachine<TeleportingEnemy> controller;
    public LayerMask targetMask;
    public Target target;
    public Vector2 disRange = new Vector2(2, 6);
    public float idleTime = 1f, afterAttackTime = 1f;
    public Wiggler wiggle;
    public float shrinkTime = 0.25f;

    public List<ProjectilePoint> points = new List<ProjectilePoint>();

    private void Awake() {
        CalculateWithHP();
        onDeath += Death;
        controller = new StateMachine<TeleportingEnemy>(new TeleportingEnemyIdle(), this);
    }

    private void Update() {
        if (target == null) {
            target = Target.GetPlayer();
            return;
        }
        controller.Update();
    }


    public void Death() {
        controller.ChangeState(new TeleportingEnemyDeath());
    }

}


public class TeleportingEnemyIdle : State<TeleportingEnemy> {

    int stage = 0;
    float timer;



    public override void Update(StateMachine<TeleportingEnemy> obj) {
        timer += Time.deltaTime;
        switch(stage) {
            case 0:
                if (timer > obj.target.idleTime/2) {
                    obj.target.wiggle.StartWiggle();
                    stage = 1;
                    timer = 0;
                }
            break;
            case 1:
                if (timer > obj.target.idleTime / 2) {
                    stage = 2;
                    timer = 0;
                }
                break;
            case 2:
                obj.target.wiggle.StopWiggle();
                Vector3 dir = new Vector3(Random.Range(obj.target.disRange.x, obj.target.disRange.y),
                                              Random.Range(obj.target.disRange.x, obj.target.disRange.y), 0);
                if (Random.Range(0, 1f) > 0.5f) {
                    dir.x *= -1;
                }
                if (Random.Range(0, 1f) > 0.5f) {
                    dir.y *= -1;
                }

                Vector3 pos = obj.target.target.transform.position + dir;
                obj.target.transform.position = BattleMap.current.VecInMap(pos);
                obj.ChangeState(new TeleportingEnemyAttack());
                break;
            
        }
    }

}


public class TeleportingEnemyAttack : State<TeleportingEnemy> {

    float timer = 0;
    bool shotYet = false;
    public override void Enter(StateMachine<TeleportingEnemy> obj) {
        obj.target.wiggle.StartWiggle();
    }

    public override void Update(StateMachine<TeleportingEnemy> obj) {
        timer += Time.deltaTime;

        if (!shotYet && timer > obj.target.afterAttackTime/2) {
            foreach (ProjectilePoint point in obj.target.points) {
                point.SetDamage(obj.target.currentStats.attackDamage);
                point.SetMask(obj.target.targetMask);
                point.Fire(obj.target.target.transform.position);
            }
            obj.target.wiggle.StopWiggle();
            shotYet = true;
        }

        if (timer > obj.target.afterAttackTime) {
            obj.ChangeState(new TeleportingEnemyIdle());
        }
    }

}

public class TeleportingEnemyDeath : State<TeleportingEnemy> {
    public float timer = 0;
    public override void Enter(StateMachine<TeleportingEnemy> obj) { }
    public override void Update(StateMachine<TeleportingEnemy> obj) {
        if (timer > obj.target.shrinkTime) {
            GameObject.Destroy(obj.target.gameObject);
            return;
        }
        obj.target.transform.localScale = new Vector3((obj.target.shrinkTime - timer) / obj.target.shrinkTime, (obj.target.shrinkTime - timer) / obj.target.shrinkTime, 1);
        timer += Time.deltaTime;
    }
}