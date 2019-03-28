using System.Collections;
using System;
using UnityEngine;

public abstract class FadeAudioSource {

    public static IEnumerator FadeAudioSourceFromTo(AudioSource audioSource, float fadeFrom, float fadeTo, float fadeTime, Action afterAction) {
        float startTime = Time.time;

        while (startTime + fadeTime >= Time.time) {
            float fractionPassed = (Time.time - startTime) / fadeTime;
            audioSource.volume = Mathf.Lerp(fadeFrom, fadeTo, fractionPassed);

            yield return new WaitForEndOfFrame();
        }
        audioSource.volume = fadeTo;
        afterAction();
    }

}