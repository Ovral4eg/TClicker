using Assets.Clicker.Scripts.State;
using DG.Tweening;
using R3;
using System;
using TMPro;
using UnityEngine;

namespace Assets.Clicker.Scripts.GameScene.View
{
    public class SoftCoinValueBinder:MonoBehaviour
    {
        IDisposable _subscribe;
        [SerializeField] private TextMeshProUGUI _textValue;

        public void Bind(GameStateProxy gamestate)
        {
            UpdateValue(gamestate.SoftCoins.CurrentValue, false);

            _subscribe = gamestate.SoftCoins.Skip(1).Subscribe(e => UpdateValue(e,true));
        }

        public void UpdateValue(double value, bool doAnimation)
        {
            _textValue.text = $" {Math.Round(value,2)}";

            if (doAnimation) transform.DOShakePosition(2, 5, 5);
        }

        private void OnDestroy()
        {
            _subscribe.Dispose();
        }
    }
}
