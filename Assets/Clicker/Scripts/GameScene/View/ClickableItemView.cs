using Assets.Clicker.Scripts.GameScene.Game;
using DG.Tweening;
using R3;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Clicker.Scripts.GameScene.View
{
    public class ClickableItemView:MonoBehaviour,IPointerDownHandler
    {
        //fileds
        IDisposable _subscribe;
        [SerializeField] private Image _itemIco;
        private GameController _gameController;

        public void Bind(GameController gameController)
        {
            _gameController = gameController;

            _subscribe = gameController.CurrentItem.Subscribe(e => UpdateSprite(e));

            gameController.ClickItem += ClickItem;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _gameController.ClickItem.Invoke(eventData.position);
        }

        private void ClickItem(Vector2 point)
        {
            var seq = DOTween.Sequence();
            seq.Append(transform.DOScale(1.2f, 0.1f));
            seq.Append(transform.DOScale(1f, 0.1f));
        }

        public void UpdateSprite(ItemConfig item)
        {
            _itemIco.sprite = item.Sprite;
        }

        private void OnDestroy()
        {
            _subscribe.Dispose();
        }
    }
}
