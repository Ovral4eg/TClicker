using Assets.Clicker.Scripts.State;
using Assets.Clicker.Scripts.Utils;
using DG.Tweening;
using R3;
using System;
using TMPro;
using UnityEngine;

namespace Assets.Clicker.Scripts.GameScene.View
{
    public class SoftCoinView:MonoBehaviour
    {
        IDisposable _subscribe;
        [SerializeField] private TextMeshProUGUI _textValue;

        public void Bind(GameStateProxy gamestate)
        {
            _subscribe = gamestate.SoftCoins.Skip(1).Subscribe(e => UpdateValue(e));
        }

        public void UpdateValue(double value)
        {
            _textValue.text = $"{StringHelper.GetStringFromValue(value)}";
        }

        private void OnDestroy()
        {
            _subscribe.Dispose();
        }
    }
}
