using UnityEngine;
using System.Collections;

public class HeartUI : MonoBehaviour
{
    public GameObject fullHeart;

    public void Set(bool value)
    {
        fullHeart.SetActive(value);
    }
}
