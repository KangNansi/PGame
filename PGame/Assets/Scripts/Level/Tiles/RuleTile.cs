using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System;

[Serializable]
public class Rule
{
    public Sprite Sprite;
    public int[] rules = new int[9];
    public bool flipX, flipY;
}

[CreateAssetMenu(menuName = "Custom Tiles/Rule Tile")]
public class RuleTile : Tile
{
    public List<Rule> rules;

    // This refreshes itself and other RoadTiles that are orthogonally and diagonally adjacent
    public override void RefreshTile(Vector3Int location, ITilemap tilemap)
    {
        for (int yd = -1; yd <= 1; yd++)
            for (int xd = -1; xd <= 1; xd++)
            {
                Vector3Int position = new Vector3Int(location.x + xd, location.y + yd, location.z);
                if (HasTile(tilemap, position))
                    tilemap.RefreshTile(position);
            }
    }

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        base.GetTileData(position, tilemap, ref tileData);
        tileData.sprite = sprite;

        List<Rule> validSprites = new List<Rule>();
        foreach(Rule rule in rules)
        {
            if(IsValid(rule, tilemap, position))
            {
                validSprites.Add(rule);
            }
        }

        if(validSprites.Count > 0)
        {
            Rule selected = validSprites[UnityEngine.Random.Range(0, validSprites.Count)];
            tileData.sprite = selected.Sprite;
            Matrix4x4 m = tileData.transform;
            m.SetTRS(Vector3.zero, Quaternion.identity, new Vector3(selected.flipX ? -1 : 1, selected.flipY ? -1 : 1, 1));
            tileData.transform = m;
            tileData.flags = TileFlags.LockTransform;
        }
    }

    private bool IsValid(Rule rule, ITilemap tilemap, Vector3Int position)
    {
        bool valid = true;
        for(int i = 0; i < 9; i++)
        {
            if (i == 4) continue;
            int x = 0;
            if (i % 3 == 0) x = -1;
            else if (i % 3 == 2) x = 1;
            int y = 0;
            if (i / 3 == 2) y = -1;
            else if (i / 3 == 0) y = 1;
            Vector3Int offset = new Vector3Int(x, y, 0);

            bool hasTile = HasTile(tilemap, position + offset);
            bool hasATile = HasATile(tilemap, position + offset);
            int ruleValue = rule.rules[i];
            if (ruleValue == 2 && !hasTile) valid = false;
            else if (ruleValue == 1 && hasTile) valid = false;
            else if (ruleValue == 3 && !hasATile) valid = false;
            else if (ruleValue == 4 && hasATile) valid = false;
        }
        return valid;
    }

    private bool HasTile(ITilemap tilemap, Vector3Int position)
    {
        return tilemap.GetTile(position) == this;
    }

    private bool HasATile(ITilemap tilemap, Vector3Int position)
    {
        return tilemap.GetTile(position) != null;
    }
}