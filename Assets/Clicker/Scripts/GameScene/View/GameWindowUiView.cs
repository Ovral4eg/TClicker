using Assets.Clicker.Scripts.AutoHarvesters;
using Assets.Clicker.Scripts.GameScene.Game;
using Assets.Clicker.Scripts.State;
using NUnit.Framework;
using R3;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Clicker.Scripts.GameScene.View
{
    public class GameWindowUiView:MonoBehaviour
    {
        //fields
        [SerializeField] private ClickableItemSelector _itemSelectorPrefab;
        [SerializeField] private Transform _itemSelectorsContainer;
        [SerializeField] private AutoHarvester _autoHarvesterPrefab;
        [SerializeField] private Transform _autoHarvestersContainer;
        private readonly CompositeDisposable _disposables = new();
        [SerializeField] private SoftCoinValueBinder _softCoinCounter;
        [SerializeField] private EnergyValueBinder _energy;
        [SerializeField] private ClickableItemBinder _clickableItem;
        [SerializeField] private HarvestRequester _harvestRequester;

        //properties
        public ClickableItemBinder ClickableItem => _clickableItem;
        public EnergyValueBinder Energy => _energy;
        public SoftCoinValueBinder SoftCoinCounter => _softCoinCounter;

        public void Bind(GameStateProxy gameState, GameHandler gameHandler)
        {
            _softCoinCounter.Bind(gameState);
            _energy.Bind(gameState);

            _clickableItem.Bind(gameHandler);
        }       

        public void CreateClickableItem(ItemConfig itemConfig, Action action)
        {          
            var newSelector = Instantiate(_itemSelectorPrefab, _itemSelectorsContainer);
            newSelector.transform.localPosition = Vector3.zero;
            newSelector.transform.localScale = Vector3.one;

            newSelector.Init(itemConfig, action);
        }

        public AutoHarvester CreateAutoHarvester(HarvesterConfig harvesterConfig)
        {
            var newHarvester = Instantiate(_autoHarvesterPrefab, _autoHarvestersContainer);

            newHarvester.transform.localPosition = Vector3.zero;
            newHarvester.transform.localScale = Vector3.one;

            newHarvester.Init(harvesterConfig);

            _harvestRequester.transform.SetAsLastSibling();

            return newHarvester;
        }
    }
}
