using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace EldosFileLib.Extentions
{
    public static class ObjectExtensions
    {
        public static bool IsNull(this object value)
        {
            return value == null;
        }

        public static bool IsNotNull(this object value)
        {
            return !value.IsNull();
        }

        public static int IntTryParse(this object value)
        {
            return value.IsNotNull<object, int>(p => { return p.ToString().IntTryParse(); });
        }

        public static DateTime DateTimeTryParse(this object value)
        {
            return value.IsNotNull<object, DateTime>(p => { return p.ToString().DateTimeTryParse(); });
        }

        public static decimal DecimalTryParse(this object value)
        {
            return value.IsNotNull<object, decimal>(p => { return p.ToString().DecimalTryParse(); });
        }

        public static bool BoolTryParse(this object value)
        {
            return value.IsNotNull<object, bool>(p => { return p.ToString().BoolTryParse(); });
        }

        public static bool DameBoolToBool(this object value)
        {
            return value.IsNotNull<object, bool>(p =>
            {
                bool result = false;

                if (p != null)
                    result = "T".Equals(p.ToString(), StringComparison.InvariantCultureIgnoreCase) || "Y".Equals(p.ToString(), StringComparison.InvariantCultureIgnoreCase) || "1".Equals(p.ToString(), StringComparison.InvariantCultureIgnoreCase) || "true".Equals(p.ToString(), StringComparison.InvariantCultureIgnoreCase) ? true : false;
                return result.ToString().BoolTryParse();

            });
        }

        public static string ToObjectItemKey(this object order, object orderItem)
        {
            return order.ToString().ToObjectItemKey(orderItem.ToString());
        }

        public static TResult IsNotNull<TInput, TResult>(this object value, Func<TInput, TResult> function)
        {
            TResult result = value != null ? function((TInput)value) : default(TResult);
            return result;
        }

        public static T InsureObjectNotNull<T>(this object value) where T : new()
        {
            T result = value.IsNull() ? new T() : (T)value;
            return result;
        }

        public static TEntity Default<TEntity>(this TEntity value)
        {
            return default(TEntity);
        }


        public static bool IsUpdatedData<T>(this T orginalValue, T newValue, bool isNullUpdatedData)
        {
            var isUpdatedData = false;

            if (newValue.IsNotNull() || (newValue.IsNull() && isNullUpdatedData))
            {
                isUpdatedData = orginalValue.IsNotNull() ? orginalValue.Equals(newValue) :
                    isNullUpdatedData.IsNotNull() ? newValue.Equals(orginalValue) : false;
            }

            return isUpdatedData;
        }

        public static T As<T>(this object value)
        {
            T result;
            try
            {
                result = (T)value;
            }
            catch
            {
                result = default(T);
            }

            return result;
        }

        public static string ToTrimString(this object value)
        {
            var result = value.IsNull() ? string.Empty : value.ToString().Trim();
            return result;
        }
        public static IEnumerable<T> ToIEnumerable<T>(this T obj)
        {
            var result = new List<T>();

            if (obj.IsNotNull())
                result.Add(obj);

            return result;
        }
    }
}
