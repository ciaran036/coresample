namespace Entities.Interfaces
{
    public interface ILookupBase
    {
        int Id { get; set; }
        string Name { get; set; }
        int SortOrder { get; set; }
        bool Active { get; set; }
    }
}