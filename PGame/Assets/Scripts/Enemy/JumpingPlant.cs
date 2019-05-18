using UnityEngine;
using System.Collections;

public class JumpingPlant : MonoBehaviour
{
    public Rigidbody2D body;
    public Collider2D groundCheck;
    public Animator animator;
    public Life life;

    private int groundedParam;
    private int jumpParam;
    private int fallParam;

    public FloatRange JumpForce;
    public FloatRange idleTimeRange;

    private Timer jumpTimer;
    private float idleTime;

    // Use this for initialization
    void Start()
    {
        jumpTimer = new Timer();
        groundedParam = Animator.StringToHash("Grounded");
        jumpParam = Animator.StringToHash("Jump");
        fallParam = Animator.StringToHash("Fall");
        jumpTimer.Stop();
        life.onDie += () => Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        bool grounded = groundCheck.IsTouchingLayers();

        if (grounded && jumpTimer.Elapsed > idleTime)
        {
            
            jumpTimer.Stop();

            animator.SetTrigger(jumpParam);
        }


        if (grounded && jumpTimer.Stopped)
        {
            PrepareJump();
        }
        animator.SetBool(groundedParam, grounded);
        animator.SetBool(fallParam, body.velocity.y < 0);
    }

    void Jump()
    {
        Vector3 direction = Quaternion.Euler(0, 0, Random.Range(-30, 30)) * Vector3.up;
        body.AddForce(direction * JumpForce.Pick(), ForceMode2D.Impulse);
    }

    void PrepareJump()
    {
        jumpTimer.Restart();
        idleTime = idleTimeRange.Pick();
    }
}
