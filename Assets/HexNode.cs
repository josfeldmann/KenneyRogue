using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HexNode : HoverBehaviour {

    public RoguelikeMapCreator map;
    public RogueHex associatedHex;
    public TileGroup tileInfo;
    public List<HexNode> neighbours = new List<HexNode>();
    public SpriteRenderer spriteRenderer;

    public override void OnHoverEnter() {
        map.SetCursor(this);
    }

    public override void OnHoverExit() {
        OnMouseExit();
    }

    public void OnMouseEnter() {
        if (tileInfo.tileType == TileType.none) return;
        TooltipMaster.current.SetHexNode(this);
    }

    public void OnMouseExit() {
        TooltipMaster.current.CloseToolTip();
    }


 
    public void SetTile() {
        spriteRenderer.sprite = tileInfo.tileSprite;
    }
}
