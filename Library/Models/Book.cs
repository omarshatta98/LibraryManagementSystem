using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int Quantity { get; set; }

        public int BorrowedQuantity { get; set; } = 0;

        public virtual ICollection<BorrowBooks> BorrowBooks { get; set; }
    }
}