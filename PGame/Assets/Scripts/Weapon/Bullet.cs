using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D body;
    public float speed;
    public float hitValue;
    public Vector2 direction;
    public ParticleSystem destroyParticle;

    public LayerMask hitLayer;

    private void Start()
    {
        transform.right = direction;
    }

    // Update is called once per frame
    void Update()
    {
        transform.right = direction;
        body.velocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ParticleSystem particle = Instantiate(destroyParticle);
        particle.transform.position = transform.position;
        Destroy(gameObject);
        if ((1<<collision.gameObject.layer & hitLayer.value) != 0)
        {
            Life life = collision.GetComponent<Life>();
            if(life != null)
            {
                life.Hit(hitValue);
            }
        }
    }
}
