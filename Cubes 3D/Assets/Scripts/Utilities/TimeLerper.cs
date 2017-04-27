using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that capsulates lerping of values over a period of time.
/// </summary>
public class TimeLerper<T> {

    public delegate T LerpF(T a, T b, float t);

    readonly T low;
    readonly T high;
    readonly float duration;
    float timeStart;

    readonly LerpF lerpF;

    /// <summary>
    /// has the duration elapsed since the start time?
    /// </summary>
    public bool isNotDone {
        get { return timeStart + duration > Time.time; }
    }

    /// <summary>
    /// Constructor. The current Time.time is used as starting time.
    /// </summary>
    /// <param name="lerpF">the function to use for lerping</param>
    /// <param name="low">start value</param>
    /// <param name="high">target value</param>
    /// <param name="duration">duration in seconds</param>
    public TimeLerper(LerpF lerpF, T low, T high, float duration) {
        this.lerpF = lerpF;
        this.low = low;
        this.high = high;
        this.duration = duration;
        timeStart = Time.time;
    }

    /// <summary>
    /// the current (time dependent) value
    /// </summary>
    public T Current() {
        return lerpF(low, high, (Time.time - timeStart) / duration);
    }

    /// <summary>
    /// Resets the start time to the current Time.time
    /// </summary>
    public void ResetTimer() {
        timeStart = Time.time;
    }

    /// <summary>
    /// Resets the start time to the given value.
    /// </summary>
    public void ResetTimer(float t) {
        timeStart = t;
    }

    /// <summary>
    /// A new TimeLerper instance with start and target value exanged.
    /// Its start time is the current time.
    /// </summary>
    public TimeLerper<T> GetInverse() {
        return new TimeLerper<T>(lerpF, high, low, duration);
    }
}
