using Entities.Interfaces;

namespace Entities
{
    public class ReactivatableEntity : IEntityBase
    {
        public int Id { get; set; }

        /// <summary>
        /// Name of the entity which we we wish to enable the reactivation of items
        /// </summary>
        public string EntityName { get; set; }

        /// <summary>
        /// Name of the property we wish to display on the 'Name' column of the list of deactivated entities
        /// </summary>
        public string DisplayPropertyName { get; set; }
    }
}
