using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

[System.Serializable]
public enum TileType {
    battle = 0, town = 1, hazard = 2, shop = 3, blacksmith = 4, bazaar = 5, hiddenMountain = 6, mountain = 7, miniboss = 8, none = 9
}

[System.Serializable]
public class RogueHex {

    public TileType tileType;
    public Vector2Int index;
    public List<Vector2Int> neighbours = new List<Vector2Int>();
    bool visited = false;

    public RogueHex(TileType tileType, Vector2Int index, List<Vector2Int> neighbours) {
        this.tileType = tileType;
        this.index = index;
        this.neighbours = neighbours;
    }
}

[System.Serializable]
public class TileGroup {
    public TileType tileType;
    public Sprite tileSprite;
    public LocalizedString tileName, tileDescription;

    public string GetName() {
        return tileName.GetLocalizedString();
    }

    public string GetDescription() {
        return tileName.GetLocalizedString();
    }

}

public class RoguelikeMapCreator : MonoBehaviour {

    public static RogueHex[,] hexGrid = null;
    public static RogueHexMap rogueMap;
    public MapDatabase mapDatabase;
    public RogueHexMap dummyMap;
    public float xOffset = 2, yOffset = 1.5f;
    public HexNode nodePrefab;
    public HexNode[,] nodes = null;
    public Transform nodeGrouping;

    private void Awake() {
        mapDatabase.Init();
        if (hexGrid == null) {
            if (rogueMap == null) {
                rogueMap = dummyMap;
            }
            GenerateGrid();
        }
        GenerateNodesFromGrids();
    }


    public void GenerateGrid() {
        hexGrid = new RogueHex[rogueMap.width + 1, rogueMap.height];
        for (int y = 0; y < rogueMap.height; y++) {
            for (int x = 0; x < rogueMap.width; x++) {
                RogueHex r = new RogueHex(TileType.battle, new Vector2Int(x, y), new List<Vector2Int>());
                hexGrid[x, y] = r;
                if (y % 2 == 1 && x == rogueMap.width - 1) {
                    RogueHex rr = new RogueHex(TileType.battle, new Vector2Int(x, y), new List<Vector2Int>());
                    hexGrid[x + 1, y] = rr;
                }
            }
        }

        //MakeNeighbours
        for (int y = 0; y < rogueMap.height; y++) {
            for (int x = 0; x < (rogueMap.width + 1); x++) {

                if (hexGrid[x,y] != null) {
                    RogueHex h = hexGrid[x, y];
                    if (x < rogueMap.width && hexGrid[x+1,y] != null) {
                        h.neighbours.Add(new Vector2Int(x + 1, y));
                    }
                    if (x > 0) {
                        h.neighbours.Add(new Vector2Int(x - 1, y));
                    }
                    if (y > 0) {
                        h.neighbours.Add(new Vector2Int(x, y - 1));
                        if (y % 2 == 0) {
                            h.neighbours.Add(new Vector2Int(x + 1, y - 1));
                        } else if (x > 0) {
                            h.neighbours.Add(new Vector2Int(x - 1, y - 1));
                        }
                    }
                    if (y < (rogueMap.height - 1)) {
                        h.neighbours.Add(new Vector2Int(x, y + 1));
                        if (y % 2 == 0) {
                            h.neighbours.Add(new Vector2Int(x + 1, y + 1));
                        } else if (x > 0) {
                            h.neighbours.Add(new Vector2Int(x - 1, y + 1));
                        }
                    }
                    
                }

            }
        }
    }


    public void GenerateNodesFromGrids() {
        nodes = new HexNode[rogueMap.width + 1, rogueMap.height];
        Vector3 offset = new Vector3(rogueMap.width * xOffset, rogueMap.height * yOffset, 0) / 2;
        for (int y = 0; y < rogueMap.height; y++) {
            for (int x = 0; x < (rogueMap.width + 1); x++) {
                if (hexGrid[x, y] != null) {
                    HexNode h = Instantiate(nodePrefab, nodeGrouping);
                    h.associatedHex = hexGrid[x, y];
                    nodes[x, y] = h;
                    if (y % 2 == 0) {
                        h.transform.localPosition = new Vector3((xOffset * (x) + 1), yOffset * y) - offset;
                    } else {
                        h.transform.localPosition = new Vector3(xOffset * (x), yOffset * y) - offset;
                    }
                }
            }
        }


        for (int y = 0; y < rogueMap.height; y++) {
            for (int x = 0; x < (rogueMap.width + 1); x++) {
                if (hexGrid[x,y] != null) {
                    HexNode n = nodes[x, y];
                    n.name = "X:" + x.ToString() + "Y:" + y.ToString(); 
                    n.neighbours = new List<HexNode>();
                    foreach (Vector2Int v in n.associatedHex.neighbours) {
                        n.neighbours.Add(nodes[v.x, v.y]);
                    }
                }
            }
        }
    }
}
