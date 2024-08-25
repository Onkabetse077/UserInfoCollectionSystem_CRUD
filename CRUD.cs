using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User_Info_Collection_System_CRUD_
{
    internal class CRUD
    {
        private string connection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\gosia\Documents\Projects\Visual Studio\C#\User Info Collection System(CRUD)\Database\UsersData.mdf"";Integrated Security=True;Connect Timeout=30";
        public int ID { set; get; }
        public string FullName { set; get; }
        public String Gender { set; get; }
        public String Contact { set; get; }
        public String Email { set; get; }
        public String DOB { set; get; }
      

        public List<CRUD> GetUserListData()
        {
            List<CRUD> listData = new List<CRUD>();

            using (SqlConnection connect = new SqlConnection(connection))
            {
                connect.Open();

                string query = "SELECT * FROM users";

                using (SqlCommand cmd = new SqlCommand(query,connect))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        CRUD crud = new CRUD();

                        crud.ID = (int)reader["id"];
                        crud.FullName = reader["full_name"].ToString();
                        crud.Gender = reader["gender"].ToString();  
                        crud.Contact = reader["contact"].ToString();
                        crud.Email = reader["email"].ToString();
                        crud.DOB = ((DateTime)reader["birth_date"]).ToString("dd/MMM/yyyy");
                        
                        listData.Add(crud);
                    }
                }
            }

            return listData;
        }
    }
}
