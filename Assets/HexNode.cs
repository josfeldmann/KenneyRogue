using System;
using System.Collections.Generic;
using UnityEngine;

public class HexNode : MonoBehaviour {

    public RogueHex associatedHex;
    public TileGroup tileInfo;
    public List<HexNode> neighbours = new List<HexNode>();
    public SpriteRenderer spriteRenderer;
    public void SetTile() {
        spriteRenderer.sprite = tileInfo.tileSprite;
    }
}
