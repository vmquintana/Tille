namespace TilleWPF.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public enum MovementType : int
    {
        Service = 0,
        Book = 1,
        Warehouse = 2,
        Other = 3
    }

    [Table("Movement")]
    public class Movement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Required]
        [EnumDataType(typeof(MovementType))]
        public int Type { get; set; }

        public string Description { get; set; }

        [Required]
        public double Price { get; set; }
    }
}
