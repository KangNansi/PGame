using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Custom Tiles/Animated Tile")]
public class AnimatedTile : TileBase
{
    public Tile.ColliderType colliderType;
    public float animationSpeed = 1;
    public Color color;
    public Sprite[] sprites;

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        tileData.transform = Matrix4x4.identity;
        tileData.color = color;
        if(sprites != null && sprites.Length > 0)
        {
            tileData.sprite = sprites[0];
            tileData.colliderType = colliderType;
        }
    }

    public override bool GetTileAnimationData(Vector3Int position, ITilemap tilemap, ref TileAnimationData tileAnimationData)
    {
        if(sprites.Length > 0)
        {
            tileAnimationData.animatedSprites = sprites;
            tileAnimationData.animationSpeed = animationSpeed;
            tileAnimationData.animationStartTime = 0;
            return true;
        }
        return false;
    }
}