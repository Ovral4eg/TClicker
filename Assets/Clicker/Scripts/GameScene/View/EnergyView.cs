using Assets.Clicker.Scripts.GameScene.Game;
using Assets.Clicker.Scripts.State;
using DG.Tweening;
using R3;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Clicker.Scripts.GameScene.View
{

    public class EnergyView:MonoBehaviour
    {
        IDisposable _subscribe;
        [SerializeField] private TextMeshProUGUI _textValue;
        [SerializeField] private Button _buttonRequestEnergy;
        private Vector3 _originPosition;
        public void Bind(GameStateProxy gameState, GameController gameController)
        {
            _originPosition= transform.position;

            UpdateValue(gameState.Energy.CurrentValue,false);

            _subscribe = gameState.Energy.Skip(1).Subscribe(e => UpdateValue(e,true));

            _buttonRequestEnergy.onClick.AddListener(() => { gameController.EnergyRequest.Invoke(); });

            gameController.LowEnergyAnimation += LowEnergyAnimation;
        }

        public void UpdateValue(float value, bool doAnimation)
        {
            _textValue.text = $" {Math.Round(value, 2)}";

            if (value > 0) 
            {
                _textValue.color = Color.black;
            }
            else
            {
                _textValue.color = Color.red;

                if (doAnimation) LowEnergyAnimation();
            }
        }

        public void LowEnergyAnimation()
        {
            var animationSequence = DOTween.Sequence();
            animationSequence.Append(transform.DOShakePosition(1, 5, 5));
            animationSequence.Append(transform.DOMove(_originPosition, 0));
        }

        private void OnDestroy()
        {
            _subscribe.Dispose();
        }
    }
}
