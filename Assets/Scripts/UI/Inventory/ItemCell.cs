using TMPro;
using UnityEngine;
using UnityEngine.UI;
using LiquidTetris.DataContainers;

namespace LiquidTetris.UI.Inventory
{
	public sealed class ItemCell : MonoBehaviour
	{
		[Space]

		[Header("Data Container References:")]
		[SerializeField] private PlayerProgress _playerProgress;
		[SerializeField] private WeaponUIData _itemData;

		[Space]

		[Header("Component references:")]
		[SerializeField] private TextMeshProUGUI _itemNameTextField;
		[SerializeField] private TextMeshProUGUI _itemCountTextField;
		[SerializeField] private TextMeshProUGUI _buyingItemCountTextField;
		[SerializeField] private TextMeshProUGUI _costTextFeild;
		[SerializeField] private Image _itemIcon;
		[SerializeField] private Image _currencyIcon;
		[SerializeField] private Image _buttonImage;

		[Space]

		[Header("Style:")]
		[SerializeField] private Sprite _realCurrencyButton;
		[SerializeField] private Sprite _gameCurrencyButton;
		[SerializeField] private Sprite _coinIcon;
		[SerializeField] private Sprite _rubleIcon;
		[SerializeField] private string _countPrefix;
		[SerializeField] private string _countSuffix;

		private int _buyingItemCount;

		public void Init(WeaponUIData itemData)
		{
			if (_itemData != null)
			{
				_itemData.CountChanged -= UpdateItemCount;
			}
			_itemData = itemData;
			_itemData.CountChanged += UpdateItemCount;
			UpdateItemCount(_itemData.Count);
			_itemNameTextField.text = itemData.Name;
			_itemIcon.sprite = _itemData.InventoryIcon;
			_currencyIcon.sprite = _coinIcon;
			_buttonImage.sprite = _gameCurrencyButton;
			UpdateBuyingItemCountTextField();
		}

		public void IncreaseBuyingItemCount()
		{
			_buyingItemCount += 1;
			UpdateBuyingItemCountTextField();
		}

		public void DecreaseBuyingItemCount()
		{
			if (_buyingItemCount > 0)
			{
				_buyingItemCount -= 1;
			}
			else if (_buyingItemCount < 0)
			{
				_buyingItemCount = 0;
			}
			UpdateBuyingItemCountTextField();
		}

		public void Buy()
		{
			if (_buyingItemCount > 0)
			{
				int totalCost = _itemData.Cost * _buyingItemCount;
				if ((int)_playerProgress.CoinsNumber.Value >= totalCost)
				{
					_itemData.Count += _buyingItemCount;
					_playerProgress.CoinsNumber.Value = (int)_playerProgress.CoinsNumber.Value - totalCost;
				}
			}
			_buyingItemCount = 0;
			UpdateBuyingItemCountTextField();
		}

		private void UpdateItemCount(int newValue)
		{
			_itemCountTextField.text = _countPrefix + newValue.ToString() + _countSuffix;
		}

		private void UpdateBuyingItemCountTextField()
		{
			_buyingItemCountTextField.text = _buyingItemCount.ToString();
			int totalCount = _buyingItemCount > 0 ? _buyingItemCount : 1;
			_costTextFeild.text = (totalCount  * _itemData.Cost).ToString();
		}
	}
}
