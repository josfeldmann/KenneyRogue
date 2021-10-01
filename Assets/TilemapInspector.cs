using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

[CustomEditor(typeof(Tilemap))]
[CanEditMultipleObjects]
public class LookAtPointEditor : Editor {
    
    

    public override void OnInspectorGUI() {
        if (GUILayout.Button("ClearAllTiles")) {
            ((Tilemap)target).ClearAllTiles();
        }
        DrawDefaultInspector();    
    }
}