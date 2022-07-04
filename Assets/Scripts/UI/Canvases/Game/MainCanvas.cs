using UnityEngine;

namespace LiquidTetris.UI.Game
{
    public sealed class MainCanvas : MonoBehaviour
    {
        [SerializeField] private GameFieldWindow _userGameFieldWindow;
        [SerializeField] private GameFieldWindow _opponentGameFieldWindow;

        public void SwapOpponentAndOperatorFields()
        {
            (_opponentGameFieldWindow.Label.text, _userGameFieldWindow.Label.text) =
                (_userGameFieldWindow.Label.text, _opponentGameFieldWindow.Label.text);
            (_opponentGameFieldWindow.Image.material, _userGameFieldWindow.Image.material) =
                (_userGameFieldWindow.Image.material, _opponentGameFieldWindow.Image.material);
            (_opponentGameFieldWindow.Image.texture, _userGameFieldWindow.Image.texture) =
                (_userGameFieldWindow.Image.texture, _opponentGameFieldWindow.Image.texture);
            (_opponentGameFieldWindow.PopupCanvasGroup.alpha, _userGameFieldWindow.PopupCanvasGroup.alpha) =
                (_userGameFieldWindow.PopupCanvasGroup.alpha, _opponentGameFieldWindow.PopupCanvasGroup.alpha);
        }
    }
}
