namespace Beast.Objects.Items
{
    public interface IContainer : IItem
    {
        int Count { get; }
        int Capacity { get; set; }
        void Add(IObject obj);
        void Remove(IObject obj);
    }
}
