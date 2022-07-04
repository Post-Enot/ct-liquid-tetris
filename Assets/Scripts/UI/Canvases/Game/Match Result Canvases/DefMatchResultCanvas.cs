using FieldIndicators;
using UnityEngine;

namespace LiquidTetris.NetworkCode
{
    public class DefMatchResultCanvas : MonoBehaviour
    {
        [Header("Component References:")]
        [SerializeField] private IntFieldIndicator _coinsDifferenceIndicator;
        [SerializeField] private IntFieldIndicator _userScoreIndicator;
        [SerializeField] private IntFieldIndicator _opponentScoreIndicator;
        [SerializeField] private StringFieldIndicator _userNicknameIndicator;
        [SerializeField] private StringFieldIndicator _opponentNicknameIndicator;

        public void ShowMatchResult(MatchResultForm matchResultForm)
        {
            if (matchResultForm.MatchResult == MatchResult.Def)
            {
                gameObject.SetActive(true);
                _coinsDifferenceIndicator.UpdateLabel(matchResultForm.CoinsDifference);
                _userScoreIndicator.UpdateLabel(matchResultForm.UserScore);
                _opponentScoreIndicator.UpdateLabel(matchResultForm.OpponentScore);
                _userNicknameIndicator.UpdateLabel(matchResultForm.UserNickname);
                _opponentNicknameIndicator.UpdateLabel(matchResultForm.OpponentNickname);
            }
        }
    }
}
