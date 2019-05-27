using UnityEngine;
using System.Collections;

public class SpriteColorModifier : LonelyScript
{
    private SpriteRenderer target;
    private Color targetColor;
    private float duration;

    private Color lastColor;
    private Timer timer;

    public SpriteColorModifier(SpriteRenderer renderer, Color targetColor, float duration)
    {
        this.target = renderer;
        this.targetColor = targetColor;
        this.duration = duration;
    }

    public override void Start()
    {
        base.Start();
        lastColor = target.color;
        timer.Restart();
    }

    public override void Update()
    {
        base.Update();
        Color current = Color.Lerp(lastColor, targetColor, NormalizedFunctions.Get(NormalizedFunctions.Function.Cubic, timer.Elapsed / duration));
        target.color = new Color(current.r, current.g, current.b, target.color.a);
        if(timer.Elapsed > duration)
        {
            running = false;
        }
    }

    public override void End()
    {
        base.End();
        target.color = lastColor;
    }
}
