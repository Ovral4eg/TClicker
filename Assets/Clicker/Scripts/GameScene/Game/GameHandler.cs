using Assets.Clicker.Scripts.AutoHarvesters;
using Assets.Clicker.Scripts.GameScene.View;
using Assets.Clicker.Scripts.State;
using DG.Tweening;
using R3;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Clicker.Scripts.GameScene.Game
{
    public class GameHandler:MonoBehaviour
    {
        [SerializeField] private GameWindowUiView _gameWindowPrefab;
        [SerializeField] private FlyingNumbersUiView _flyingNumberPrefab;
        [SerializeField] private List<ItemConfig> _itemConfigs;
        [SerializeField] private List<HarvesterConfig> _harvesterConfigs;
     
        private GameStateProxy _gameState;
        private float _currentModificator = 1;
        private double _harvestersBonus = 0;
        private int _currentHarvesterIndex = 0;
        private ReactiveProperty<ItemConfig> _currentItem;
        private GameWindowUiView _gameWindow;
        public ReactiveProperty<ItemConfig> CurrentItem => _currentItem;
        public void Init(GameSceneRootUi sceneUi, GameStateProxy gameState)
        {
            _gameState = gameState;

            _gameWindow = Instantiate(_gameWindowPrefab);
            sceneUi.AttachSceneUi(_gameWindow.gameObject);

            _currentItem = new ReactiveProperty<ItemConfig>(_itemConfigs[0]);

            _gameWindow.Bind(gameState, this);

            _gameWindow.ClickableItem.OnItemClicked += OnClickItem;
            _gameWindow.Energy.OnRequestEnergy += Energy_OnRequestEnergy;

            CreateClickableItems();

            CreateHarvester(_currentHarvesterIndex);
        }

        private void CreateClickableItems()
        {
            foreach (var i in _itemConfigs)
            {
                Action selectItem = () =>
                {
                    SelectItem(i);
                };

                _gameWindow.CreateClickableItem(i, selectItem);
            }
        }

        private void CreateHarvester(int index)
        {
            if (_currentHarvesterIndex >= _harvesterConfigs.Count - 1) return;

            _gameWindow.CreateAutoHarvester(_harvesterConfigs[_currentHarvesterIndex]);

            _currentHarvesterIndex++;
        }

        private void SelectItem(ItemConfig item)
        {
            _currentItem.OnNext(item);
        }

        private void Energy_OnRequestEnergy(object sender, System.EventArgs e)
        {
            var currentEnergy = _gameState.Energy.CurrentValue;
            _gameState.Energy.OnNext(currentEnergy + 10);
        }
        public void OnClickItem(object sender, ClickArgs e)
        {
            if (_gameState.Energy.CurrentValue >= _currentItem.CurrentValue.ClickCost) 
            {
                _gameState.Energy.OnNext(_gameState.Energy.Value - _currentItem.CurrentValue.ClickCost);

                var clickProduction = _currentItem.CurrentValue.BaseProduction * _currentModificator + _harvestersBonus;
                var resultValue = _gameState.SoftCoins.CurrentValue + clickProduction ;
                _gameState.SoftCoins.OnNext(resultValue);

                var newFlyNumber = Instantiate(_flyingNumberPrefab, _gameWindow.transform);
                newFlyNumber.transform.position = e.point;
                newFlyNumber.SetValue(clickProduction);

                var targetPosition = _gameWindow.SoftCoinCounter.transform.position;

                var seq = DOTween.Sequence();
                seq.Append(newFlyNumber.transform.DOScale(Vector3.one * 4f, .3f));
                seq.Append(newFlyNumber.transform.DOScale(Vector3.one * 0f, .7f));
                seq.Join(newFlyNumber.transform.DOMove(targetPosition, 1));

                //seq.Insert(0.2f, newFlyNumber.transform.DOScale(Vector3.one * 1.3f, 0.8f));

                Destroy(newFlyNumber, 1);
            }
            else
            {
                _gameWindow.Energy.AnimateLowEnergy();
            }
        }
    }
}
