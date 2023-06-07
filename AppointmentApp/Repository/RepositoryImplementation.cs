using AppointmentApp.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Management;

namespace AppointmentApp.Repository
{
    public class RepositoryImplementation : IRepository
    {
        private string connString = ConfigurationManager.ConnectionStrings["Connectionstring"].ConnectionString;
        private SqlConnection conn = null;
        private string Get_Query = "Select * from Appointment order by date ASC";
        

        public void AddAsync(Appointment appointment)
        {
            try
            {

                conn  = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand();  
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = conn;
                string dt = appointment.Date.ToShortDateString();
                cmd.CommandText = $"INSERT INTO Appointment (subject, Date, Meetingwith, Reason, Vanu, isVip) values ('{appointment.Subject}','{dt}', '{appointment.Meetingwith}', '{appointment.Reason}', '{appointment.vanue}', '{appointment.isVip}')";
                conn.Open();

                cmd.ExecuteNonQuery();
                conn.Close();

            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            
        }

       

        public List<Appointment> GetAllAsync()
        {
            
            List<Appointment> list = new List<Appointment>();
            try
            {
                conn = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand(Get_Query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Appointment
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Subject = Convert.ToString(reader["Subject"]),
                        Date = Convert.ToDateTime(reader["date"]),
                        Meetingwith = Convert.ToString(reader["Meetingwith"]),
                        Reason = Convert.ToString(reader["Reason"]),
                        vanue = Convert.ToString(reader["Vanu"]),
                        isVip = Convert.ToBoolean(reader["isvip"])

                    });
                }
                return list;
            } catch(Exception ex) { Console.WriteLine(ex.Message);}
            finally
            {
                conn.Close();
            }
            return list;
            
            
        }

        public Appointment GetAsync(int id)
        {
            string query = $"Select * from Appointment where id={id}";
            Appointment model = new Appointment();
            try
            {
                conn = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open ();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    model.Id = Convert.ToInt32(reader["id"]);
                    model.Subject = Convert.ToString(reader["Subject"]);
                    model.Date = Convert.ToDateTime(reader["date"]);
                    model.Meetingwith = Convert.ToString(reader["Meetingwith"]);
                    model.Reason = Convert.ToString(reader["Reason"]);
                    model.vanue = Convert.ToString(reader["Vanu"]);
                    model.isVip = Convert.ToBoolean(reader["isvip"]);
                }

            }catch(Exception ex) { Console.WriteLine(ex.Message); }
            finally
            {
                conn.Close();
            }


            return model;

        }

        public bool UpdateAsync(Appointment appointment)
        {
            string query = $"Update Appointment Set Subject= '{appointment.Subject}', Date='{appointment.Date.ToShortDateString()}', Meetingwith='{appointment.Meetingwith}', Reason='{appointment.Reason}', Vanu='{appointment.vanue}', isVip='{appointment.isVip}' where id={appointment.Id}";
            try
            {
                conn = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = conn;
                cmd.CommandText = query;
                conn.Open ();
                int result =cmd.ExecuteNonQuery();
                if (result == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { 
                conn.Close();
            }
            return false;
        }

        public bool DeleteAsync(int id)
        {
            try
            {
                conn = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = conn;
                cmd.CommandText = $"Delete from Appointment where id={id}";
                conn.Open ();
                int result =cmd.ExecuteNonQuery();
                if (result == 0)
                    return false;
                else
                    return true;
            }catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { conn.Close(); }

            return false;
        }
    }
}