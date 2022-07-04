using UnityEngine;
using System.Collections;

public sealed class Pause
{
    public Pause(MonoBehaviour performer)
    {
        Performer = performer;
    }

    public readonly MonoBehaviour Performer;

    public bool IsPaused { get; private set; }

    private Coroutine _routine;

    public void Start(float durationInSeconds)
    {
        if (!IsPaused)
        {
            _routine = Performer.StartCoroutine(Countdown(durationInSeconds));
        }
    }

    public void StartAnyway(float durationInSeconds)
    {
        Stop();
        _routine = Performer.StartCoroutine(Countdown(durationInSeconds));
    }

    public void Stop()
    {
        if (IsPaused)
        {
            Performer.StopCoroutine(_routine);
        }
    }

    private IEnumerator Countdown(float durationInSeconds)
    {
        IsPaused = true;
        yield return new WaitForSeconds(durationInSeconds);
        IsPaused = false;
    }
}
