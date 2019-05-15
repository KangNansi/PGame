using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level : MonoBehaviour
{
    [SerializeField]
    private List<LevelChunk> chunks;
    [SerializeField]
    private List<Vector2Int> keys;

    private Dictionary<Vector2Int, LevelChunk> chunkDic;

    private void CheckDictionary()
    {
        if(chunkDic == null)
        {
            chunkDic = new Dictionary<Vector2Int, LevelChunk>();
            for(int i = 0; i < chunks.Count; i++)
            {
                chunkDic.Add(keys[i], chunks[i]);
            }
        }
    }

    private Vector2Int Get(int x, int y)
    {
        if (x < 0) x--;
        if (y < 0) y--;

        CheckDictionary();
        Vector2Int key = new Vector2Int(x / LevelChunk.CHUNK_SIZE, y / LevelChunk.CHUNK_SIZE);
        if (!chunkDic.ContainsKey(key))
        {
            GameObject go = new GameObject(key.x + " " + key.y);
            go.transform.SetParent(transform);
            go.transform.position = new Vector3(key.x, key.y) * LevelChunk.CHUNK_SIZE;
            LevelChunk chunk = go.AddComponent<LevelChunk>();
            chunk.Initialize();
            chunkDic.Add(key, chunk);
            chunks.Add(chunk);
            keys.Add(key);
        }
        return key;
    }

    public LevelBlock this[int x, int y] {
        get {
            return chunkDic[Get(x, y)][x % LevelChunk.CHUNK_SIZE, y % LevelChunk.CHUNK_SIZE];
        }
        set {
            chunkDic[Get(x, y)][x % LevelChunk.CHUNK_SIZE, y % LevelChunk.CHUNK_SIZE] = value;
        }
    } 
}
