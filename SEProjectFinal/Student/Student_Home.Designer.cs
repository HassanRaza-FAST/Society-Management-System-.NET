namespace SEProjectFinal
{
    partial class Student_Home
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
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.Viewing_societies_label = new System.Windows.Forms.Label();
            this.Viewing_Socities_Grid = new System.Windows.Forms.DataGridView();
            this.exitbtn = new System.Windows.Forms.Button();
            this.createSocietylbl = new System.Windows.Forms.LinkLabel();
            this.joinsocietybtn = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.Viewing_Socities_Grid)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Welcome Student";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(713, 27);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Log out";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(30, 119);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(76, 13);
            this.linkLabel1.TabIndex = 3;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "View Societies";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // Viewing_societies_label
            // 
            this.Viewing_societies_label.AutoSize = true;
            this.Viewing_societies_label.Location = new System.Drawing.Point(377, 100);
            this.Viewing_societies_label.Name = "Viewing_societies_label";
            this.Viewing_societies_label.Size = new System.Drawing.Size(62, 13);
            this.Viewing_societies_label.TabIndex = 4;
            this.Viewing_societies_label.Text = "All societies";
            // 
            // Viewing_Socities_Grid
            // 
            this.Viewing_Socities_Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Viewing_Socities_Grid.Location = new System.Drawing.Point(237, 141);
            this.Viewing_Socities_Grid.Name = "Viewing_Socities_Grid";
            this.Viewing_Socities_Grid.Size = new System.Drawing.Size(369, 192);
            this.Viewing_Socities_Grid.TabIndex = 5;
            // 
            // exitbtn
            // 
            this.exitbtn.Location = new System.Drawing.Point(713, 90);
            this.exitbtn.Name = "exitbtn";
            this.exitbtn.Size = new System.Drawing.Size(75, 23);
            this.exitbtn.TabIndex = 6;
            this.exitbtn.Text = "Exit";
            this.exitbtn.UseVisualStyleBackColor = true;
            this.exitbtn.Click += new System.EventHandler(this.exitbtn_Click);
            // 
            // createSocietylbl
            // 
            this.createSocietylbl.AutoSize = true;
            this.createSocietylbl.Location = new System.Drawing.Point(33, 156);
            this.createSocietylbl.Name = "createSocietylbl";
            this.createSocietylbl.Size = new System.Drawing.Size(84, 13);
            this.createSocietylbl.TabIndex = 8;
            this.createSocietylbl.TabStop = true;
            this.createSocietylbl.Text = "Create Societies";
            this.createSocietylbl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.createSocietylbl_LinkClicked);
            // 
            // joinsocietybtn
            // 
            this.joinsocietybtn.AutoSize = true;
            this.joinsocietybtn.Location = new System.Drawing.Point(33, 194);
            this.joinsocietybtn.Name = "joinsocietybtn";
            this.joinsocietybtn.Size = new System.Drawing.Size(72, 13);
            this.joinsocietybtn.TabIndex = 9;
            this.joinsocietybtn.TabStop = true;
            this.joinsocietybtn.Text = "Join Societies";
            this.joinsocietybtn.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.joinsocietybtn_LinkClicked);
            // 
            // Student_Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.joinsocietybtn);
            this.Controls.Add(this.createSocietylbl);
            this.Controls.Add(this.exitbtn);
            this.Controls.Add(this.Viewing_Socities_Grid);
            this.Controls.Add(this.Viewing_societies_label);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Name = "Student_Home";
            this.Text = "Student_Home";
            this.Load += new System.EventHandler(this.Student_Home_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Viewing_Socities_Grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label Viewing_societies_label;
        private System.Windows.Forms.DataGridView Viewing_Socities_Grid;
        private System.Windows.Forms.Button exitbtn;
        private System.Windows.Forms.LinkLabel createSocietylbl;
        private System.Windows.Forms.LinkLabel joinsocietybtn;
    }
}