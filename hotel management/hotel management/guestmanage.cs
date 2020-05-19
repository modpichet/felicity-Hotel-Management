﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Data;

namespace hotel_management
{
    class guestmanage
    {
        connect conn = new connect();

        public bool insertguest(string fname,string lname,string gd,string adr,string t,string m)
        {
            MySqlCommand command = new MySqlCommand();
            string insertQuery = " INSERT INTO `guestmanage`( `firstname`, `lastname`, `gender`, `address`, `tel`, `mail`) VALUES (@fn,@ln,@g,@Adr,@t,@m)";
            command.CommandText = insertQuery;
            command.Connection = conn.GetConnection();

            //@fn,@ln,@g,@Adr,@t,@m
            command.Parameters.Add("@fn",MySqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@ln", MySqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@g", MySqlDbType.Text).Value = gd;
            command.Parameters.Add("@Adr", MySqlDbType.VarChar).Value = adr;
            command.Parameters.Add("@t", MySqlDbType.VarChar).Value = t;
            command.Parameters.Add("@m", MySqlDbType.VarChar).Value = m;

            conn.openConnection();

            if(command.ExecuteNonQuery() == 1)
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
        //สร้าง Function เพื่อเก็บข้อมูลผู้เข้าพัก
        public DataTable getGuets()
        {
            MySqlCommand command = new MySqlCommand(" SELECT * FROM `guestmanage`",conn.GetConnection());
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table = new DataTable();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;
        }
        public bool editGuets(int id,string fname, string lname, string gd, string adr, string t, string m)
        {
            MySqlCommand command = new MySqlCommand();
            string editQuery = "UPDATE `guestmanage` SET `firstname`=@fn,`lastname`=@ln,`gender`=@g,`address`=@Adr,`tel`=@t,`mail`=@m WHERE `ID` = @id ";
            command.CommandText = editQuery;
            command.Connection = conn.GetConnection();

            //@Id,@fn,@ln,@g,@Adr,@t,@m
            command.Parameters.Add("@id", MySqlDbType.VarChar).Value = id;
            command.Parameters.Add("@fn", MySqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@ln", MySqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@g", MySqlDbType.Text).Value = gd;
            command.Parameters.Add("@Adr", MySqlDbType.VarChar).Value = adr;
            command.Parameters.Add("@t", MySqlDbType.VarChar).Value = t;
            command.Parameters.Add("@m", MySqlDbType.VarChar).Value = m;

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
        public bool deleteGuests(int id)
        {
            MySqlCommand command = new MySqlCommand();
            string deleteQuery = "DELETE FROM `guestmanage` WHERE `ID`=@id";
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
