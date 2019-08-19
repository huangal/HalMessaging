using System;
using System.Collections.Generic;
using HalMessaging.Contracts;

namespace HalMessaging.Services
{
    public interface IMessageService
    {
        List<Message> GetMessages();
        Message GetMessage(int messageId);
    }
}
