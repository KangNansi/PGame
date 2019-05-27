using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LifeBar : MonoBehaviour
{
    public Life life;
    public HeartUI heartPrefab;

    private List<HeartUI> hearts = new List<HeartUI>();

    // Use this for initialization
    void Start()
    {
        life.onHit += Refresh;

        for(int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < Mathf.RoundToInt(life.maxLife); i++)
        {
            HeartUI instance = Instantiate(heartPrefab, transform);
            hearts.Add(instance);
        }
    }

    private void Refresh()
    {
        for(int i = 0; i < Mathf.RoundToInt(life.maxLife); i++)
        {
            hearts[i].Set(life.CurrentLife >= i+1);
        }
    }
}
