using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Service.Interfaces
{
    public interface IProxyData
    {
        int Id { get; set; }
        string ProxyString { get; set; }
    }
}
