using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/MapDatabase")]
public class MapDatabase : ScriptableObject {
    public static Dictionary<TileType, TileGroup> tileDict = null;
    public List<TileGroup> tiles = new List<TileGroup>();
    


    public void Init() {
        if (tileDict == null) {
            tileDict = new Dictionary<TileType, TileGroup>();
            foreach (TileGroup t in tiles) {
                tileDict.Add(t.tileType, t);
            }
        }
    }
}
