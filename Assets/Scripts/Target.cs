using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public bool isPlayer = false;

    public static List<Target> playertargets = new List<Target>();
    public static List<Target> enemyTarget = new List<Target>();
    private void Awake() {
        if (isPlayer) {
            playertargets.Add(this);
        } else {
            enemyTarget.Add(this);
        }

    }

    private void OnDestroy() {
        if (isPlayer) {
            playertargets.Remove(this);
        } else {
            enemyTarget.Remove(this);
        }

    }

    public static Target GetEnemy() {
        if (enemyTarget.Count > 0) {
            return enemyTarget.PickRandom();
        } else {
            return null;
        }
    }

    public static Target GetClosestEnemy(Transform source) {
        float min = 10000;
        Target target = null;
        foreach (Target t in enemyTarget) {
            float f = Vector3.Distance(source.position, t.transform.position);
            if (min > f) {
                min = f;
                target = t;
            }
        }
        return target;
    }


    internal static Target GetPlayer() {
        return playertargets.PickRandom();
    }

    public static Target GetClosestPlayer(Transform source) {
        float min = 10000;
        Target target = null;
        foreach (Target t in playertargets) {
            float f = Vector3.Distance(source.position, t.transform.position);
            if (min > f) {
                min = f;
                target = t;
            }
        }
        return target;
    }
}
