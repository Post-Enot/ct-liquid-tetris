using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LiquidTetris.UI
{
	[Serializable]
	public sealed class WeaponPanelCathegory
	{
		[SerializeField] private string _name;
		[SerializeField] private List<WeaponDataActionShell> _weapons;

		public string Name => _name;
		public List<WeaponDataActionShell> Weapons => _weapons;
	}
}
