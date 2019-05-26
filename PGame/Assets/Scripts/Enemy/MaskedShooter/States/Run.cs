using UnityEngine;
using System.Collections;

namespace MaskedShooterStates
{
    public class Run : State
    {
        private AnimatorParameter runParam;
        private Rigidbody2D body;
        private Collider2D fallChecker;
        private LayerMask groundLayer;
        private float speed;
        private float xTarget;

        private Timer timer;
        private float direction;

        public Run(AnimatorParameter runParam, Rigidbody2D body, Collider2D fallChecker, LayerMask groundLayer, float speed, float xTarget)
        {
            this.runParam = runParam;
            this.body = body;
            this.speed = speed;
            this.fallChecker = fallChecker;
            this.groundLayer = groundLayer;
            this.xTarget = xTarget;
        }

        public override void Start()
        {
            base.Start();
            
            timer.Restart();
            direction = body.transform.position.x > xTarget ? -1 : 1;
            body.transform.localScale = new Vector3(direction, 1, 1);
        }

        public override void Update()
        {
            if (timer.Elapsed > 1.5f || !fallChecker.IsTouchingLayers(groundLayer))
            {
                Stop();
                return;
            }
            runParam.SetBool(true);
            body.velocity = new Vector2(direction * speed, body.velocity.y);
        }

        public override void End()
        {
            runParam.SetBool(false);
        }
    }
}
