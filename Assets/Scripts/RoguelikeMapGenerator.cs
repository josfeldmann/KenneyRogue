using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


[System.Serializable]
public class RoomGroup {
    public List<Room> rooms = new List<Room>();

}

public class RoomConnectionInfo {
    public bool top, bottom, right, left;

    public RoomConnectionInfo(bool top, bool bottom, bool right, bool left) {
        this.top = top;
        this.bottom = bottom;
        this.right = right;
        this.left = left;
    }

    public override bool Equals(object obj) {
        RoomConnectionInfo r = (RoomConnectionInfo)obj;
        return r.top == top && r.bottom == bottom && r.right == right && r.left == left; 
    }

    public override int GetHashCode() {
        int i = 0;
        if (top) i += 1;
        if (bottom) i += 10;
        if (left) i += 100;
        if (right) i += 1000;
        return i;
    }
}


public class RoguelikeMapGenerator : MonoBehaviour
{
    public Camera cam;
    public int gridwidth = 8, gridheight = 8;
    public Vector3Int startPos = new Vector3Int(3, 3, 0);
    private Vector3Int currentPos = new Vector3Int(3,3,0);

    public int numRooms = 10;

    public bool[,] roomGrid;
    public Room[,] rooms;


    public Room startRoom;
    public List<Room> roomprefabs;
    public Transform groupingtransform;

    public Dictionary<RoomConnectionInfo, RoomGroup> connectionToRooms = new Dictionary<RoomConnectionInfo, RoomGroup>();


    private void Awake() {
   
    }

    public List<Vector3Int> openSpots = new List<Vector3Int>();

    public void Setup() {

        connectionToRooms = new Dictionary<RoomConnectionInfo, RoomGroup>();
        foreach (Room r in roomprefabs) {

            RoomConnectionInfo con = new RoomConnectionInfo(r.topJoin != null, r.bottomJoin != null, r.rightJoin != null, r.leftJoin != null);
            if (connectionToRooms.ContainsKey(con)) {
                connectionToRooms[con].rooms.Add(r);
            } else {
                RoomGroup g = new RoomGroup();
                g.rooms = new List<Room>() { r };
                connectionToRooms.Add(con, g);
            }

        }


        currentPos = startPos;
        roomGrid = new bool[gridwidth, gridheight];
        rooms = new Room[gridwidth, gridheight];
        for (int x = 0; x < gridwidth; x++)
            for (int y = 0; y < gridheight; y++) {
                roomGrid[x, y] = false;
            }
        openSpots = new List<Vector3Int>();
      
        if (currentPos.x < gridwidth - 1 && !roomGrid[currentPos.x + 1, currentPos.y]) {
            openSpots.Add(currentPos + Vector3Int.right);
        }
        if (currentPos.x > 0 && !roomGrid[currentPos.x - 1, currentPos.y]) {
            openSpots.Add(currentPos - Vector3Int.right);
        }
        if (currentPos.y < gridheight - 1 && !roomGrid[currentPos.x, currentPos.y + 1]) {
            openSpots.Add(currentPos + Vector3Int.up);
        }
        if (currentPos.y > 0 && !roomGrid[currentPos.x, currentPos.y - 1]) {
            openSpots.Add(currentPos - Vector3Int.up);
        }

        roomGrid[startPos.x, startPos.y] = transform;


        for (int i = 0; i < numRooms; i++) {

            currentPos = openSpots.PickRandom();
            roomGrid[currentPos.x, currentPos.y] = true;
            openSpots.Remove(currentPos);

            if (currentPos.x < gridwidth - 1 && !roomGrid[currentPos.x + 1, currentPos.y]) {
                openSpots.Add(currentPos + Vector3Int.right);
            }
            if (currentPos.x > 0 && !roomGrid[currentPos.x - 1, currentPos.y]) {
                openSpots.Add(currentPos - Vector3Int.right);
            }
            if (currentPos.y < gridheight - 1 && !roomGrid[currentPos.x, currentPos.y + 1]) {
                openSpots.Add(currentPos + Vector3Int.up);
            }
            if (currentPos.y > 0 && !roomGrid[currentPos.x, currentPos.y - 1]) {
                openSpots.Add(currentPos - Vector3Int.up);
            }


        }

        RoomConnectionInfo defRoom = new RoomConnectionInfo(true, true, true, true);

        for (int x = 0; x < gridwidth; x++)
            for (int y = 0; y < gridheight; y++) {
                if (roomGrid[x, y]) {

                    
                    Room rPrefab = null;
                    
                    bool up =  (y + 1) < roomGrid.GetLength(1) && roomGrid[x, y + 1];
                    bool down = y > 0 && roomGrid[x, y - 1];
                    bool right = (x + 1) < roomGrid.GetLength(0) && roomGrid[x + 1, y];
                    bool left = x > 0 && roomGrid[x - 1, y];

                    RoomConnectionInfo con = new RoomConnectionInfo(up, down, right, left);

                    if (connectionToRooms.ContainsKey(con)) {
                        rPrefab = connectionToRooms[con].rooms.PickRandom();
                    } else {
                        rPrefab = connectionToRooms[defRoom].rooms.PickRandom();
                    }




                    Room r = Instantiate(rPrefab, new Vector3(x * Room.width, y * Room.height, 0), Quaternion.identity, groupingtransform);
                    rooms[x, y] = r;
                } else {
                    rooms[x, y] = null;
                }
            }

        cam.transform.position = new Vector3((startPos.x + 0.5f) * Room.width, (startPos.y + 0.5f) * Room.height, -10);

    }





}

public static class MyExtensions {

    public static T PickRandom<T>(this List<T> list) {
        return list[Random.Range(0, list.Count)];
    }


    public static string RemoveSpecialCharacters(this string str) {
        StringBuilder sb = new StringBuilder();
        foreach (char c in str) {
            if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_') {
                sb.Append(c);
            }
        }
        return sb.ToString();
    }


}




