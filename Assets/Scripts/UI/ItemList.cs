using System.Collections.Generic;
using UnityEngine;

namespace LiquidTetris.UI.Inventory
{
	public sealed class ItemList : MonoBehaviour
	{
		[SerializeField] private GameObject _cellPrefab;
		[SerializeField] private List<WeaponUIData> _items;

		private void Awake()
		{
			for (int i = 0; i < _items.Count; i += 1)
			{
				GameObject cellObject = Instantiate(_cellPrefab, transform);
				ItemCell cell = cellObject.GetComponent<ItemCell>();
				cell.Init(_items[i]);
			}
		}
	}
}
