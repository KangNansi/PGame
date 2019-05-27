using UnityEngine;
using System.Collections;

public class SpriteBlink : LonelyScript
{
    private SpriteRenderer target;
    private float blinkSpeed;

    private Timer timer;

    public SpriteBlink(SpriteRenderer renderer, float blinkSpeed)
    {
        this.target = renderer;
        this.blinkSpeed = blinkSpeed;
    }

    public override void Start()
    {
        base.Start();
        timer.Restart();
    }

    public override void Update()
    {
        base.Update();
        float elapsed = timer.Elapsed;
        float normalized = (elapsed * blinkSpeed) % 1f;
        
        float alpha = 1-(normalized < 0.75f ? 0 : 0.5f);
        target.color = new Color(target.color.r, target.color.g, target.color.b, alpha);
    }

    public override void End()
    {
        base.End();
        target.color = new Color(target.color.r, target.color.g, target.color.b, 1);
    }
}
