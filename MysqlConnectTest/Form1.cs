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

namespace MysqlConnectTest
{
    public partial class MysqlTestForm : Form
    {
        string  strHost, strUserName, strPassword, strPort;
        string  strFirstName, strLastName, strEmail, strGender, strID, strPhoneNumber;

        MySqlConnection mySqlConnect;   // Mysql Connection

        private void cmbSearch_SelectedValueChanged(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            this.ActiveControl = txtSearch;
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            string strQuery;
            string strSearchText;

            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                strSearchText = (txtSearch.Text).Trim('\r', '\n');

                switch (cmbSearch.SelectedIndex)
                {
                    case 0:
                        try
                        {
                            strQuery = "select * from users.tbl_users WHERE first_name LIKE '%" + strSearchText + "%' or last_name LIKE '%" + strSearchText + "%';";
                            MySqlCommand MySqlCommand = new MySqlCommand(strQuery, mySqlConnect);
                            mySqlConnect.Open();
                            //For offline connection we weill use  MySqlDataAdapter class.  
                            MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                            MyAdapter.SelectCommand = MySqlCommand;

                            DataTable dTable = new DataTable();

                            int count = MyAdapter.Fill(dTable);

                            if (count == 0)
                            {
                                MessageBox.Show("There is no 'username' you are looking for");
                            }
                            else if (count == 1)
                            {
                                MessageBox.Show(count.ToString() + " user found");
                            }
                            else
                            {
                                MessageBox.Show(count.ToString() + " users found");
                            }

                            dataGrdView.DataSource = dTable; // here i have assign dTable object to the dataGridView1 object to display data.  
                            mySqlConnect.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        break;
                    case 1:
                        try
                        {
                            strQuery = "select * from users.tbl_users WHERE id = '" + strSearchText + "';";
                            MySqlCommand MySqlCommand = new MySqlCommand(strQuery, mySqlConnect);
                            mySqlConnect.Open();
                            //For offline connection we weill use  MySqlDataAdapter class.  
                            MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                            MyAdapter.SelectCommand = MySqlCommand;
                            DataTable dTable = new DataTable();
                            int count = MyAdapter.Fill(dTable);

                            if (count == 0)
                            {
                                MessageBox.Show("There is no 'id' you are looking for");
                            }
                            else if (count == 1)
                            {
                                MessageBox.Show(count.ToString() + " user found");
                            }
                            else
                            {
                                MessageBox.Show(count.ToString() + " users found");
                            }

                            dataGrdView.DataSource = dTable; // here i have assign dTable object to the dataGridView1 object to display data.  
                            mySqlConnect.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        break;
                    case 2:
                        try
                        {
                            strQuery = "select * from users.tbl_users WHERE phone_number LIKE '%" + strSearchText + "%';";
                            MySqlCommand MySqlCommand = new MySqlCommand(strQuery, mySqlConnect);
                            mySqlConnect.Open();
                            //For offline connection we weill use  MySqlDataAdapter class.  
                            MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                            MyAdapter.SelectCommand = MySqlCommand;
                            DataTable dTable = new DataTable();
                            int count = MyAdapter.Fill(dTable);

                            if (count == 0)
                            {
                                MessageBox.Show("There is no 'PhoneNumber' you are looking for");
                            }
                            else if (count == 1)
                            {
                                MessageBox.Show(count.ToString() + " user found");
                            }
                            else
                            {
                                MessageBox.Show(count.ToString() + " users found");
                            }

                            dataGrdView.DataSource = dTable; // here i have assign dTable object to the dataGridView1 object to display data.  
                            mySqlConnect.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        break;
                }
            }
        }

        public MysqlTestForm()
        {
            InitializeComponent();

            cmbGender.Items.Add("male");
            cmbGender.Items.Add("female");
            cmbGender.SelectedIndex = 0;

            cmbSearch.Items.Add("UserName");
            cmbSearch.Items.Add("ID");
            cmbSearch.Items.Add("PhoneNumber");
            cmbSearch.SelectedIndex = 0;

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (cmbSearch.SelectedIndex == 2)
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(txtSearch.Text, "[^0-9]"))
                {
                    MessageBox.Show("Please enter only numbers.");
                    txtSearch.Text = txtSearch.Text.Remove(txtSearch.Text.Length - 1);
                }
            }
        }

        private void txtPhoneNumber_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtPhoneNumber.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                txtPhoneNumber.Text = txtPhoneNumber.Text.Remove(txtPhoneNumber.Text.Length - 1);
            }
        }

        private void dataGrdView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGrdView.CurrentRow;
            if (!row.IsNewRow)
            {
                txtID.Text = row.Cells["id"].Value.ToString();
                txtFirstName.Text = row.Cells[1].Value.ToString();
                txtLastName.Text = row.Cells[2].Value.ToString();
                txtEmail.Text = row.Cells[3].Value.ToString();
                cmbGender.SelectedIndex = (row.Cells[4].Value.ToString() == "0" ? 0: 1);
                txtPhoneNumber.Text = row.Cells[5].Value.ToString();
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        public void GetValueFromText()
        {
            strHost = txtHost.Text;
            strUserName = txtUserName.Text;
            strPassword = txtPassword.Text;
            strPort = txtPort.Text;

            strID = txtID.Text;
            strFirstName = txtFirstName.Text;
            strLastName = txtLastName.Text;
            strEmail = txtEmail.Text;
            strGender = cmbGender.SelectedIndex.ToString();
            strPhoneNumber = txtPhoneNumber.Text;
        }

        public void SetMysqlConnection()
        {
            GetValueFromText();

            string strDbConnection = "datasource=" + strHost + ";username=" + strUserName + ";password=" + strPassword + ";port=" + strPort;

            mySqlConnect = new MySqlConnection(strDbConnection);
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            try
            {
                GetValueFromText();

                SetMysqlConnection();

                string strQuery = "select * from users.tbl_users;";
                MySqlCommand MySqlCommand = new MySqlCommand(strQuery, mySqlConnect);
                mySqlConnect.Open();  
                //For offline connection we weill use  MySqlDataAdapter class.  
                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = MySqlCommand;
                DataTable dTable = new DataTable();
                MyAdapter.Fill(dTable);
                dataGrdView.DataSource = dTable; // here i have assign dTable object to the dataGridView1 object to display data.  
                mySqlConnect.Close();  
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                GetValueFromText();
                SetMysqlConnection();

                //This is my insert query in which i am taking input from the user through windows forms  
                string strQuery = "insert into users.tbl_users(first_name,last_name,email,gender,phone_number) values('" + strFirstName + "','" + strLastName + "','" + strEmail + "','" + strGender + "','" + strPhoneNumber + "');";
                
                //This is command class which will handle the query and connection object.  
                MySqlCommand MySqlCommand = new MySqlCommand(strQuery, mySqlConnect);
                MySqlDataReader MySqlReader;
                mySqlConnect.Open();
                MySqlReader = MySqlCommand.ExecuteReader();     // Here our query will be executed and data saved into the database.  
                MessageBox.Show("Save Data Success!");

                btnRead_Click(sender, e);

                while (MySqlReader.Read())
                {
                }
                mySqlConnect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                GetValueFromText();
                SetMysqlConnection();

                //This is my update query in which i am taking input from the user through windows forms and update the record.  
                string strQuery = "update users.tbl_users set first_name='" + strFirstName + "',last_name='" + strLastName + "',email='" + strEmail + "',gender='" + strGender + "',phone_number='" + strPhoneNumber + "' where id='" + strID + "';";
                //This is  MySqlConnection here i have created the object and pass my connection string.  

                MySqlCommand MyCommand = new MySqlCommand(strQuery, mySqlConnect);
                MySqlDataReader MyReader;
                mySqlConnect.Open();
                MyReader = MyCommand.ExecuteReader();
                MessageBox.Show("Data Updated");

                btnRead_Click(sender, e);

                while (MyReader.Read())
                {
                }
                mySqlConnect.Close();//Connection closed here  
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                GetValueFromText();
                SetMysqlConnection();

                string Query = "delete from users.tbl_users where id='" + strID + "';";

                MySqlCommand MyCommand = new MySqlCommand(Query, mySqlConnect);
                MySqlDataReader MyReader;
                mySqlConnect.Open();
                MyReader = MyCommand.ExecuteReader();
                MessageBox.Show("Data Deleted");

                btnRead_Click(sender, e);

                txtID.Text = "";
                txtFirstName.Text = "";
                txtLastName.Text = "";
                txtEmail.Text = "";
                cmbGender.SelectedIndex = 0;
                txtPhoneNumber.Text = "";

                while (MyReader.Read())
                {
                }
                mySqlConnect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
