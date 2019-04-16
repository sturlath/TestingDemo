using System;
using System.ComponentModel.DataAnnotations;

namespace TestingDemo.Core.Entities
{
    /// <summary>
    /// Entity Framework base class
    /// </summary>
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    }
}