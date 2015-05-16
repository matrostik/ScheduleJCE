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
            btnSubmit.Enabled = false;
            this.Staff = new Staff();
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
            for (int i = 8; i < 20; i++)
            {
                var time = string.Format("{0}:00-{1}:00", i, i + 1);
                gridData.Rows.Add(time);
            }
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
                ToggleControls();
                lblTitle.Text = string.Format("{0} {1}'s Schedule", first, last);
                FillGrid(list);
            }
        }

        private void FillGrid(LinkedList<TimeConstraint> list)
        {
            foreach (var tc in list)
            {
                int column = (int)((ConstraintDayOfWeek)Enum.Parse(typeof(ConstraintDayOfWeek), tc.Day));
                var diff = tc.EndHour - tc.BeginHour;
                for (int i = 0; i < diff; i++)
                {
                    gridData.Rows[tc.BeginHour + i - 8].Cells[column].Selected = true;
                }
            }

        }

        private void ToggleControls()
        {
            txtFirstName.Enabled = txtLastName.Enabled = btnOK.Enabled = !btnOK.Enabled;
            gridData.Enabled = btnSubmit.Enabled = !btnSubmit.Enabled;
            gridData.ClearSelection();
            lblTitle.Text = string.Empty;
        }

        private void gridData_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            //as event caused by 'unselectable' will select it again
            if (e.Cell.ColumnIndex == 0)
            {
                e.Cell.Selected = false;
                return;
            }
            //selectionChanged = true;
            //e.Cell.Style.BackColor = Color.Red;
        }

        //private bool selectionChanged;

        private void gridData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //gridData.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Red;

            //if (!selectionChanged)
            //{
            //    gridData.ClearSelection();
            //    selectionChanged = true;
            //    gridData.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Red;
            //}
            //else
            //{
            //    selectionChanged = false;
            //}
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            LinkedList<TimeConstraint> list = new LinkedList<TimeConstraint>();
            foreach (DataGridViewTextBoxCell item in gridData.SelectedCells)
            {
                list.AddLast(new TimeConstraint((ConstraintDayOfWeek)item.ColumnIndex, item.RowIndex + 8, item.RowIndex + 9));

            }
            Staff.AddConstraints(txtFirstName.Text, txtLastName.Text, list);
            ToggleControls();
            txtFirstName.Text = txtLastName.Text = string.Empty;
        }


    }
}
