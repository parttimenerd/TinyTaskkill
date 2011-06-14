/*
 * Erstellt mit SharpDevelop.
 * Benutzer: Johannes
 * Datum: 05.03.2011
 * Zeit: 16:19
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */
namespace TinyTaskkill
{
	partial class ProcessView
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessView));
			this.dataGridView = new System.Windows.Forms.DataGridView();
			this.Inhalt = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.comboBox = new System.Windows.Forms.ComboBox();
			this.close_button = new System.Windows.Forms.Button();
			this.threadview_button = new System.Windows.Forms.Button();
			this.fontDialog = new System.Windows.Forms.FontDialog();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridView
			// 
			this.dataGridView.AllowUserToAddRows = false;
			this.dataGridView.AllowUserToDeleteRows = false;
			this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.dataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
			this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
									this.Inhalt});
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridView.DefaultCellStyle = dataGridViewCellStyle2;
			this.dataGridView.Location = new System.Drawing.Point(0, 25);
			this.dataGridView.MultiSelect = false;
			this.dataGridView.Name = "dataGridView";
			this.dataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
			this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView.Size = new System.Drawing.Size(292, 268);
			this.dataGridView.TabIndex = 0;
			this.dataGridView.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.DataGridViewRowStateChanged);
			this.dataGridView.KeyUp += new System.Windows.Forms.KeyEventHandler(this.comboBox_KeyUp);
			// 
			// Inhalt
			// 
			this.Inhalt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.Inhalt.FillWeight = 300F;
			this.Inhalt.HeaderText = "Inhalt";
			this.Inhalt.MaxInputLength = 200;
			this.Inhalt.MinimumWidth = 100;
			this.Inhalt.Name = "Inhalt";
			this.Inhalt.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// comboBox
			// 
			this.comboBox.FormattingEnabled = true;
			this.comboBox.Location = new System.Drawing.Point(0, 0);
			this.comboBox.Name = "comboBox";
			this.comboBox.Size = new System.Drawing.Size(295, 21);
			this.comboBox.Sorted = true;
			this.comboBox.TabIndex = 1;
			this.comboBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.comboBox_KeyUp);
			// 
			// close_button
			// 
			this.close_button.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.close_button.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.close_button.Location = new System.Drawing.Point(0, 299);
			this.close_button.Name = "close_button";
			this.close_button.Size = new System.Drawing.Size(154, 22);
			this.close_button.TabIndex = 2;
			this.close_button.Text = "Abmelden";
			this.close_button.UseVisualStyleBackColor = true;
			this.close_button.Click += new System.EventHandler(this.Close_buttonClick);
			// 
			// threadview_button
			// 
			this.threadview_button.Location = new System.Drawing.Point(160, 299);
			this.threadview_button.Name = "threadview_button";
			this.threadview_button.Size = new System.Drawing.Size(132, 22);
			this.threadview_button.TabIndex = 4;
			this.threadview_button.Text = "ThreadView";
			this.threadview_button.UseVisualStyleBackColor = true;
			this.threadview_button.Click += new System.EventHandler(this.Threadview_buttonClick);
			// 
			// ProcessView
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.CancelButton = this.close_button;
			this.ClientSize = new System.Drawing.Size(294, 325);
			this.Controls.Add(this.threadview_button);
			this.Controls.Add(this.close_button);
			this.Controls.Add(this.comboBox);
			this.Controls.Add(this.dataGridView);
			this.Cursor = System.Windows.Forms.Cursors.Default;
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(600, 700);
			this.MinimumSize = new System.Drawing.Size(150, 200);
			this.Name = "ProcessView";
			this.Text = "ProcessView";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProcessViewFormClosing);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.comboBox_KeyUp);
			this.Resize += new System.EventHandler(this.ProcessViewResize);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.FontDialog fontDialog;
		private System.Windows.Forms.Button threadview_button;
		private System.Windows.Forms.Button close_button;
		private System.Windows.Forms.ComboBox comboBox;
		private System.Windows.Forms.DataGridViewTextBoxColumn Inhalt;
		private System.Windows.Forms.DataGridView dataGridView;
	}
}
