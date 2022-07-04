using UnityEngine;
using System;

[Serializable]
public sealed class TextSample
{
	[SerializeField] [TextArea] private string _text;
	[SerializeField] private float _durationInSeconds;

	public string Text => _text;
	public float DurationInSeconds => _durationInSeconds;
}
