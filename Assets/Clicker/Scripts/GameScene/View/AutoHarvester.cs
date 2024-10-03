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
        public void Init(HarvesterConfig harvesterConfig)
        {
            _ico.sprite = harvesterConfig.Sprite;

            _textHarvestInfo.text = $"Добыча:\n{Math.Round(harvesterConfig.BaseProduction, 2)}\nкаждые\n{Math.Round(harvesterConfig.Time, 1)} сек";
        }
    }
}
