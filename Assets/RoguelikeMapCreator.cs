using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

[System.Serializable]
public enum TileType {
    battle = 0, town = 1, hazard = 2, shop = 3, armory = 4, bazaar = 5, mountainPass = 6, mountain = 7, miniboss = 8, none = 9, boss = 10, startSpot = 11,  hiddentreasure = 12, mysteryEvent = 13
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
    public string dummyseed;
    public static RogueHex[,] hexGrid = null;
    public static RogueHexMap rogueMap;
    public static string currentSeed = null;
    public MapDatabase mapDatabase;
    public RogueHexMap dummyMap;
    public float xOffset = 2, yOffset = 1.5f;
    public HexNode nodePrefab;
    public HexNode[,] nodes = null;
    public Transform nodeGrouping;

    private void Awake() {
        
        mapDatabase.Init();
        if (hexGrid == null) {
            if (currentSeed == null) currentSeed = dummyseed;
            Random.seed = currentSeed.GetHashCode();
            if (rogueMap == null) {
                rogueMap = dummyMap;
            }
            GenerateGrid();
            MakeWalls();
            GenerateRandomSpaces();
            hexGrid[rogueMap.startSpot.x, rogueMap.startSpot.y].tileType = TileType.none;
            Vector2Int endSpot = new Vector2Int(Random.Range(rogueMap.bossSpotrange.x, rogueMap.bossSpotrange.y), rogueMap.height - 1);
            hexGrid[endSpot.x, endSpot.y].tileType = TileType.boss;
            MakeSeedMountains();
            MakeExtraMountains();
            
        }
        GenerateNodesFromGrids();
        SortingOrderFix();
    }


    public void MakeSeedMountains() {
        int seedMountainAmount = Random.Range(rogueMap.seedMountainAmount.x, rogueMap.seedMountainAmount.y);
        if (seedMountainAmount == 0) return;
        List<Vector2Int> availableList = new List<Vector2Int>();

        for (int y = 0; y < rogueMap.height; y++) {
            for (int x = 0; x < rogueMap.width + 1; x++) {
                if (hexGrid[x, y] != null && hexGrid[x, y].tileType == TileType.battle) {
                    bool isSeedSpot = true;
                    foreach (Vector2Int v in hexGrid[x, y].neighbours) {
                       
                        if (hexGrid[v.x, v.y] != null && hexGrid[v.x, v.y].tileType == TileType.mountain) {
                            isSeedSpot = false;
                        }
                    }
                    if (isSeedSpot) availableList.Add(new Vector2Int(x, y));
                }
            }
        }

        for (int i = 0; i < seedMountainAmount; i++) {
            if (availableList.Count == 0) return;
            int index = Random.Range(0, availableList.Count);
            Vector2Int pos = availableList[index];
            RogueHex hex = hexGrid[pos.x, pos.y];
            hex.tileType = TileType.mountain;
            foreach (Vector2Int v in hex.neighbours) {
                if (hexGrid[v.x, v.y] != null && hexGrid[v.x, v.y].tileType == TileType.battle) {
                    if (!availableList.Contains(v)) availableList.Add(v);
                }
            }
            availableList.RemoveAt(index);
        }


    }

    public void MakeExtraMountains() {
        int numMountains = Random.Range(rogueMap.mountainAmount.x, rogueMap.mountainAmount.y);
        if (numMountains <= 0) return;
        List<Vector2Int> availableList = new List<Vector2Int>();
        for (int y = 0; y < rogueMap.height; y++) {
            for (int x = 0; x < rogueMap.width + 1; x++) {
                if (hexGrid[x,y] != null && hexGrid[x,y].tileType == TileType.mountain) {
                    foreach (Vector2Int v in hexGrid[x,y].neighbours) {
                        if (hexGrid[v.x, v.y] != null && hexGrid[v.x, v.y].tileType == TileType.battle) {
                            if (!availableList.Contains(v)) availableList.Add(v);
                        }
                    }
                }
            }
        }

       
        for (int i = 0; i < numMountains; i++ ) {
            if (availableList.Count == 0) return;
            int index = Random.Range(0, availableList.Count);
            Vector2Int pos = availableList[index];
            RogueHex hex = hexGrid[pos.x, pos.y];
            hex.tileType = TileType.mountain;
            foreach (Vector2Int v in hex.neighbours) {
                if (hexGrid[v.x, v.y] != null && hexGrid[v.x, v.y].tileType == TileType.battle) {
                    if (!availableList.Contains(v)) availableList.Add(v);
                }
            }
            availableList.RemoveAt(index);
        }


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

    public void MakeWalls() {
        if (rogueMap.rowTop) {
            for (int i = 0; i < rogueMap.width + 1; i++) {
                if(hexGrid[i, rogueMap.height-1] != null) {
                    hexGrid[i, rogueMap.height - 1].tileType = TileType.mountain;
                }
            }
        }
        if (rogueMap.rowBot) {
            for (int i = 0; i < rogueMap.width + 1; i++) {
                if (hexGrid[i, 0] != null) {
                    hexGrid[i, 0].tileType = TileType.mountain;
                }
            }
        }
        if (rogueMap.rowLeft) {
            for (int i = 0; i < rogueMap.height; i++) {
                if (hexGrid[0, i] != null) {
                    hexGrid[0, i].tileType = TileType.mountain;
                }
            }
        }

        if (rogueMap.rowRight) {
            for (int i = 0; i < rogueMap.height; i++) {

                if (i % 2 == 0) {
                    if (hexGrid[rogueMap.width - 1, i] != null) {
                        hexGrid[rogueMap.width - 1, i].tileType = TileType.mountain;
                    }
                } else { 
                    if (hexGrid[rogueMap.width, i] != null) {
                        hexGrid[rogueMap.width, i].tileType = TileType.mountain;
                    }
                }
            }
        }
    }

    public void SortingOrderFix() {
        for (int y = 0; y < rogueMap.height; y++) {
            for (int x = 0; x < (rogueMap.width + 1); x++) {
               if (nodes[x,y] != null) {
                    nodes[x, y].spriteRenderer.sortingOrder = 3 + rogueMap.height - y;
                }
            }
        }
    }


    public void MakeTiles(TileType type, int number) {
        List<RogueHex> list = new List<RogueHex>();
        for (int y = 0; y < rogueMap.height; y++) {
            for (int x = 0; x < (rogueMap.width + 1); x++) {
                if (hexGrid[x, y] != null && hexGrid[x, y].tileType == TileType.battle) {
                    list.Add(hexGrid[x, y]);
                }
            }
        }


        for (int i = 0; i < number; i++) {
            int index = Random.Range(0, list.Count);
            RogueHex h = list[index];
            list[index].tileType = type;
            foreach (Vector2Int v in list[index].neighbours) {
                if (list.Contains(hexGrid[v.x, v.y])) {
                    list.Remove(hexGrid[v.x, v.y]);
                }
            }
            list.Remove(h);
        }
    }

    public void GenerateRandomSpaces() {
        MakeTiles(TileType.bazaar, rogueMap.numberOfBazaar);
        MakeTiles(TileType.shop, rogueMap.numberOfShops);
        MakeTiles(TileType.miniboss, rogueMap.numberOfMiniBosses);
        MakeTiles(TileType.armory, rogueMap.numberOfArmory);
        MakeTiles(TileType.hazard, rogueMap.numberOfHazard);
        MakeTiles(TileType.town, rogueMap.numberOfTowns);
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
                    n.tileInfo = MapDatabase.tileDict[n.associatedHex.tileType];
                    foreach (Vector2Int v in n.associatedHex.neighbours) {
                        n.neighbours.Add(nodes[v.x, v.y]);
                    }
                    n.SetTile();
                }
            }
        }
    }
}
