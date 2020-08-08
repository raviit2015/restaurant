using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Shared
{
    public class JsonResponse<T>
    {
        public ServiceStatus status;

        /**
         * request data field
         */
        public T data;

        public void setStatus(ServiceStatus status)
        {
            this.status = status;
        }

        public void setData(T data)
        {
            this.data = data;
        }

        public string toString()
        {
            return "JSONResponse(status=" + this.status + ", data=" + this.data + ")";
        }
    }
}
