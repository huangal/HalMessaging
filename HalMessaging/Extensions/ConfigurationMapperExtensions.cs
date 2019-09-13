using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HalMessaging.Attributes;

namespace HalMessaging.Extensions
{


    /// <summary>
    /// 
    /// </summary>
    //public class ConfigurationMapperExtensions
    //{
    //    public T Map<T>(List<Setting> settings)
    //    {
    //        if (settings == null) return default(T);
    //        var instance = Activator.CreateInstance<T>();
    //        if (settings.Count <= 0) return instance;

    //        foreach (var property in instance.GetType().GetProperties())
    //        {
    //            var attributeName = GetAttributeName(property.CustomAttributes) ?? property.Name;
    //            var setting = settings.FirstOrDefault(x => x.Name.Equals(attributeName));
    //            if (setting != null && property != null && property.CanWrite)
    //                property.SetValue(instance, setting.Value);
    //        }
    //        return instance;
    //    }

    //    private string GetAttributeName(IEnumerable<Attribute> attributes)
    //    {
    //        var customAttributes = attributes as IList<Attribute> ?? attributes.ToList();
    //        if (!customAttributes.Any()) return null;
    //        var attribute = customAttributes.FirstOrDefault();
    //        return (attribute as SettingName)?.Name;
    //    }


    //    private string GetAttributeName(IEnumerable<CustomAttributeData> attributes)
    //    {
    //        var customAttributes = attributes as IList<CustomAttributeData> ?? attributes.ToList();
    //        if (!customAttributes.Any()) return null;
    //        var argument = customAttributes.FirstOrDefault()?.ConstructorArguments.FirstOrDefault();
    //        return argument?.Value.ToString();
    //    }
    //}
}
