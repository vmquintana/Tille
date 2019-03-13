namespace TilleWPF.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Book")]
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Country { get; set; }

        [Required]
        public int NumberOfClients { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateIn { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOut { get; set; }

        [Required]
        public double EstimatedPrice { get; set; }

        public int MovementId { get; set; }
        public virtual Movement Movement { get; set; }
    }
}
