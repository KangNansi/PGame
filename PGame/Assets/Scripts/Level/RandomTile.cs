using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu()]
public class RandomTile : Tile
{
    public Texture2D icon;
    public Sprite[] sprites;

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        base.GetTileData(position, tilemap, ref tileData);
        tileData.sprite = sprites[UnityEngine.Random.Range(0, sprites.Length)];
    }
}