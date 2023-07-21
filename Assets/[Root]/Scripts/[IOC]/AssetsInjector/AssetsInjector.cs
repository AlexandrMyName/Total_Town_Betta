using System;
using UnityEngine;
using System.Reflection;

public static class AssetsInjector 
{
    public static T Inject<T>(this AssetsContext context, T target)
    {
        var targetType = target.GetType();
        var fieldsInfo = targetType.GetFields(BindingFlags.Instance | BindingFlags.Public| BindingFlags.NonPublic);

        Type type = target.GetType();
        while (type != null)
        {
            for (int i = 0; i < fieldsInfo.Length; i++)
            {
                var field = fieldsInfo[i];

                var attribute = field.GetCustomAttribute(typeof(InjectAssetAttribute)) as InjectAssetAttribute;
                if (attribute == null) continue;
               
                var objectToInject = context.GetObjectOfType(field.FieldType, attribute.NameAsset);
                field.SetValue(target, objectToInject);
            }
            type = type.BaseType;
        }
        return target;
    }
}
