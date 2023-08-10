using GameWarriors.PoolDomain.Abstraction;
using System;
using System.Collections.Generic;

namespace GameWarriors.PoolDomain.Core
{
    /// <summary>
    /// This class apply method call by the polymorphism way, call the "Initialize" method and pass the service provider on the classes which implement "IInitializable" interface.
    /// </summary>
    public class LocatorInitializer : IBehaviorInitializer<string>
    {
        private readonly IServiceProvider _serviceProvider;

        public LocatorInitializer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public string MethodName => string.Empty;

        public void InitializeBehavior(string id, object behavior)
        {
            IInitializable initializable = behavior as IInitializable;
            initializable?.Initialize(_serviceProvider);
        }

        public void InitializeBehaviors(string id, IEnumerable<object> items)
        {
            foreach (var behavior in items)
            {
                InitializeBehavior(id, behavior);
            }
        }

        public void Loading(IEnumerable<(string, Type)> initItems)
        {
            
        }
    }
}
