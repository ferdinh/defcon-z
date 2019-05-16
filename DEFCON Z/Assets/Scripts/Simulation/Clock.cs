using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// This class keep tracks in-game time (Game Cycle).
///
/// Game time will notify subscribers when a cycle has passes.
/// </summary>
public class Clock : MonoBehaviour
{
    /// <summary>
    /// Occurs when [a game cycle passes].
    /// </summary>
    public event EventHandler GameCycleElapsed;

    /// <summary>
    /// No. of days that has elapsed in the game.
    /// </summary>
    /// <value>
    /// The game's current day.
    /// </value>
    public int GameDay { get; private set; }

    private float _timeScale;

    /// <summary>
    /// Gets or sets the time scale.
    /// Time scale is in real time seconds.
    /// Defaults to every two seconds.
    /// Value should be more than 0.
    /// </summary>
    public float TimeScale
    {
        get
        {
            return _timeScale;
        }
        set
        {
            if (value < 0)
            {
                value = 0;
            }

            _timeScale = value;
        }
    }

    /// <summary>
    /// Awakes this instance. There can only be one of Clock Instance.
    /// </summary>
    private void Awake()
    {
    }

    /// <summary>
    /// Starts this instance.
    /// </summary>
    private void Start()
    {
        TimeScale = 2.0f;
        StartCoroutine(UpdateTime());
    }

    /// <summary>
    /// Update the game cycle based on specified real time interval (Time scale
    /// in seconds).
    /// </summary>
    private IEnumerator UpdateTime()
    {
        yield return new WaitForSeconds(TimeScale);
        while (true)
        {
            increaseTime();

            // Raise the event.
            OnGameCycleElapsed(new EventArgs());

            yield return new WaitForSeconds(TimeScale);
        }

        void increaseTime()
        {
            GameDay++;
        }
    }

    /// <summary>
    /// Raises the <see cref="E:GameCycleElapsed" /> event.
    /// </summary>
    /// <param name="eventArgs">The <see cref="EventArgs"/> instance containing the event data.</param>
    protected virtual void OnGameCycleElapsed(EventArgs eventArgs)
    {
        var handler = GameCycleElapsed;
        GameCycleElapsed?.Invoke(this, eventArgs);
    }
}