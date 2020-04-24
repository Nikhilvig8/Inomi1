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
    public class AssignTaskCon
    {
        public static void InsertAssignTask(string dataEntry, string Id)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("InsertAssignTask", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DataEntry", dataEntry);
            cmd.Parameters.AddWithValue("@Id", Id);
            cmd.Parameters.AddWithValue("@CreatedBy", "Admin");
            if (con.State == ConnectionState.Open) { }
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

        public static DataTable AssignTaskDetails(string Str, string UsertypeId)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("GetAssignTask", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Str", Str);
            cmd.Parameters.AddWithValue("@UsertypeId", UsertypeId);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

        public static DataTable EditAssignTaskDetails(string id)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("GetAssignTaskById", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

        public static void DeleteAssignTaskDetails(string id)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("DeleteAssignTaskDetails", con);
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
