using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : StatUnit
{
    public bool isEnemy = true;
    public float baseTimeInBetweenAttacks = 2f;
    public LayerMask layerMask;
    public TargetCircle targetCircle;
    public StateMachine<Turret> stateMachine;
    public Transform aimTransform;
    public List<ProjectilePoint> points;
    public Target target;
    public float baseProjectileSpeed = 10f;
    public SpriteHealthBar healthBar;
    public float shrinkTime = 0.25f;
    private void Awake() {
        CalculateWithHP();
        targetCircle.targetMask = layerMask;
        stateMachine = new StateMachine<Turret>(new TurretWaitState(), this);
        foreach (ProjectilePoint p in points) {
            p.SetMask(layerMask);
        }
        onDeath += Death;
    }

    public void Death() {
        stateMachine.ChangeState(new TurretDeathState());
    }

    private void Update() {
        if (target == null) {
            if (isEnemy) {
                target = Target.GetClosestPlayer(transform);
            } else {
                target = Target.GetClosestEnemy(transform);
            }
        }
        stateMachine.Update();
    }

    public void AimAtTarget() {
        aimTransform.right = -(target.transform.position - transform.position);
    }


}


public class TurretWaitState : State<Turret> {

    private float timer = 0;
    public float timeInBetweenAttacks = 0;
    public override void Enter(StateMachine<Turret> obj) {
        timeInBetweenAttacks = obj.target.baseTimeInBetweenAttacks / (obj.target.currentStats.attackSpeed / 100);
    }

    public override void Update(StateMachine<Turret> obj) {
        if (obj.target.target == null) {
            timer = 0;
            return;
        }
        timer += Time.deltaTime;
        obj.target.AimAtTarget();
        if (timer > timeInBetweenAttacks) {
            obj.ChangeState(new TurretAttackState(obj.target.target));
        }
       
    }


}


public class TurretAttackState : State<Turret> {

    private Target tar;

    public TurretAttackState(Target t) {
        tar = t;
    }

    public override void Enter(StateMachine<Turret> obj) {
        foreach (ProjectilePoint p in obj.target.points) {
            p.SetDamage(obj.target.currentStats.attackDamage);
            p.SetSpeed(obj.target.baseProjectileSpeed * obj.target.currentStats.attackSpeed/100);
            p.Fire(tar.transform.position);
        }
    }

    public override void Update(StateMachine<Turret> obj) {
        obj.ChangeState(new TurretWaitState());
    }

}

public class TurretDeathState : State<Turret> {

    public float timer = 0;
    public override void Enter(StateMachine<Turret> obj) {
        obj.target.healthBar.gameObject.SetActive(false);
    }
    public override void Update(StateMachine<Turret> obj) {
        if (timer > obj.target.shrinkTime) {
            GameObject.Destroy(obj.target.gameObject);
            return;
        }
        obj.target.transform.localScale = new Vector3((obj.target.shrinkTime - timer) / obj.target.shrinkTime, (obj.target.shrinkTime - timer) / obj.target.shrinkTime, 1);
        timer += Time.deltaTime;
    }
}
