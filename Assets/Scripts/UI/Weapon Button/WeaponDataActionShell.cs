using System;
using UnityEngine;
using UnityEngine.Events;

namespace LiquidTetris.UI
{
	[Serializable]
	public sealed class WeaponDataActionShell
	{
		[SerializeField] private UnityEvent _action;
		[SerializeField] private WeaponUIData _weaponUIData;

		public WeaponUIData WeaponUIData => _weaponUIData;

		public void InvokeAction()
		{
			_action?.Invoke();
		}
	}
}
