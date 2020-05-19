using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace hotel_management
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //ออกจากโปรแกรม
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //เมื่อกดปุ่ม login
        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
           
            connect conn = new connect();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand();
            String query = "SELECT * FROM `user` WHERE `username`=@usn AND `password`=@pass";

            command.CommandText = query;
            command.Connection = conn.GetConnection();

            command.Parameters.Add("@usn", MySqlDbType.VarChar).Value = username.text;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = password.Text;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            // if the username and the password exists
            if (table.Rows.Count > 0)
            {
                
                // show หน้าหลัก
                this.Hide();
                home homeform = new home();
                homeform.Show();
                
            }
            else
            {
                if (username.text.Trim().Equals(""))
                {
                    MessageBox.Show("กรุณากรอกชื่อผู้ใช้", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (password.Text.Trim().Equals(""))
                {
                    MessageBox.Show("กรุณากรอกรหัสผ่าน", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("ชื่อผู้ใช้หรือรหัสผ่านนี้ไม่มีอยู่", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
    }
}
