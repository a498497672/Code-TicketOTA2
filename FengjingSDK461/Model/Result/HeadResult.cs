using FengjingSDK461.Model.Response;
using System;

namespace FengjingSDK461.Model.Result
{
    public class HeadResult
    {
        public static HeadResponse V1
        {
            get
            {
                return new HeadResponse
                {
                    ProtocolVersion = "V1",
                    InvokeUser = "LuoHuShan",
                    InvokeTime = DateTime.Now.ToString("yyyy-MM-dd")
                };
            }
        }
    }
}
