using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Focient.Database;
using Focient.Models;

namespace Focient.Forms
{
    public partial class FormDashboard : Form
    {
        private DateTime startDateOfWeek;
        private DateTime? selectedDate; // Menyimpan tanggal yang dipilih
        private int userId;

        public FormDashboard(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            startDateOfWeek = GetStartOfWeek(DateTime.Today);
            GenerateWeekDaysUI();
        }

        private DateTime GetStartOfWeek(DateTime date)
        {
            int diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
            return date.AddDays(-1 * diff).Date;
        }

        private void GenerateWeekDaysUI()
        {
            flowLayoutDates.Controls.Clear();
            for (int i = 0; i < 7; i++)
            {
                DateTime currentDate = startDateOfWeek.AddDays(i);
                Button dateBtn = new Button
                {
                    Width = 100,
                    Height = 60,
                    Text = currentDate.ToString("dd - MM"),
                    Tag = currentDate,
                    BackColor = IsPlanned(currentDate) ? Color.MediumSeaGreen : Color.LightGray
                };

                dateBtn.Click += DateBtn_Click;
                flowLayoutDates.Controls.Add(dateBtn);
            }

            lblMonth.Text = startDateOfWeek.ToString("MMMM yyyy", new CultureInfo("en-US"));
            lblWeek.Text = $"Week {CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(startDateOfWeek, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday)}";
        }

        private bool IsPlanned(DateTime date)
        {
            return DatabaseHelper.HasPlanOnDate(userId, date);
        }

        private void DateBtn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null) return;

            selectedDate = (DateTime)btn.Tag;
            lblMonth.Text = selectedDate.Value.ToString("MMMM yyyy", new CultureInfo("en-US"));

            var plans = DatabaseHelper.GetPlansForDate(userId, selectedDate.Value);
            lstActivities.Items.Clear();

            if (plans.Count > 0)
            {
                lstActivities.Visible = true;
                lblPlaceholder.Visible = false;
                btnAddPlan.Visible = false;

                foreach (var plan in plans)
                {
                    var activities = DatabaseHelper.GetActivitiesByPlanId(plan.Id);
                    foreach (var act in activities)
                    {
                        lstActivities.Items.Add($"{act.ActivityName} ({act.StartTime:HH:mm} - {act.EndTime:HH:mm})");
                    }
                }
            }
            else
            {
                lstActivities.Visible = false;
                lblPlaceholder.Visible = true;
                btnAddPlan.Visible = true;
            }
        }

        private void btnAddPlan_Click(object sender, EventArgs e)
        {
            if (selectedDate == null)
            {
                MessageBox.Show("Please select a date first.");
                return;
            }

            var planForm = new FormPlan(userId, selectedDate.Value);
            planForm.ShowDialog();

            // Refresh after closing
            GenerateWeekDaysUI();
        }
    }
}