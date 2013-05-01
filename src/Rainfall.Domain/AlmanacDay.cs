using System;

namespace Rainfall.Domain
{
    public class AlmanacDay:IEntity
    {
        public virtual int Id { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual double Precipitation { get; set; }
        public virtual double TempHigh { get; set; }
        public virtual double TempLow { get; set; }
    }
}