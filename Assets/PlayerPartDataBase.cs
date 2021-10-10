using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
[CreateAssetMenu(menuName = "Data/PlayerPartDatabase")]
public class PlayerPartDataBase : ScriptableObject {

    public List<RaceObject> races = new List<RaceObject>();
    public List<WeaponObject> weapons = new List<WeaponObject>();
    public List<AbilityObject> abilities = new List<AbilityObject>();

    public void PopulateFromEditor() {
#if UNITY_EDITOR
        races = new List<RaceObject>();
        weapons = new List<WeaponObject>();
        abilities = new List<AbilityObject>();
        string[] guids = AssetDatabase.FindAssets("t:TrackedObject");
        foreach (string s in guids) {
            UnityEngine.Object o = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(AssetDatabase.GUIDToAssetPath(s));

            if (o is RaceObject) {
                races.Add((RaceObject)o);
            } else if (o is WeaponObject) {
                weapons.Add((WeaponObject)o);
            } else if (o is AbilityObject) {
                AbilityObject a = (AbilityObject)o;
                abilities.Add(a);
            }
        }

        EditorUtility.SetDirty(this);

#endif
    }
}

