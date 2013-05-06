using System;

namespace Rainfall.Domain.Entities
{
    public class AlmanacHourly:IEntity
    {
        public virtual int Id { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual int Hour { get; set; }
        public virtual double Precipitation { get; set; }
        public virtual double Temperature { get; set; }
        public virtual int AlmanacDayId { get; set; }
    }
}