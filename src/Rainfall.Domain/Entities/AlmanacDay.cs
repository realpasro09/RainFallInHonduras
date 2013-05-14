using System;
using System.Collections.Generic;
using System.Linq;

namespace Rainfall.Domain.Entities
{
    public class AlmanacDay:IEntity
    {
        public virtual int Id { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual City City { get; set; }
        public virtual IEnumerable<AlmanacHourly> AlmanacHourly { get; set; }
        public virtual double Get24HrsTempHigh()
        {
            return AlmanacHourly.Max(almanacHourly => almanacHourly.Temperature);
        }
        public virtual double Get24HrsTempLow()
        {
            return AlmanacHourly.Min(almanacHourly => almanacHourly.Temperature);
        }
        public virtual double Get24HrsPrecipitation()
        {
            return AlmanacHourly.Sum(almanacHourly => almanacHourly.Precipitation);
        }
    }
}