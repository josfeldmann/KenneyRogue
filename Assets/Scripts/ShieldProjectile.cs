using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldProjectile : MonoBehaviour
{
    public TrailRenderer trail;
    public ShieldWeapon weapon;
    public LayerMask targetMask;
    public float damage = 1;
    public float baseSpeed = 5;
    public float speed = 10;
    public Vector3 target;
    public bool goingToTarget = false;
    public float rotationSpeed = 360f;
    public bool active = false;
    public List<Unit> units = new List<Unit>();
    public Transform toReturnTo;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (active && Layers.InMask(targetMask, collision.gameObject.layer)) {
            Unit u = collision.gameObject.GetComponent<Unit>();
            if (!units.Contains(u)) {
                units.Add(u);
                u.TakeDamage(damage);
            }
        }
    }


    private void Update() {
        if (active) {
            if (goingToTarget) {
                if (transform.position == target) {
                    goingToTarget = false;
                    units.Clear();
                } else {
                    transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
                }
            } else {
                if (transform.position == toReturnTo.position) {
                    weapon.ReturnShield(this);
                } else {
                    speed += Time.deltaTime * baseSpeed;
                    transform.position = Vector3.MoveTowards(transform.position, toReturnTo.position, speed * Time.deltaTime);
                }
            }
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
    }
}
