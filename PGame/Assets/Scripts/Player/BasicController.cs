using UnityEngine;
using System.Collections;
using PlayerStates;
using System;

public class BasicController : PlayerController
{
    public bool debug;

    public Rigidbody2D body;

    // Detectors
    public PointDetector2D ground;

    public Weapon weapon;

    public Life life;

    public Animator animator;
    public AnimatorParameter isWalkingParam;
    public AnimatorParameter isFallingParam;

    public float speed;
    public float jumpForce;
    public float jumpAddedForce;
    public float jumpAddTime;
    public Vector2 hitForce;

    private StateMachine<BaseState> machine;

    // States
    Run run;
    Air air;
    Hit hit;

    private float facingDirection = 1;
    private Timer jumpTimer;

    private void Initialize()
    {
        isWalkingParam.Initialize(animator);
        isFallingParam.Initialize(animator);

        machine = new StateMachine<BaseState>();

        Idle idle = new Idle(body);
        run = new Run(isWalkingParam, body, speed);
        air = new Air(isFallingParam, body, speed);
        hit = new Hit(body, life, hitForce);

        machine.transitions.Add(new StateMachineTransition<BaseState>(idle, run, () => Mathf.Abs(h) > 0.05f));
        machine.transitions.Add(new StateMachineTransition<BaseState>(run, idle, () => Mathf.Abs(h) <= 0.05f));

        machine.transitions.Add(new StateMachineTransition<BaseState>(idle, air, () => !ground.IsOverlapped));
        machine.transitions.Add(new StateMachineTransition<BaseState>(air, idle, () => ground.IsOverlapped && Mathf.Abs(h) <= 0.05f && body.velocity.y < 0));
        machine.transitions.Add(new StateMachineTransition<BaseState>(run, air, () => !ground.IsOverlapped));
        machine.transitions.Add(new StateMachineTransition<BaseState>(air, run, () => ground.IsOverlapped && Mathf.Abs(h) > 0.05f && body.velocity.y < 0));

        machine.transitions.Add(new StateMachineTransition<BaseState>(hit, idle, () => ground.IsOverlapped && body.velocity.y <= 0));

        machine.SetState(idle);

        life.onHit += onHit;
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
                facingDirection = h < 0 ? -1 : h > 0 ? 1 : facingDirection;
                transform.localScale = new Vector3(facingDirection, 1, 1);
            }
            machine.Current.facingDirection = facingDirection;
        }
        machine.Update();

        
    }

    private void FixedUpdate()
    {
        // Jump control
        if (jumpTimer.Elapsed < jumpAddTime && aDown)
        {
            body.AddForce(Vector2.up * jumpAddedForce * (jumpAddTime - jumpTimer.Elapsed), ForceMode2D.Force);
        }
        else if(ground.LastOverlapped < 0.2f && jumpTimer.Elapsed > 0.4f && a < 0.2f)
        {
            Jump();
        }
    }

    private void Jump()
    {
        body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jumpTimer.Restart();
    }

    public override void B()
    {

    }

    public override void BUp()
    {

    }

    public override void C()
    {
        weapon.Shoot();
    }

    private void OnValidate()
    {
        Initialize();
        machine.DebugMode = debug;
    }
}
