using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharSprite {
    public char c;
    public Sprite sprite;
}

[CreateAssetMenu(menuName = "SpriteAlphabet")]
public class SpriteAlphabet : ScriptableObject
{

    public List<CharSprite> chars = new List<CharSprite>();


}
