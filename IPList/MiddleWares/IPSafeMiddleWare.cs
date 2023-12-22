using Microsoft.Extensions.Options;
using System.Net;

namespace IPList.MiddleWares
{
    public class IPSafeMiddleWare
    {
        private readonly RequestDelegate _delegate;
        private readonly IPList _ipList;
        public IPSafeMiddleWare(IOptions<IPList> ipList, RequestDelegate @delegate)
        {
            _ipList = ipList.Value;
            _delegate = @delegate;
        }

        public async Task Invoke(HttpContext context)
        {
            IPAddress reqipAdress = context.Connection.RemoteIpAddress;
            bool isWhiteList = _ipList.WhiteList.Where(x => IPAddress.Parse(x).Equals(reqipAdress)).Any();
            if (!isWhiteList)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return;
            }
            await _delegate(context); 
        } 

    }
}
