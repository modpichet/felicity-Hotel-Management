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
    public partial class roomform : UserControl
    {
        public roomform()
        {
            InitializeComponent();
        }

        roommanage room = new roommanage();

        private void roomform_Load(object sender, EventArgs e)
        {
            comboBoxRoomType.DataSource = room.roomTypeList();
            comboBoxRoomType.DisplayMember = "label";
            comboBoxRoomType.ValueMember = "category_id";

            DataGridView1.DataSource = room.getRooms();
        }

        private void addRoom_Click(object sender, EventArgs e)
        {

            int type = Convert.ToInt32(comboBoxRoomType.SelectedValue.ToString());
            string free = "";
            try
            {
                int number = Convert.ToInt32(TbRoomNumber.Text);
                if (radioButtonYES.Checked)
                {
                    free = "Yes";
                }
                else if (radioButtonNO.Checked)
                {
                    free = "No";
                }
                if(room.addRoom(number,type,free))
                {
                    DataGridView1.DataSource = room.getRooms();
                    MessageBox.Show("เพิ่มห้องสำเร็จ", "Room Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("เพิ่มห้องไม่สำเร็จ", "Room Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
               catch (Exception ex)
            {
               MessageBox.Show(ex.Message, "Room Number Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void clear_Click(object sender, EventArgs e)
        {
            TbRoomNumber.Text = "";
            comboBoxRoomType.SelectedIndex = 0;
            radioButtonYES.Checked = true;
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TbRoomNumber.Text = DataGridView1.CurrentRow.Cells[0].Value.ToString();
            comboBoxRoomType.SelectedValue = DataGridView1.CurrentRow.Cells[1].Value;

            string free = DataGridView1.CurrentRow.Cells[2].Value.ToString();

            if (free.Equals("Yes"))
            {
                radioButtonYES.Checked = true;
            }
            else if (free.Equals("No"))
            {
                radioButtonNO.Checked = true;
            }

        }

        private void edit_Click(object sender, EventArgs e)
        {
            
            int type = Convert.ToInt32(comboBoxRoomType.SelectedValue.ToString());
            String free = "";
            try
            {
                int number = Convert.ToInt32(TbRoomNumber.Text);
                if (radioButtonYES.Checked)
                {
                    free = "Yes";
                }
                else if (radioButtonNO.Checked)
                {
                    free = "No";
                }
                if (room.editRoom(number,type,free))
                {
                    DataGridView1.DataSource = room.getRooms();
                    MessageBox.Show("แก้ไขข้อมูลห้องสำเร็จ", "Room Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("เเก้ไขข้อมูลห้องไม่สำเร็จ", "Room Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Room Number Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            try
            {
                int number = Convert.ToInt32(TbRoomNumber.Text);
                if (room.deleteRoom(number))
                {
                    DataGridView1.DataSource = room.getRooms();
                    MessageBox.Show("ลบข้อมูลห้องสำเร็จ", "Room Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("ลบข้อมูลห้องไม่สำเร็จ", "Room Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Room Number Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
