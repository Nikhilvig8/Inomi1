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
    public class CalendarCon
    {
        public static void InsertEventDetails(string dataEntry, string UsertypeId)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("InsertEventDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DataEntry", dataEntry);
            cmd.Parameters.AddWithValue("@UsertypeId", UsertypeId);
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

        public static void InsertDoToListEventDetails(string dataEntry, string UsertypeId)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("InsertToDoListEventDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DataEntry", dataEntry);
            cmd.Parameters.AddWithValue("@UsertypeId", UsertypeId);
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

        public static void InsertCalendarEventDetails(string dataEntry, string UsertypeId)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("InsertCalendarEventDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DataEntry", dataEntry);
            cmd.Parameters.AddWithValue("@UsertypeId", UsertypeId);
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
        public static DataTable GetEvnetDetails(string UsertypeId, string UserType)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("GetEventDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UsertypeId", UsertypeId);
            cmd.Parameters.AddWithValue("@UserType", UserType);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

        public static DataTable GetToDoListEvnetDetails(string UsertypeId, string UserType)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("GetToDoListEventDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UsertypeId", UsertypeId);
            cmd.Parameters.AddWithValue("@UserType", UserType);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

        public static DataTable GetDashboardEvnetDetails(string UsertypeId, string UserType)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("GetDashboardEventDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UsertypeId", UsertypeId);
            cmd.Parameters.AddWithValue("@UserType", UserType);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

        public static DataTable GetCalendarEvnetDetails(string UsertypeId, string UserType)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            SqlCommand cmd = new SqlCommand("GetCalendarEventDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UsertypeId", UsertypeId);
            cmd.Parameters.AddWithValue("@UserType", UserType);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }
    }
}
