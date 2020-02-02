namespace Entities.Interfaces
{
    /// <summary>
    /// Defines an entity that can be activated or deactivated (soft deleted).
    /// </summary>
    public interface IActivatableEntity
    {
        bool Active { get; set; }
    }
}