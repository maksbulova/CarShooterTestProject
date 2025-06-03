namespace Helper.PoolSystem
{
    public interface IPoolableItem
    {
        public void OnPoolCreate();
        public void OnPoolGet();
        public void OnPoolRelease();
        public void OnPoolDestroy();
    }
}
