using UnityEngine;

namespace LiquidTetris.DataContainers
{
	[CreateAssetMenu(fileName = "Settings", menuName = "Data Containers/Settings")]
	public sealed class Settings : ScriptableObject
	{
		[SerializeField] private bool _isSoundEnabled;
		[SerializeField] private ControlType _controlType;

		public bool IsSoundEnabled
		{
			get => _isSoundEnabled;
			set => _isSoundEnabled = value;
		}
		public ControlType ControlType
		{
			get => _controlType;
			set => _controlType = value;
		}
	}
}
