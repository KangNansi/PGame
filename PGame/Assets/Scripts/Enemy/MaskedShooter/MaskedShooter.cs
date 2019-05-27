using UnityEngine;
using System.Collections;
using MaskedShooterStates;

public class MaskedShooter : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D body;
    public Collider2D fallChecker;
    public LayerMask groundLayer;

    public AnimatorParameter runParam;

    public float minIdleWait, maxIdleWait;
    public float runSpeed;

    private StateMachine<State> machine;

    private void Initialize()
    {
        runParam.Initialize(animator);
        machine = new StateMachine<State>();

        Idle idle = new Idle(minIdleWait, maxIdleWait);
        Run run = new Run(runParam, body, fallChecker, groundLayer, runSpeed, transform.position.x);

        machine.transitions.Add(new StateMachineTransition<State>(idle, run, null));
        machine.transitions.Add(new StateMachineTransition<State>(run, idle, null));

        machine.SetState(idle);
    }

    void Check()
    {
        if(!runParam.Initialized || machine == null)
        {
            Initialize();
        }
    }

    // Use this for initialization
    void Start()
    {
        Check();
    }

    // Update is called once per frame
    void Update()
    {
        Check();
        machine.Update();
    }

    

    private void OnValidate()
    {
        Initialize();
    }
}
