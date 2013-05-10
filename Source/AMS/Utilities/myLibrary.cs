using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
using System.Reflection;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace myLibrary
{
    public class myGuid
    {
        #region Methods

        public static Guid StringToGuid(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                value = DateTime.Now.Millisecond.ToString();
            }
            MD5 md5Hasher = MD5.Create();
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(value));
            return new Guid(data);
        }

        public static Guid StringToGuid()
        {
            return StringToGuid(null);
        }

        #endregion
    }

    public class myCryptography
    {
        private static RandomNumberGenerator passwordGenerator = RNGCryptoServiceProvider.Create();

        #region Methods

        public static string GeneratePassword()
        {
            byte[] secureBits = new byte[20];
            passwordGenerator.GetBytes(secureBits);
            return Convert.ToBase64String(secureBits);
        }

        #endregion
    }

    public class myDataTable
    {
        #region Methods

        public static DataTable Create<T>(IEnumerable<T> iEnumerable)
        {
            try
            {
                //If the input iEnumerable is null, return null
                if (iEnumerable == null)
                    return null;
                //If the input iEnumerable has no record, return null
                if (iEnumerable.Count() < 1)
                    return null;
                //Otherwise, do things to convert the input iEnumerable to dtResult
                DataTable dtResult = new DataTable();
                //Build column structures for dtResult
                PropertyInfo[] properties = null;
                properties = iEnumerable.ElementAt(0).GetType().GetProperties();
                foreach (var propertyInfo in properties)
                {
                    Type columnType = propertyInfo.PropertyType;
                    if (columnType.IsGenericType && columnType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        columnType = columnType.GetGenericArguments()[0];
                    }
                    dtResult.Columns.Add(new DataColumn(propertyInfo.Name, columnType));
                }
                //Fill data to dtResult
                foreach (var record in iEnumerable)
                {
                    DataRow dataRow = dtResult.NewRow();
                    foreach (var propertyInfo in properties)
                    {
                        if (propertyInfo.GetValue(record, null) == null)
                            dataRow[propertyInfo.Name] = DBNull.Value;
                        else
                            dataRow[propertyInfo.Name] = propertyInfo.GetValue(record, null);
                    }
                    dtResult.Rows.Add(dataRow);
                }
                //Return dtResult
                return dtResult;
            }
            catch
            {
                return null;
            }
        }

        public static DataTable Create<T>(List<T> list)
        {
            try
            {
                //If the input list is null, return null
                if (list == null)
                    return null;
                //If the input list has no item, return null
                if (list.Count < 1)
                    return null;
                //Otherwise, do things to convert the input list to dtResult
                DataTable dtResult = new DataTable();
                //Build column structures for dtResult
                PropertyInfo[] properties = null;
                properties = list.ElementAt(0).GetType().GetProperties();
                foreach (var propertyInfo in properties)
                {
                    Type columnType = propertyInfo.PropertyType;
                    if (columnType.IsGenericType && columnType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        columnType = columnType.GetGenericArguments()[0];
                    }
                    dtResult.Columns.Add(new DataColumn(propertyInfo.Name, columnType));
                }
                //Fill data to dtResult
                foreach (var item in list)
                {
                    DataRow dataRow = dtResult.NewRow();
                    foreach (var propertyInfo in properties)
                    {
                        if (propertyInfo.GetValue(item, null) == null)
                            dataRow[propertyInfo.Name] = DBNull.Value;
                        else
                            dataRow[propertyInfo.Name] = propertyInfo.GetValue(item, null);
                    }
                    dtResult.Rows.Add(dataRow);
                }
                //Return dtResult
                return dtResult;
            }
            catch
            {
                return null;
            }
        }

        public static DataTable Create<T>(T[] array)
        {
            try
            {
                //If the input array is null, return null
                if (array == null)
                    return null;
                //If the input array has no item, return null
                if (array.Length < 1)
                    return null;
                //Otherwise, do things to convert the input array to dtResult
                DataTable dtResult = new DataTable();
                //Build column structures for dtResult
                PropertyInfo[] properties = null;
                properties = array.ElementAt(0).GetType().GetProperties();
                foreach (var propertyInfo in properties)
                {
                    Type columnType = propertyInfo.PropertyType;
                    if (columnType.IsGenericType && columnType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        columnType = columnType.GetGenericArguments()[0];
                    }
                    dtResult.Columns.Add(new DataColumn(propertyInfo.Name, columnType));
                }
                //Fill data to dtResult
                foreach (var item in array)
                {
                    DataRow dataRow = dtResult.NewRow();
                    foreach (var propertyInfo in properties)
                    {
                        if (propertyInfo.GetValue(item, null) == null)
                            dataRow[propertyInfo.Name] = DBNull.Value;
                        else
                            dataRow[propertyInfo.Name] = propertyInfo.GetValue(item, null);
                    }
                    dtResult.Rows.Add(dataRow);
                }
                //Return dtResult
                return dtResult;
            }
            catch
            {
                return null;
            }
        }

        #endregion
    }

    public class myString
    {
        #region Methods

        public static string RemoveMarks(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return "";
            }
            Regex v_reg_regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string v_str_FormD = source.Normalize(NormalizationForm.FormD);
            return v_reg_regex.Replace(v_str_FormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        public static string RemoveHtmlTags(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return "";
            }
            return Regex.Replace(source, "<.*?>", string.Empty);
        }

        public static string TrimAll(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return "";
            }
            return Regex.Replace(source.Trim(), "[ ]+", " ");
        }

        #endregion
    }
}