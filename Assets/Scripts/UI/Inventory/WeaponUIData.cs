using System;
using UnityEngine;

namespace LiquidTetris.UI
{
	[CreateAssetMenu(fileName = "WeaponUIData", menuName = "UI/WeaponUIData")]
	public sealed class WeaponUIData : ScriptableObject
	{
		[Space]

		[SerializeField] private string _name;
		[SerializeField] private int _count;
		[SerializeField] private float _pauseDurationBetweenUsing;

		[Header("Price Data:")]
		[SerializeField] private int _cost;
		[SerializeField] private Currency _currency;

		[Header("Graphics:")]
		[SerializeField] private Sprite _inventoryIcon;
		[SerializeField] private Sprite _weaponPanelIcon;

		public string Name => _name;
		public float PauseDurationBetweenUsing => _pauseDurationBetweenUsing;
		public int Count
		{
			get => _count;
			set
			{
				_count = value;
				CountChanged?.Invoke(value);
			}
		}
		public int Cost => _cost;
		public Currency Currency => _currency;
		public Sprite InventoryIcon => _inventoryIcon;
		public Sprite WeaponPanelIcon => _weaponPanelIcon;

		public event Action<int> CountChanged;
	}
}
