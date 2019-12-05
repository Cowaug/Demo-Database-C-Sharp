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

namespace SQL_SERVER_CSHARP
{
    public partial class Form1 : Form
    {
        SqlConnection cnn;
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string connetionString;
                connetionString = @"Data Source=EXOS;Initial Catalog=company1;Integrated Security=True;";
                //connetionString = @"Data Source=EXOS2NDPC;Initial Catalog=company1;User ID=abc;Password=123;";
                cnn = new SqlConnection(connetionString);
                cnn.Open();

                MessageBox.Show("Connected !");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            SqlCommand command;
            SqlDataReader dataReader;
            String sql, output = "";

            
            try
            {
                    sql = textBox1.Text;
                command = new SqlCommand(sql, cnn);

                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    for(int i=0; i < dataReader.FieldCount; i++)
                    {
                        output = output +
                        dataReader.GetValue(i) + " - ";
                    }
                    output = output.Substring(0, output.Length - 3)+"\n";
                }
                MessageBox.Show(output);
                dataReader.Close();
                command.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                cnn.Close();
                MessageBox.Show("Closed !");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand command;
                SqlDataAdapter adapter = new SqlDataAdapter();
                String sql = "";
                //sql = "insert into EmployeeTB values ('John', 'B', 'Smith', '000000000', convert(datetime, '09-01-1965', 105), '731 Fondren, Houston, TX', 'M', 30000, '333445555', 5);";
                    sql = textBox1.Text;
                command = new SqlCommand(sql, cnn);

                adapter.InsertCommand = command;
                adapter.InsertCommand.ExecuteNonQuery();
                MessageBox.Show("Successful");
                command.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
