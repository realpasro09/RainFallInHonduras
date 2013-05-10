using System;

namespace Rainfall.Domain
{
    public static class SystemDateTime
    {
        static SystemDateTime()
        {
            Now = () => DateTime.Now; 
        }
        public static Func<DateTime> Now { get; set; }
    }
}