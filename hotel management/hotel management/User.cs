﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace hotel_management
{
    public partial class User : UserControl
    {
        usermanage user = new usermanage();

        public User()
        {
            InitializeComponent();
        }

        private void save_Click(object sender, EventArgs e)
        {
            string username = tbusername.Text;
            string password = tbpassword.Text;
            string name = tbname.Text;
            string tel = tbtel.Text;
            string mail = tbmail.Text;

            if (username.Trim().Equals(""))
                {
                    MessageBox.Show("กรุณากรอกชื่อผู้ใช้", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (password.Trim().Equals(""))
                    {
                    MessageBox.Show("กรุณากรอกรหัสผ่าน", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (name.Trim().Equals(""))
                {
                    MessageBox.Show("กรุณากรอกชื่อ-สกุล", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (tel.Trim().Equals(""))
                {
                    MessageBox.Show("กรุณากรอกเบอร์โทรศัพท์", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (mail.Trim().Equals(""))
                {
                    MessageBox.Show("กรุณากรอก Mail", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Boolean insertguest = user.insertUser(username,password,name,tel,mail);

                    if (insertguest)
                    {
                        DataGridView1.DataSource = user.getUser();
                        MessageBox.Show("บันทึกข้อมูลสำเร็จ", "Guests Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error ไม่สามารถบันทึกข้อมูลได้", "Guests Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
            }
        }

        private void User_Load(object sender, EventArgs e)
        {
            DataGridView1.DataSource = user.getUser();
        }

        private void edit_Click(object sender, EventArgs e)
        {
            int id;
            string username = tbusername.Text;
            string password = tbpassword.Text;
            string name = tbname.Text;
            string tel = tbtel.Text;
            string mail = tbmail.Text;
            try
            {
                id = Convert.ToInt32(TBID.Text);

                if (username.Trim().Equals("") || password.Trim().Equals("") || name.Trim().Equals("") || tel.Trim().Equals(""))
                {
                    MessageBox.Show("กรุณาป้อนข้อมูลให้ครบถ้วน", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Boolean insertguest = user.editUser(id,username, password, name, tel, mail);

                    if (insertguest)
                    {
                        DataGridView1.DataSource = user.getUser();
                        MessageBox.Show("แก้ไขข้อมูลสำเร็จ", "Guests Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error ไม่สามารถแก้ไขข้อมูลได้", "Guests Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ID Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TBID.Text = DataGridView1.CurrentRow.Cells[0].Value.ToString();
            tbusername.Text = DataGridView1.CurrentRow.Cells[1].Value.ToString();
            tbpassword.Text = DataGridView1.CurrentRow.Cells[2].Value.ToString();
            tbname.Text = DataGridView1.CurrentRow.Cells[3].Value.ToString();
            tbtel.Text = DataGridView1.CurrentRow.Cells[4].Value.ToString();
            tbmail.Text = DataGridView1.CurrentRow.Cells[5].Value.ToString();
        }

        private void clear_Click(object sender, EventArgs e)
        {
            TBID.Text = "";
            tbusername.Text = "";
            tbpassword.Text = "";
            tbtel.Text = "";
            tbname.Text = "";
            tbmail.Text = "";
        }

        private void delete_Click(object sender, EventArgs e)
        {
            //ลองทำดูก่อน
            try
            {
                int id = Convert.ToInt32(TBID.Text);

                if (user.deleteUser(id))
                {
                    DataGridView1.DataSource = user.getUser();
                    MessageBox.Show("ลบข้อมูลสำเร็จ", "Guests Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error ไม่สามารถลบข้อมูลได้", "Guests Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            ///เมื่อเกิด Error
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ID Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
