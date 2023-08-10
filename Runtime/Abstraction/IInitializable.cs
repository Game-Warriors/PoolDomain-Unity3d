using System;
namespace GameWarriors.PoolDomain.Abstraction
{
    public interface IInitializable
    {
        void Initialize(IServiceProvider serviceProvider);
    }
}
