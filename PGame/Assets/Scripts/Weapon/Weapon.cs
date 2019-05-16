using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    public Bullet bulletPrefab;

    public void Shoot()
    {
        Bullet bullet = Instantiate(bulletPrefab);
        bullet.direction = Mathf.Sign(transform.lossyScale.x) * Vector2.right;
        bullet.transform.position = transform.position;
    }
}
