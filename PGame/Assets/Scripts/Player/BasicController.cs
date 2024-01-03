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
    public LayerMask hitLayer;

    private StateMachine<BaseState> machine;

    // States
    Idle idle;
    Run run;
    Air air;
    Hit hit;
    Dash dash;

    private float facingDirection = 1;
    private bool canJump = true;
    private Timer jumpTimer;

    private bool canDash = true;
    private Timer dashTimer;

    private Transform target;
    public LayerMask targetLayer;
    public float targetRadius;
    public float minDot;
    public RectTransform targetHUD;

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

        machine.transitions.Add(new StateMachineTransition<BaseState>(idle, run, () => Mathf.Abs(h) > 0.2f && !weapon.IsShooting));
        machine.transitions.Add(new StateMachineTransition<BaseState>(run, idle, () => Mathf.Abs(h) <= 0.2f || weapon.IsShooting));

        machine.transitions.Add(new StateMachineTransition<BaseState>(idle, air, () => !ground.IsOverlapped));
        machine.transitions.Add(new StateMachineTransition<BaseState>(air, idle, () => ground.IsOverlapped && Mathf.Abs(h) <= 0.05f));
        machine.transitions.Add(new StateMachineTransition<BaseState>(run, air, () => !ground.IsOverlapped));
        machine.transitions.Add(new StateMachineTransition<BaseState>(air, run, () => ground.IsOverlapped && Mathf.Abs(h) > 0.05f));

        machine.transitions.Add(new StateMachineTransition<BaseState>(hit, idle, () => ground.IsOverlapped && hit.Elapsed > 0.5f));

        machine.transitions.Add(new StateMachineTransition<BaseState>(dash, idle, null));
        machine.transitions.Add(new StateMachineTransition<BaseState>(dash, idle, () => ground.IsOverlapped && dashTimer.Elapsed > 0.2f));

        machine.SetState(idle);

        life.onHit += onHit;
        Life.anyHit += (layer) =>
        {
            if ((hitLayer &  1 << layer) != 0)
            {
                canJump = true;
            }
        };
        rollCollider.onEnter += onRollCollide;
        machine.setup += SetupScript;
    }

    private void onRollCollide(Collision2D collision)
    {
        if(machine.Current == dash && (entityLayer & 1 << collision.gameObject.layer) != 0)
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

    private void SetupScript(BaseState state)
    {
        if (state != null)
        {
            state.h = h;
            if (state.ControlFacingDirection)
            {
                facingDirection = h < -0.2 ? -1 : h > 0.2 ? 1 : facingDirection;
            }
            state.facingDirection = facingDirection;
        }
    }

    private void Update()
    {
        UpdateTarget();

        SetupScript(machine.Current);
        transform.localScale = new Vector3(facingDirection, 1, 1);
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
        if (!canJump && ground.IsOverlapped && jumpTimer.Elapsed > 0.4f)
        {
            canJump = true;
        }
    }

    private void UpdateTarget() {
        Vector2 direction = new Vector2(h, v);
        Collider2D[] inRange = Physics2D.OverlapCircleAll(transform.position, targetRadius, targetLayer);
        float maxDot = 0;
        target = null;
        for(int i = 0; i < inRange.Length; i++)
        {
            float dot = Vector2.Dot((inRange[i].transform.position - transform.position).normalized, direction);
            if(dot > Mathf.Max(maxDot,minDot))
            {
                maxDot = dot;
                target = inRange[i].transform;
            }
        }
        if(target != null)
        {
            targetHUD.gameObject.SetActive(true);
            targetHUD.position = Camera.main.WorldToScreenPoint(target.position);
        }
        else
        {
            targetHUD.gameObject.SetActive(false);
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
            else if (canJump && jumpTimer.Elapsed > 0.4f && a < 0.2f)
            {
                Jump();
                canJump = false;
            }
        }
    }

    private void Jump()
    {
        Vector2 direction = new Vector2(h, v).normalized;
        body.velocity = Vector2.zero;
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
