using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ApiSandboxAPI.Core
{
    public class ApiSandboxAPIResponse
    {
        public static ApiSandboxAPIResponse Create(HttpStatusCode _code, Object _result, string _err)
        {
            return new ApiSandboxAPIResponse(_code, _result, _err);
        }

        public HttpStatusCode status { get; set; }
        public Object result { get; set; }
        public string errormessage { get; set; }

        protected ApiSandboxAPIResponse(HttpStatusCode _status, Object _result = null, string _err = null)
        {
            status = _status;
            result = _result;
            errormessage = _err;
        }
    }
}
