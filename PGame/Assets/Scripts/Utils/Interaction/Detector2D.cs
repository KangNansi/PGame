using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Detector2D : MonoBehaviour
{
    public Collider2D trigger;
    public ContactFilter2D filter;

    public Collider2D[] entities = new Collider2D[5];
    
    public int EntityCount { get; private set; }

    // Update is called once per frame
    void Update()
    {
        EntityCount = trigger.OverlapCollider(filter, entities);
    }

    
}
