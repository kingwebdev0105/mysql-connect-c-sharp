using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MysqlConnectTest
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            this.ActiveControl = txtUser;
        }

        MysqlTestForm frm;

        private void button1_Click(object sender, EventArgs e)
        {
            if ((txtUser.Text == "admin") && (txtPass.Text == "spa"))  
            {
                this.Hide();
                frm = new MysqlTestForm();
                frm.Show();
            }
            else
            {
                MessageBox.Show("Please type correctly user name and password!");
            }
        }

        private void txtUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            string strUser = txtUser.Text;
            string strPass = txtPass.Text;

            if (e.KeyChar == (char)Keys.Enter)
            {
                if(strPass.Equals("spa") && strUser.Equals("admin"))
                {
                    this.Hide();
                    frm = new MysqlTestForm();
                    frm.Show();
                }
                else if(strUser.Length == 0)
                {
                    MessageBox.Show("Please type admin name!");
                    this.ActiveControl = txtUser;
                }
                else if(strPass.Length == 0)
                {
                    this.ActiveControl = txtPass;
                }
                else
                {
                    MessageBox.Show("Please type admin password!");
                    this.ActiveControl = txtPass;
                }
            }
        }

        private void txtPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            string strUser = txtUser.Text;
            string strPass = txtPass.Text;

            if (e.KeyChar == (char)Keys.Enter)
            {
                if (strPass.Equals("spa") && strUser.Equals("admin"))
                {
                    this.Hide();
                    frm = new MysqlTestForm();
                    frm.Show();
                }
                else if (strUser.Length == 0)
                {
                    MessageBox.Show("Please type admin name!");
                    this.ActiveControl = txtUser;
                }
                else if (strPass.Length == 0)
                {
                    MessageBox.Show("Please type admin password!");
                    this.ActiveControl = txtPass;
                }
                else
                {
                    MessageBox.Show("Please type correctly admin name and password!");
                    this.ActiveControl = txtUser;
                }
            }
        }
    }
}
