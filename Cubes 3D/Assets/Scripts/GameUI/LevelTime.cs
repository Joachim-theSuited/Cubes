using UnityEngine;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Text))]
public class LevelTime : MonoBehaviour {

	public static LevelTime INSTANCE;

	private Text text;

	private float startTime = -1;
	private TimeSpan lastTime = new TimeSpan();
	private bool running = false;

	// Use this for initialization
	void Start () {
		INSTANCE = this;
		text = GetComponent<Text>();

		startTimer();
	}
	
	// Update is called once per frame
	void Update () {
		if(running) {
			lastTime = TimeSpan.FromSeconds(Time.time - startTime);
			text.text = String.Format("{0:00}:{1:00}.{2:000}", lastTime.Minutes, lastTime.Seconds, lastTime.Milliseconds);
		}
	}

	/// <summary>
	/// Calling this method will start updating the displayed clock as well as resetting the elapsed time to 0.
	/// </summary>
	public void startTimer() {
		running = true;
		startTime = Time.time;
	}

	public TimeSpan elapsedTime() {
		return lastTime;
	}

	/// <summary>
	/// Calling this method will stop updating the time display. Elapsed Time is saved and elapsedTime() may be called after this to retrieve the last updated time.
	/// </summary>
	public void stopTimer() {
		running = false;
	}

}
