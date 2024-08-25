using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace User_Info_Collection_System_CRUD_
{
    public partial class MainForm : Form
    {
        string connection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\gosia\Documents\Projects\Visual Studio\C#\User Info Collection System(CRUD)\Database\UsersData.mdf"";Integrated Security=True;Connect Timeout=30";
        public MainForm()
        {
            InitializeComponent();
            DisplayData();
            ClearFields();
        }


        public void DisplayData()
        {
            CRUD userData = new CRUD();

            List<CRUD> listData = userData.GetUserListData();

            dgvUserData.DataSource = listData;
        }

        public void ClearFields()
        {
            tbFullName.Text = "";
            cbGender.SelectedIndex = -1;
            tbContact.Text = "";
            tbEmail.Text = "";
            DisplayData();
        }


        private int getID = 0;
        private void lblX_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to close App?","Confirmation Message",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
                Application.Exit();
            }
            
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (tbFullName.Text == "" || cbGender.SelectedIndex == -1 || tbContact.Text == "" | tbEmail.Text == "")
            {
                MessageBox.Show("Please fill in all the blanks fields!", "Error Message", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {
                using (SqlConnection connect = new SqlConnection(connection))
                {
                    connect.Open();

                    string insertData = "INSERT INTO users (full_name,gender,contact,email,birth_date,date_inserted) " +
                                        "VALUES(@full_name,@gender,@contact,@email,@birth_date,@date_inserted)";

                    using (SqlCommand cmd = new SqlCommand(insertData,connect))
                    {
                        cmd.Parameters.AddWithValue("@full_name", tbFullName.Text.Trim());
                        cmd.Parameters.AddWithValue("@gender", cbGender.Text.Trim());
                        cmd.Parameters.AddWithValue("@contact", tbContact.Text.Trim());
                        cmd.Parameters.AddWithValue("@email", tbEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@birth_date", dtpDOB.Value);
                        cmd.Parameters.AddWithValue("@date_inserted", DateTime.Today.Date);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Information has been succesfully Added ", "Confirmation Message!", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        ClearFields();
                    }
                    connect.Close();
                }
            }
            DisplayData();
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (tbFullName.Text == "" || cbGender.SelectedIndex == -1 || tbContact.Text == "" | tbEmail.Text == "")
            {
                MessageBox.Show("Please select an item first!", "Error Message!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {
                if (MessageBox.Show("Are you sure you want to Update ID: " + getID + "?", "Confirmation Message",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (SqlConnection connect = new SqlConnection(connection))
                    {
                        connect.Open();

                        string updateData =
                            "UPDATE users SET full_name = @full_name,gender = @gender,contact = @contact" +
                            ",email = @email,birth_date = @birth_date,date_updated = @date_updated WHERE id = @id";

                        using (SqlCommand cmd = new SqlCommand(updateData, connect))
                        {
                            cmd.Parameters.AddWithValue("@id", getID);
                            cmd.Parameters.AddWithValue("@full_name", tbFullName.Text.Trim());
                            cmd.Parameters.AddWithValue("@gender", cbGender.Text.Trim());
                            cmd.Parameters.AddWithValue("@contact", tbContact.Text.Trim());
                            cmd.Parameters.AddWithValue("@email", tbEmail.Text.Trim());
                            cmd.Parameters.AddWithValue("@birth_date", dtpDOB.Value);
                            cmd.Parameters.AddWithValue("@date_updated", DateTime.Today.Date);

                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Information has been succesfully Updated", "Confrimation Message!",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                            ClearFields();
                        }

                        connect.Close();
                    }
                }
            }
            DisplayData();
        }

        private void dgvUserData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dgvUserData.Rows[e.RowIndex];
                getID = Convert.ToInt32(row.Cells[0].Value);
                tbFullName.Text = row.Cells[1].Value.ToString();
                cbGender.Text = row.Cells[2].Value.ToString();
                tbContact.Text = row.Cells[3].Value.ToString();
                tbEmail.Text = row.Cells[4].Value.ToString();
                dtpDOB.Text = row.Cells[5].Value.ToString();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (tbFullName.Text == "" || cbGender.SelectedIndex == -1 || tbContact.Text == "" | tbEmail.Text == "")
            {
                MessageBox.Show("Please select an item first!", "Error Message!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {
                if (MessageBox.Show("Are you sure you want to Delete ID: " + getID + "?", "Confirmation Message",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    using (SqlConnection connect = new SqlConnection(connection))
                    {
                        connect.Open();

                        string updateData =
                            "DELETE FROM users WHERE id = @id";

                        using (SqlCommand cmd = new SqlCommand(updateData, connect))
                        {
                            cmd.Parameters.AddWithValue("@id", getID);

                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Information has been succesfully Deleted", "Confirmation Message!",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                            ClearFields();
                        }

                        connect.Close();
                    }
                }
            }
            DisplayData();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
    }
}
