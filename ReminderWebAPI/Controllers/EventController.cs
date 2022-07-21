using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ReminderWebAPI.Models;
using System.Data;

namespace ReminderWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public EventController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet(Name = "GetEvent")]
        public JsonResult Get()
        {
            string query = @"select * from events";
            DataTable table = new DataTable();

            try
            {
                string sqlDataSource = _configuration.GetConnectionString("Default");
                MySqlDataReader sqlReader;
                using (MySqlConnection sqlCon = new MySqlConnection(sqlDataSource))
                {
                    sqlCon.Open();
                    using (MySqlCommand sqlCommand = new MySqlCommand(query, sqlCon))
                    {
                        sqlReader = sqlCommand.ExecuteReader();
                        table.Load(sqlReader);

                        sqlReader.Close();
                        sqlCon.Close();
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new JsonResult(table);
        }
        [HttpPost(Name = "PostEvent")]
        public JsonResult Post(Event ev)
        {
            string query = @"insert into events(eventDescription, eventDateTimer) values (@eventDescription, @eventDateTimer);";
            DataTable table = new DataTable();

            try
            {
                string sqlDataSource = _configuration.GetConnectionString("Default");
                MySqlDataReader sqlReader;
                using (MySqlConnection sqlCon = new MySqlConnection(sqlDataSource))
                {
                    sqlCon.Open();
                    using (MySqlCommand sqlCommand = new MySqlCommand(query, sqlCon))
                    {
                        sqlCommand.Parameters.AddWithValue("@eventDescription", ev.eventDescription);
                        sqlCommand.Parameters.AddWithValue("@eventDateTimer", ev.eventDateTimer);

                        sqlReader = sqlCommand.ExecuteReader();
                        table.Load(sqlReader);

                        sqlReader.Close();
                        sqlCon.Close();
                    }
                }//test
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new JsonResult("Added seccessfully");
        }
        [HttpPut(Name = "PutEvent")]
        public JsonResult Put(Event ev)
        {
            string query = @"update events set eventDescription=@eventDescription, eventDateTimer=@eventDateTimer where eventId=@eventId;";
            DataTable table = new DataTable();

            try
            {
                string sqlDataSource = _configuration.GetConnectionString("Default");
                MySqlDataReader sqlReader;
                using (MySqlConnection sqlCon = new MySqlConnection(sqlDataSource))
                {
                    sqlCon.Open();
                    using (MySqlCommand sqlCommand = new MySqlCommand(query, sqlCon))
                    {
                        sqlCommand.Parameters.AddWithValue("@eventId", ev.eventId);
                        sqlCommand.Parameters.AddWithValue("@eventDescription", ev.eventDescription);
                        sqlCommand.Parameters.AddWithValue("@eventDateTimer", ev.eventDateTimer);

                        sqlReader = sqlCommand.ExecuteReader();
                        table.Load(sqlReader);

                        sqlReader.Close();
                        sqlCon.Close();
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new JsonResult("Updated seccessfully");
        }
        [HttpDelete(Name = "DeleteEvent")]
        public JsonResult Delete (int id)
        {
            string query = @"delete from events where eventId=@eventId;";
            DataTable table = new DataTable();

            try
            {
                string sqlDataSource = _configuration.GetConnectionString("Default");
                MySqlDataReader sqlReader;
                using (MySqlConnection sqlCon = new MySqlConnection(sqlDataSource))
                {
                    sqlCon.Open();
                    using (MySqlCommand sqlCommand = new MySqlCommand(query, sqlCon))
                    {
                        sqlCommand.Parameters.AddWithValue("@eventId", id);

                        sqlReader = sqlCommand.ExecuteReader();
                        table.Load(sqlReader);

                        sqlReader.Close();
                        sqlCon.Close();
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new JsonResult("Deleted seccessfully");
        }
    }
}
