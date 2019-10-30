using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ImplicitMapper.Tests.Classes
{
    public static class Cast
    {
        public static void Clone(object source, object target)
        {
            var baseProp = source.GetType().GetProperties();
            foreach (var item in target.GetType().GetProperties())
            {
                var p = baseProp.FirstOrDefault(a => a.Name == item.Name);
                if (p != null)
                {
                    bool isNullable = (item.PropertyType.IsGenericType && item.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>));
                    if (item.PropertyType.IsValueType || item.PropertyType.IsEnum || item.PropertyType.Equals(typeof(string)) || isNullable)
                    {
                        object val = p.GetValue(source, null);

                        if (isNullable)
                        {
                            if (val != null && Nullable.GetUnderlyingType(item.PropertyType) != p.PropertyType)
                                item.SetValue(target, Convert.ChangeType(val, Nullable.GetUnderlyingType(item.PropertyType)));
                            else
                                item.SetValue(target, val);
                        }
                        else if (val != null)
                        {
                            if (item.PropertyType != p.PropertyType)
                                item.SetValue(target, Convert.ChangeType(val, item.PropertyType));
                            else
                                item.SetValue(target, val);
                        }
                        else if (item.PropertyType.Equals(typeof(string)))
                            item.SetValue(target, val);
                    }
                }
            }
        }
        public static T DeepClone<T>(T obj)
        {
            using (var ms = new System.IO.MemoryStream())
            {
                var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;
                var result = (T)formatter.Deserialize(ms);
                ms.Close();
                return result;
            }
        }

        public static IEnumerable<M> ToViewModel<T, M>(this IEnumerable<T> data)
        {
            var viewModel = typeof(M).GetMethods(BindingFlags.Static | BindingFlags.Public)
               .Where(m => m.Name == "op_Explicit" || m.Name == "op_Implicit")
               .Where(m => m.ReturnType == typeof(M))
               .Where(m => m.GetParameters().Length == 1 && m.GetParameters()[0].ParameterType == typeof(T))
               .FirstOrDefault();

            var dataList = new List<M>();

            foreach (var item in data)
                dataList.Add((M)viewModel.Invoke(null, new object[] { item }));

            return dataList;
        }
    }
}