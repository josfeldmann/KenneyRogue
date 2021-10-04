using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadierEnemy : Unit
{
    public LayerMask targettingMask;
    public GameObject targetting;
    public Projectile projectile;
    public Wiggler wiggler;
    public StateMachine<GrenadierEnemy> controller;
    [HideInInspector] public Target target;
    public Vector2 WaitTime = new Vector2(3, 5);
    public Vector2 ExplosionPlacementVariation = new Vector2(0.25f, 1f);
    public float aimTime = 2f;
    public float deathScaleSpeed = 1f;

    private void Awake() {
        onDeath += GrenadierDeath;
        controller = new StateMachine<GrenadierEnemy>(new GrenadierIdleState(), this);
        
    }

    public void GrenadierDeath() {
        controller.ChangeState(new GrenadierDeath());
    }

    private void Update() {
        if (target == null) {
            target = Target.GetPlayer();
            return;
        }
        controller.Update();
    }
}

public class GrenadierIdleState : State<GrenadierEnemy> {

    private float TimeToAttack;

    public override void Enter(StateMachine<GrenadierEnemy> obj) {
        TimeToAttack = Time.time + Random.Range(obj.target.WaitTime.x, obj.target.WaitTime.y);
    }
    public override void Update(StateMachine<GrenadierEnemy> obj) {



        if (obj.target.target != null) {
            if (TimeToAttack < Time.time) {
                obj.ChangeState(new GrenadierAttack());
            }
        }
    }
}

public class GrenadierAttack : State<GrenadierEnemy> {

    GameObject targeting;
    Projectile projectile;
    private float waitTime;
    public override void Enter(StateMachine<GrenadierEnemy> obj) {
        obj.target.wiggler.StartWiggle();
        waitTime = Time.time + obj.target.aimTime;
        Vector3 placement = new Vector3(Random.Range(obj.target.ExplosionPlacementVariation.x, obj.target.ExplosionPlacementVariation.y), Random.Range(obj.target.ExplosionPlacementVariation.x, obj.target.ExplosionPlacementVariation.y));

        targeting = GameObject.Instantiate(obj.target.targetting , obj.target.target.transform.position + placement, Quaternion.identity);
        

    }

    public override void Update(StateMachine<GrenadierEnemy> obj) {
        if (Time.time > waitTime) {
            projectile = GameObject.Instantiate(obj.target.projectile, obj.target.transform.position, Quaternion.identity);

            projectile.Init(obj.target.targettingMask);
            projectile.AimProjectileAt(targeting.transform.position);
            obj.ChangeState(new GrenadierIdleState());
        }
    }

    public override void Exit(StateMachine<GrenadierEnemy> obj) {
        obj.target.wiggler.StopWiggle();
        GameObject.Destroy(targeting);

    }

}

public class GrenadierDeath : State<GrenadierEnemy> {

    public float timer = 0;

    public override void Enter(StateMachine<GrenadierEnemy> obj) {
        obj.target.wiggler.StartWiggle();
    }

    public override void Update(StateMachine<GrenadierEnemy> obj) {

        if (timer > 1) {
            GameObject.Destroy(obj.target.gameObject);
            return;
        }

        obj.target.transform.localScale = new Vector3(1 - timer, 1 - timer, 1);

        timer += Time.deltaTime;

    }
}
