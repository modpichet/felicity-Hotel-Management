using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hotel_management
{
    public partial class guest : UserControl
    {
        guestmanage guests = new guestmanage();
        public guest()
        {
            InitializeComponent();
        }

        private void save_Click(object sender, EventArgs e)
        {
            String fname = TBfirstname.Text;
            String lname = TBlastname.Text;
            String gender = TBgender.Text;
            String address = TBaddress.Text;
            String tel = TBtel.Text;
            String mail = TBmail.Text;
            if (fname.Trim().Equals("") || lname.Trim().Equals("") || gender.Trim().Equals("") || address.Trim().Equals(""))
            {
                MessageBox.Show("กรุณาป้อนข้อมูลให้ครบถ้วน", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Boolean insertguest = guests.insertguest(fname, lname, gender, address, tel, mail);

                if (insertguest)
                {
                    DataGridView1.DataSource = guests.getGuets();
                    MessageBox.Show("บันทึกข้อมูลสำเร็จ", "Guests Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error ไม่สามารถบันทึกข้อมูลได้", "Guests Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        }

        private void guest_Load(object sender, EventArgs e)
        {
            DataGridView1.DataSource = guests.getGuets();
        }

        private void edit_Click(object sender, EventArgs e)
        {
            int id;
            String fname = TBfirstname.Text;
            String lname = TBlastname.Text;
            String gender = TBgender.Text;
            String address = TBaddress.Text;
            String tel = TBtel.Text;
            String mail = TBmail.Text;

            try
            {
                id = Convert.ToInt32(TBID.Text);

                if (fname.Trim().Equals("") || lname.Trim().Equals("") || gender.Trim().Equals("") || address.Trim().Equals(""))
                {
                    MessageBox.Show("กรุณาป้อนข้อมูลให้ครบถ้วน", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Boolean insertguest = guests.editGuets(id,fname, lname, gender, address, tel, mail);

                    if (insertguest)
                    {
                        DataGridView1.DataSource = guests.getGuets();
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
            TBfirstname.Text = DataGridView1.CurrentRow.Cells[1].Value.ToString();
            TBlastname.Text = DataGridView1.CurrentRow.Cells[2].Value.ToString();
            TBgender.Text = DataGridView1.CurrentRow.Cells[3].Value.ToString();
            TBaddress.Text = DataGridView1.CurrentRow.Cells[4].Value.ToString();
            TBtel.Text = DataGridView1.CurrentRow.Cells[5].Value.ToString();
            TBmail.Text = DataGridView1.CurrentRow.Cells[6].Value.ToString();

        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void delete_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(TBID.Text);

                if (guests.deleteGuests(id))
                {
                    DataGridView1.DataSource = guests.getGuets();
                    MessageBox.Show("ลบข้อมูลสำเร็จ", "Guests Management", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Error ไม่สามารถลบข้อมูลได้", "Guests Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ID Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void clear_Click(object sender, EventArgs e)
        {

                    TBID.Text = "";
                    TBfirstname.Text = "";
                    TBlastname.Text = "";
                    TBgender.Text = "";
                    TBaddress.Text = "";
                    TBtel.Text = "";
                    TBmail.Text = "";
        }

        private void DataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
