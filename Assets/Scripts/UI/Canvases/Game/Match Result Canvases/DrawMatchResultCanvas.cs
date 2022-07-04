using FieldIndicators;
using UnityEngine;

namespace LiquidTetris.NetworkCode
{
    public class DrawMatchResultCanvas : MonoBehaviour
    {
        [Header("Component References:")]
        [SerializeField] private IntFieldIndicator _userScoreIndicator;
        [SerializeField] private IntFieldIndicator _opponentScoreIndicator;
        [SerializeField] private StringFieldIndicator _userNicknameIndicator;
        [SerializeField] private StringFieldIndicator _opponentNicknameIndicator;

        public void ShowMatchResult(MatchResultForm matchResultForm)
        {
            if (matchResultForm.MatchResult == MatchResult.Draw)
            {
                gameObject.SetActive(true);
                _userScoreIndicator.UpdateLabel(matchResultForm.UserScore);
                _opponentScoreIndicator.UpdateLabel(matchResultForm.OpponentScore);
                _userNicknameIndicator.UpdateLabel(matchResultForm.UserNickname);
                _opponentNicknameIndicator.UpdateLabel(matchResultForm.OpponentNickname);
            }
        }

    }
}
