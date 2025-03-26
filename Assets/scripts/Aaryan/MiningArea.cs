using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class MiningArea : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase mineableTile; // Reference to the type of tile that can be mined

    public void MineBlock()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int tilePos = tilemap.WorldToCell(worldPos);

        if (tilemap.GetTile(tilePos) == mineableTile)
        {
            tilemap.SetTile(tilePos, null);
        }
    }
}
