using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BattleMap))]
public class BattleMapEditor : Editor {

    public override void OnInspectorGUI() {
        if (GUILayout.Button("Grab EnemySpots")) {
            ((BattleMap)target).GetSpawnSpots();
        }
        base.OnInspectorGUI();
    }

}
