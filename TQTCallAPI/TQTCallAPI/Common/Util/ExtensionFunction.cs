using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Reflection;
using System.Web.Mvc;

namespace SSC.StudyRecords.Common.Util
{
    public static class ExtensionFunction
    {
        /// <summary>
        /// Check null for a object
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public static bool IsNull(this object obj)
        {
            if (obj == null || DBNull.Value == obj)
                return true;
            return false;
        }
        public static bool IsNotNull(this object obj)
        {
            return !IsNull(obj);
        }
        public static bool StringIsNullEmptyWhiteSpace(this string obj)
        {
            if (obj == null || string.IsNullOrEmpty(obj) || (string.IsNullOrWhiteSpace(obj)))
                return true;
            return false;
        }
        public static string ConvertToString(this object value)
        {
            if (value == null)
                return string.Empty;
            return value.ToString();
        }
        public static bool IsEmpty(this Guid guid)
        {
            if (guid.IsNull() || guid.Equals(Guid.Empty))
                return true;
            return false;
        }
        public static bool IsNotEmpty(this Guid guid)
        {
            return !IsEmpty(guid);
        }
        public static bool IsNullOrEmpty(this Guid? guid)
        {
            if (guid.IsNull() || guid.Equals(Guid.Empty))
                return true;
            return false;
        }
        public static bool IsNotNullAndEmpty(this Guid? guid)
        {
            return !IsNullOrEmpty(guid);
        }
        public static string HashSSCPassword(this string value)
        {
            return null;
        }
        public static decimal CDecimal(this object value, int defaultValue = 0)
        {
            if (value.IsNull())
                return defaultValue;
            try
            {
                return Convert.ToDecimal(value);
            }
            catch
            { return defaultValue; }
        }
        public static int CInt(this object value, int defaultValue = 0)
        {
            if (value.IsNull())
                return defaultValue;
            try
            {
                return Convert.ToInt32(value);
            }
            catch
            { return defaultValue; }
        }

		public static Guid? CGuid(this string value)
		{
			try
			{
				if (value.StringIsNullEmptyWhiteSpace())
					return null;
				return Guid.Parse(value);
			}
			catch
			{ return null; }
		}
        public static string FormatDecimal(this decimal? value, string format = "{0:#0.##}")
        {
            try
            {
                if (value == null)
                    return string.Empty;
                return string.Format(format, value.CDecimal());
            }
            catch
            { return string.Empty; }
        }


        public static SelectList ToSelectList<TEnum>(this TEnum enumObj, int minValue = 0)
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            var values = from TEnum e in Enum.GetValues(typeof(TEnum)) where e.ToInt32(null) >= minValue
                         select new { Id = e, Name = e.GetEnumDescription() };
            return new SelectList(values, "Id", "Name", enumObj);
        }

        public static string GetEnumDescription<TEnum>(this TEnum en)
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }
            return en.ToString();
        }

        /// <summary>
        /// Adds sub-totals to a list of items, along with a grand total for the whole list.
        /// </summary>
        /// <param name="elements">Group and/or sort this yourself before calling WithRollup.</param>
        /// <param name="primaryKeyOfElement">Given a TElement, return the property that you want sub-totals for.</param>
        /// <param name="calculateSubTotalElement">Given a group of elements, return a TElement that represents the sub-total.</param>
        /// <param name="grandTotalElement">A TElement that represents the grand total.</param>
        //public static List<TElement> WithRollup<TElement, TKey>(this IEnumerable<TElement> elements,
        //    Func<TElement, List<TKey>> primaryKeysOfElement,
        //    Func<IGrouping<TKey, TElement>, TElement> calculateSubTotalElement,
        //    TElement grandTotalElement)
        //{
        //    // Create a new list the items, subtotals, and the grand total.
        //    List<TElement> results = new List<TElement>();
        //    var lookup = elements.ToLookup(primaryKeysOfElement);
        //    foreach (var group in lookup)
        //    {
        //        // Add items in the current group
        //        results.AddRange(group);
        //        // Add subTotal for current group
        //        results.Add(calculateSubTotalElement(group));
        //    }
        //    // Add grand total
        //    results.Add(grandTotalElement);

        //    return results;
        //}

        /// <summary>
        /// Adds sub-totals to a list of items, along with a grand total for the whole list.
        /// </summary>
        /// <param name="elements">Group and/or sort this yourself before calling WithRollup.</param>
        /// <param name="primaryKeyOfElement">Given a TElement, return the property that you want sub-totals for.</param>
        /// <param name="calculateSubTotalElement">Given a group of elements, return a TElement that represents the sub-total.</param>
        /// <param name="grandTotalElement">A TElement that represents the grand total.</param>
        public static List<TElement> WithRollup<TElement, TKey>(this IEnumerable<TElement> elements,
            Func<TElement, TKey> primaryKeyOfElement,
            Func<IGrouping<TKey, TElement>, TElement> calculateSubTotalElement,
            TElement grandTotalElement)
        {
            // Create a new list the items, subtotals, and the grand total.
            List<TElement> results = new List<TElement>();
            var lookup = elements.ToLookup(primaryKeyOfElement);
            foreach (var group in lookup)
            {
                // Add items in the current group
                results.AddRange(group);
                // Add subTotal for current group
                results.Add(calculateSubTotalElement(group));
            }
            // Add grand total
            results.Add(grandTotalElement);

            return results;
        }


        public static T GetPropertyValue<T>(this object obj, string PropertyName)
        {
            try
            {
                var propertyinfo = obj.GetType().GetProperties().FirstOrDefault(proc => proc.Name.Equals(PropertyName, StringComparison.OrdinalIgnoreCase));
                if (!propertyinfo.IsNull() && propertyinfo.CanWrite)
                {
                    return (T)propertyinfo.GetValue(obj);
                }
            }
            catch (Exception ex)
            {

            }
            return default(T);
        }

    }
}