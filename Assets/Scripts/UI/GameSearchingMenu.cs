using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LiquidTetris.UI
{
    public class GameSearchingMenu : MonoBehaviour
    {
        public const int NicknameMinLength = 3;

        [SerializeField] private PlayerProgress _playerProgress;
        [SerializeField] private string _invalidMinLengthResponce;
        [SerializeField] private string _successfulValidationResponce;

        [Header("Component references:")]
        [SerializeField] private TextMeshProUGUI _validationResponceLabel;
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private Button _startMatchmakingButton;

        private void Start()
        {
            if (_playerProgress.Nickname.Value != null)
            {
                _inputField.text = _playerProgress.Nickname.Value;
                ValidateNickname(_playerProgress.Nickname.Value);
            }
            else
            {
                ValidateNickname("");
            }
        }

        public void ValidateNickname(string nickname)
        {
            if (nickname.Length < NicknameMinLength)
            {
                _validationResponceLabel.text = _invalidMinLengthResponce;
                _startMatchmakingButton.interactable = false;
            }
            else
            {
                _validationResponceLabel.text = _successfulValidationResponce;
                _playerProgress.Nickname.Value = nickname;
                _startMatchmakingButton.interactable = true;
                _playerProgress.Save();
            }
        }
    }
}
