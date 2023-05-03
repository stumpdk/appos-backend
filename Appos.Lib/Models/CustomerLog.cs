using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appos.Lib.Models
{
    public class CustomerLog
    {
        public int Id { get; set; }
        public string? Event { get; set; }
        public string? Details { get; set; }
        public DateTime Created { get; set; }
        public int CustomerId { get; set; }
    }
}
