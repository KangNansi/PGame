using UnityEngine;
using System.Collections;
using PlayerStates;
using System;

public class BasicController : PlayerController
{
    public bool debug;

    public Rigidbody2D body;
    public SpriteRenderer sprite;

    // Detectors
    public PointDetector2D ground;
    public ColliderEvent rollCollider;

    public LayerMask entityLayer;

    public Weapon weapon;

    public Life life;

    public Animator animator;
    public AnimatorParameter isWalkingParam;
    public AnimatorParameter isFallingParam;
    public AnimatorParameter rollParam;

    public float speed;
    public float jumpForce;
    public float jumpAddedForce;
    public float jumpAddTime;
    public Vector2 hitForce;
    public float dashForce;
    public float dashDuration;

    private StateMachine<BaseState> machine;

    // States
    Idle idle;
    Run run;
    Air air;
    Hit hit;
    Dash dash;

    private float facingDirection = 1;
    private Timer jumpTimer;

    private bool canDash = true;
    private Timer dashTimer;

    private void Initialize()
    {
        isWalkingParam.Initialize(animator);
        isFallingParam.Initialize(animator);
        rollParam.Initialize(animator);

        machine = new StateMachine<BaseState>();

        idle = new Idle(body);
        run = new Run(isWalkingParam, body, speed);
        air = new Air(isFallingParam, body, speed);
        hit = new Hit(body, sprite, life, hitForce);
        dash = new Dash(this);

        machine.transitions.Add(new StateMachineTransition<BaseState>(idle, run, () => Mathf.Abs(h) > 0.2f));
        machine.transitions.Add(new StateMachineTransition<BaseState>(run, idle, () => Mathf.Abs(h) <= 0.2f));

        machine.transitions.Add(new StateMachineTransition<BaseState>(idle, air, () => !ground.IsOverlapped));
        machine.transitions.Add(new StateMachineTransition<BaseState>(air, idle, () => ground.IsOverlapped && Mathf.Abs(h) <= 0.05f));
        machine.transitions.Add(new StateMachineTransition<BaseState>(run, air, () => !ground.IsOverlapped));
        machine.transitions.Add(new StateMachineTransition<BaseState>(air, run, () => ground.IsOverlapped && Mathf.Abs(h) > 0.05f));

        machine.transitions.Add(new StateMachineTransition<BaseState>(hit, idle, () => ground.IsOverlapped && hit.Elapsed > 0.5f));

        machine.transitions.Add(new StateMachineTransition<BaseState>(dash, idle, null));

        machine.SetState(idle);

        life.onHit += onHit;
        rollCollider.onEnter += onRollCollide;
    }

    private void onRollCollide(Collision2D collision)
    {
        if(machine.Current == dash)
        {
            Vector2 bestNormal = Vector2.zero;
            float bestDot = 1;
            for(int i = 0; i < collision.contactCount; i++)
            {
                Vector2 normal = collision.contacts[i].normal;
                float dot = Vector2.Dot(dash.direction, normal);
                if(dot < bestDot)
                {
                    bestDot = dot;
                    bestNormal = normal;
                }
            }

            body.AddForce(bestNormal * 12f, ForceMode2D.Impulse);
            canDash = true;
            if (bestDot < -0.5f)
            {
                
                //machine.SetState(idle);
            }
        }
    }

    private void onHit()
    {
        machine.SetState(hit);
    }

    private void Check()
    {
        if(machine == null)
        {
            Initialize();
        }
    }

    private void Start()
    {
        Check();
    }

    private void Update()
    {
        if (machine.Current != null)
        {
            machine.Current.h = h;
            if (machine.Current.ControlFacingDirection)
            {
                facingDirection = h < -0.2 ? -1 : h > 0.2 ? 1 : facingDirection;
                transform.localScale = new Vector3(facingDirection, 1, 1);
            }
            machine.Current.facingDirection = facingDirection;
        }
        machine.Update();

        // Weapon Direction control
        
        if(new Vector2(h, v).magnitude > 0.77f)
        {
            Vector2 direction = new Vector2(facingDirection * h, v).normalized;
            float rotation = Quaternion.FromToRotation(Vector2.right, direction).eulerAngles.z;
            rotation = Mathf.RoundToInt(rotation / 45f) * 45f;
            weapon.transform.localRotation = Quaternion.Euler(0,0,rotation);
        }
        

        if (!canDash && dashTimer.Elapsed > 0.5f && ground.IsOverlapped)
        {
            canDash = true;
        }
    }

    private void FixedUpdate()
    {
        // Jump control
        if(machine.Current.CanJump)
        {
            if (jumpTimer.Elapsed < jumpAddTime && aDown)
            {
                body.AddForce(Vector2.up * jumpAddedForce * (jumpAddTime - jumpTimer.Elapsed), ForceMode2D.Force);
            }
            else if (ground.LastOverlapped < 0.2f && jumpTimer.Elapsed > 0.4f && a < 0.2f)
            {
                Jump();
            }
        }
    }

    private void Jump()
    {
        Vector2 direction = new Vector2(h, v).normalized;
        body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jumpTimer.Restart();
    }

    public override void B()
    {
        if(canDash)
        {
            Vector2 direction = new Vector2(h, v).normalized;
            dash.direction = direction;
            machine.SetState(dash);
            canDash = false;
            dashTimer.Restart();
        }
    }

    public override void BUp()
    {

    }

    public override void C()
    {
        weapon.Activate();
    }

    public override void CUp()
    {
        weapon.Deactivate();
    }

    private void OnValidate()
    {
        Initialize();
        machine.DebugMode = debug;
    }
}
