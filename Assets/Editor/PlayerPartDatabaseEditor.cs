using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerPartDataBase))]
public class PlayerPartDatabaseEditor : Editor {

    public override void OnInspectorGUI() {
        if (GUILayout.Button("Generate From Editor")) {
            ((PlayerPartDataBase)target).PopulateFromEditor();
        }
        base.OnInspectorGUI();
    }

}

