using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ViewModels
{
    public class UserInput<T>
    {
        public T Data { get; set; }
        public string key { get; set; }
    }
}
