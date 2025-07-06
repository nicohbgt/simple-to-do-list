using System;
using System.Windows.Forms;
using Focient.Models;
using Focient.Database;

namespace Focient.Forms
{
    public partial class FormPlan : Form
    {
        private int userId;
        private DateTime selectedDate;

        public FormPlan(int userId, DateTime selectedDate)
        {
            InitializeComponent();
            this.userId = userId;
            this.selectedDate = selectedDate;
            lblDate.Text = selectedDate.ToString("dddd, dd MMMM yyyy");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPlanName.Text) || string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Please complete all fields.");
                return;
            }

            var newPlan = new PlanModel
            {
                UserId = userId,
                PlanName = txtPlanName.Text.Trim(),
                DateOfPlan = selectedDate,
                Intensity = (IntensityLevel)cmbIntensity.SelectedIndex, // 0 = Low, 1 = Medium, 2 = High
                Description = txtDescription.Text.Trim()
            };

            try
            {
                DatabaseHelper.InsertPlan(newPlan);
                MessageBox.Show("Plan created successfully!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving plan: " + ex.Message);
            }
        }
    }
}