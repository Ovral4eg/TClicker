using Assets.Clicker.Scripts.GameScene.Game;
using Assets.Clicker.Scripts.GameScene.View;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Clicker.Scripts.Game
{
    public class AutoHarvestHandler
    {
        private GameController _gameController;
        private double tapBonus;
        public AutoHarvestHandler(GameController gameController)
        {
            _gameController=gameController; ;
        }

        private List<AutoHarvester> _allHarvesters = new();
        public void AddHarvester(AutoHarvester newHarvester)
        {
            _allHarvesters.Add(newHarvester);
        }

        public void Process()
        {
            foreach (var h in _allHarvesters)
            {
                if(Time.time >= h.TimeToHarvest)
                {
                    var harvestValue = h.GetHarvestValue();
                    h.UpdateTimeToHarvest();

                    UpdateTapBonus(harvestValue);

                    _gameController.Harvest(harvestValue,tapBonus);
                }
            }
        }

        private void UpdateTapBonus(double value)
        {
            tapBonus += value * 0.1;
        }
    }
}
