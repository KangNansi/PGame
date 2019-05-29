using UnityEngine;
using System.Collections;

public class Hitter : MonoBehaviour
{
    public LayerMask layerMask;
    public float damage;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if((1 << collision.gameObject.layer & layerMask) != 0)
        {
            Life life = collision.GetComponent<Life>();
            if(life != null)
            {
                life.Hit(gameObject, damage);
            }
        }
    }
}
