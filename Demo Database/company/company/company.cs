using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace company
{
    public partial class company : Form
    {
        SqlConnection connection;
        SqlCommand command;
        string str = "Data Source=DESKTOP-05DVS5F;Initial Catalog=company1;Integrated Security=True";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        void LoadData()
        {
            command = connection.CreateCommand();
            command.CommandText = "Select * from Department";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dgvCompany.DataSource = table;
            
        }
        void Reset() {
            btnAdd.Enabled = true;
            tboxDno.ReadOnly = false;
            tboxDno.Text ="";
            tBoxDname.Text ="";
            tBoxMgrssn.Text = "";
            timeBoxMgrstrart.Value = DateTime.Today;
        }
        public company()
        {
            connection = new SqlConnection(str);
            connection.Open();
            InitializeComponent();
        }

        private void company_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dgvCompany_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            btnAdd.Enabled = false;
            tboxDno.ReadOnly = true;
            int i;
            i = dgvCompany.CurrentRow.Index;
            tboxDno.Text = dgvCompany.Rows[i].Cells[1].Value.ToString();
            tBoxDname.Text = dgvCompany.Rows[i].Cells[0].Value.ToString();
            tBoxMgrssn.Text = dgvCompany.Rows[i].Cells[2].Value.ToString();
            timeBoxMgrstrart.Text = dgvCompany.Rows[i].Cells[3].Value.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "Execute AddDepartment @name,@num,@Mgrssn,@Mgrstart";
                command.Parameters.Add("@name", SqlDbType.VarChar, 10).Value = tBoxDname.Text.ToString();
                command.Parameters.Add("@num", SqlDbType.Int).Value = tboxDno.Text.ToString();
                command.Parameters.Add("@Mgrssn", SqlDbType.Char, 10).Value = tBoxMgrssn.Text.ToString();
                command.Parameters.Add("@Mgrstart", SqlDbType.Date).Value = timeBoxMgrstrart.Value.ToString();
                command.ExecuteNonQuery();
                LoadData();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try{ 
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "Execute EditDepartment @name,@num,@Mgrssn,@Mgrstart";
                command.Parameters.Add("@name", SqlDbType.VarChar, 10).Value = tBoxDname.Text.ToString();
                command.Parameters.Add("@num", SqlDbType.Int).Value = tboxDno.Text.ToString();
                command.Parameters.Add("@Mgrssn", SqlDbType.Char, 10).Value = tBoxMgrssn.Text.ToString();
                command.Parameters.Add("@Mgrstart", SqlDbType.Date).Value = timeBoxMgrstrart.Value.ToString();
                command.ExecuteNonQuery();
                LoadData();
                Reset();
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "Execute DeleteDepartment @num";
                command.Parameters.Add("@num", SqlDbType.Int).Value = tboxDno.Text.ToString();
                command.ExecuteNonQuery();
                LoadData();
                Reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }
    }
}
