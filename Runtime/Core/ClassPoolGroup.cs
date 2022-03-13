using System.Collections.Generic;
using UnityEngine;
using System;

namespace GameWarriors.PoolDomain.Core
{
    public class ClassPoolGroup<TU> where TU : new()
    {
        private class ClassPool<T> where T : new()
        {
            private Queue<T> _queue;
            private Action<T> OnCreate;

            public ClassPool(int poolCount, Action<T> onCreate)
            {
                _queue = new Queue<T>(poolCount);
                OnCreate = onCreate;
                for (int i = 0; i < poolCount; ++i)
                {
                    var tmp = new T();
                    onCreate?.Invoke(tmp);
                    _queue.Enqueue(tmp);
                }
            }
            public void AddItem(T item)
            {
                _queue.Enqueue(item);
            }

            public T GetItem()
            {
                T item;
                if (_queue.Count > 2)
                {
                    item = _queue.Dequeue();
                }
                else
                {
                    item = new T();
                    OnCreate(item);
                }
                return item;
            }

            public T[] ToArray()
            {
                return _queue.ToArray();
            }


        }

        private static Dictionary<Type, ClassPool<TU>> _pool;

        public static TU GetItem(Type type)
        {
            if (_pool.TryGetValue(type, out var queue))
                return queue.GetItem();
            else
                Debug.LogError("There is no such class queue type in poolManager");
            return default(TU);
        }

        public static void AddItem(Type type, TU item)
        {
            if (_pool.TryGetValue(type, out var queue))
                queue.AddItem(item);
            else
                Debug.LogError("There is no such class queue type in poolManager");
        }
    }


}