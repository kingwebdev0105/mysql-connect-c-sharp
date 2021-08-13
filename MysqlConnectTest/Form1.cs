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
        string  strFirstName, strLastName, strEmail, strGender, strID;

        MySqlConnection mySqlConnect;   // Mysql Connection

        public MysqlTestForm()
        {
            InitializeComponent();
        }

        private void dataGrdView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGrdView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

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
                txtGender.Text = row.Cells[4].Value.ToString();
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
            strGender = txtGender.Text;

        }

        public void SetMysqlConnection()
        {
            GetValueFromText();

            string strDbConnection = "datasource=" + strHost + ";username=" + strUserName + ";password=" + strPassword + ";port=" + strPort;

            mySqlConnect = new MySqlConnection(strDbConnection);
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
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
                string strQuery = "insert into users.tbl_users(first_name,last_name,email,gender) values('" + strFirstName + "','" + strLastName + "','" + strEmail + "','" + strGender + "');";
                
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
                string strQuery = "update users.tbl_users set first_name='" + strFirstName + "',last_name='" + strLastName + "',email='" + strEmail + "',gender='" + strGender + "' where id='" + strID + "';";
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
                txtGender.Text = "";

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
