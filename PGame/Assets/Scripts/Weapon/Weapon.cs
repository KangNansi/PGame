using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    public Bullet bulletPrefab;

    public Transform shootOrigin;

    public float shootFrequency;

    private Timer timer;
    private bool activated = false;
    public bool IsShooting {
        get {
            return activated;
        }
    }

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
        bullet.direction = transform.lossyScale.x * shootOrigin.transform.right;
        bullet.transform.position = shootOrigin.position;
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
