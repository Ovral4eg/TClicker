namespace Assets.Clicker.Scripts.PoolObject
{
    public interface IPoolObjectCreator<T>
    {
        T Create();
    }
}
