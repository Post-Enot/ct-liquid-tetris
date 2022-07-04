using TMPro;
using UnityEngine;

namespace LiquidTetris.UI
{
    public class InventoryIcon : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _countLabel;
        [SerializeField] private PlayerProgress _playerProgress;

        private int _count;

        public void Plus()
        {
            _count += 1;
            UpdateLabel();
        }

        public void Minus()
        {
            if (_count > 0)
            {
                _count -= 1;
            }
            UpdateLabel();
        }

        public void Clear()
        {
            if ((_playerProgress.CoinsNumber.Value - (50 * _count)) >= 0)
            {
                _playerProgress.CoinsNumber.Value = _playerProgress.CoinsNumber.Value - (50 * _count);
                _playerProgress.BombsNumber.Value = _playerProgress.BombsNumber.Value + _count;
                _count = 0;
                UpdateLabel();
            }
        }

        private void UpdateLabel()
        {
            _countLabel.text = _count.ToString();
        }
    }
}
