using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace LiquidTetris.UI.Game
{
    [Serializable]
    public class GameFieldWindow
    {
        [SerializeField] private RawImage _gameFieldImage;
        [SerializeField] private TextMeshProUGUI _label;
        [SerializeField] private CanvasGroup _popupCanvasGroup;

        public RawImage Image => _gameFieldImage;
        public TextMeshProUGUI Label => _label;
        public CanvasGroup PopupCanvasGroup => _popupCanvasGroup;
    }
}
