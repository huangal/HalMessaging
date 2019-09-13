using System;
namespace HalMessaging.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SettingNameAttribute : Attribute
    {
        public SettingNameAttribute(string name)
        {
            this.Name = name;
        }

        public string Name { get; }
    }

}
