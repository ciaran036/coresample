using System;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Interfaces;

namespace Entities
{
    public class Document : IEntityBase
    {
        public int Id { get; set; }

        /// <summary>
        /// Filename including extension
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// File extension
        /// </summary>
        public string Extension { get; set; }

        public string Caption { get; set; }

        public DateTime DateAdded { get; set; }

        public int UserId { get; set; }

        /// <summary>
        /// A unique reference consisting of 2 characters representing the area it is associated with and the User ID
        /// </summary>
        public string Ref { get; set; }

        public int DocumentTypeId { get; set; }

        [ForeignKey(nameof(DocumentTypeId))]
        public virtual DocumentType DocumentType { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
    }
}
