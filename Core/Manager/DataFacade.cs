/* documentation
 *001
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using System.Configuration;

namespace Core.Manager
{
    public class DataFacade
    {

        public static DataTable DTSQLCommand(string sqlQuery,List<SqlParameter> listParam)
        {
           
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MainConnectionString"].ConnectionString;
            string commandText = sqlQuery;
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    

                    command.Parameters.AddRange(listParam.ToArray());

                    connection.Open();

                    dt.Load(command.ExecuteReader());

                    command.Parameters.Clear();

                }

            }
            return dt;
        }

        public static string DTSQLVoidCommand(string sqlQuery, List<SqlParameter> listParam)
        {

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MainConnectionString"].ConnectionString;
            string commandText = sqlQuery;
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    try
                    {
                    command.Parameters.AddRange(listParam.ToArray());                  
                        connection.Open();
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                        return "1";
                    }
                    catch (SqlException ex)
                    {
                        return "0";
                    }
                    
                }               
            }

        }

        public static string DTSQLListInsert<T>(List<T> list, string tablename)
        {

            DataTable dt = new DataTable();
            dt = Common.HelperFacade.ConvertToDataTable(list);

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MainConnectionString"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("", conn))
                {
                    try
                    {
                        conn.Open();

                        using (SqlBulkCopy bulkcopy = new SqlBulkCopy(conn))
                        {
                            bulkcopy.BulkCopyTimeout = 660;
                            bulkcopy.DestinationTableName = tablename;
                            bulkcopy.WriteToServer(dt);
                            bulkcopy.Close();
                        }
                        return "1";

                    }
                    catch(SqlException ex)
                    {
                        //return ex.ToString();
                        return "0";
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
              

        }


        public static string DTSQLListUpdate<T>(List<T> list, string TableName, List<string> updateCollumn, List<string> onCollumn)
        {
            DataTable dt = new DataTable("#TmpTable");
            //clsBulkOperation blk = new clsBulkOperation();
            dt = Common.HelperFacade.ConvertToDataTable(list);

            //ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MainConnectionString"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("", conn))
                {
                    try
                    {
                        conn.Open();

                        //Creating temp table on database
                        command.CommandText = "SELECT TOP 1 * INTO #TmpTable FROM " + TableName + ";DELETE FROM #TmpTable";
                        command.ExecuteNonQuery();

                        //Bulk insert into temp table
                        using (SqlBulkCopy bulkcopy = new SqlBulkCopy(conn))
                        {
                            bulkcopy.BulkCopyTimeout = 660;
                            bulkcopy.DestinationTableName = "#TmpTable";
                            bulkcopy.WriteToServer(dt);
                            bulkcopy.Close();
                        }

                        // Updating destination table, and dropping temp table
                        command.CommandTimeout = 300;
                        command.CommandText = "UPDATE " + TableName + " SET " + Common.HelperFacade.UpdateCollumnSetBuilder(updateCollumn) + " FROM " + TableName + " a INNER JOIN #TmpTable Temp ON " + Common.HelperFacade.OnBySetBuilder(onCollumn) + "; DROP TABLE #TmpTable;";
                        command.ExecuteNonQuery();

                        return "1";
                    }
                    catch (SqlException ex)
                    {
                        // Handle exception properly
                        //return ex.ToString();
                        return "0";
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }


        }


        public static void RunStoredProcedure(string sp)
        {

            //string conString = @"Data Source=JOSHUA-PC\SQLEXPRESS;Initial Catalog=sfa_courierNew;User ID=sa;Password=cuki123";
            string conString = System.Configuration.ConfigurationManager.ConnectionStrings["sp"].ConnectionString;
            SqlConnection conn = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand();
            Int32 rows;

            cmd.CommandText = sp;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
            }




        }


        public static string RunStoredProcedureWithParameter(string storedProcedure, List<SqlParameter> listParam)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MainConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(storedProcedure, con))
                {
                    cmd.Parameters.AddRange(listParam.ToArray());
                    cmd.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();

                        cmd.Parameters.Clear();

                        return "SQL Command Success";
                    }
                    catch (SqlException ex)
                    {
                        return ex.ToString();
                    }
                }
            }
        }

    }
}
