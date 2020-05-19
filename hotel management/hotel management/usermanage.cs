using MySql.Data.MySqlClient;
using MySql.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace hotel_management
{
    class usermanage
    {
        connect conn = new connect();

        public bool insertUser(string username, string password, string name, string tel, string mail)
        {
            MySqlCommand command = new MySqlCommand();
            String insertQuery = " INSERT INTO `user`( `username`, `password`, `name`, `tel`, `mail`) VALUES (@usn,@pass,@n,@t,@m)";
            command.CommandText = insertQuery;
            command.Connection = conn.GetConnection();

            command.Parameters.Add("@usn", MySqlDbType.VarChar).Value = username;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = password;
            command.Parameters.Add("@n", MySqlDbType.VarChar).Value = name;
            command.Parameters.Add("@t", MySqlDbType.VarChar).Value = tel;
            command.Parameters.Add("@m", MySqlDbType.VarChar).Value = mail;

            conn.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                conn.closeConnection();
                return true;
            }
            else
            {
                conn.closeConnection();
                return false;
            }
        }

        public DataTable getUser()
        {
            MySqlCommand command = new MySqlCommand(" SELECT * FROM `user`", conn.GetConnection());
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table = new DataTable();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;

        }
        public bool editUser(int id, string username, string password, string name, string tel, string mail)
        {
            MySqlCommand command = new MySqlCommand();
            String editQuery = "UPDATE `user` SET `username`=@usn,`password`=@pass,`name`=@n,`tel`=@t,`mail`=@m WHERE `ID` = @id ";
            command.CommandText = editQuery;
            command.Connection = conn.GetConnection();

            command.Parameters.Add("@id", MySqlDbType.VarChar).Value = id;
            command.Parameters.Add("@usn", MySqlDbType.VarChar).Value = username;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = password;
            command.Parameters.Add("@n", MySqlDbType.VarChar).Value = name;
            command.Parameters.Add("@t", MySqlDbType.VarChar).Value = tel;
            command.Parameters.Add("@m", MySqlDbType.VarChar).Value = mail;

            conn.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                conn.closeConnection();
                return true;
            }
            else
            {
                conn.closeConnection();
                return false;
            }
        }
        public bool deleteUser(int id)
        {
            MySqlCommand command = new MySqlCommand();
            string deleteQuery = "DELETE FROM `user` WHERE `ID`=@id";
            command.CommandText = deleteQuery;
            command.Connection = conn.GetConnection();

            //@Id,@fn,@ln,@g,@Adr,@t,@m
            command.Parameters.Add("@id", MySqlDbType.VarChar).Value = id;

            conn.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                conn.closeConnection();
                return true;
            }
            else
            {
                conn.closeConnection();
                return false;
            }

        }
    }
}
