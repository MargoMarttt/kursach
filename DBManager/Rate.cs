//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DatabaseEntities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Rate
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public System.DateTime TimeOfCommit { get; set; }
        public int ProducedCarId { get; set; }
    
        public virtual Car ProducedCar { get; set; }
    }
}