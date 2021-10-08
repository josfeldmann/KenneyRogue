using System.Collections.Generic;
using UnityEngine;

public class HexNode : MonoBehaviour {

    public RogueHex associatedHex;
    public List<HexNode> neighbours = new List<HexNode>();

}
