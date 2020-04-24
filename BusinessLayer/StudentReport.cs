using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Models;
using DataLayer;
using System.Xml;

namespace BusinessLayer
{
   public class StudentReport
    {
        public static DataTable GetStudentReport(string Str, string UsertypeId)
        {
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            //SqlCommand cmd = new SqlCommand("GetReport_Student", con);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@Str", Str);
            //cmd.Parameters.AddWithValue("@UsertypeId", UsertypeId);
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //sda.Fill(dt);
            //return dt;


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                cmd = new SqlCommand("GetReport_Student", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Str", Str);
                cmd.Parameters.AddWithValue("@UsertypeId", UsertypeId);
                da.SelectCommand = cmd;
                da.Fill(dt);
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return dt;
        }
    }
}
