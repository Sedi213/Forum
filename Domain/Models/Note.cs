using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Note
    {
        public Guid id { get; set; }
        public Guid userId { get; set; }
        public string title { get; set; }
        public string text { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
