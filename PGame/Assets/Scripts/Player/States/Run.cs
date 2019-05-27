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
            
        }

        public override void Update()
        {
            base.Update();
            isWalkingParam.SetBool(true);
            float move = (h < -0.2 || h > 0.2) ? h : 0;
            body.velocity = new Vector2(move * speed, body.velocity.y);
        }

        public override void End()
        {
            base.End();
            isWalkingParam.SetBool(false);
        }
    }
}

