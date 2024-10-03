using System;
using TMPro;
using UnityEngine;

namespace Assets.Clicker.Scripts.GameScene.View
{
    public class FlyingNumbersUiView:MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textValue;
        public void SetValue(double value)
        {
            _textValue.text = $"{Math.Round(value, 2)}";
        }
    }
}
