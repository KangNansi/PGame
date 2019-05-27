using UnityEngine;
using System.Collections;

namespace PlayerStates
{
    public class Air : BaseState
    {
        private AnimatorParameter isFallingParam;
        private Rigidbody2D body;
        private float speed;

        public Air(AnimatorParameter isFallingParam, Rigidbody2D body, float speed)
        {
            this.isFallingParam = isFallingParam;
            this.body = body;
            this.speed = speed;
            this.ControlFacingDirection = true;
        }

        public override void Start()
        {
            base.Start();
        }

        public override void Update()
        {
            base.Update();
            isFallingParam.SetBool(body.velocity.y < 0);
            body.velocity = new Vector2(h * speed, body.velocity.y);
        }

        public override void End()
        {
            base.End();
            isFallingParam.SetBool(false);
        }
    }
}

