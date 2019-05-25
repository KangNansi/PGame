using UnityEngine;
using System.Collections;

namespace MaskedShooterStates
{
    public class Run : State
    {
        private AnimatorParameter runParam;
        private Rigidbody2D body;
        private float speed;

        private Timer timer;
        private float direction;

        public Run(AnimatorParameter runParam, Rigidbody2D body, float speed)
        {
            this.runParam = runParam;
            this.body = body;
            this.speed = speed;
        }

        public override void Start()
        {
            base.Start();
            runParam.SetBool(true);
            timer.Restart();
            direction = Random.Range(0, 2) == 0 ? -1 : 1;
            body.transform.localScale = new Vector3(direction, 1, 1);
        }

        public override void Update()
        {
            body.velocity = new Vector2(direction * speed, body.velocity.y);
            Debug.Log(body.velocity);
            if(timer.Elapsed > 1.5f)
            {
                Stop();
            }
        }

        public override void End()
        {
            runParam.SetBool(false);
        }
    }
}
