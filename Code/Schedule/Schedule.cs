using Schedule.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Schedule
{
    public partial class Schedule : Form
    {

        public Staff Staff { get; set; }


        public Schedule()
        {
            InitializeComponent();
            gridData.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            // init properties
            this.Staff = new Staff();

            Lecturer l1 = new Lecturer("Bruce", "Lee");
            Staff.AddLecturer(l1);
            Lecturer l2 = new Lecturer("Ros", "Ha");
            Staff.AddLecturer(l2);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            var first = txtFirstName.Text;
            var last = txtLastName.Text;
            if (string.IsNullOrWhiteSpace(first) || string.IsNullOrWhiteSpace(last))
            {
                MessageBox.Show("Please enter First and Last name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                var list = this.Staff.GetConstraints(first, last);
                if (list == null)
                {
                    MessageBox.Show("Lecturer not exist", "Not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtFirstName.Text = txtLastName.Text = string.Empty;
                    return;
                }

                lblTitle.Text = string.Format("{0} {1}'s Schedule", first, last);
                txtFirstName.Enabled = txtLastName.Enabled = btnOK.Enabled = false;
                gridData.DataSource = BuildGrid();
            }
        }

        private DataTable BuildGrid()
        {
            DataTable table = new DataTable();
            table.Columns.Add(" ", typeof(string));
            foreach (var item in Enum.GetValues(typeof(ConstraintDayOfWeek)))
            {
                table.Columns.Add(item.ToString(), typeof(string));
                
            }
            for (int i = 8; i < 20; i++)
            {
                var time = string.Format("{0}:00-{1}:00",i,++i);
                table.Rows.Add(time);
            }
            //table.Columns.Add("CITY", typeof(string));
            return table;
        }
    }
}
