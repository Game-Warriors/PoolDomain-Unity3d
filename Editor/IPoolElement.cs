namespace GameWarriors.PoolDomain.Editor
{
    public interface IPoolElementGroup
    {
        string name { get; }
        int Count { get; }
        void DrawElement(int index, int width, int height);
        void AddNewElement();
        void SaveElement<T>(T input);
    }
}