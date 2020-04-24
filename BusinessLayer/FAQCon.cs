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
    public class FAQCon
    {
        public static void InsertFAQData(string dataEntry, string Id)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("InsertFAQDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DataEntry", dataEntry);
            cmd.Parameters.AddWithValue("@Id", Id);
            cmd.Parameters.AddWithValue("@CreatedBy", "Admin");
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            int i = cmd.ExecuteNonQuery();
            if (con.State != ConnectionState.Closed)
            {
                con.Close();
            }
        }

        public static DataTable GetFAQDetails()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("GetFAQDetail", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

        public static DataTable EditFAQDetails(string id)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("GetFAQById", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

        public static void DeleteFAQDetails(string id)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("DeleteFAQDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@CreatedBy", "Admin");
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            int i = cmd.ExecuteNonQuery();
            if (con.State != ConnectionState.Closed)
            {
                con.Close();
            }
        }
    }
}
