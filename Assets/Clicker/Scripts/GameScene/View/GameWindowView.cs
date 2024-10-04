using Assets.Clicker.Scripts.AutoHarvesters;
using Assets.Clicker.Scripts.GameScene.Game;
using Assets.Clicker.Scripts.State;
using Assets.Clicker.Scripts.Utils;
using R3;
using System;
using TMPro;
using UnityEngine;

namespace Assets.Clicker.Scripts.GameScene.View
{
    public class GameWindowView:MonoBehaviour
    {
        //fields
        private readonly CompositeDisposable _disposables = new();

        [SerializeField] private ClickableItemSelector _itemSelectorPrefab;
        [SerializeField] private Transform _itemSelectorsContainer;
        [SerializeField] private AutoHarvester _autoHarvesterPrefab;
        [SerializeField] private Transform _autoHarvestersContainer;      
        [SerializeField] private SoftCoinView _softCoinCounter;
        [SerializeField] private EnergyView _energy;
        [SerializeField] private ClickableItemView _clickableItem;
        [SerializeField] private HarvestRequester _harvestRequester;
        [SerializeField] private TextMeshProUGUI _textTapBonus;

        //properties
        public ClickableItemView ClickableItem => _clickableItem;
        public EnergyView Energy => _energy;
        public SoftCoinView SoftCoinCounter => _softCoinCounter;

        public void Bind(GameStateProxy gameState, GameController  gameController)
        {
            _softCoinCounter.Bind(gameState);
            _energy.Bind(gameState, gameController);

            _clickableItem.Bind(gameController);

            _harvestRequester.Init(() => gameController.RequestAutoHarvester?.Invoke());

            _disposables.Add(gameController.TapBonus.Subscribe(e => UpdateTapBonus(e))); 
        }

        private void UpdateTapBonus(double e)
        {
            _textTapBonus.text = $"Tap bonus:\n{StringHelper.GetStringFromValue(e)}";
        }

        public void CreateClickableItem(ItemConfig itemConfig, Action action)
        {          
            var newSelector = Instantiate(_itemSelectorPrefab, _itemSelectorsContainer);
            newSelector.transform.localPosition = Vector3.zero;
            newSelector.transform.localScale = Vector3.one;

            newSelector.Init(itemConfig, action);
        }

        public AutoHarvester CreateAutoHarvester(HarvesterConfig harvesterConfig, bool isLastHarvester)
        {
            var newHarvester = Instantiate(_autoHarvesterPrefab, _autoHarvestersContainer);

            newHarvester.transform.localPosition = Vector3.zero;
            newHarvester.transform.localScale = Vector3.one;

            newHarvester.Init(harvesterConfig);

            if (isLastHarvester)
            {
                _harvestRequester.gameObject.SetActive(false);
            }
            else
            {
                _harvestRequester.gameObject.SetActive(true);
                _harvestRequester.transform.SetAsLastSibling();
            }            

            return newHarvester;
        }

        private void OnDestroy()
        {
            _disposables.Dispose();
        }
    }
}
