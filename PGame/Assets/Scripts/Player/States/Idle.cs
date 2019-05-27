using UnityEngine;
using System.Collections;

namespace PlayerStates
{
    public class Idle : BaseState
    {
        private Rigidbody2D body;

        public Idle(Rigidbody2D body)
        {
            this.body = body;
            this.ControlFacingDirection = true;
        }

        public override void Start()
        {
            base.Start();
            body.velocity = new Vector2(0, body.velocity.y);
        }
    }
}

