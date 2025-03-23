using UnityEngine;
using UnityEngine.Tilemaps;

public class BlockPlacer : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase tile;
    public Collider2D miningArea; // Reference to the mining area collider


    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left Click to Place Block
        {
            PlaceBlock();
        }
        else if (Input.GetMouseButtonDown(1)) // Right Click to Remove Block
        {
            RemoveBlock();
        }
    }

    public void PlaceBlock()
    {


        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int tilePos = tilemap.WorldToCell(worldPos);

        if (IsInsideMiningArea(tilePos) && tilemap.GetTile(tilePos) == null)
        {
            tilemap.SetTile(tilePos, tile);
 
        }
    }

    public void RemoveBlock()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int tilePos = tilemap.WorldToCell(worldPos);

        if (IsInsideMiningArea(tilePos) && tilemap.GetTile(tilePos) != null)
        {
            tilemap.SetTile(tilePos, null);

        }
    }

    private bool IsInsideMiningArea(Vector3Int tilePos)
    {
        Vector3 worldPos = tilemap.CellToWorld(tilePos);
        return miningArea.bounds.Contains(worldPos);
    }
}