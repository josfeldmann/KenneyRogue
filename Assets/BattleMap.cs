using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMap : MonoBehaviour
{
    public RogueHexMap dummyMap;
    public static BattleMap current;
    public float xmin, xmax, ymin, ymax;

    public List<EnemySpawnSpot> spawnSpot = new List<EnemySpawnSpot>();


  



    public Vector3 VecInMap(Vector3 v) {

        return new Vector3(Mathf.Clamp(v.x, xmin, xmax), Mathf.Clamp(v.y, ymin, ymax), 0);

    }

    public void GetSpawnSpots() {
        spawnSpot = new List<EnemySpawnSpot>();
        foreach (Transform t in transform) {
            EnemySpawnSpot s = t.GetComponent<EnemySpawnSpot>();
            if (s != null) spawnSpot.Add(s);
        }
    }

    private void Awake() {
        current = this;
    }
    public static Vector3 v = new Vector3(0.1f, 0.1f, 1);
    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(new Vector3(0, ymin, 0), 0.5f);
        Gizmos.DrawSphere(new Vector3(0, ymax, 0), 0.5f);
        Gizmos.DrawSphere(new Vector3(xmin, 0, 0), 0.5f);
        Gizmos.DrawSphere(new Vector3(xmax, 0, 0), 0.5f);
    }



    public void SpawnEnemies() {
        List<EnemySpawnSpot> spotsToUse;
        EnemySpawnSpot playerSpawnSpot = spawnSpot.PickRandom();
        
    }

}
