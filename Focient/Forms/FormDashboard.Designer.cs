namespace Focient.Forms
{
    partial class FormDashboard
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblAppName = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblMonth = new System.Windows.Forms.Label();
            this.lblWeek = new System.Windows.Forms.Label();
            this.flowLayoutDates = new System.Windows.Forms.FlowLayoutPanel();
            this.panelActivities = new System.Windows.Forms.Panel();
            this.lblPlaceholder = new System.Windows.Forms.Label();
            this.lstActivities = new System.Windows.Forms.ListBox();
            this.btnAddPlan = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // lblAppName
            this.lblAppName.AutoSize = true;
            this.lblAppName.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblAppName.Location = new System.Drawing.Point(20, 10);
            this.lblAppName.Name = "lblAppName";
            this.lblAppName.Text = "FOCIENT";

            // lblSubtitle
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitle.Location = new System.Drawing.Point(20, 45);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Text = "Simple and Efficient to gain your focus";

            // lblMonth
            this.lblMonth.AutoSize = true;
            this.lblMonth.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblMonth.Location = new System.Drawing.Point(20, 80);
            this.lblMonth.Name = "lblMonth";
            this.lblMonth.Text = "July 2025";

            // lblWeek
            this.lblWeek.AutoSize = true;
            this.lblWeek.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblWeek.Location = new System.Drawing.Point(250, 85);
            this.lblWeek.Name = "lblWeek";
            this.lblWeek.Text = "Week 2";

            // flowLayoutDates
            this.flowLayoutDates.Location = new System.Drawing.Point(20, 120);
            this.flowLayoutDates.Name = "flowLayoutDates";
            this.flowLayoutDates.Size = new System.Drawing.Size(600, 70);

            // panelActivities
            this.panelActivities.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelActivities.Location = new System.Drawing.Point(20, 210);
            this.panelActivities.Name = "panelActivities";
            this.panelActivities.Size = new System.Drawing.Size(600, 300);

            // lblPlaceholder
            this.lblPlaceholder.AutoSize = true;
            this.lblPlaceholder.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblPlaceholder.Location = new System.Drawing.Point(150, 350);
            this.lblPlaceholder.Name = "lblPlaceholder";
            this.lblPlaceholder.Text = "Please choose the day that you already plan";

            // lstActivities
            this.lstActivities.Location = new System.Drawing.Point(30, 220);
            this.lstActivities.Name = "lstActivities";
            this.lstActivities.Size = new System.Drawing.Size(580, 280);
            this.lstActivities.Visible = false;

            // btnAddPlan
            this.btnAddPlan.Location = new System.Drawing.Point(20, 520);
            this.btnAddPlan.Name = "btnAddPlan";
            this.btnAddPlan.Size = new System.Drawing.Size(300, 30);
            this.btnAddPlan.Text = "Add Plan for Selected Date";
            this.btnAddPlan.UseVisualStyleBackColor = true;
            this.btnAddPlan.Click += new System.EventHandler(this.btnAddPlan_Click);

            // FormDashboard
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(640, 580);
            this.Controls.Add(this.lblAppName);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblMonth);
            this.Controls.Add(this.lblWeek);
            this.Controls.Add(this.flowLayoutDates);
            this.Controls.Add(this.panelActivities);
            this.Controls.Add(this.lblPlaceholder);
            this.Controls.Add(this.lstActivities);
            this.Controls.Add(this.btnAddPlan);
            this.Name = "FormDashboard";
            this.Text = "Dashboard";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblAppName;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblMonth;
        private System.Windows.Forms.Label lblWeek;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutDates;
        private System.Windows.Forms.Panel panelActivities;
        private System.Windows.Forms.Label lblPlaceholder;
        private System.Windows.Forms.ListBox lstActivities;
        private System.Windows.Forms.Button btnAddPlan;
    }
}