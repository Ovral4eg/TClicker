using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Clicker.Scripts.GameScene.View
{
    public class ClickableItemSelector:MonoBehaviour
    {
        [SerializeField] private Image _ico;
        [SerializeField] private Button _button;

        public void Init(ItemConfig item, Action action)
        {
            _ico.sprite = item.Sprite;
            _button.onClick.AddListener(() => action());
        }
    }
}
