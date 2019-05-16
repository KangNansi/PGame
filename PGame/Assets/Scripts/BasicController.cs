using UnityEngine;
using System.Collections;

public class BasicController : PlayerController
{
    public Rigidbody2D body;
    public Collider2D groundCheck;
    public SpriteRenderer spriteRenderer;

    public Weapon weapon;

    public Animator animator;
    private int isWalkingParam;
    private int isRollingParam;

    public float speed;
    public float jumpForce;
    public float rollSpeed;
    public float rollSpeedGain;

    private bool isRolling;

    private void Start()
    {
        isWalkingParam = Animator.StringToHash("IsWalking");
        isRollingParam = Animator.StringToHash("IsRolling");
    }

    private void FixedUpdate()
    {
        if (!isRolling)
        {
            body.velocity = new Vector2(h * speed, body.velocity.y);
            bool isWalking = Mathf.Abs(body.velocity.x) > 0.0001f && groundCheck.IsTouchingLayers();
            animator.SetBool(isWalkingParam, isWalking);
            if (isWalking)
            {
                animator.speed = Mathf.Max(0.3f, Mathf.Abs(Mathf.Sin(h * 3.14f * 0.5f)));
                transform.localScale = new Vector3(h < 0f ? -1 : 1, 1, 1);
                //spriteRenderer.flipX = h < 0f;
            }
        }
        else
        {
            float velocityMagnitude = body.velocity.magnitude;
            velocityMagnitude = Mathf.Min(rollSpeed, velocityMagnitude + Time.deltaTime * rollSpeedGain);
            body.velocity = body.velocity.normalized * velocityMagnitude;
        }
    }

    public override void A()
    {
        if (groundCheck.IsTouchingLayers())
        {
            body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    public override void B()
    {
        animator.SetBool(isRollingParam, true);
        isRolling = true;
        body.freezeRotation = false;
    }

    public override void BUp()
    {
        animator.SetBool(isRollingParam, false);
        isRolling = false;
        transform.localRotation = Quaternion.identity;
        body.freezeRotation = true;
    }

    public override void C()
    {
        weapon.Shoot();
    }
}
