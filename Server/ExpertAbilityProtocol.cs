using DatabaseEntities;
using System;
using System.Collections.Generic;


namespace TCPConnectionAPI_C_sharp_
{
    public class ExpertAbilityProtocol : IExpertAbilityProtocol
    {
        public IExpertMethod expertMethod { get; set; }

        public IDataModify DBconnection;

        public bool Rate(Car entity, Expert expert, float rate)
        {
            var ratedObj = expertMethod.Rate(entity, expert, rate) as Car;
            return DBconnection.UpdateCar(ratedObj);
        }

        public void Dispose()
        {
            DBconnection.Dispose();
        }

        public List<Car> FindCarsWhere(Func<Car, bool> comparer)
        {
            return DBconnection.FindCarsWhere(comparer);
        }

        public ExpertAbilityProtocol()
        {
            DBconnection = new DatabaseWrapper();
        }

    }
}
