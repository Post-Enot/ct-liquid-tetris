using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace LiquidTetris.UI.Game
{
	public sealed class WeaponSlot : MonoBehaviour
	{
		[SerializeField] private Image _icon;
		[SerializeField] private TextMeshProUGUI _countIndicator;

		public WeaponDataActionShell Content { get; private set; }

		public void ActivateContent()
		{
			Content?.InvokeAction();
		}

		public void Put(WeaponDataActionShell weaponDataActionShell)
		{
			if (Content != null)
			{
				Content.WeaponUIData.CountChanged -= UpdateCountIndicator;
			}
			Content = weaponDataActionShell;
			Content.WeaponUIData.CountChanged += UpdateCountIndicator;
			UpdateCountIndicator(weaponDataActionShell.WeaponUIData.Count);
			_icon.sprite = weaponDataActionShell.WeaponUIData.WeaponPanelIcon;
			_icon.enabled = true;
			_countIndicator.enabled = true;
		}

		public void RemoveContent()
		{
			_icon.sprite = null;
			_icon.enabled = false;
			_countIndicator.enabled = false;
		}

		private void UpdateCountIndicator(int count)
		{
			_countIndicator.text = count.ToString();
		}
	}
}
