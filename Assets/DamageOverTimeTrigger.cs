using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOverTimeTrigger : MonoBehaviour
{

    public LayerMask targetLayer;
    public List<Unit> units = new List<Unit>();
    public float intervalTime = 0.1f;
    float timer = 0;
    public float damagePerInterval;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (Layers.InMask( targetLayer, collision.gameObject.layer)) {
            Unit u = collision.gameObject.GetComponent<Unit>();
            if (!units.Contains(u)) {
                units.Add(u);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (Layers.InMask(targetLayer, collision.gameObject.layer)) {
            Unit u = collision.gameObject.GetComponent<Unit>();
            if (units.Contains(u)) {
                units.Remove(u);
            }
        }
    }

    private void Update() {
        timer += Time.deltaTime;
        if (timer >= intervalTime) {
            timer -= intervalTime;
            foreach (Unit u in units) {
                if (u != null) {
                    u.TakeDamage(damagePerInterval);
                }
            }
        }
    }

}
