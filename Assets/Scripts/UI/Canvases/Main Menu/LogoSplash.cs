using UnityEngine;

namespace LiquidTetris.UI.MainMenu
{
	public sealed class LogoSplash : MonoBehaviour
	{
		[Header("Logo:")]
		[SerializeField] private Animator _logoAnimator;
		[SerializeField] private AnimationEvents _logoAnimationEvents;
		[SerializeField] private string _logoSplashTriggerKey;
		[SerializeField] private string _logoSplashEndKey;

		[Header("Logo Canvas:")]
		[SerializeField] private Animator _logoCanvasAnimator;
		[SerializeField] private AnimationEvents _logoCanvasAnimationEvents;
		[SerializeField] private string _logoCanvasHideTriggerKey;
		[SerializeField] private string _logoCanvasHideEndKey;

		[Header("Main Canvas:")]
		[SerializeField] private Animator _mainCanvasAnimator;
		[SerializeField] private string _mainCanvasShowWithFadeInKey;

		[Header("GameObject references:")]
		[SerializeField] private GameObject _logoCanvas;

		[Header("Params:")]
		[SerializeField] private bool _showLogo;

		private void Awake()
		{
			DontDestroyOnLoad(this);
			if (_showLogo)
			{
				_logoCanvas.SetActive(true);
				_logoAnimator.SetTrigger(_logoSplashTriggerKey);
				_showLogo = false;
				_logoAnimationEvents.SubscribeOn(_logoSplashEndKey, HideLogoCanvas);
			}
			else
			{
				ShowMainCanvas();
			}
		}

		private void HideLogoCanvas()
		{
			_logoAnimationEvents.UnsubscribeFrom(_logoSplashEndKey, HideLogoCanvas);
			_logoCanvasAnimator.SetTrigger(_logoCanvasHideTriggerKey);
			_logoCanvasAnimationEvents.SubscribeOn(_logoCanvasHideEndKey, PrepareToShowMainCanvas);
		}

		private void PrepareToShowMainCanvas()
		{
			_logoCanvas.SetActive(false);
			_logoCanvasAnimationEvents.UnsubscribeFrom(_logoCanvasHideEndKey, PrepareToShowMainCanvas);
			ShowMainCanvas();
		}

		private void ShowMainCanvas()
		{
			_mainCanvasAnimator.SetTrigger(_mainCanvasShowWithFadeInKey);
		}
	}
}
