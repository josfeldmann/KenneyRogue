using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour {

    public const int width = 18, height = 10;

    public int sizex = 1, sizey = 1;
    public Tilemap top, bottom;
    [HideInInspector] public int x, y;

    public Transform topJoin, bottomJoin, rightJoin, leftJoin;


    private void OnDrawGizmos() {
        
        if (topJoin != null) {
            Gizmos.DrawCube(topJoin.position - new Vector3(0, 0.25f, 0), new Vector3(2, 0.5f, 0));
        }
        if (bottomJoin != null) {
            Gizmos.DrawCube(bottomJoin.position + new Vector3(0, 0.25f, 0), new Vector3(2, 0.5f, 0));
        }
        if (rightJoin != null) {
            Gizmos.DrawCube(rightJoin.position + new Vector3(-0.25f, 0), new Vector3(0.5f, 2f, 0));
        }
        if (leftJoin != null) {
            Gizmos.DrawCube(leftJoin.position + new Vector3(0.25f, 0), new Vector3(0.5f, 2f, 0));
        }
    }

    internal bool CanFit(bool[,] roomGrid, int x, int y) {

        if (topJoin == null && (y + 1) < roomGrid.GetLength(1) && roomGrid[x, y + 1]) {
            return false;
        }
        if (bottomJoin == null && y > 0 && roomGrid[x, y - 1]) {
            return false;
        }
        if (rightJoin == null && (x + 1) < roomGrid.GetLength(0) && roomGrid[x + 1, y]) {
            return false;
        }
        if (leftJoin == null && x > 0 && roomGrid[x - 1, y]) {
            return false;
        }

        return true;

    }
}




