using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/RogueHexMap")]
public class RogueHexMap : ScriptableObject {
    public string key = "forest";
    public int width = 5;
    public int height = 6;
    public int numberOfHazard = 7;
    public int numberOfShops = 4;
    public int numberOfArmory = 4;
    public int numberOfBazaar = 1;
    public int numberOfTowns = 5;
    public int numberOfMiniBosses = 5;
    public int numberOfSkillShop = 3;
    public bool rowTop = false, rowBot = false, rowLeft, rowRight;
    public Vector2Int startSpot = new Vector2Int(0, 5);
    public Vector2Int bossSpotrange = new Vector2Int(1, 7);
    public Vector2Int mountainAmount = new Vector2Int(3, 10);
    public Vector2Int seedMountainAmount = new Vector2Int(0, 3);

    public List<BattleMap> battleMaps = new List<BattleMap>();

}
