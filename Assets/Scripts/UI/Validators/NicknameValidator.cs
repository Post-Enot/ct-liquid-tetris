using UnityEngine;
using System.Text.RegularExpressions;
using TMPro;

namespace LiquidTetris.UI
{
    [CreateAssetMenu(fileName = "Nickname Validator", menuName = "Input Validators/Nickname Validator")]
    public class NicknameValidator : TMP_InputValidator
    {
        [SerializeField] private int _characterLimit;

        public override char Validate(ref string text, ref int cursorPosition, char enteredChar)
        {
            if (text.Length < _characterLimit && Regex.IsMatch($"{enteredChar}", "[a-zA-Z0-9\\-_]"))
            {
                text = text.Insert(cursorPosition, enteredChar.ToString());
                cursorPosition += 1;
            }
            return enteredChar;
        }
    }
}
