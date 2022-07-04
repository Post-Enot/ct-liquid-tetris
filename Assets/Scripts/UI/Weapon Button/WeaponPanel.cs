using UnityEngine;
using System.Collections.Generic;
using TMPro;

namespace LiquidTetris.UI.Game
{
    public sealed class WeaponPanel : MonoBehaviour
    {
		[SerializeField] private WeaponSlot[] _weaponSlots;
		[SerializeField] private List<WeaponPanelCathegory> _weaponCathegories;
		[SerializeField] private Animator _panelAnimator;
		[SerializeField] private Animator _arrowsAnimator;
		[SerializeField] private TextMeshProUGUI _label;
		[SerializeField] private string _defaultLabelValue;

		[Header("Animation key")]
		[SerializeField] private string _openAnimationKey;
		[SerializeField] private string _closeAnimationKey;
		[SerializeField] private string _arrowRotationAnimationKey;

		public bool IsOpened { get; private set; }

		private WeaponPanelCathegory CurrentCathegory => _weaponCathegories[_currentCathegoryIndex];
		private int _currentCathegoryIndex;

		public void HundleTouchOnButton()
		{
			if (IsOpened)
			{
				SwitchOnNextCathegory();
			}
			else
			{
				Open();
			}
		}

		public void Close()
		{
			IsOpened = false;
			_label.text = _defaultLabelValue;
			_panelAnimator.SetTrigger(_closeAnimationKey);
		}

		private void Open()
		{
			IsOpened = true;
			_label.text = CurrentCathegory.Name;
			_panelAnimator.SetTrigger(_openAnimationKey);
		}

		private void SwitchOnNextCathegory()
		{
			if (_weaponCathegories.Count >= 2)
			{
				_arrowsAnimator.SetTrigger(_arrowRotationAnimationKey);
				_currentCathegoryIndex += 1;
				if (_currentCathegoryIndex >= _weaponCathegories.Count)
				{
					_currentCathegoryIndex = 0;
				}
				_label.text = CurrentCathegory.Name; 
				int border = _weaponSlots.Length;
				if (border > CurrentCathegory.Weapons.Count)
				{
					border = CurrentCathegory.Weapons.Count;
				}
				for (int i = 0; i < border; i += 1)
				{
					_weaponSlots[i].Put(CurrentCathegory.Weapons[i]);
				}
				for (int i = border; i < _weaponSlots.Length; i += 1)
				{
					_weaponSlots[i].RemoveContent();
				}
			}
		}
	}
}
