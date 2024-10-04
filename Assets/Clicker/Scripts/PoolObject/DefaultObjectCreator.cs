using UnityEngine;

namespace Assets.Clicker.Scripts.PoolObject
{
    public class DefaultObjectCreator<T> : IPoolObjectCreator<T> where T : class, new()
    {
        private GameObject _prefab;
        private Transform _container;
        public DefaultObjectCreator(GameObject prefab, Transform container) 
        {
            _container=container;
            _prefab = prefab;
        }
        T IPoolObjectCreator<T>.Create()
        {
            var newObject = GameObject.Instantiate(_prefab,_container);
            newObject.transform.localScale = Vector3.one;

            var component = newObject.GetComponent<T>(); 

            return component;
        }
    }
}
