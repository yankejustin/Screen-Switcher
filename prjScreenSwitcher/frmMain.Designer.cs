namespace prjScreenSwitcher
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();

                ProcStartWatch.Stop();
                ProcStartWatch.EventArrived -= ProcessWatcher_EventArrived;
                ProcStartWatch.Dispose();

                ProcStopWatch.Stop();
                ProcStopWatch.EventArrived -= ProcessWatcher_EventArrived;
                ProcStopWatch.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lstProcesses = new System.Windows.Forms.ListBox();
            this.grpCommands = new System.Windows.Forms.GroupBox();
            this.cboMonitors = new System.Windows.Forms.ComboBox();
            this.lblMonitors = new System.Windows.Forms.Label();
            this.lblProcessName = new System.Windows.Forms.Label();
            this.btnGetProcessInfo = new System.Windows.Forms.Button();
            this.txtProcessName = new System.Windows.Forms.TextBox();
            this.ToolTips = new System.Windows.Forms.ToolTip(this.components);
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bmpScreenshot = new System.Windows.Forms.PictureBox();
            this.grpCommands.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bmpScreenshot)).BeginInit();
            this.SuspendLayout();
            // 
            // lstProcesses
            // 
            this.lstProcesses.FormattingEnabled = true;
            this.lstProcesses.ItemHeight = 18;
            this.lstProcesses.Location = new System.Drawing.Point(12, 13);
            this.lstProcesses.Name = "lstProcesses";
            this.lstProcesses.ScrollAlwaysVisible = true;
            this.lstProcesses.Size = new System.Drawing.Size(195, 436);
            this.lstProcesses.TabIndex = 0;
            this.lstProcesses.DoubleClick += new System.EventHandler(this.lstProcesses_DoubleClick);
            // 
            // grpCommands
            // 
            this.grpCommands.Controls.Add(this.bmpScreenshot);
            this.grpCommands.Controls.Add(this.cboMonitors);
            this.grpCommands.Controls.Add(this.lblMonitors);
            this.grpCommands.Location = new System.Drawing.Point(231, 45);
            this.grpCommands.Name = "grpCommands";
            this.grpCommands.Size = new System.Drawing.Size(551, 497);
            this.grpCommands.TabIndex = 2;
            this.grpCommands.TabStop = false;
            this.grpCommands.Text = "Window Options";
            // 
            // cboMonitors
            // 
            this.cboMonitors.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMonitors.FormattingEnabled = true;
            this.cboMonitors.Location = new System.Drawing.Point(74, 56);
            this.cboMonitors.Name = "cboMonitors";
            this.cboMonitors.Size = new System.Drawing.Size(151, 26);
            this.cboMonitors.TabIndex = 3;
            this.cboMonitors.SelectedIndexChanged += new System.EventHandler(this.cboMonitors_SelectedIndexChanged);
            // 
            // lblMonitors
            // 
            this.lblMonitors.AutoSize = true;
            this.lblMonitors.Location = new System.Drawing.Point(82, 35);
            this.lblMonitors.Name = "lblMonitors";
            this.lblMonitors.Size = new System.Drawing.Size(134, 18);
            this.lblMonitors.TabIndex = 2;
            this.lblMonitors.Text = "Current Monitor";
            this.lblMonitors.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblProcessName
            // 
            this.lblProcessName.Location = new System.Drawing.Point(352, 11);
            this.lblProcessName.Name = "lblProcessName";
            this.lblProcessName.Size = new System.Drawing.Size(82, 21);
            this.lblProcessName.TabIndex = 3;
            this.lblProcessName.Text = "Process:";
            this.lblProcessName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnGetProcessInfo
            // 
            this.btnGetProcessInfo.Location = new System.Drawing.Point(12, 455);
            this.btnGetProcessInfo.Name = "btnGetProcessInfo";
            this.btnGetProcessInfo.Size = new System.Drawing.Size(195, 41);
            this.btnGetProcessInfo.TabIndex = 0;
            this.btnGetProcessInfo.Text = "Update Processes";
            this.btnGetProcessInfo.UseVisualStyleBackColor = true;
            this.btnGetProcessInfo.Click += new System.EventHandler(this.btnGetProcessInfo_Click);
            // 
            // txtProcessName
            // 
            this.txtProcessName.BackColor = System.Drawing.SystemColors.Window;
            this.txtProcessName.Location = new System.Drawing.Point(440, 8);
            this.txtProcessName.Name = "txtProcessName";
            this.txtProcessName.ReadOnly = true;
            this.txtProcessName.Size = new System.Drawing.Size(233, 26);
            this.txtProcessName.TabIndex = 4;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(48, 22);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // bmpScreenshot
            // 
            this.bmpScreenshot.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bmpScreenshot.Location = new System.Drawing.Point(6, 104);
            this.bmpScreenshot.Name = "bmpScreenshot";
            this.bmpScreenshot.Size = new System.Drawing.Size(540, 384);
            this.bmpScreenshot.TabIndex = 4;
            this.bmpScreenshot.TabStop = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 554);
            this.Controls.Add(this.txtProcessName);
            this.Controls.Add(this.lblProcessName);
            this.Controls.Add(this.grpCommands);
            this.Controls.Add(this.lstProcesses);
            this.Controls.Add(this.btnGetProcessInfo);
            this.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Screen Switcher";
            this.grpCommands.ResumeLayout(false);
            this.grpCommands.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bmpScreenshot)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstProcesses;
        private System.Windows.Forms.GroupBox grpCommands;
        private System.Windows.Forms.Label lblProcessName;
        private System.Windows.Forms.Button btnGetProcessInfo;
        private System.Windows.Forms.ToolTip ToolTips;
        private System.Windows.Forms.TextBox txtProcessName;
        private System.Windows.Forms.ComboBox cboMonitors;
        private System.Windows.Forms.Label lblMonitors;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.PictureBox bmpScreenshot;
    }
}

