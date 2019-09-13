using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using HalMessaging.Attributes;
using Microsoft.Extensions.Configuration;

namespace HalMessaging.Extensions
{
    public static class ConfigurationExtensions
    {
        public static T Map<T>(this List<Setting> settings) where T : class
        {
            T instance = Activator.CreateInstance<T>();
            if (settings == null || settings.Count <= 0)
                return instance;
            foreach (PropertyInfo property in instance.GetType().GetProperties())
            {
                string settingName = GetAttributeName(property.GetCustomAttributes()) ?? property.Name;
                Setting setting = settings.FirstOrDefault(x => x.Name.Equals(settingName));
                if (setting != null && property != null && property.CanWrite)
                    property.SetValue(instance, setting.Value);
            }
            return instance;
        }

        public static T Map<T>(this Dictionary<string, string> settings) where T : class
        {
            T instance = Activator.CreateInstance<T>();
            if (settings == null || settings.Count <= 0) return instance;
            foreach (PropertyInfo property in instance.GetType().GetProperties())
            {
                string settingName = GetAttributeName(property.GetCustomAttributes()) ?? property.Name;
                KeyValuePair<string, string> keyValuePair = settings.FirstOrDefault(x => x.Key.Equals(settingName));
                if (keyValuePair.Key != null && property != null && property.CanWrite)
                    property.SetValue(instance, keyValuePair.Value);
            }
            return instance;
        }
               
        public static T Map<T>(this IConfiguration configuration) where T : class
        {
            T instance = Activator.CreateInstance<T>();
                       
            if (configuration == null)  return instance;
                       

            foreach (PropertyInfo property in instance.GetType().GetProperties())
            {
                string settingName = GetAttributeName(property.GetCustomAttributes()) ?? property.Name;

                string value = configuration[settingName];
                if (!string.IsNullOrEmpty(value) && property != null && property.CanWrite)
                {
                    property.SetValue(instance, value);
                }
            }
            return instance;
        }

        private static string GetAttributeName(IEnumerable<Attribute> attributes)
        {
            try
            {
                IList<Attribute> source = attributes as IList<Attribute> ?? attributes.ToList();
                if (!source.Any())
                    return null;
                return (source.FirstOrDefault() as SettingNameAttribute)?.Name;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                return null;
            }
        }
    }
}


/*
public static class ConfigurationExtensions
    {
        public static T Map<T>(this List<Setting> settings) where T : class
        {
            T instance = Activator.CreateInstance<T>();
            if (settings == null || settings.Count <= 0)
                return instance;
            foreach (PropertyInfo property in instance.GetType().GetProperties())
            {
                string settingName = ConfigurationExtensions.GetAttributeName(property.GetCustomAttributes()) ?? property.Name;
                Setting setting = settings.FirstOrDefault<Setting>((Func<Setting, bool>)(x => x.Name.Equals(settingName)));
                if (setting != null && property != (PropertyInfo)null && property.CanWrite)
                    property.SetValue((object)instance, (object)setting.Value);
            }
            return instance;
        }

        public static T Map<T>(this Dictionary<string, string> settings) where T : class
        {
            T instance = Activator.CreateInstance<T>();
            if (settings == null || settings.Count <= 0) return instance;
            foreach (PropertyInfo property in instance.GetType().GetProperties())
            {
                string settingName = ConfigurationExtensions.GetAttributeName(property.GetCustomAttributes()) ?? property.Name;
                KeyValuePair<string, string> keyValuePair = settings.FirstOrDefault<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>)(x => x.Key.Equals(settingName)));
                if (keyValuePair.Key != null && property != (PropertyInfo)null && property.CanWrite)
                    property.SetValue((object)instance, (object)keyValuePair.Value);
            }
            return instance;
        }
               
        public static T Map<T>(this IConfiguration configuration) where T : class
        {
            T instance = Activator.CreateInstance<T>();
                       
            if (configuration == null)  return instance;
                       

            foreach (PropertyInfo property in instance.GetType().GetProperties())
            {
                string settingName = ConfigurationExtensions.GetAttributeName(property.GetCustomAttributes()) ?? property.Name;

                string value = configuration[settingName];
                if (!string.IsNullOrEmpty(value) && property != (PropertyInfo)null && property.CanWrite)
                {
                    property.SetValue((object)instance, (object)value);
                }
            }
            return instance;
        }

        private static string GetAttributeName(IEnumerable<Attribute> attributes)
        {
            try
            {
                IList<Attribute> source = attributes as IList<Attribute> ?? (IList<Attribute>)attributes.ToList<Attribute>();
                if (!source.Any<Attribute>())
                    return (string)null;
                return (source.FirstOrDefault<Attribute>() as SettingNameAttribute)?.Name;
            }
            catch (Exception ex)
            {
                Trace.WriteLine((object)ex);
                return (string)null;
            }
        }
    }
*/
