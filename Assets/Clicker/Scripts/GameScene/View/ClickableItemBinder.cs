using Assets.Clicker.Scripts.GameScene.Game;
using DG.Tweening;
using R3;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Clicker.Scripts.GameScene.View
{
    public class ClickableItemBinder:MonoBehaviour,IPointerDownHandler
    {
        //fileds
        IDisposable _subscribe;
        [SerializeField] private Image _itemIco;

        //events
        public event EventHandler<ClickArgs> OnItemClicked;

        public void Bind(GameHandler gameHandler)
        {
            _subscribe = gameHandler.CurrentItem.Subscribe(e => UpdateSprite(e));
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnItemClicked?.Invoke(this, new ClickArgs { point = eventData.position });

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
