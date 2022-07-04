using UnityEngine;

namespace LiquidTetris.UI.MainMenu
{
	[RequireComponent(typeof(Animator))]
	public sealed class MainCanvas : MonoBehaviour
	{
		[Header("Animations keys:")]
		[SerializeField] private string _show;
		[SerializeField] private string _showWithFadeIn;

		private Animator _animator;

		public void Show()
		{
			gameObject.SetActive(true);
			_animator.SetTrigger(_show);
		}

		public void ShowWithFadeIn()
		{
			gameObject.SetActive(true);
			_animator.SetTrigger(_showWithFadeIn);
		}

		public void Hide()
		{
			gameObject.SetActive(false);
		}

		private void Awake()
		{
			_animator = GetComponent<Animator>();
		}
	}
}
