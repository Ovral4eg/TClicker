﻿using Assets.Clicker.Scripts.State;
using DG.Tweening;
using R3;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Clicker.Scripts.GameScene.View
{
    public class EnergyValueBinder:MonoBehaviour
    {
        IDisposable _subscribe;
        [SerializeField] private TextMeshProUGUI _textValue;
        [SerializeField] private Button _buttonRequestEnergy;

        public event EventHandler OnRequestEnergy; 
        public void Bind(GameStateProxy gameState)
        {
            UpdateValue(gameState.Energy.CurrentValue,false);

            _subscribe = gameState.Energy.Skip(1).Subscribe(e => UpdateValue(e,true));

            _buttonRequestEnergy.onClick.AddListener(() => { OnRequestEnergy?.Invoke(this, EventArgs.Empty); });
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

                if (doAnimation) AnimateLowEnergy();
            }
        }

        public void AnimateLowEnergy()
        {
            transform.DOShakePosition(2, 5, 5);
        }

        private void OnDestroy()
        {
            _subscribe.Dispose();
        }
    }
}
