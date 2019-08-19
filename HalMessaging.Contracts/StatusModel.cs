using System;
namespace HalMessaging.Contracts
{
    public class StatusModel
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string Description { get; set; }
    }
}
