using Assets.Clicker.Scripts.AutoHarvesters;
using Assets.Clicker.Scripts.Game;
using Assets.Clicker.Scripts.GameScene.View;
using Assets.Clicker.Scripts.PoolObject;
using Assets.Clicker.Scripts.State;
using DG.Tweening;
using R3;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Clicker.Scripts.GameScene.Game
{
    public delegate void ClickItem(Vector2 point);
    public delegate void EnergyRequest();
    public delegate void LowEnergyAnimation();
    public delegate void RequestAutoHarvester();
    public delegate void Harvest(double value, double tapBonus);

    public class GameController:MonoBehaviour
    {
        //fields
        [SerializeField] private GameWindowView _gameWindowPrefab;
        [SerializeField] private FlyingNumbersView _flyingNumberPrefab;
        [SerializeField] private List<ItemConfig> _itemConfigs;
        [SerializeField] private List<HarvesterConfig> _harvesterConfigs;
        private GameStateProxy _gameState;
        private float _currentModificator = 1;
        private int _nextHarvesterIndex = 0;
        private ReactiveProperty<ItemConfig> _currentItem;
        private ReactiveProperty<double> _tapBonus;
        private GameWindowView _gameWindow;
        private AutoHarvestHandler _autoHarvestHandler;
        private ObjectPool<FlyingNumbersView> _flyingNumbersPool;

        public ClickItem ClickItem;
        public EnergyRequest EnergyRequest;
        public LowEnergyAnimation LowEnergyAnimation;
        public RequestAutoHarvester RequestAutoHarvester;
        public Harvest Harvest;

        //properties
        public ReactiveProperty<ItemConfig> CurrentItem => _currentItem;
        public ReactiveProperty<double> TapBonus => _tapBonus;
        public void Init(GameSceneRootUi sceneUi, GameStateProxy gameState)
        {
            _gameState = gameState;

            _autoHarvestHandler = new AutoHarvestHandler(this);           

            this.EnergyRequest += OnRequestEnergy;
            this.ClickItem += OnClickItem;
            this.RequestAutoHarvester += OnRequestAutoHarvester;
            this.Harvest += OnHarvest;

            _gameWindow = Instantiate(_gameWindowPrefab);
            sceneUi.AttachSceneUi(_gameWindow.gameObject);

            _flyingNumbersPool = new ObjectPool<FlyingNumbersView>(
               new DefaultObjectCreator<FlyingNumbersView>(_flyingNumberPrefab.gameObject, _gameWindow.transform));

            _currentItem = new ReactiveProperty<ItemConfig>(_itemConfigs[0]);
            _tapBonus = new ReactiveProperty<double>(0);

            _gameWindow.Bind(gameState, this);          

            CreateClickableItems();

            CreateHarvester(_nextHarvesterIndex);
        }

        private void OnRequestEnergy()
        {
            var currentEnergy = _gameState.Energy.CurrentValue;
            _gameState.Energy.OnNext(currentEnergy + 10);
        }

        public void OnClickItem(Vector2 position)
        {
            if (_gameState.Energy.CurrentValue >= _currentItem.CurrentValue.ClickCost)
            {
                _gameState.Energy.OnNext(_gameState.Energy.Value - _currentItem.CurrentValue.ClickCost);

                var clickProduction = _currentItem.CurrentValue.BaseProduction * _currentModificator + TapBonus.CurrentValue;
                var resultValue = _gameState.SoftCoins.CurrentValue + clickProduction;
                _gameState.SoftCoins.OnNext(resultValue);

                var flyingNumber = _flyingNumbersPool.GetObject();

                flyingNumber.transform.position = position;
                flyingNumber.transform.localScale = Vector3.one;
                flyingNumber.SetValue(clickProduction);                

                var targetPosition = _gameWindow.SoftCoinCounter.transform.position;

                var animationDuration = 1f;

                var seq = DOTween.Sequence();
                seq.Append(flyingNumber.transform.DOScale(Vector3.one * 4f, animationDuration * .3f));
                seq.Append(flyingNumber.transform.DOScale(Vector3.one * 0f, animationDuration * .7f));
                seq.Join(flyingNumber.transform.DOMove(targetPosition, animationDuration));

                StartCoroutine(flyingNumber.ReturnToPool(_flyingNumbersPool, animationDuration*1.5f));
            }
            else
            {
                LowEnergyAnimation?.Invoke();
            }
        }

        public void OnRequestAutoHarvester()
        {
            CreateHarvester(_nextHarvesterIndex);
        }

        public void OnHarvest(double value, double tapBonus)
        {           
            var resultValue = _gameState.SoftCoins.CurrentValue + value;
            _gameState.SoftCoins.OnNext(resultValue);
            _tapBonus.OnNext(tapBonus);
        }

        private void CreateHarvester(int index)
        {
            if (_nextHarvesterIndex > _harvesterConfigs.Count - 1) return;

            var isLastHarvester = _nextHarvesterIndex == _harvesterConfigs.Count-1;

            var newHarvester = _gameWindow.CreateAutoHarvester(_harvesterConfigs[_nextHarvesterIndex], isLastHarvester);

            _autoHarvestHandler.AddHarvester(newHarvester);

            _nextHarvesterIndex++;
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

        private void SelectItem(ItemConfig item)
        {
            _currentItem.OnNext(item);
        }

        private void Update()
        {
           if(_autoHarvestHandler!=null) _autoHarvestHandler.Process();
        }
    }
}
