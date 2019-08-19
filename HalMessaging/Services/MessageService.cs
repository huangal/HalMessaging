using System;
using System.Collections.Generic;
using System.Linq;
using HalMessaging.Contracts;

namespace HalMessaging.Services
{
    public class MessageService : IMessageService
    {
        public MessageService()
        {
        }

        public Message GetMessage(int messageId)
        {
            return GetMessages(messageId).FirstOrDefault( x => x.Id == messageId);
        }

        public List<Message> GetMessages(int messageId)
        {
            int id = 1;
            // var products = GetProducts();

            GenFu.GenFu.Configure<Message>()
                .Fill(x => x.Id, () => { return id++; });
                //.Fill(x => x.Name).AsFirstName()
                //.Fill(x => x.Last).AsLastName()
                // .Fill(x => x.Age).WithinRange(18, 70)
                // .Fill(x => x.Email).AsEmailAddress()
                // .Fill(x => x.Product).WithRandom(Message);

            List<Message> messages = GenFu.GenFu.ListOf<Message>(messageId);
            return messages;
        }

        public List<Message> GetMessages()
        {
            return GetMessages(10);
        }
    }
}
