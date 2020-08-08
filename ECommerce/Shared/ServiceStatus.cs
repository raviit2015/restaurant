using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Shared
{
    public class ServiceStatus
    {
        public int code { get; set; } = 0;

        public string message { get; set; } = "";

        public ServiceStatus(int code)
        {
            this.code = code;
        }

        public ServiceStatus(int code, String message)
        {
            this.code = code;
            this.message = message;
        }

        public ServiceStatus()
        {
        }
    }
}
