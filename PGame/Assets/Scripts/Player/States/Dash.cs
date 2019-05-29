using UnityEngine;
using System.Collections;

namespace PlayerStates
{
    public class Dash : BaseState
    {

        public Vector2 direction;
        private Timer timer;

        public Dash(BasicController controller)
        {
            this.controller = controller;
            this.ControlFacingDirection = true;
            this.CanJump = true;
            this.CanDash = true;
        }

        public override void Start()
        {
            base.Start();

            controller.body.velocity = Vector2.zero;
            controller.body.AddForce(controller.dashForce * direction, ForceMode2D.Impulse);
            controller.rollParam.SetBool(true);
            timer.Restart();
        }

        public override void Update()
        {
            base.Update();
            controller.sprite.transform.localRotation *= Quaternion.Euler(0, 0, 90f * -25f * Time.deltaTime);
            if(timer.Elapsed > controller.dashDuration)
            {
                Stop();
            }
        }

        public override void End()
        {
            base.End();
            controller.rollParam.SetBool(false);
            controller.sprite.transform.localRotation = Quaternion.identity;
        }
    }
}

