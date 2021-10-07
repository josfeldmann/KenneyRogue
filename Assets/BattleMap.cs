using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMap : MonoBehaviour
{
    public static BattleMap current;
    public float xmin, xmax, ymin, ymax;

    public Vector3 VecInMap(Vector3 v) {

        return new Vector3(Mathf.Clamp(v.x, xmin, xmax), Mathf.Clamp(v.y, ymin, ymax), 0);

    }


    private void Awake() {
        current = this;
    }
}
