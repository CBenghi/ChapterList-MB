namespace ChapterListMB
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.addChapterButton = new System.Windows.Forms.Button();
            this.removeChapterButton = new System.Windows.Forms.Button();
            this.shiftPositionBackButton = new System.Windows.Forms.Button();
            this.shiftPositionFwdButton = new System.Windows.Forms.Button();
            this.chaptersDGV = new System.Windows.Forms.DataGridView();
            this.ChapterStatus = new System.Windows.Forms.DataGridViewImageColumn();
            this.positionCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.titleCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.titleArtistStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.chaptersCountStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.comboBoxNewChapterName = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.txtAllTranscriptsFilter = new System.Windows.Forms.TextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.chaptersDGV)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // addChapterButton
            // 
            this.addChapterButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addChapterButton.Location = new System.Drawing.Point(7, 460);
            this.addChapterButton.Name = "addChapterButton";
            this.addChapterButton.Size = new System.Drawing.Size(40, 35);
            this.addChapterButton.TabIndex = 0;
            this.addChapterButton.Text = "Add";
            this.toolTip1.SetToolTip(this.addChapterButton, "Create a new chapter at the current player position.");
            this.addChapterButton.UseVisualStyleBackColor = true;
            this.addChapterButton.Click += new System.EventHandler(this.addChapterButton_Click);
            // 
            // removeChapterButton
            // 
            this.removeChapterButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.removeChapterButton.Location = new System.Drawing.Point(53, 460);
            this.removeChapterButton.Name = "removeChapterButton";
            this.removeChapterButton.Size = new System.Drawing.Size(57, 35);
            this.removeChapterButton.TabIndex = 2;
            this.removeChapterButton.Text = "Remove";
            this.toolTip1.SetToolTip(this.removeChapterButton, "Delete the selected chapter.");
            this.removeChapterButton.UseVisualStyleBackColor = true;
            this.removeChapterButton.Click += new System.EventHandler(this.removeChapterButton_Click);
            // 
            // shiftPositionBackButton
            // 
            this.shiftPositionBackButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.shiftPositionBackButton.Location = new System.Drawing.Point(386, 460);
            this.shiftPositionBackButton.Name = "shiftPositionBackButton";
            this.shiftPositionBackButton.Size = new System.Drawing.Size(40, 35);
            this.shiftPositionBackButton.TabIndex = 5;
            this.shiftPositionBackButton.Text = "<<";
            this.toolTip1.SetToolTip(this.shiftPositionBackButton, "Nudges the position of the selected chapter. backwards.,");
            this.shiftPositionBackButton.UseVisualStyleBackColor = true;
            this.shiftPositionBackButton.Click += new System.EventHandler(this.shiftPositionBackButton_Click);
            // 
            // shiftPositionFwdButton
            // 
            this.shiftPositionFwdButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.shiftPositionFwdButton.Location = new System.Drawing.Point(432, 460);
            this.shiftPositionFwdButton.Name = "shiftPositionFwdButton";
            this.shiftPositionFwdButton.Size = new System.Drawing.Size(40, 35);
            this.shiftPositionFwdButton.TabIndex = 6;
            this.shiftPositionFwdButton.Text = ">>";
            this.toolTip1.SetToolTip(this.shiftPositionFwdButton, "Nudges the position of the selected chapter. forwards,");
            this.shiftPositionFwdButton.UseVisualStyleBackColor = true;
            this.shiftPositionFwdButton.Click += new System.EventHandler(this.shiftPositionFwdButton_Click);
            // 
            // chaptersDGV
            // 
            this.chaptersDGV.AllowUserToAddRows = false;
            this.chaptersDGV.AllowUserToDeleteRows = false;
            this.chaptersDGV.AllowUserToResizeColumns = false;
            this.chaptersDGV.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            this.chaptersDGV.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.chaptersDGV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chaptersDGV.BackgroundColor = System.Drawing.SystemColors.Window;
            this.chaptersDGV.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.chaptersDGV.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.chaptersDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.chaptersDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ChapterStatus,
            this.positionCol,
            this.titleCol});
            this.chaptersDGV.Location = new System.Drawing.Point(8, 10);
            this.chaptersDGV.MinimumSize = new System.Drawing.Size(200, 200);
            this.chaptersDGV.MultiSelect = false;
            this.chaptersDGV.Name = "chaptersDGV";
            this.chaptersDGV.RowHeadersVisible = false;
            this.chaptersDGV.RowHeadersWidth = 30;
            this.chaptersDGV.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.chaptersDGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.chaptersDGV.Size = new System.Drawing.Size(464, 444);
            this.chaptersDGV.TabIndex = 7;
            this.chaptersDGV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.OnChaptersDgvCellClick);
            this.chaptersDGV.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.chaptersDGV_CellEndEdit);
            this.chaptersDGV.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.chaptersDGV_CellMouseDoubleClick);
            this.chaptersDGV.SelectionChanged += new System.EventHandler(this.OnChaptersDgvSelectionChanged);
            // 
            // ChapterStatus
            // 
            this.ChapterStatus.HeaderText = "";
            this.ChapterStatus.Name = "ChapterStatus";
            this.ChapterStatus.ReadOnly = true;
            this.ChapterStatus.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ChapterStatus.Width = 30;
            // 
            // positionCol
            // 
            this.positionCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.positionCol.DataPropertyName = "TimeCode";
            this.positionCol.HeaderText = "Position";
            this.positionCol.Name = "positionCol";
            this.positionCol.ReadOnly = true;
            this.positionCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.positionCol.Width = 50;
            // 
            // titleCol
            // 
            this.titleCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.titleCol.DataPropertyName = "Title";
            this.titleCol.HeaderText = "Chapter Title";
            this.titleCol.Name = "titleCol";
            this.titleCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.titleArtistStatusLabel,
            this.chaptersCountStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 542);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(509, 24);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // titleArtistStatusLabel
            // 
            this.titleArtistStatusLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.titleArtistStatusLabel.Name = "titleArtistStatusLabel";
            this.titleArtistStatusLabel.Size = new System.Drawing.Size(55, 19);
            this.titleArtistStatusLabel.Text = "titleArtist";
            // 
            // chaptersCountStatusLabel
            // 
            this.chaptersCountStatusLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.chaptersCountStatusLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.chaptersCountStatusLabel.Name = "chaptersCountStatusLabel";
            this.chaptersCountStatusLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chaptersCountStatusLabel.Size = new System.Drawing.Size(56, 19);
            this.chaptersCountStatusLabel.Text = "chapters";
            // 
            // comboBoxNewChapterName
            // 
            this.comboBoxNewChapterName.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.comboBoxNewChapterName.FormattingEnabled = true;
            this.comboBoxNewChapterName.Items.AddRange(new object[] {
            "<Default>",
            "Intro",
            "Verse",
            "Prechorus",
            "Chorus",
            "Bridge",
            "Solo",
            "Outro"});
            this.comboBoxNewChapterName.Location = new System.Drawing.Point(116, 468);
            this.comboBoxNewChapterName.Name = "comboBoxNewChapterName";
            this.comboBoxNewChapterName.Size = new System.Drawing.Size(85, 21);
            this.comboBoxNewChapterName.TabIndex = 9;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(11, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(486, 527);
            this.tabControl1.TabIndex = 10;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.pictureBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(478, 501);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Images";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(6, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(466, 489);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.txtFilter);
            this.tabPage3.Controls.Add(this.listBox1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(478, 501);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Transcript";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // txtFilter
            // 
            this.txtFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilter.Location = new System.Drawing.Point(12, 15);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(453, 20);
            this.txtFilter.TabIndex = 1;
            this.txtFilter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilter_KeyPress);
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 41);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(453, 446);
            this.listBox1.TabIndex = 0;
            this.listBox1.DoubleClick += new System.EventHandler(this.listBox1_DoubleClick);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.treeView1);
            this.tabPage4.Controls.Add(this.txtAllTranscriptsFilter);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(478, 501);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Search";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.FullRowSelect = true;
            this.treeView1.Location = new System.Drawing.Point(12, 39);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(453, 450);
            this.treeView1.TabIndex = 3;
            this.treeView1.DoubleClick += new System.EventHandler(this.treeView1_DoubleClick);
            // 
            // txtAllTranscriptsFilter
            // 
            this.txtAllTranscriptsFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAllTranscriptsFilter.Location = new System.Drawing.Point(12, 13);
            this.txtAllTranscriptsFilter.Name = "txtAllTranscriptsFilter";
            this.txtAllTranscriptsFilter.Size = new System.Drawing.Size(453, 20);
            this.txtAllTranscriptsFilter.TabIndex = 2;
            this.txtAllTranscriptsFilter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.chaptersDGV);
            this.tabPage1.Controls.Add(this.comboBoxNewChapterName);
            this.tabPage1.Controls.Add(this.shiftPositionFwdButton);
            this.tabPage1.Controls.Add(this.removeChapterButton);
            this.tabPage1.Controls.Add(this.shiftPositionBackButton);
            this.tabPage1.Controls.Add(this.addChapterButton);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(478, 501);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Chapters";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 566);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.MinimumSize = new System.Drawing.Size(325, 350);
            this.Name = "MainForm";
            this.Text = "SyncView";
            ((System.ComponentModel.ISupportInitialize)(this.chaptersDGV)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addChapterButton;
        private System.Windows.Forms.Button removeChapterButton;
        private System.Windows.Forms.Button shiftPositionBackButton;
        private System.Windows.Forms.Button shiftPositionFwdButton;
        private System.Windows.Forms.DataGridView chaptersDGV;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel titleArtistStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel chaptersCountStatusLabel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox comboBoxNewChapterName;
        private System.Windows.Forms.DataGridViewImageColumn ChapterStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn positionCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn titleCol;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TextBox txtAllTranscriptsFilter;
        private System.Windows.Forms.TreeView treeView1;
    }
}