using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TilleWPF.Model
{
    public class ProductMov
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Required]
        public int Amount { get; set; }

        public int? MovementId { get; set; }
        public virtual Movement Movement { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
