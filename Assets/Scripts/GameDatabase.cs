using System.Collections.Generic;
using UnityEngine;

public class GameDatabase : MonoBehaviour {

    public SpriteAlphabet alphabet;
    public static Dictionary<char, Sprite> charSprite;
    public static bool initYet = false;
    public SpriteRenderer spriteLetterPrefab;

    private void Awake() {
        if (!initYet) {

            LoadAlphabet(alphabet);
            SpriteWord.rendererprefab = spriteLetterPrefab;

        }

    }


    public static void LoadAlphabet(SpriteAlphabet abet) {
        charSprite = new Dictionary<char, Sprite>();

        for (int i = 0; i < abet.chars.Count; i++) {
            charSprite.Add(abet.chars[i].c, abet.chars[i].sprite);
        }

        initYet = true;
    }


}

