using UnityEngine;
using System.Collections;

namespace PlayerStates
{
    public class Hit : BaseState
    {
        private Rigidbody2D body;
        private Life life;
        private Vector2 force;

        public Hit(Rigidbody2D body, Life life, Vector2 force)
        {
            this.body = body;
            this.life = life;
            this.force = force;
        }

        public override void Start()
        {
            base.Start();
            body.velocity = new Vector2(force.x * -Mathf.Sign(body.transform.localScale.x), force.y);
            life.isInvicible = true;
        }

        public override void End()
        {
            base.End();
            TimeManager.Delay(1f, () =>
            {
                life.isInvicible = false;
            });
            
        }
    }
}
