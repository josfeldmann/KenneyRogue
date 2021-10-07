using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public LayerMask target;
    public float speed = 5f;
    public float damage = 1f;
    public float expireTime = 10f;

    [Header("Push")]
    public float pushForce = 500f;


    private void Update() {
        ProjectileUpdate();
    }

    public void Init(LayerMask mask) {
        target = mask;
        Destroy(gameObject, expireTime);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (Layers.InMask(target, collision.gameObject.layer)) {

            Unit u = collision.gameObject.GetComponent<Unit>();
            u.TakeDamage(damage);
            // u.rb.AddForce((collision.gameObject.transform.position - transform.position).normalized * pushForce);
           // TextSpawner.SpawnTextAt(u.transform.position + new Vector3(0,1,0), damage.ToString(), 1);
            Destroy(gameObject);
        }
    }

    public virtual void AimProjectileAt(Vector3 vector) {
        transform.right = (vector - transform.position);
    }

    public virtual void AimProjectileAt(Target target) {
        AimProjectileAt(target.transform.position);
    }

    public virtual void ProjectileUpdate() {
        transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
    }



}
