using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseEntities;

namespace Server
{
    public interface IExpertAbilityProtocol : IClientAbilityProtocol
    {
        IExpertMethod expertMethod { get; set; }
        //bool Rate(Vehicle entity, Expert expert, float rate);
    }
}
