using GameWarriors.PoolDomain.Abstraction;
using System.Collections.Generic;
using System;

namespace GameWarriors.PoolDomain.Core
{
    /// <summary>
    /// This class apply both method argument injection and method call by the polymorphism. 
    /// Injecting on unity "MonoBehaviour" driven classes script on specific method by the name "Initialize".
    /// Call the "Initialize" method and pass the service provider on the classes which implement "IInitializable" interface.
    /// </summary>
    public class CompondInitializer : IBehaviorInitializer<string>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<string, BehaviorInitItem> _itemTable;

        public string MethodName => "Initialize";

        public CompondInitializer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _itemTable = new Dictionary<string, BehaviorInitItem>();
        }

        public void InitializeBehavior(string id, object behavior)
        {
            if (behavior is IInitializable initializable)
            {
                initializable.Initialize(_serviceProvider);
            }
            else if (_itemTable.TryGetValue(id, out var initItem))
                initItem.InvokeInit(_serviceProvider, behavior);
        }

        public void InitializeBehaviors(string id, IEnumerable<object> items)
        {
            if (_itemTable.TryGetValue(id, out var initItem))
            {
                foreach (object item in items)
                {
                    if (item is IInitializable initializable)
                    {
                        initializable.Initialize(_serviceProvider);
                    }
                    else
                        initItem.InvokeInit(_serviceProvider, item);
                }
            }
        }

        public void Loading(IEnumerable<(string, Type)> initItems)
        {
            foreach (var item in initItems)
            {
                string key = item.Item1;
                Type itemType = item.Item2;
                if (itemType != null)
                    _itemTable.Add(key, new BehaviorInitItem(itemType, MethodName));
                else
                    _itemTable.Add(key, null);
            }
        }
    }
}
