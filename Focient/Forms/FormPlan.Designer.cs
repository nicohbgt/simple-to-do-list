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

        private Label lblTitle;
        private Label lblDate;
        private TextBox txtPlanName;
        private ComboBox cmbIntensity;
        private TextBox txtDescription;
        private Button btnSave;

        public FormPlan(int userId, DateTime selectedDate)
        {
            InitializeComponent();
            this.userId = userId;
            this.selectedDate = selectedDate;
            lblDate.Text = selectedDate.ToString("dddd, dd MMMM yyyy");
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.lblDate = new Label();
            this.txtPlanName = new TextBox();
            this.cmbIntensity = new ComboBox();
            this.txtDescription = new TextBox();
            this.btnSave = new Button();

            // lblTitle
            this.lblTitle.Text = "Create New Plan";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.AutoSize = true;

            // lblDate
            this.lblDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDate.Location = new System.Drawing.Point(20, 60);
            this.lblDate.AutoSize = true;

            // txtPlanName
            this.txtPlanName.PlaceholderText = "Plan Name";
            this.txtPlanName.Location = new System.Drawing.Point(20, 100);
            this.txtPlanName.Width = 300;

            // cmbIntensity
            this.cmbIntensity.Location = new System.Drawing.Point(20, 140);
            this.cmbIntensity.Width = 300;
            this.cmbIntensity.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbIntensity.Items.AddRange(new string[] { "Low", "Medium", "High" });
            this.cmbIntensity.SelectedIndex = 0;

            // txtDescription
            this.txtDescription.PlaceholderText = "Description";
            this.txtDescription.Location = new System.Drawing.Point(20, 180);
            this.txtDescription.Size = new System.Drawing.Size(300, 100);
            this.txtDescription.Multiline = true;

            // btnSave
            this.btnSave.Text = "Save Plan";
            this.btnSave.Location = new System.Drawing.Point(20, 300);
            this.btnSave.Width = 300;
            this.btnSave.Click += new EventHandler(this.btnSave_Click);

            // FormPlan
            this.ClientSize = new System.Drawing.Size(350, 360);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.txtPlanName);
            this.Controls.Add(this.cmbIntensity);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.btnSave);
            this.Text = "New Plan";
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