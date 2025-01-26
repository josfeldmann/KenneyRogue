using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager {
    public static bool paused = false;

    public static void Pause() {
        paused = true;
        Time.timeScale = 0;
    }

    public static void UnPause() {
        paused = false;
        Time.timeScale = 1;
    }

}

public class RunAtPlayerEnemy : Unit
{
    public LayerMask targetLayer;
    public Target target;
    public float runSpeed = 50f;
    public float damage = 1f;
    public StateMachine<RunAtPlayerEnemy> stateMachine;
    public float freezeAfterHit = 3f;
    public float aggroRange = 5f;
    

    private void Awake() {
        onDeath += Die;
        stateMachine = new StateMachine<RunAtPlayerEnemy>( new WaitTillInRange(), this);
    }

    private void Update() {
        if (GameManager.paused) return;
        if (target == null) {
            target = Target.GetPlayer();
            return;
        }
        stateMachine.Update();

    }

    private void FixedUpdate() {
        if (GameManager.paused) return;
        stateMachine.FixedUpdate();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        stateMachine.currentState.OnCollisionEnter2D(collision, stateMachine);
    }

    public void Die() {
        Destroy(gameObject);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
       
    }

}

public class WaitTillInRange : State<RunAtPlayerEnemy> {

    public override void Update(StateMachine<RunAtPlayerEnemy> obj) {
        if (obj.target.target != null) {
            Debug.Log("wait " + Time.time);
            if (Vector3.Distance(obj.target.transform.position, obj.target.target.transform.position) < obj.target.aggroRange) {
                obj.ChangeState(new RunAtPlayerRunState());
            }
        } else {
            Debug.Log("null");
        }
    }

}


public class RunAtPlayerRunState : State<RunAtPlayerEnemy> {
    public override void FixedUpdate(StateMachine<RunAtPlayerEnemy> obj) {
        obj.target.rb.linearVelocity =  ((obj.target.target.transform.position - obj.target.transform.position).normalized * obj.target.runSpeed);
    }

    public override void OnCollisionEnter2D(Collision2D col, StateMachine<RunAtPlayerEnemy> obj) {
        if (Layers.InMask(obj.target.targetLayer, col.gameObject.layer)) {
            Unit u = col.gameObject.GetComponent<Unit>();
            u.TakeDamage(obj.target.damage);
            obj.ChangeState(new RunAtPlayerFreezeState());
        }
    }
}

public class RunAtPlayerFreezeState : State<RunAtPlayerEnemy> {

    private float endTime = 0;
    public override void Enter(StateMachine<RunAtPlayerEnemy> obj) {
        endTime = Time.time + obj.target.freezeAfterHit;
        obj.target.rb.linearVelocity = Vector2.zero;
    }

    public override void Update(StateMachine<RunAtPlayerEnemy> obj) {
        if (endTime  < Time.time ) {
            obj.ChangeState(new RunAtPlayerRunState());
        }
    }


}







