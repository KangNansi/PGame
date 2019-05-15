using UnityEngine;
using System.Collections;

public class LevelChunk : MonoBehaviour
{
    public const int CHUNK_SIZE = 40;

    [SerializeField]
    private LevelBlock[] blocks = new LevelBlock[CHUNK_SIZE * CHUNK_SIZE];

    public void Initialize()
    {
        for(int i = 0; i < CHUNK_SIZE * CHUNK_SIZE; i++)
        {
            blocks[i] = new LevelBlock();
        }
    }

    public LevelBlock this[int x, int y] {
        get{
            Debug.Log(x + " " + y);
            return blocks[x * CHUNK_SIZE + y];
        }
        set {
            blocks[x * CHUNK_SIZE + y] = value;
        }
    }
}
