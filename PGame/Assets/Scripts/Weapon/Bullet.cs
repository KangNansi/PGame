using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D body;
    public float speed;
    public Vector2 direction;
    public ParticleSystem destroyParticle;

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
    }
}
