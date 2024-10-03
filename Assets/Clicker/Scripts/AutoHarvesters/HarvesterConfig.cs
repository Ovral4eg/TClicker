using UnityEngine;

namespace Assets.Clicker.Scripts.AutoHarvesters
{
    [CreateAssetMenu(fileName = "AutoHarvester", menuName = "Clicker/Configs/AutoHarvester", order = 1)]
    public class HarvesterConfig : ScriptableObject
    {
        [SerializeField] private Sprite _sprite;
        public Sprite Sprite => _sprite;

        [SerializeField] private double _baseProduction = 10;
        public double BaseProduction => _baseProduction;

        [SerializeField] private float _time=5f;
        public float Time => _time;
    }
}
