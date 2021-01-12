using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class BorrowBooks
    {
        [Key, Column(Order = 0)]
        public int BookId { get; set; }

        [Key, Column(Order = 1)]
        public int UserId { get; set; }

        public virtual Book Book { get; set; }
        public virtual User User { get; set; }

    }
}