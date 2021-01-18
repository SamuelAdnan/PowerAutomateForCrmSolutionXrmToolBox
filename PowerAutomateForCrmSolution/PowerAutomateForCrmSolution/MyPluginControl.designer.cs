
namespace PowerAutomateForCrmSolution
{
	partial class MyPluginControl
	{
		/// <summary> 
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Nettoyage des ressources utilisées.
		/// </summary>
		/// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Code généré par le Concepteur de composants

		/// <summary> 
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.toolStripMenu = new System.Windows.Forms.ToolStrip();
			this.tsbClose = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.listViewTriggers = new System.Windows.Forms.ListView();
			this.listViewActions = new System.Windows.Forms.ListView();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.listViewsolflows = new System.Windows.Forms.ListView();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.listViewFlows = new System.Windows.Forms.ListView();
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnflowowner = new System.Windows.Forms.Button();
			this.btnshareflow = new System.Windows.Forms.Button();
			this.btnunshare = new System.Windows.Forms.Button();
			this.lblerror = new System.Windows.Forms.Label();
			this.btnaddflowtosol = new System.Windows.Forms.Button();
			this.btncreatesolution = new System.Windows.Forms.Button();
			this.cmbpub = new System.Windows.Forms.ComboBox();
			this.txtsol = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.cmbSolutions = new System.Windows.Forms.ComboBox();
			this.btnNewSolution = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.txtboxflowname = new System.Windows.Forms.TextBox();
			this.btnCreateflow = new System.Windows.Forms.Button();
			this.pnlsol = new System.Windows.Forms.Panel();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.cmbusers = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.toolTipshareflow = new System.Windows.Forms.ToolTip(this.components);
			this.toolTipflow = new System.Windows.Forms.ToolTip(this.components);
			this.toolStripMenu.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.panel1.SuspendLayout();
			this.pnlsol.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStripMenu
			// 
			this.toolStripMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClose,
            this.toolStripSeparator3});
			this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
			this.toolStripMenu.Name = "toolStripMenu";
			this.toolStripMenu.Size = new System.Drawing.Size(1276, 25);
			this.toolStripMenu.TabIndex = 4;
			this.toolStripMenu.Text = "toolStrip1";
			// 
			// tsbClose
			// 
			this.tsbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbClose.Name = "tsbClose";
			this.tsbClose.Size = new System.Drawing.Size(86, 22);
			this.tsbClose.Text = "Close this tool";
			this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// timer1
			// 
			this.timer1.Interval = 250;
			// 
			// listViewTriggers
			// 
			this.listViewTriggers.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewTriggers.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.listViewTriggers.HideSelection = false;
			this.listViewTriggers.Location = new System.Drawing.Point(3, 8);
			this.listViewTriggers.MultiSelect = false;
			this.listViewTriggers.Name = "listViewTriggers";
			this.listViewTriggers.Size = new System.Drawing.Size(313, 711);
			this.listViewTriggers.TabIndex = 2;
			this.listViewTriggers.UseCompatibleStateImageBehavior = false;
			this.listViewTriggers.View = System.Windows.Forms.View.Tile;
			// 
			// listViewActions
			// 
			this.listViewActions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewActions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.listViewActions.HideSelection = false;
			this.listViewActions.Location = new System.Drawing.Point(322, 8);
			this.listViewActions.MultiSelect = false;
			this.listViewActions.Name = "listViewActions";
			this.listViewActions.Size = new System.Drawing.Size(313, 711);
			this.listViewActions.TabIndex = 3;
			this.listViewActions.UseCompatibleStateImageBehavior = false;
			this.listViewActions.View = System.Windows.Forms.View.Tile;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 4;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.06266F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.06266F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.06266F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.81203F));
			this.tableLayoutPanel2.Controls.Add(this.listViewTriggers, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.listViewActions, 1, 1);
			this.tableLayoutPanel2.Controls.Add(this.listViewsolflows, 3, 1);
			this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 2, 1);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 108);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 2;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(1276, 722);
			this.tableLayoutPanel2.TabIndex = 6;
			this.tableLayoutPanel2.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel2_Paint);
			// 
			// listViewsolflows
			// 
			this.listViewsolflows.BackColor = System.Drawing.Color.WhiteSmoke;
			this.listViewsolflows.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewsolflows.Enabled = false;
			this.listViewsolflows.HideSelection = false;
			this.listViewsolflows.Location = new System.Drawing.Point(960, 8);
			this.listViewsolflows.MultiSelect = false;
			this.listViewsolflows.Name = "listViewsolflows";
			this.listViewsolflows.Size = new System.Drawing.Size(313, 711);
			this.listViewsolflows.TabIndex = 5;
			this.listViewsolflows.UseCompatibleStateImageBehavior = false;
			this.listViewsolflows.View = System.Windows.Forms.View.Tile;
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.ColumnCount = 1;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 313F));
			this.tableLayoutPanel3.Controls.Add(this.listViewFlows, 0, 1);
			this.tableLayoutPanel3.Controls.Add(this.panel1, 0, 0);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(641, 8);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 2;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(313, 711);
			this.tableLayoutPanel3.TabIndex = 6;
			// 
			// listViewFlows
			// 
			this.listViewFlows.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewFlows.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.listViewFlows.HideSelection = false;
			this.listViewFlows.Location = new System.Drawing.Point(3, 38);
			this.listViewFlows.MultiSelect = false;
			this.listViewFlows.Name = "listViewFlows";
			this.listViewFlows.Size = new System.Drawing.Size(307, 670);
			this.listViewFlows.TabIndex = 5;
			this.listViewFlows.UseCompatibleStateImageBehavior = false;
			this.listViewFlows.View = System.Windows.Forms.View.Tile;
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tableLayoutPanel3.SetColumnSpan(this.panel1, 2);
			this.panel1.Controls.Add(this.btnflowowner);
			this.panel1.Controls.Add(this.btnshareflow);
			this.panel1.Controls.Add(this.btnunshare);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(3, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(307, 29);
			this.panel1.TabIndex = 6;
			// 
			// btnflowowner
			// 
			this.btnflowowner.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnflowowner.Location = new System.Drawing.Point(175, 1);
			this.btnflowowner.Name = "btnflowowner";
			this.btnflowowner.Size = new System.Drawing.Size(100, 23);
			this.btnflowowner.TabIndex = 10;
			this.btnflowowner.Text = "&Change &owner";
			this.btnflowowner.UseVisualStyleBackColor = true;
			this.btnflowowner.Click += new System.EventHandler(this.btnflowowner_Click);
			// 
			// btnshareflow
			// 
			this.btnshareflow.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnshareflow.Location = new System.Drawing.Point(3, 2);
			this.btnshareflow.Name = "btnshareflow";
			this.btnshareflow.Size = new System.Drawing.Size(75, 23);
			this.btnshareflow.TabIndex = 8;
			this.btnshareflow.Text = "Sha&re flow";
			this.btnshareflow.UseVisualStyleBackColor = true;
			this.btnshareflow.Click += new System.EventHandler(this.btnshareflow_Click_1);
			// 
			// btnunshare
			// 
			this.btnunshare.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnunshare.Location = new System.Drawing.Point(84, 1);
			this.btnunshare.Name = "btnunshare";
			this.btnunshare.Size = new System.Drawing.Size(85, 23);
			this.btnunshare.TabIndex = 9;
			this.btnunshare.Text = "&Unshare flow";
			this.btnunshare.UseVisualStyleBackColor = true;
			this.btnunshare.Click += new System.EventHandler(this.btnunshare_Click);
			// 
			// lblerror
			// 
			this.lblerror.AutoSize = true;
			this.lblerror.Location = new System.Drawing.Point(920, 9);
			this.lblerror.Name = "lblerror";
			this.lblerror.Size = new System.Drawing.Size(0, 13);
			this.lblerror.TabIndex = 7;
			// 
			// btnaddflowtosol
			// 
			this.btnaddflowtosol.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.btnaddflowtosol.Enabled = false;
			this.btnaddflowtosol.Location = new System.Drawing.Point(538, 54);
			this.btnaddflowtosol.Name = "btnaddflowtosol";
			this.btnaddflowtosol.Size = new System.Drawing.Size(162, 23);
			this.btnaddflowtosol.TabIndex = 10;
			this.btnaddflowtosol.Text = "&Add Flow to selected solution";
			this.btnaddflowtosol.UseVisualStyleBackColor = true;
			this.btnaddflowtosol.Click += new System.EventHandler(this.btnaddflowtosol_Click);
			// 
			// btncreatesolution
			// 
			this.btncreatesolution.Location = new System.Drawing.Point(416, 2);
			this.btncreatesolution.Name = "btncreatesolution";
			this.btncreatesolution.Size = new System.Drawing.Size(137, 23);
			this.btncreatesolution.TabIndex = 2;
			this.btncreatesolution.Text = "&Create Solution";
			this.btncreatesolution.UseVisualStyleBackColor = true;
			this.btncreatesolution.Click += new System.EventHandler(this.btncreatesolution_Click);
			// 
			// cmbpub
			// 
			this.cmbpub.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbpub.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.cmbpub.FormattingEnabled = true;
			this.cmbpub.Location = new System.Drawing.Point(207, 3);
			this.cmbpub.Name = "cmbpub";
			this.cmbpub.Size = new System.Drawing.Size(202, 21);
			this.cmbpub.TabIndex = 1;
			// 
			// txtsol
			// 
			this.txtsol.Location = new System.Drawing.Point(4, 3);
			this.txtsol.Name = "txtsol";
			this.txtsol.Size = new System.Drawing.Size(197, 20);
			this.txtsol.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(81, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Select Solution:";
			// 
			// cmbSolutions
			// 
			this.cmbSolutions.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.cmbSolutions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbSolutions.FormattingEnabled = true;
			this.cmbSolutions.Location = new System.Drawing.Point(93, 13);
			this.cmbSolutions.Name = "cmbSolutions";
			this.cmbSolutions.Size = new System.Drawing.Size(289, 21);
			this.cmbSolutions.TabIndex = 1;
			this.cmbSolutions.SelectedIndexChanged += new System.EventHandler(this.cmbSolutions_SelectedIndexChanged);
			// 
			// btnNewSolution
			// 
			this.btnNewSolution.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.btnNewSolution.Location = new System.Drawing.Point(394, 12);
			this.btnNewSolution.Margin = new System.Windows.Forms.Padding(9, 3, 3, 3);
			this.btnNewSolution.Name = "btnNewSolution";
			this.btnNewSolution.Size = new System.Drawing.Size(131, 23);
			this.btnNewSolution.TabIndex = 2;
			this.btnNewSolution.Text = "Add a new &Solution";
			this.btnNewSolution.UseVisualStyleBackColor = true;
			this.btnNewSolution.Click += new System.EventHandler(this.btnNewSolution_Click);
			// 
			// label2
			// 
			this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 59);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(61, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Flow name:";
			// 
			// txtboxflowname
			// 
			this.txtboxflowname.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.txtboxflowname.Enabled = false;
			this.txtboxflowname.Location = new System.Drawing.Point(93, 55);
			this.txtboxflowname.Name = "txtboxflowname";
			this.txtboxflowname.Size = new System.Drawing.Size(289, 20);
			this.txtboxflowname.TabIndex = 5;
			// 
			// btnCreateflow
			// 
			this.btnCreateflow.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.btnCreateflow.Enabled = false;
			this.btnCreateflow.Location = new System.Drawing.Point(394, 54);
			this.btnCreateflow.Margin = new System.Windows.Forms.Padding(9, 3, 3, 3);
			this.btnCreateflow.Name = "btnCreateflow";
			this.btnCreateflow.Size = new System.Drawing.Size(131, 23);
			this.btnCreateflow.TabIndex = 6;
			this.btnCreateflow.Text = "Create &PowerAutomate";
			this.btnCreateflow.UseVisualStyleBackColor = true;
			this.btnCreateflow.Click += new System.EventHandler(this.btnCreateflow_Click);
			// 
			// pnlsol
			// 
			this.pnlsol.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.tableLayoutPanel1.SetColumnSpan(this.pnlsol, 3);
			this.pnlsol.Controls.Add(this.btncreatesolution);
			this.pnlsol.Controls.Add(this.cmbpub);
			this.pnlsol.Controls.Add(this.txtsol);
			this.pnlsol.Location = new System.Drawing.Point(538, 12);
			this.pnlsol.Name = "pnlsol";
			this.pnlsol.Size = new System.Drawing.Size(589, 24);
			this.pnlsol.TabIndex = 9;
			this.pnlsol.Visible = false;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 6;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 295F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 182F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 474F));
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.cmbSolutions, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.btnNewSolution, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.txtboxflowname, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.btnCreateflow, 2, 1);
			this.tableLayoutPanel1.Controls.Add(this.pnlsol, 3, 0);
			this.tableLayoutPanel1.Controls.Add(this.btnaddflowtosol, 3, 1);
			this.tableLayoutPanel1.Controls.Add(this.cmbusers, 5, 1);
			this.tableLayoutPanel1.Controls.Add(this.label3, 4, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 25);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1276, 83);
			this.tableLayoutPanel1.TabIndex = 5;
			this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
			// 
			// cmbusers
			// 
			this.cmbusers.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.cmbusers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbusers.FormattingEnabled = true;
			this.cmbusers.Location = new System.Drawing.Point(805, 55);
			this.cmbusers.Name = "cmbusers";
			this.cmbusers.Size = new System.Drawing.Size(223, 21);
			this.cmbusers.TabIndex = 12;
			// 
			// label3
			// 
			this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(720, 59);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(72, 13);
			this.label3.TabIndex = 13;
			this.label3.Text = "System users:";
			// 
			// MyPluginControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.lblerror);
			this.Controls.Add(this.tableLayoutPanel2);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.toolStripMenu);
			this.Name = "MyPluginControl";
			this.Size = new System.Drawing.Size(1276, 830);
			this.Load += new System.EventHandler(this.MyPluginControl_Load);
			this.toolStripMenu.ResumeLayout(false);
			this.toolStripMenu.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel3.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.pnlsol.ResumeLayout(false);
			this.pnlsol.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.ToolStrip toolStripMenu;
		private System.Windows.Forms.ToolStripButton tsbClose;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.ListView listViewTriggers;
		private System.Windows.Forms.ListView listViewActions;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Label lblerror;
		private System.Windows.Forms.Button btnaddflowtosol;
		private System.Windows.Forms.Button btncreatesolution;
		private System.Windows.Forms.ComboBox cmbpub;
		private System.Windows.Forms.TextBox txtsol;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cmbSolutions;
		private System.Windows.Forms.Button btnNewSolution;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtboxflowname;
		private System.Windows.Forms.Button btnCreateflow;
		private System.Windows.Forms.Panel pnlsol;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.ListView listViewsolflows;
		private System.Windows.Forms.ComboBox cmbusers;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ToolTip toolTipshareflow;
		private System.Windows.Forms.Button btnshareflow;
		private System.Windows.Forms.Button btnunshare;
		private System.Windows.Forms.Button btnflowowner;
		private System.Windows.Forms.ToolTip toolTipflow;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.ListView listViewFlows;
		private System.Windows.Forms.Panel panel1;
	}
}
