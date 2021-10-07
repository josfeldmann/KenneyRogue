using Pathfinding;
using System.Collections;
using UnityEngine;

public class MeleeEnemy : StatUnit
{
    public LayerMask targetLayer;
    public bool isEnemy;
    public float AttackRadius = 1f;
    public float refreshRate = 0.25f;
    public StateMachine<MeleeEnemy> controller;
    public AIAgent agent;
    public Target target;
    public Transform aim;
    public Wiggler wiggler;
    public MeleeHitBox box;
    public float shrinkTime = 0.25f;
    public Transform visual;

    private void Awake() {
        CalculateWithHP();
        box.targetLayer = targetLayer;
        controller = new StateMachine<MeleeEnemy>(new MeleeEnemyWalking(), this);
        box.damage = currentStats.attackDamage;
        box.gameObject.SetActive(false);
        onDeath += Death;
    }

    private void Update() {
        if (target == null) {
            if (isEnemy) {
                target = Target.GetPlayer();
            } else {
                target = Target.GetEnemy();
            }
            return;
        }
        controller.Update();
    }

    public void Death() {
        StopAllCoroutines();
        box.gameObject.SetActive(false);
        wiggler.StopWiggle();
        controller.ChangeState(new MeleeEnemyDeath());
    }

    public void AimAtTarget() {
        aim.transform.right = transform.localScale.x *  (target.transform.position - transform.position).normalized;
    }

   public void FaceTarget() {
        if (target.transform.position.x > transform.position.x && visual.transform.localScale.x != 1) {
            visual.transform.localScale = new Vector3(1, 1, 1);
        }
        if (target.transform.position.x < transform.position.x && visual.transform.localScale.x != -1) {
            visual.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public IEnumerator Stab() {
        Vector3 startPos = transform.position;
        wiggler.StartWiggle();
        yield return new WaitForSeconds(wiggleAttackTime);
        wiggler.StopWiggle();
        AimAtTarget();
        box.gameObject.SetActive(true);
        box.Activate();
        yield return new WaitForSeconds(attackLinger);
        box.Deactivate();
        box.gameObject.SetActive(false);
        yield return new WaitForSeconds(waitAfterAttack);
        controller.ChangeState(new MeleeEnemyWalking());
    }

    [Header("Attacks")]
    public float wiggleAttackTime = 0.4f;
    public float attackLinger = 0.1f;
    public float waitAfterAttack = 1f;


}

public class MeleeEnemyWalking : State<MeleeEnemy> {
    float timer = 0;

    public override void Update(StateMachine<MeleeEnemy> obj) {
        timer += Time.deltaTime;
        obj.target.FaceTarget();
        obj.target.AimAtTarget();
        if (timer > obj.target.refreshRate) {
            obj.target.agent.SetPosition(obj.target.transform.position, obj.target.target.transform.position);
            timer = 0;
        }
        if (Vector3.Distance(obj.target.transform.position, obj.target.target.transform.position) < obj.target.AttackRadius) {
            obj.target.agent.Stop();
            obj.ChangeState(new MeleeEnemyAttack());
        }
        
    }
}


public class MeleeEnemyAttack : State<MeleeEnemy> {


    public override void Enter(StateMachine<MeleeEnemy> obj) {
        obj.target.StartCoroutine(obj.target.Stab());
    }

    public override void Update(StateMachine<MeleeEnemy> obj) {
        
    }


}

public class MeleeEnemyDeath : State<MeleeEnemy> {
    public float timer = 0;
    public override void Enter(StateMachine<MeleeEnemy> obj) { }
    public override void Update(StateMachine<MeleeEnemy> obj) {
        if (timer > obj.target.shrinkTime) {
            GameObject.Destroy(obj.target.gameObject);
            return;
        }
        obj.target.transform.localScale = new Vector3((obj.target.shrinkTime - timer) / obj.target.shrinkTime, (obj.target.shrinkTime - timer) / obj.target.shrinkTime, 1);
        timer += Time.deltaTime;
    }
}





