using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Clicker.Scripts.GameScene.View
{
    public class HarvestRequester:MonoBehaviour
    {
        [SerializeField] private Button _button;
        private Action Request;
        public void Init(Action Request)
        {
            _button.onClick.AddListener(() => Request());
        }
       
    }
}
