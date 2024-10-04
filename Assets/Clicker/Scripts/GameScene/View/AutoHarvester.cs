using Assets.Clicker.Scripts.AutoHarvesters;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Clicker.Scripts.GameScene.View
{
    public class AutoHarvester:MonoBehaviour
    {
        [SerializeField] private Image _ico;
        [SerializeField] private TextMeshProUGUI _textHarvestInfo;
        private HarvesterConfig _harvesterConfig;
        public float TimeToHarvest = 0;
        public void Init(HarvesterConfig harvesterConfig)
        {
            _harvesterConfig= harvesterConfig;

            _ico.sprite = harvesterConfig.Sprite;

            _textHarvestInfo.text = $"Harvest:\n{Math.Round(harvesterConfig.BaseProduction, 2)}\nevery\n{Math.Round(harvesterConfig.Time, 1)} s";
        }

        public double GetHarvestValue()
        {
            return _harvesterConfig.BaseProduction;
        }

        public void UpdateTimeToHarvest()
        {
            TimeToHarvest = Time.time + _harvesterConfig.Time;
        }
    }
}
