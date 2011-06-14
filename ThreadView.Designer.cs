// Copyright © 2011 Parttimenerd, licenced under the GPL Version 3 

namespace TinyTaskkill
{
	partial class ThreadView
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ThreadView));
			this.dataGridView = new System.Windows.Forms.DataGridView();
			this.Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.CPU_Auslastung = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.UProzessorzeit = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.TProzessorzeit = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Wartegrund = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.fontDialog = new System.Windows.Forms.FontDialog();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridView
			// 
			this.dataGridView.AllowUserToAddRows = false;
			this.dataGridView.AllowUserToDeleteRows = false;
			this.dataGridView.AllowUserToOrderColumns = true;
			this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
			this.dataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
			this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
									this.Name,
									this.Status,
									this.Column1,
									this.CPU_Auslastung,
									this.UProzessorzeit,
									this.TProzessorzeit,
									this.Wartegrund});
			this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridView.Location = new System.Drawing.Point(0, 0);
			this.dataGridView.Name = "dataGridView";
			this.dataGridView.ReadOnly = true;
			this.dataGridView.RowTemplate.ReadOnly = true;
			this.dataGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView.Size = new System.Drawing.Size(445, 244);
			this.dataGridView.TabIndex = 0;
			this.dataGridView.KeyUp += new System.Windows.Forms.KeyEventHandler(this.DataGridViewKeyUp);
			// 
			// Name
			// 
			this.Name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.Name.HeaderText = GUITexts.get("ID");
			this.Name.Name = GUITexts.get("Name");
			this.Name.ReadOnly = true;
			this.Name.ToolTipText = GUITexts.get("ID of the thread");
			this.Name.Width = 60;
			// 
			// Status
			// 
			this.Status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.Status.HeaderText = GUITexts.get("State");
			this.Status.Name = GUITexts.get("State");
			this.Status.ReadOnly = true;
            this.Status.ToolTipText = GUITexts.get("State of the thread");
			this.Status.Width = 62;
			// 
			// Column1
			// 
            this.Column1.HeaderText = GUITexts.get("Priority");
			this.Column1.Name = "Column1";
			this.Column1.ReadOnly = true;
			this.Column1.ToolTipText = GUITexts.get("Priority of the thread");
			this.Column1.Width = 67;
			// 
			// CPU_Auslastung
			// 
			this.CPU_Auslastung.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.CPU_Auslastung.HeaderText = GUITexts.get("CPU usage");
			this.CPU_Auslastung.Name = GUITexts.get("CPU usage");
			this.CPU_Auslastung.ReadOnly = true;
            this.CPU_Auslastung.ToolTipText = GUITexts.get("CPU usage of the thread");
			this.CPU_Auslastung.Width = 109;
			// 
			// UProzessorzeit
			// 
			this.UProzessorzeit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.UProzessorzeit.HeaderText = GUITexts.get("UProcessortime");
			this.UProzessorzeit.Name = GUITexts.get("UProcessortime");
			this.UProzessorzeit.ReadOnly = true;
            this.UProzessorzeit.ToolTipText = GUITexts.get("Total UserProcessortime");
			this.UProzessorzeit.Width = 102;
			// 
			// TProzessorzeit
			// 
			this.TProzessorzeit.HeaderText = GUITexts.get("Processortime");
			this.TProzessorzeit.Name = GUITexts.get("Processortime");
			this.TProzessorzeit.ReadOnly = true;
            this.TProzessorzeit.ToolTipText = GUITexts.get("Total Processortime");
			this.TProzessorzeit.Width = 101;
			// 
			// Wartegrund
			// 
			this.Wartegrund.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Wartegrund.HeaderText = GUITexts.get("Reason for waiting");
            this.Wartegrund.Name = GUITexts.get("Reason, why the thread is waiting");
			this.Wartegrund.ReadOnly = true;
			this.Wartegrund.ToolTipText = "";
			this.Wartegrund.Width = 88;
			// 
			// ThreadView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.ClientSize = new System.Drawing.Size(445, 244);
			this.Controls.Add(this.dataGridView);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(600, 300);
	//		this.Name = "ThreadView";
			this.Text = "ThreadView";
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.DataGridViewKeyUp);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.FontDialog fontDialog;
		private System.Windows.Forms.DataGridViewTextBoxColumn Wartegrund;
		private System.Windows.Forms.DataGridViewTextBoxColumn Status;
		private System.Windows.Forms.DataGridViewTextBoxColumn TProzessorzeit;
		private System.Windows.Forms.DataGridViewTextBoxColumn UProzessorzeit;
		private System.Windows.Forms.DataGridViewTextBoxColumn CPU_Auslastung;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Name;
		private System.Windows.Forms.DataGridView dataGridView;
	}
}
