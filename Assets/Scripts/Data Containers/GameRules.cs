using UnityEngine;

namespace LiquidTetris
{
    [CreateAssetMenu(fileName = "GameRules", menuName = "Data Containers/Game Rules")]
    public class GameRules : ScriptableObject
    {
        [SerializeField] private float _pointRadiusInDrawingField;
        [SerializeField] private float _startMaxShapeFallSpeed;
        [SerializeField] private float _shapeMaxSpeedIncreaseFactor;
        [SerializeField] private float _maxShapeAreaInUnit;
        [SerializeField] private float _scoreForOneShapeAreaUnit;
        [SerializeField] private float _controlDurationAfterSurfaceTouchInSeconds;
        [SerializeField][Range(0, 1)] private float _colliderOverlapFactor;
        [SerializeField] private float _matchDurationInSeconds;
        [SerializeField] private float _overflowDurationInSeconds;
        [SerializeField] private int _bankContributionInPercentage;
        [SerializeField] private int _prizeInCoinsForWin;

        [Header("Shape sending:")]
        [SerializeField] private int _shapeQueueMaxElemenetCount;
        [SerializeField] private float _pauseBetweenSendingInSeconds;

        public float PointRadiusInDrawingField => _pointRadiusInDrawingField;
        public float ColliderOverlapFactor => _colliderOverlapFactor;
        public float StartMaxShapeFallSpeed => _startMaxShapeFallSpeed;
        public float ShapeMaxSpeedIncreaseFactor => _shapeMaxSpeedIncreaseFactor;
        public float MaxShapeAreaInUnit => _maxShapeAreaInUnit;
        public float ScoreForOneShapeAreaUnit => _scoreForOneShapeAreaUnit;
        public float ControlDurationAfterSurfaceTouchInSeconds => _controlDurationAfterSurfaceTouchInSeconds;
        public float MatchDurationInSeconds => _matchDurationInSeconds;
        public float OverflowDurationInSeconds => _overflowDurationInSeconds;
        public int BankContributionInPercentage => _bankContributionInPercentage;
        public int PrizeInCoinsForWin => _prizeInCoinsForWin;
        public int ShapeQueueMaxElemenetCount => _shapeQueueMaxElemenetCount;
        public float PauseBetweenSendingInSeconds => _pauseBetweenSendingInSeconds;

        public int CalculateContribution(int coinsCount)
        {
            return Mathf.CeilToInt(coinsCount / 100f * BankContributionInPercentage);
        }
    }
}
