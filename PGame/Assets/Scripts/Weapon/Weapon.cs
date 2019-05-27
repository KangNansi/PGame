using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    public Bullet bulletPrefab;

    public float shootFrequency;

    private Timer timer;
    private bool activated = false;

    public void Activate()
    {
        if(timer.Elapsed > 1 / shootFrequency)
        {
            Shoot();
            timer.Restart();
        }

        
        activated = true;
    }

    public void Deactivate()
    {
        activated = false;
    }

    private void Shoot()
    {
        Bullet bullet = Instantiate(bulletPrefab);
        bullet.direction = Mathf.Sign(transform.lossyScale.x) * Vector2.right;
        bullet.transform.position = transform.position;
    }

    private void Update()
    {
        if(timer.Elapsed > 1 / shootFrequency && activated)
        {
            timer -= (1 / shootFrequency);
            Shoot();
        }
    }
}
