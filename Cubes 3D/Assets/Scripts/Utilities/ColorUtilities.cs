using System.Collections;
using UnityEngine;

public abstract class ColorUtilities {

    /// <summary>
    /// Use this IEnumerator to let the color of a sprite fade from one value to another over a given amount of time.
    /// During fading the color of the sprite will be determined by Color.Lerp
    /// </summary>
    /// <param name="sprite">the SpriteRenderer to change color</param>
    /// <param name="fadeFrom">the initial color the sprite will take during fading</param>
    /// <param name="fadeTo">the final color the sprite will have</param>
    /// <param name="fadeTime">the amount of time fading will take</param>
    public static IEnumerator FadeColorOfSprite(SpriteRenderer sprite, Color fadeFrom, Color fadeTo, float fadeTime)
    {
        float startTime = Time.time;

        while (startTime + fadeTime >= Time.time)
        {
            float fractionPassed = (Time.time - startTime) / fadeTime;
            sprite.color = Color.Lerp(fadeFrom, fadeTo, fractionPassed);

            yield return new WaitForEndOfFrame();
        }
        sprite.color = fadeTo;
    }

    public static IEnumerator FadeColorOfMaterial(Material material, Color fadeFrom, Color fadeTo, float fadeTime)
    {
        float startTime = Time.time;

        while(startTime + fadeTime >= Time.time)
        {
            float fractionPassed = (Time.time - startTime) / fadeTime;
            material.color = Color.Lerp(fadeFrom, fadeTo, fractionPassed);

            yield return new WaitForEndOfFrame();
        }
        material.color = fadeTo;
    }

}
