using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseEntities;

namespace Server
{
    public interface IClientAbilityProtocol : IDisposable
    {
        //List<Vehicle> FindVehiclesWhere(Func<Vehicle, bool> comparer);
    }
}
