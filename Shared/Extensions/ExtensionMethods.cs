using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Shared.Extensions
{
    public static class HelperExtensions
    {
        private static Random rng = new Random();

        public static T ValueOrDefault<T>(this object value)
        {
            if (value == null || value.ToString() == "")
            {
                return default;
            }

            return (T)Convert.ChangeType(value, typeof(T));
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static string ToDescription(this Enum val)
        {
            var type = val.GetType();

            var memberInfo = type.GetMember(val.ToString());

            var attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes == null || attributes.Length != 1)
            {
                //如果没有定义描述，就把当前枚举值的对应名称返回
                return val.ToString();
            }
            

            return (attributes.Single() as DescriptionAttribute).Description;
        }
    }
}
