namespace TilleWPF.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public enum ServiceType : int
    {
        Clean = 0,
        Taxi = 1,
        Breakfast = 2
    }

    [Table("Service")]
    public partial class Service
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [EnumDataType(typeof(ServiceType))]
        public int Type { get; set; }

        [Required]
        public string Description { get; set; }

        public int MovementId { get; set; }
        public virtual Movement Movement { get; set; }
    }
}
