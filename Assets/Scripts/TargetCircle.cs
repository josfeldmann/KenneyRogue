using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCircle : MonoBehaviour
{
    public LayerMask targetMask;

    public List<Target> targets = new List<Target>();
    

    private void OnTriggerEnter2D(Collider2D collision) {
        if (Layers.InMask(targetMask, collision.gameObject.layer)) {
            Target u = collision.gameObject.GetComponent<Target>();
            if (!targets.Contains(u)) {
                targets.Add(u);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (Layers.InMask(targetMask, collision.gameObject.layer)) {
            Target u = collision.gameObject.GetComponent<Target>();
            if (targets.Contains(u)) {
                targets.Remove(u);
            }
        }
    }

    public Target GetTarget() {
        if (targets.Count <= 0) {
            return null;
        }
        Target u = targets.PickRandom();
        if (u == null) {
            targets.RemoveAll(item => item == null);
            if (targets.Count > 0) {
                return targets.PickRandom();
            } else {
                return null;
            }
        }
        return u;
    }


}
