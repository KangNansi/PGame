using UnityEngine;
using System.Collections;

namespace PlayerStates
{
    public class Hit : BaseState
    {
        private Rigidbody2D body;
        private SpriteRenderer sprite;
        private Life life;
        private Vector2 force;
        private Timer timer;

        private SpriteBlink blink;

        public float Elapsed {
            get {
                return timer.Elapsed;
            }
        }

        public Hit(Rigidbody2D body, SpriteRenderer sprite, Life life, Vector2 force)
        {
            this.body = body;
            this.sprite = sprite;
            this.life = life;
            this.force = force;
            this.CanJump = false;
        }

        public override void Start()
        {
            base.Start();
            body.velocity = new Vector2(force.x * -Mathf.Sign(body.transform.localScale.x), force.y);
            life.isInvicible = true;
            timer.Restart();
            blink = sprite.DoBlink(4f);
        }

        public override void End()
        {
            base.End();
            TimeManager.Delay(1f, () =>
            {
                life.isInvicible = false;
                blink.Kill();
            });
        }
    }
}
