using Assets.Clicker.Scripts.PoolObject;
using Assets.Clicker.Scripts.Utils;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Clicker.Scripts.GameScene.View
{
    public class FlyingNumbersView:MonoBehaviour, IPoolable
    {
        [SerializeField] private TextMeshProUGUI _textValue;

        public void ResetState()
        {
            gameObject.SetActive(false);
        }

        public void SetValue(double value)
        {
            _textValue.text = $"{StringHelper.GetStringFromValue(value)}";

            gameObject.SetActive(true);
        }

        public IEnumerator ReturnToPool(ObjectPool<FlyingNumbersView> _flyingNumbersPool, float duration)
        {
            yield return new WaitForSeconds(duration);

            var poolObject = this;

            _flyingNumbersPool.ReturnObject(ref poolObject);
        }
    }
}
