namespace PoolSystem
{
    public interface IPoolableItem
    {
        public void CreateByPool();
        public void GetByPool();
        public void ReleaseByPool();
        public void DestroyByPool();
    }
}
