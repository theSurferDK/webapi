using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DomainModels
{
    public class Book : BookBase
    {
        public User User { get; set; }
    }
}
