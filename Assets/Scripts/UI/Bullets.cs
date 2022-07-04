using UnityEngine;
using UnityEngine.UI;

public sealed class Bullets : MonoBehaviour
{
	[Header("Sprites:")]
	[SerializeField] private Sprite _disabledBullet;
	[SerializeField] private Sprite _enabledBullet;

	[Header("References:")]
	[SerializeField] private Image[] _bulletImages;

	public void UpdateHorizontalBullets(Vector2 scroll)
	{
		float part = 1f / (_bulletImages.Length * 2);
		int count = (int)(scroll.x / part);
		count += 1;
		count /= 2;
		foreach (var image in _bulletImages)
		{
			image.sprite = _disabledBullet;
		}
		if (count < 1)
		{
			count = 1;
		}
		if (count > _bulletImages.Length)
		{
			count = _bulletImages.Length;
		}
		_bulletImages[count - 1].sprite = _enabledBullet;
	}
}
