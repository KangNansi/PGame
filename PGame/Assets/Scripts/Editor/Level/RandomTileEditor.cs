using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RandomTile))]
public class RandomTileEditor : EditorBase<RandomTile>
{


    public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
    {
        RandomTile t = target as RandomTile;
        Texture2D tex = new Texture2D(width, height);
        EditorUtility.CopySerialized(AssetPreview.GetAssetPreview(t.sprite), tex);
        return tex; 
    }
}