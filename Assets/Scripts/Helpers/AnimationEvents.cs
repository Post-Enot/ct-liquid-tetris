using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public sealed class AnimationEvents : MonoBehaviour
{
	private readonly Dictionary<string, HashSet<Action>> _eventSubscribers = new Dictionary<string, HashSet<Action>>();

	public void UnsubscribeFrom(string eventKey, Action action)
	{
		if (_eventSubscribers.ContainsKey(eventKey))
		{
			_ = _eventSubscribers[eventKey].Remove(action);
		}
	}

	public void SubscribeOn(string eventKey, Action action)
	{
		if (!_eventSubscribers.ContainsKey(eventKey))
		{
			var actionSet = new HashSet<Action>();
			_eventSubscribers.Add(eventKey, actionSet);
		}
		_eventSubscribers[eventKey].Add(action);
	}

	private void InvokeAnimation(string eventKey)
	{
		if (_eventSubscribers.ContainsKey(eventKey))
		{
			List<Action> actions = _eventSubscribers[eventKey].ToList();
			foreach (Action action in actions)
			{
				action?.Invoke();
			}
		}
	}
}
