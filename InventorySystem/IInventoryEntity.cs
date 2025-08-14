public interface IInventoryEntity
{
    int Id { get; }
    string Name { get; }
    int Quantity { get; }
    DateTime DateAdded { get; }
}
