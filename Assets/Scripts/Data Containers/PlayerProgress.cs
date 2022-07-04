using FieldIndicators;
using LiquidTetris.DataContainers;
using UnityEngine;

namespace LiquidTetris
{
    [CreateAssetMenu(fileName = "PlayerProgress", menuName = "Player Progress")]
    public sealed class PlayerProgress : ScriptableObject
    {
        [Space]
        [SerializeField] private string _localFilePath;

        [Header("Indicatable fields:")]
        [SerializeField] private IntReferenceField _coinsNumber;
        [SerializeField] private IntReferenceField _winsNumber;
        [SerializeField] private IntReferenceField _defeatsNumber;
        [SerializeField] private IntReferenceField _bombsNumber;
        [SerializeField] private StringReferenceField _nickname;

        public IntReferenceField CoinsNumber => _coinsNumber;
        public IntReferenceField WinsNumber => _winsNumber;
        public IntReferenceField DefeatsNumber => _defeatsNumber;
        public IntReferenceField BombsNumber => _bombsNumber;
        public StringReferenceField Nickname => _nickname;

        public void Upload()
        {
            var saver = new XmlSaver<PlayerProgressDataForm>();
            PlayerProgressDataForm dataForm = saver.Upload($"{Application.persistentDataPath}/{_localFilePath}");
            SynchWithDataForm(dataForm);
        }

        public void Save()
        {
            var dataForm = new PlayerProgressDataForm(this);
            var saver = new XmlSaver<PlayerProgressDataForm>();
            saver.Save($"{Application.persistentDataPath}/{_localFilePath}", dataForm);
        }

        private void OnEnable()
        {
            Upload();
            CoinsNumber.ValueChanged += Save;
            WinsNumber.ValueChanged += Save;
            DefeatsNumber.ValueChanged += Save;
            BombsNumber.ValueChanged += Save;
            Nickname.ValueChanged += Save;
        }

        private void OnDisable()
        {
            CoinsNumber.ValueChanged -= Save;
            WinsNumber.ValueChanged -= Save;
            DefeatsNumber.ValueChanged -= Save;
            BombsNumber.ValueChanged -= Save;
            Nickname.ValueChanged -= Save;
        }

        private void Save<T>(T _)
        {
            Save();
        }

        private void SynchWithDataForm(PlayerProgressDataForm dataForm)
        {
            CoinsNumber.Value = dataForm.CoinsCount;
            WinsNumber.Value = dataForm.WinsCount;
            DefeatsNumber.Value = dataForm.DefeatsCount;
            BombsNumber.Value = dataForm.BombsCount;
            if (dataForm.Nickname != "" && dataForm.Nickname != null)
            {
                Nickname.Value = dataForm.Nickname;
            }
            else
            {
                Nickname.Value = GenerateRandomNickname();
            }
        }

        private string GenerateRandomNickname()
        {
            return $"Player{Random.Range(1, 1000)}";
        }
    }
}
