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
        private Vector3 _originPosition;

        public void Bind(GameStateProxy gamestate)
        {
            _originPosition = transform.position;            

            UpdateValue(gamestate.SoftCoins.CurrentValue, false);

            _subscribe = gamestate.SoftCoins.Skip(1).Subscribe(e => UpdateValue(e,true));
        }

        public void UpdateValue(double value, bool doAnimation)
        {
            _textValue.text = $"{StringHelper.GetStringFromValue(value)}";

            if (doAnimation) DoAnimation();
        }

        public void DoAnimation()
        {
            var animationSequence = DOTween.Sequence();
            animationSequence.Append(transform.DOShakePosition(0.1f, 5, 5));
            animationSequence.Append(transform.DOMove(_originPosition, 0.1f));
        }

        private void OnDestroy()
        {
            _subscribe.Dispose();
        }
    }
}
