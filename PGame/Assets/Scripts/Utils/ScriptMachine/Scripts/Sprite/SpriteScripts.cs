using UnityEngine;
using System.Collections;

public static class SpriteScripts
{
    public static void DoColor(this SpriteRenderer target, Color targetColor, float duration)
    {
        ScriptMachine.Launch(new SpriteColorModifier(target, targetColor, duration));
    }

    public static SpriteBlink DoBlink(this SpriteRenderer target, float speed)
    {
        SpriteBlink blink = new SpriteBlink(target, speed);
        ScriptMachine.Launch(blink);
        return blink;
    }
}
