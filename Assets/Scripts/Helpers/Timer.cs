using System;
using System.Collections;
using UnityEngine;

public sealed class Timer
{
	public Timer(MonoBehaviour coroutinePerformer)
	{
		CoroutinePerformer = coroutinePerformer;
	}

	public readonly MonoBehaviour CoroutinePerformer;

	public bool IsActive { get; private set; }
	public float SecondsLeft { get; private set; }

	private Coroutine _timerCoroutine;
	private Action _onTimeUp;
	private Action<float> _onUpdated;
	private Action _onStopped;

	public void Start(
		float seconds,
		Action onTimeUp = null,
		Action<float> onUpdated = null,
		Action onStopped = null)
	{
		if (!IsActive)
		{
			Activate(seconds, onTimeUp, onUpdated, onStopped);
		}
	}

	public void StartAnyway(
		float seconds,
		Action onTimeUp = null,
		Action<float> onUpdated = null,
		Action onStopped = null)
	{
		if (IsActive)
		{
			Stop();
		}
		Activate(seconds, onTimeUp, onUpdated, onStopped);
	}

	public void ChangeTime(float delta)
	{
		SecondsLeft += delta;
	}

	public void Stop()
	{
		if (IsActive)
		{
			CoroutinePerformer.StopCoroutine(_timerCoroutine);
			_onStopped?.Invoke();
			IsActive = false;
		}
	}

	private void Activate(float seconds, Action onTimeUp, Action<float> onUpdated, Action onStopped)
	{
		_onTimeUp = onTimeUp;
		_onUpdated = onUpdated;
		_onStopped = onStopped;
		SecondsLeft = seconds;
		_timerCoroutine = CoroutinePerformer.StartCoroutine(Countdown());
		IsActive = true;
	}

	private IEnumerator Countdown()
	{
		while (SecondsLeft > 0)
		{
			yield return null;
			SecondsLeft -= Time.deltaTime;
			_onUpdated?.Invoke(SecondsLeft);
		}
		_onTimeUp?.Invoke();
		IsActive = false;
	}
}
