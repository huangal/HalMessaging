using System;
using Microsoft.AspNetCore.Mvc;

namespace HalMessaging.Contracts
{
	/// <summary>
	/// Contais Message that needs to be delivered
	/// </summary>
	public class Message
	{
		public int Id { get; set; }
		public string Subject { get; set; }
		public int CreatorId { get; set; }
		public string Body { get; set; }
		public DateTime CreatedDate { get; set; }

		public int ParentMessageId { get; set; }
		public DateTime ExpiryDate { get; set; }

		public bool IsReminder { get; set; }

		public DateTime NextRemindDate { get; set; }

        /// <summary>
        /// How often it will be reminded
        /// </summary>
		public int ReminderFrequencyId { get; set; }

		
	}
}
