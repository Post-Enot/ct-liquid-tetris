using System;
using System.Collections.Generic;
using UnityEngine;

namespace LiquidTetris.UI
{
	public sealed class Carousel : MonoBehaviour
	{
		[SerializeField] private GameObject[] _variants;

		public GameObject CurrentVariant => _variants[_currentVariantIndex];

		public event Action<string> ValueChanged;
		
		private int _currentVariantIndex = 0;
		private Dictionary<string, int> _variantIndexHash;

		public void SetVariant(string variantKey)
		{
			if (_variantIndexHash is null)
			{
				InitIndexHash();
			}
			if (_variantIndexHash.ContainsKey(variantKey))
			{
				CurrentVariant.SetActive(false);
				int variantIndex = _variantIndexHash[variantKey];
				SwitchVariant(variantIndex);
			}
			else
			{
				Debug.LogWarning($"Variant \"{variantKey}\" not found.");
			}
		}

		public void MoveLeft()
		{
			int newVariantIndex = _currentVariantIndex - 1;
			if (newVariantIndex < 0)
			{
				newVariantIndex = _variants.Length - 1;
			}
			SwitchVariant(newVariantIndex);
		}

		public void MoveRight()
		{
			int newVariantIndex = _currentVariantIndex + 1;
			if (newVariantIndex >= _variants.Length)
			{
				newVariantIndex = 0;
			}
			SwitchVariant(newVariantIndex);
		}

		public void DisableAllVariants()
		{
			foreach (GameObject variant in _variants)
			{
				variant.SetActive(false);
			}
		}

		private void SwitchVariant(int newVariantIndex)
		{
			CurrentVariant.SetActive(false);
			_variants[newVariantIndex].SetActive(true);
			_currentVariantIndex = newVariantIndex;
			ValueChanged?.Invoke(CurrentVariant.name);
		}

		private void InitIndexHash()
		{
			_variantIndexHash = new Dictionary<string, int>();
			for (int i = 0; i < _variants.Length; i += 1)
			{
				string variantName = _variants[i].name;
				if (!_variantIndexHash.ContainsKey(variantName))
				{
					_variantIndexHash.Add(variantName, i);
				}
				else
				{
					Debug.LogError("Name match found. Carousel option object names must be unique.");
				}
			}
		}
	}
}
