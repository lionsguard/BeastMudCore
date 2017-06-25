namespace Beast
{
    public interface IWorld
    {
        void Initialize();
        void Update(ITime time);
        void Shutdown();
    }
}
