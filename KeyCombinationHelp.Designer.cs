// Copyright © 2011 Parttimenerd, licenced under the GPL Version 3

namespace TinyTaskkill
{
	partial class KeyCombinationHelp
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
			this.dataGridView = new System.Windows.Forms.DataGridView();
			this.Kombination = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Ereignis = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridView
			// 
			this.dataGridView.AllowUserToAddRows = false;
			this.dataGridView.AllowUserToDeleteRows = false;
			this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.dataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
			this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
									this.Kombination,
									this.Ereignis});
			this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridView.Location = new System.Drawing.Point(0, 0);
			this.dataGridView.Name = "dataGridView";
			this.dataGridView.ReadOnly = true;
			this.dataGridView.RowHeadersVisible = false;
			this.dataGridView.Size = new System.Drawing.Size(284, 257);
			this.dataGridView.TabIndex = 0;
			// 
			// Kombination
			// 
			this.Kombination.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
			this.Kombination.HeaderText = GUITexts.get("Key");
			this.Kombination.Name = GUITexts.get("Combination");
			this.Kombination.ReadOnly = true;
			this.Kombination.Width = 59;
			// 
			// Ereignis
			// 
			this.Ereignis.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.Ereignis.HeaderText = GUITexts.get("Event");
			this.Ereignis.Name = GUITexts.get("Event");
			this.Ereignis.ReadOnly = true;
			// 
			// KeyCombinationHelp
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.ClientSize = new System.Drawing.Size(284, 257);
			this.Controls.Add(this.dataGridView);
			this.Icon = global::TinyTaskkill.Resourcen.Icon;
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(500, 600);
			this.MinimizeBox = false;
			this.Name = "KeyCombinationHelp";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = GUITexts.get("Help");
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.DataGridViewTextBoxColumn Ereignis;
		private System.Windows.Forms.DataGridViewTextBoxColumn Kombination;
		private System.Windows.Forms.DataGridView dataGridView;
	}
}
