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
        List<DataGridViewCell> SelectedCells { get; set; }

        public Schedule()
        {
            InitializeComponent();

            // init properties
            btnSubmit.Enabled = false;
            this.Staff = new Staff();
            SelectedCells = new List<DataGridViewCell>();
            BuildGrid();


            Lecturer l1 = new Lecturer("Bruce", "Lee");
            Staff.AddLecturer(l1);
            Lecturer l2 = new Lecturer("Ros", "Ha");
            Staff.AddLecturer(l2);
            l2 = new Lecturer("R", "H");
            Staff.AddLecturer(l2);
        }

        private void BuildGrid()
        {
            gridData.Rows.Clear();
            gridData.Rows.Add(12);
            for (int i = 8; i < 20; i++)
            {
                var time = string.Format("{0}:00-{1}:00", i, i + 1);
                gridData.Rows[i - 8].HeaderCell.Value = time;
            }
            // disable initial selection
            gridData.Rows[0].Cells[0].Selected = false;
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
                    var result = MessageBox.Show("Lecturer not exist, do you want to create new with this information?", "Not found", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if(result== DialogResult.OK) // create new lecture
                        Staff.AddLecturer(new Lecturer(first, last));
                    // reset inputs
                    txtFirstName.Text = txtLastName.Text = string.Empty;
                    return;
                }
                ToggleControls();
                lblTitle.Text = string.Format("{0} {1}'s Schedule", first, last);
                SelectedCells.Clear();                
                FillGrid(list);
                gridData.Visible = true;
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            LinkedList<TimeConstraint> list = new LinkedList<TimeConstraint>();
            foreach (DataGridViewTextBoxCell item in SelectedCells)
            {
                list.AddLast(new TimeConstraint((ConstraintDayOfWeek)item.ColumnIndex, item.RowIndex + 8, item.RowIndex + 9));
            }
            Staff.AddConstraints(txtFirstName.Text, txtLastName.Text, list);
            ToggleControls();
            txtFirstName.Text = txtLastName.Text = string.Empty;
            BuildGrid();
        }

        private void FillGrid(LinkedList<TimeConstraint> list)
        {
            foreach (var tc in list)
            {
                int column = (int)((ConstraintDayOfWeek)Enum.Parse(typeof(ConstraintDayOfWeek), tc.Day));
                var diff = tc.EndHour - tc.BeginHour;
                for (int i = 0; i < diff; i++)
                {
                    DataGridViewCell row = gridData.Rows[tc.BeginHour + i - 8].Cells[column];
                    row.Style.BackColor = Color.DarkRed;
                    SelectedCells.Add(row);
                }
            }
        }

        private void ToggleControls()
        {
            txtFirstName.Enabled = txtLastName.Enabled = btnOK.Enabled = !btnOK.Enabled;
            gridData.Enabled = btnSubmit.Enabled = !btnSubmit.Enabled;
            gridData.ClearSelection();
            lblTitle.Text = string.Empty;
            gridData.Visible = !gridData.Visible;
        }

        private void gridData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
                return;
            if (SelectedCells.Contains(gridData.CurrentCell))
            {
                SelectedCells.Remove(gridData.CurrentCell);
                gridData.CurrentCell.Style.BackColor = Color.WhiteSmoke;
                gridData.CurrentCell.Selected = false;
            }
            else
            {
                SelectedCells.Add(gridData.CurrentCell);
                gridData.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.DarkRed;

            }
        }

    }
}
