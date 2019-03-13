using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TilleWPF.Model
{
    [Table("Product")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public int AmountBoughtCurrentMonth { get; set; }

        [Required]
        public double CurrentMonthCost { get; set; }

        public virtual ICollection<ProductMov> ProductMovs { get; set; }
    }
}
