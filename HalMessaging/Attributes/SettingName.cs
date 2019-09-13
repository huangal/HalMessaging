using System;
namespace HalMessaging.Attributes
{

    public class Setting
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }


    public class ReceiptConfigurations
    {

        [SettingName("Customer")]
        public string LastName { get; set; }

        public string Resource { get; set; }

        public string Type { get; set; }

        [SettingName("sectionbase")]
        public string Section { get; set; }

        public string BaseUri { get; set; }
    }


    public class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAdress { get; set; }
        public string PhoneNumber { get; set; }
    }
}
