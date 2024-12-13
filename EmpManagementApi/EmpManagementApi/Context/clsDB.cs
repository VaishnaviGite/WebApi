using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EmpManagementApi.Context
{
    public class clsDB
    {
        SqlConnection con;
        public IConfiguration _configuration { get; }

        public clsDB()
        {
            //strConn = Config.ConStr;
            //con = new SqlConnection(strConn);

            con = new SqlConnection(Config.ConStr);

        }
        public clsDB(string conStr)
        {
            con = new SqlConnection(conStr);
        }
        //public clsDB(string conStr, string token)
        //{
        //    token = Config.TokenFromHeader;
        //    con = new SqlConnection(conStr);
        //}

        public DataTable ExecuteDataTable(string command)
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = command;
                if (con.State == ConnectionState.Broken || con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                da.SelectCommand = cmd;
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                da.Dispose();
                dt.Dispose();
            }
        }


        public DataTable ExecuteDataTable1(string storedProcedure)
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = storedProcedure;
                //cmd.Parameters.Clear();
                if (con.State == ConnectionState.Broken || con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                da.SelectCommand = cmd;
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                da.Dispose();
                dt.Dispose();
            }
        }


        public DataTable ExecuteDataTable(string storedProcedure, SqlParameter[] p)
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = storedProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddRange(p);
                if (con.State == ConnectionState.Broken || con.State == ConnectionState.Closed)
                {
                    con.Open();
                }


                da.SelectCommand = cmd;
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                da.Dispose();
                dt.Dispose();
            }
        }
        public DataSet ExecuteDataSet(string command)
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = command;

                if (con.State == ConnectionState.Broken || con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                da.SelectCommand = cmd;
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                da.Dispose();
                ds.Dispose();
            }
        }

        public DataSet ExecuteDataSet2(string command)
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = command;

                if (con.State == ConnectionState.Broken || con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                da.SelectCommand = cmd;
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                da.Dispose();
                ds.Dispose();
            }
        }

        public DataSet ExecuteDataSet(string storedProcedure, SqlParameter[] p)
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = storedProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddRange(p);
                if (con.State == ConnectionState.Broken || con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                da.SelectCommand = cmd;
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                cmd.Dispose();
                da.Dispose();
                ds.Dispose();
            }
        }

        public DataSet ExecuteDataSet1(string storedProcedure)
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = storedProcedure;
                cmd.Parameters.Clear();
                if (con.State == ConnectionState.Broken || con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                da.SelectCommand = cmd;
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                cmd.Dispose();
                da.Dispose();
                ds.Dispose();
            }
        }

        public DataSet ExecuteDataSetWithourParam(string storedProcedure)
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = storedProcedure;
                if (con.State == ConnectionState.Broken || con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                da.SelectCommand = cmd;
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                cmd.Dispose();
                da.Dispose();
                ds.Dispose();
            }
        }

        public SqlDataReader ExecuteReader(string storedProcedure, string p)
        {
            SqlCommand cmd = new SqlCommand();
            //SqlDataAdapter da = new SqlDataAdapter();
            //DataSet ds = new DataSet();
            SqlDataReader sdr;
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = storedProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@PrefixText", SqlDbType.NVarChar, 50);
                cmd.Parameters["@PrefixText"].Value = p;
                if (con.State == ConnectionState.Broken || con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                sdr = cmd.ExecuteReader();
                return sdr;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                cmd.Dispose();
                //da.Dispose();
                //ds.Dispose();
            }
        }

        public string ExecuteNoneQuery(string storedProcedure, SqlParameter[] p)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = storedProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddRange(p);
                if (con.State == ConnectionState.Broken || con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string rowsAffected = Convert.ToString(cmd.ExecuteScalar());
                return rowsAffected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
            }
        }
        public string ExecuteNoneQuery(string storedProcedure, SqlParameter[] p, string pname, ref int retVal)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = storedProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddRange(p);
                if (con.State == ConnectionState.Broken || con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string rowsAffected = Convert.ToString(cmd.ExecuteScalar());
                retVal = Convert.ToInt32(cmd.Parameters[pname].Value.ToString());
                return rowsAffected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
            }
        }


        public int ExecuteNoneQuerys(string storedProcedure, SqlParameter[] p)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = storedProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddRange(p);
                if (con.State == ConnectionState.Broken || con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                int i = cmd.ExecuteNonQuery();
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
            }
        }
        private void CloseConn()
        {
            try
            {
                con.Close();
            }
            catch
            {
            }
        }
    }

    public static class Config
    {
        public static string ConStr = "";
        public static string DownloadUrl = "";
        public static string ModelImgPath = "";
        public static string Svcurl = "";
        public static string Key = "";
        public static string Issuer = "";
        public static string ConStrFromHeader = "";
        public static string TokenFromHeader = "";
        public static string newConn = "";
        public static int loggedinuserId = 0;
        public static string WebRootPath = "";
        public static string mail = "";
        public static string password = "";
        public static string host = "";
        public static int port = 0;
        public static bool usessl = false;
        public static string SMSapiUrl = "";
    }
}

