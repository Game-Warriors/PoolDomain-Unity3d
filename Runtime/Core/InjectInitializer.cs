using GameWarriors.PoolDomain.Abstraction;
using System;
using System.Collections.Generic;

namespace GameWarriors.PoolDomain.Core
{
    /// <summary>
    /// This class apply method argument injection on unity "MonoBehaviour" driven classes script on specific method by the name "Initialize"
    /// </summary>
    public class InjectInitializer : IBehaviorInitializer<string>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<string, BehaviorInitItem> _itemTable;

        public string MethodName => "Initialize";

        public InjectInitializer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _itemTable = new Dictionary<string, BehaviorInitItem>();
        }

        public void InitializeBehavior(string id, object behavior)
        {
            if (_itemTable.TryGetValue(id, out var initItem))
                initItem.InvokeInit(_serviceProvider, behavior);
        }

        public void InitializeBehaviors(string id, IEnumerable<object> items)
        {
            if (_itemTable.TryGetValue(id, out var initItem))
            {
                foreach (object item in items)
                {
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
            }
        }
    }
}