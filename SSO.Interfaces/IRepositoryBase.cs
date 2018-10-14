using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSO.Interfaces
{
    public interface IRepositoryBase
    {
        int InsertRecord(object source);
    }
}
