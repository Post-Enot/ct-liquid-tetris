using UnityEngine;

public sealed class WeaponList : MonoBehaviour
{
	[SerializeField] private string _openTrigger;
	[SerializeField] private string _closeTrigger;

	private Animator _animator;
	private bool _isOpen;

	public void Show()
	{
		if (!_isOpen)
		{
			_animator.SetTrigger(_openTrigger);
			_isOpen = true;
		}
	}

	public void Hide()
	{
		if (_isOpen)
		{
			_animator.SetTrigger(_closeTrigger);
			_isOpen = false;
		}
	}

	private void Awake()
	{
		_animator = GetComponent<Animator>();
	}
}
