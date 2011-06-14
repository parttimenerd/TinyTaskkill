// Copyright © 2011 Parttimenerd, licenced under the GPL Version 3

namespace TinyTaskkill
{
	partial class ServiceView
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServiceView));
			this.dataGridView = new System.Windows.Forms.DataGridView();
			this.Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Beschreibung = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Pausieren = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Stoppen = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Schließen = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.AbhängigeDienste = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.AbhänngigeVonDienste = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.fontDialog = new System.Windows.Forms.FontDialog();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridView
			// 
			this.dataGridView.AllowUserToAddRows = false;
			this.dataGridView.AllowUserToDeleteRows = false;
			this.dataGridView.AllowUserToOrderColumns = true;
			this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.dataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
			this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
									this.Name,
									this.Beschreibung,
									this.Status,
									this.Pausieren,
									this.Stoppen,
									this.Schließen,
									this.AbhängigeDienste,
									this.AbhänngigeVonDienste});
			this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.dataGridView.Location = new System.Drawing.Point(0, 0);
			this.dataGridView.Name = "dataGridView";
			this.dataGridView.ReadOnly = true;
			this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView.Size = new System.Drawing.Size(507, 342);
			this.dataGridView.StandardTab = true;
			this.dataGridView.TabIndex = 0;
			this.dataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1CellContentClick);
			this.dataGridView.KeyUp += new System.Windows.Forms.KeyEventHandler(this.DataGridViewKeyUp);
			// 
			// Name
			// 
			this.Name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.Name.HeaderText = GUITexts.get("Name");
			this.Name.Name = GUITexts.get("Name");
			this.Name.ReadOnly = true;
            this.Name.ToolTipText = GUITexts.get("Name of the service");
			this.Name.Width = 80;
			// 
			// Beschreibung
			// 
			this.Beschreibung.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Beschreibung.HeaderText = GUITexts.get("Description");
            this.Beschreibung.Name = GUITexts.get("Description");
			this.Beschreibung.ReadOnly = true;
            this.Beschreibung.ToolTipText = GUITexts.get("Description of the service");
			this.Beschreibung.Width = 120;
			// 
			// Status
			// 
			this.Status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.Status.HeaderText = GUITexts.get("State");
			this.Status.Name = GUITexts.get("State");
			this.Status.ReadOnly = true;
            this.Status.ToolTipText = GUITexts.get("State of the service");
			this.Status.Width = 62;
			// 
			// Pausieren
			// 
			this.Pausieren.HeaderText = GUITexts.get("Pause");
			this.Pausieren.Name = GUITexts.get("Pause");
			this.Pausieren.ReadOnly = true;
			this.Pausieren.Visible = false;
			this.Pausieren.Width = 79;
			// 
			// Stoppen
			// 
			this.Stoppen.HeaderText = GUITexts.get("Stop");
			this.Stoppen.Name = GUITexts.get("Stop");
			this.Stoppen.ReadOnly = true;
			this.Stoppen.Visible = false;
			this.Stoppen.Width = 72;
			// 
			// Schließen
			// 
			this.Schließen.HeaderText = GUITexts.get("Abort");
			this.Schließen.Name = GUITexts.get("Abort");
			this.Schließen.ReadOnly = true;
			this.Schließen.Visible = false;
			this.Schließen.Width = 79;
			// 
			// AbhängigeDienste
			// 
			this.AbhängigeDienste.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.AbhängigeDienste.HeaderText = GUITexts.get("Depending services");
            this.AbhängigeDienste.Name = GUITexts.get("Depending services");
			this.AbhängigeDienste.ReadOnly = true;
            this.AbhängigeDienste.ToolTipText = GUITexts.get("Services which are dependent on this one");
			this.AbhängigeDienste.Width = 112;
			// 
			// AbhänngigeVonDienste
			// 
            this.AbhänngigeVonDienste.HeaderText = GUITexts.get("Dependents on services");
            this.AbhänngigeVonDienste.Name = GUITexts.get("Dependents on services");
			this.AbhänngigeVonDienste.ReadOnly = true;
            this.AbhänngigeVonDienste.ToolTipText = GUITexts.get("Services, this one depents on");
			this.AbhänngigeVonDienste.Width = 136;
			// 
			// ServiceView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(507, 342);
			this.Controls.Add(this.dataGridView);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Text = "ServiceView";
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.DataGridViewKeyUp);
			this.Resize += new System.EventHandler(this.ServiceViewResize);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.FontDialog fontDialog;
		private System.Windows.Forms.DataGridViewTextBoxColumn Schließen;
		private System.Windows.Forms.DataGridViewTextBoxColumn Stoppen;
		private System.Windows.Forms.DataGridViewTextBoxColumn Pausieren;
		private System.Windows.Forms.DataGridViewTextBoxColumn AbhänngigeVonDienste;
		private System.Windows.Forms.DataGridView dataGridView;
		private System.Windows.Forms.DataGridViewTextBoxColumn AbhängigeDienste;
		private System.Windows.Forms.DataGridViewTextBoxColumn Beschreibung;
		private System.Windows.Forms.DataGridViewTextBoxColumn Status;
		private System.Windows.Forms.DataGridViewTextBoxColumn Name;
	}
}
