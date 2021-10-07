using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KeyCodeToSprite {
    public KeyCode keyCode;
    public Sprite sprite;
}

[CreateAssetMenu(menuName = "Data/StatDatabase")]
public class StatDatabase : ScriptableObject {

    public static Dictionary<StatEnum, StatGroup> stats = new System.Collections.Generic.Dictionary<StatEnum, StatGroup>();
    public static bool initYet = false;
    public static Dictionary<KeyCode, Sprite> keyCodeToSprite = new Dictionary<KeyCode, Sprite>();
    public List<StatGroup> statgroups = new List<StatGroup>();
    public List<KeyCodeToSprite> keysToSprites = new List<KeyCodeToSprite>();
    public void Init() {
        if (!initYet) {
            keyCodeToSprite = new Dictionary<KeyCode, Sprite>();
            stats = new Dictionary<StatEnum, StatGroup>();
            foreach (KeyCodeToSprite code in keysToSprites) {
                keyCodeToSprite.Add(code.keyCode, code.sprite);
            }
            foreach (StatGroup st in statgroups) {
                stats.Add(st.statEnum, st);
            }
            initYet = true;
        }
    }

}
