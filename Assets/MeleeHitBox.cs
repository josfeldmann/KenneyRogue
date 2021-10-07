using System.Collections.Generic;
using UnityEngine;

public class MeleeHitBox  : MonoBehaviour {

    bool active = false;
    public LayerMask targetLayer;
    public float damage;
    public List<Unit> units = new List<Unit>();   

    public void Activate() {
        active = true;
        units.Clear();
    }

    public void Deactivate() {
        active = false;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (Layers.InMask(targetLayer, collision.gameObject.layer)) {
            Unit u = collision.GetComponent<Unit>();
            if (!units.Contains(u)) {
                u.TakeDamage(damage);
                units.Add(u);
            }
        }
    }

}





