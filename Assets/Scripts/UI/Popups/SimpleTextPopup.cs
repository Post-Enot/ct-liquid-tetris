using UnityEngine;
using TMPro;

namespace LiquidTetris.UI.Game
{
	[RequireComponent(typeof(TextMeshProUGUI))]
	public sealed class SimpleTextPopup : MonoBehaviour
	{
		public void Show(string text)
		{
			var textMesh = GetComponent<TextMeshProUGUI>();
			textMesh.text = text;
		}

		public void HandleEndOfDisappearingAnimation()
		{
			Destroy(gameObject);
		}
	}
}
