using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel;


namespace Core.Common
{
    public class HelperFacade
    {
        public static DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        public static string UpdateCollumnSetBuilder(List<string> collumnName)
        {
            int countList = collumnName.Count;
            StringBuilder sb = new StringBuilder();
            int count = 1;
            foreach (string col in collumnName)
            {
                if (countList == count)
                {
                    sb.Append("[" + col + "] = Temp.[" + col + "]");
                }
                else
                {
                    sb.Append("[" + col + "] = Temp.[" + col + "],");

                    count++;
                }
            }



            return sb.ToString();


        }

        public static string OnBySetBuilder(List<string> collumnName)
        {
            int countList = collumnName.Count;
            StringBuilder sb = new StringBuilder();
            int count = 1;
            foreach (string col in collumnName)
            {
                if (countList == count)
                {
                    sb.Append("a.[" + col + "] = Temp.[" + col + "]");
                }
                else
                {
                    sb.Append("a.[" + col + "] = Temp.[" + col + "] AND ");

                    count++;
                }
            }

            return sb.ToString();


        }
    }
}
