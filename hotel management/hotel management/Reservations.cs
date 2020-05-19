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
    public partial class Reservations : UserControl
    {
        public Reservations()
        {
            InitializeComponent();
        }
        roommanage room = new roommanage();
        reservationsmanage reservation = new reservationsmanage();
        private void Reservations_Load(object sender, EventArgs e)
        {
            //แสดง ประเภทของห้อง
            comboRoomType.DataSource = room.roomTypeList();
            comboRoomType.DisplayMember = "label";
            comboRoomType.ValueMember = "category_id";
            //แสดง หมายเลขห้อง จากการเลือก ประเภทของห้อง

            int type = Convert.ToInt32(comboRoomType.SelectedValue.ToString());
            comboRoomNumber.DataSource = room.roomByType(type);
            comboRoomNumber.DisplayMember = "number";
            comboRoomNumber.ValueMember = "number";

            DataGridView1.DataSource = reservation.getAllReserv();

        }
        // clear 
        private void clear_Click(object sender, EventArgs e)
        {
            tbReservId.Text = "";
            tbguestId.Text = "";
            //comboRoomNo.Text = "";
            //comboRoomType.SelectedIndex = 0;
            DatetimeIn.Value = DateTime.Now;
            DatetimeOut.Value = DateTime.Now;

        }

        private void comboRoomType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //แสดง หมายเลขห้อง จากการเลือก ประเภทของห้อง
                int type = Convert.ToInt32(comboRoomType.SelectedValue.ToString());
                comboRoomNumber.DataSource = room.roomByType(type);
                comboRoomNumber.DisplayMember = "number";
                comboRoomNumber.ValueMember = "number";
            }
            catch (Exception)
            {

            }
        }

        private void addReserv_Click(object sender, EventArgs e)
        {
            try
            {


            int guestId = Convert.ToInt32(tbguestId.Text);
            int roomNumber = Convert.ToInt32(comboRoomNumber.SelectedValue);
            DateTime dateIn = DatetimeIn.Value;
            DateTime dateOut = DatetimeOut.Value;

            if (dateIn < DateTime.Now)
            {
                MessageBox.Show("วันที่เข้าพักต้องมากกว่าหรือเท่ากับวันนี้", "Invalid Date In", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (dateOut < dateIn)
            {
                MessageBox.Show("วันที่ออกต้องมากกว่าหรือเท่ากับวันที่เข้าพัก", "Invalid Date Out", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (reservation.addReservation(roomNumber, guestId, dateIn, dateOut))
                {
                    room.setRoomFree(roomNumber, "No");
                    DataGridView1.DataSource = reservation.getAllReserv();
                        MessageBox.Show("ทำการจองห้องสำเร็จ", "Add Reservation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                    }
                else
                {
                    MessageBox.Show("การจองห้องไม่สำเร็จ", "Add Reservation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
           catch (Exception ex)
           {
               MessageBox.Show(ex.Message, "Add Reservation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
}

           
        

        private void tbname_TextChanged(object sender, EventArgs e)
        {

        }

        private void edit_Click(object sender, EventArgs e)
        {
            try
            {
                int rservId = Convert.ToInt32(tbReservId.Text);
                int guestId = Convert.ToInt32(tbguestId.Text);
                int roomNumber = Convert.ToInt32(DataGridView1.CurrentRow.Cells[1].Value.ToString());
                DateTime dateIn = DatetimeIn.Value;
                DateTime dateOut = DatetimeOut.Value;

                // date in must be = or > today date
                // date out must be = or > date in
                if (dateIn < DateTime.Now)
                {
                    MessageBox.Show("วันที่เข้าพักต้องมากกว่าหรือเท่ากับวันนี้", "Invalid Date In", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (dateOut < dateIn)
                {
                    MessageBox.Show("วันที่ออกต้องมากกว่าหรือเท่ากับวันที่เข้าพัก", "Invalid Date Out", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    //rservId
                    if (reservation.editReserv(rservId,roomNumber,guestId, dateIn, dateOut))
                    {
                        // set the room free column to NO
                        // you can add a message if the room is edited
                        room.setRoomFree(roomNumber,"No");
                        DataGridView1.DataSource = reservation.getAllReserv();
                        MessageBox.Show("การจองได้แก้ไขเรียบร้อย", "Edit Reservation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("การจองแก้ไขไม่ได้", "Add Reservation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add Reservation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void delete_Click(object sender, EventArgs e)
        {
            try
            {
                int reservId = Convert.ToInt32(tbReservId.Text);
                int roomNumber = Convert.ToInt32(DataGridView1.CurrentRow.Cells[1].Value.ToString());
                if (reservation.deleteReservation(reservId))
                {
                    DataGridView1.DataSource = reservation.getAllReserv();
                    //หลังจากลบข้อมูลการจองแล้ว ห้องก็จะว่าง

                    room.setRoomFree(roomNumber, "Yes");
                    MessageBox.Show("การจองได้ลบเรียบร้อย", "Delete Reservation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete Reservation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tbReservId.Text = DataGridView1.CurrentRow.Cells[0].Value.ToString();
            //เลือก room type จาก combobox ของ type ดังนั้นจะต้องรู้จัก type ของห้อง 
            //รับค่า room Id
            int roomId = Convert.ToInt32(DataGridView1.CurrentRow.Cells[1].Value.ToString());

            //เลือก room type จาก comboBox
            comboRoomType.SelectedValue = room.getRoomType(roomId);
            //เลือก room number จาก comboBox
            comboRoomNumber.SelectedValue = roomId;
            tbguestId.Text = DataGridView1.CurrentRow.Cells[2].Value.ToString();

        }

    }
}

