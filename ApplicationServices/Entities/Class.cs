using ApplicationServices.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.Entities
{
    public class Class
    {
        public int Id { get; set; }
        public string Alias { get; set; }
        public Teacher? TeacherId { get; set; }
    }
}
