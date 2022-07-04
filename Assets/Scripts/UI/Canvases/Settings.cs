using UnityEngine;

namespace LiquidTetris.UI
{
	public sealed class Settings : MonoBehaviour
	{
		[SerializeField] private DataContainers.Settings _settings;

		[SerializeField] private Carousel _soundStatusCarousel;
		[SerializeField] private Carousel _controlStatusCarousel;

		private void OnEnable()
		{
			_soundStatusCarousel.DisableAllVariants();
			UpdateCarousel(_settings.IsSoundEnabled, _soundStatusCarousel);
			_soundStatusCarousel.ValueChanged += ChangeSoundStatus;
			_controlStatusCarousel.DisableAllVariants();
			UpdateCarousel(_settings.ControlType, _controlStatusCarousel);
			_controlStatusCarousel.ValueChanged += ChangeControlStatus;
		}

		private void UpdateCarousel<T>(T fieldValue, Carousel carousel)
		{
			string variantID = fieldValue.ToString();
			carousel.SetVariant(variantID);
		}

		private void ChangeSoundStatus(string value)
		{
			_settings.IsSoundEnabled = value == true.ToString();
		}

		private void ChangeControlStatus(string value)
		{
			if (ControlType.Finger.ToString() == value)
			{
				_settings.ControlType = ControlType.Finger;
			}
			else if (ControlType.Joystick.ToString() == value)
			{
				_settings.ControlType = ControlType.Joystick;
			}
		}
	}
}
