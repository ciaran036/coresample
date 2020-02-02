using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.Interfaces
{
    public interface IGuidEntityBase
    {
        [Key]
        Guid Id { get; set; }
    }
}