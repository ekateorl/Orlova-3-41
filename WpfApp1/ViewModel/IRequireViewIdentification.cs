using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.ViewModel
{
    public interface IRequireViewIdentification
    {
        Guid ViewID { get; }
    }
}
