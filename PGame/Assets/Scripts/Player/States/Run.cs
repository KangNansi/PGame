using UnityEngine;
using System.Collections;

namespace PlayerStates
{
    public class Run : BaseState
    {
        private AnimatorParameter isWalkingParam;
        private Rigidbody2D body;
        private float speed;

        public Run(AnimatorParameter isWalkingParam, Rigidbody2D body, float speed)
        {
            this.isWalkingParam = isWalkingParam;
            this.body = body;
            this.speed = speed;
            this.ControlFacingDirection = true;
        }

        public override void Start()
        {
            base.Start();
            isWalkingParam.SetBool(true);
        }

        public override void Update()
        {
            base.Update();
            body.velocity = new Vector2(h * speed, body.velocity.y);
        }

        public override void End()
        {
            base.End();
            isWalkingParam.SetBool(false);
        }
    }
}

