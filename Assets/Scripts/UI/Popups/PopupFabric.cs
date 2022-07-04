using UnityEngine;

namespace LiquidTetris.UI.Game
{
	public sealed class PopupFabric : MonoBehaviour
	{
		[SerializeField] private GameObject _simpleTextPopupPrefab;

		[Header("References:")]
		[SerializeField] private Transform _parentTransformForPrefabs;

		public void ShowSimpleTextPopup(string text, Vector2 worldPosition)
		{
			GameObject popupObject = Instantiate(_simpleTextPopupPrefab, worldPosition, Quaternion.identity, _parentTransformForPrefabs);
			var simpleTextPopup = popupObject.GetComponent<SimpleTextPopup>();
			simpleTextPopup.Show(text);
		}
	}
}
