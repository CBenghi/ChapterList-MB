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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.addChapterButton = new System.Windows.Forms.Button();
            this.removeChapterButton = new System.Windows.Forms.Button();
            this.shiftPositionBackButton = new System.Windows.Forms.Button();
            this.shiftPositionFwdButton = new System.Windows.Forms.Button();
            this.chaptersDGV = new System.Windows.Forms.DataGridView();
            this.ChapterStatus = new System.Windows.Forms.DataGridViewImageColumn();
            this.positionCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.titleCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblTopmost = new System.Windows.Forms.ToolStripStatusLabel();
            this.titleArtistStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.chaptersCountStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblImageTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblLirycsTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.comboBoxNewChapterName = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.reloadImagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jumpToNextImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyImagesAsLyricsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAllImagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmdSetNextSlideTime = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.cmdSetImageName = new System.Windows.Forms.Button();
            this.txtImageName = new System.Windows.Forms.TextBox();
            this.cmbImage = new System.Windows.Forms.ComboBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.chkFindSelect = new System.Windows.Forms.CheckBox();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyImageTimestampToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyLyricsTimestampToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.txtAllTranscriptsFilter = new System.Windows.Forms.TextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblNext = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlPointer = new System.Windows.Forms.Panel();
            this.chkShowPointer = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.chaptersDGV)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // addChapterButton
            // 
            this.addChapterButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addChapterButton.Location = new System.Drawing.Point(7, 475);
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
            this.removeChapterButton.Location = new System.Drawing.Point(53, 475);
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
            this.shiftPositionBackButton.Location = new System.Drawing.Point(409, 475);
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
            this.shiftPositionFwdButton.Location = new System.Drawing.Point(455, 475);
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
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            this.chaptersDGV.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
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
            this.chaptersDGV.Size = new System.Drawing.Size(487, 459);
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
            this.lblTopmost,
            this.titleArtistStatusLabel,
            this.chaptersCountStatusLabel,
            this.lblImageTime,
            this.lblLirycsTime,
            this.toolStripStatusLabel1,
            this.lblNext});
            this.statusStrip1.Location = new System.Drawing.Point(0, 542);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(509, 24);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblTopmost
            // 
            this.lblTopmost.Name = "lblTopmost";
            this.lblTopmost.Size = new System.Drawing.Size(63, 19);
            this.lblTopmost.Text = "Is topmost";
            this.lblTopmost.Click += new System.EventHandler(this.lblLyricsTime_Click);
            // 
            // titleArtistStatusLabel
            // 
            this.titleArtistStatusLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.titleArtistStatusLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.titleArtistStatusLabel.Name = "titleArtistStatusLabel";
            this.titleArtistStatusLabel.Size = new System.Drawing.Size(59, 19);
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
            // lblImageTime
            // 
            this.lblImageTime.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.lblImageTime.Name = "lblImageTime";
            this.lblImageTime.Size = new System.Drawing.Size(17, 19);
            this.lblImageTime.Text = "0";
            this.lblImageTime.Click += new System.EventHandler(this.lblImageTime_Click);
            // 
            // lblLirycsTime
            // 
            this.lblLirycsTime.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.lblLirycsTime.Name = "lblLirycsTime";
            this.lblLirycsTime.Size = new System.Drawing.Size(17, 19);
            this.lblLirycsTime.Text = "0";
            this.lblLirycsTime.Click += new System.EventHandler(this.toolStripStatusLabel2_Click);
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
            this.comboBoxNewChapterName.Location = new System.Drawing.Point(127, 483);
            this.comboBoxNewChapterName.Name = "comboBoxNewChapterName";
            this.comboBoxNewChapterName.Size = new System.Drawing.Size(85, 21);
            this.comboBoxNewChapterName.TabIndex = 9;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(509, 542);
            this.tabControl1.TabIndex = 10;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.pnlPointer);
            this.tabPage2.Controls.Add(this.pictureBox1);
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(501, 516);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Images";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.ContextMenuStrip = this.contextMenuStrip1;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(3, 24);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(495, 468);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reloadImagesToolStripMenuItem,
            this.jumpToNextImageToolStripMenuItem,
            this.copyImagesAsLyricsToolStripMenuItem,
            this.saveAllImagesToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(188, 92);
            // 
            // reloadImagesToolStripMenuItem
            // 
            this.reloadImagesToolStripMenuItem.Name = "reloadImagesToolStripMenuItem";
            this.reloadImagesToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.reloadImagesToolStripMenuItem.Text = "Reload images";
            this.reloadImagesToolStripMenuItem.Click += new System.EventHandler(this.reloadImagesToolStripMenuItem_Click);
            // 
            // jumpToNextImageToolStripMenuItem
            // 
            this.jumpToNextImageToolStripMenuItem.Name = "jumpToNextImageToolStripMenuItem";
            this.jumpToNextImageToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.jumpToNextImageToolStripMenuItem.Text = "Jump to next image";
            this.jumpToNextImageToolStripMenuItem.Click += new System.EventHandler(this.jumpToNextImageToolStripMenuItem_Click);
            // 
            // copyImagesAsLyricsToolStripMenuItem
            // 
            this.copyImagesAsLyricsToolStripMenuItem.Name = "copyImagesAsLyricsToolStripMenuItem";
            this.copyImagesAsLyricsToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.copyImagesAsLyricsToolStripMenuItem.Text = "Copy images as lyrics";
            this.copyImagesAsLyricsToolStripMenuItem.Click += new System.EventHandler(this.copyImagesAsLyricsToolStripMenuItem_Click);
            // 
            // saveAllImagesToolStripMenuItem
            // 
            this.saveAllImagesToolStripMenuItem.Name = "saveAllImagesToolStripMenuItem";
            this.saveAllImagesToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.saveAllImagesToolStripMenuItem.Text = "Save all images";
            this.saveAllImagesToolStripMenuItem.Click += new System.EventHandler(this.saveAllImagesToolStripMenuItem_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chkShowPointer);
            this.panel2.Controls.Add(this.cmdSetNextSlideTime);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 492);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(495, 21);
            this.panel2.TabIndex = 5;
            // 
            // cmdSetNextSlideTime
            // 
            this.cmdSetNextSlideTime.Location = new System.Drawing.Point(0, 0);
            this.cmdSetNextSlideTime.Name = "cmdSetNextSlideTime";
            this.cmdSetNextSlideTime.Size = new System.Drawing.Size(128, 20);
            this.cmdSetNextSlideTime.TabIndex = 4;
            this.cmdSetNextSlideTime.Text = "Set next slide here";
            this.cmdSetNextSlideTime.UseVisualStyleBackColor = true;
            this.cmdSetNextSlideTime.Click += new System.EventHandler(this.cmdSetNextSlideTime_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.cmdSetImageName);
            this.panel1.Controls.Add(this.txtImageName);
            this.panel1.Controls.Add(this.cmbImage);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(495, 21);
            this.panel1.TabIndex = 3;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(0, 0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(48, 20);
            this.button3.TabIndex = 6;
            this.button3.Text = "|> /  ||";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(215, 1);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(40, 20);
            this.button2.TabIndex = 5;
            this.button2.Text = "Go";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // cmdSetImageName
            // 
            this.cmdSetImageName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSetImageName.Location = new System.Drawing.Point(455, 0);
            this.cmdSetImageName.Name = "cmdSetImageName";
            this.cmdSetImageName.Size = new System.Drawing.Size(40, 20);
            this.cmdSetImageName.TabIndex = 4;
            this.cmdSetImageName.Text = "Set";
            this.cmdSetImageName.UseVisualStyleBackColor = true;
            this.cmdSetImageName.Click += new System.EventHandler(this.cmdSetImageName_Click);
            // 
            // txtImageName
            // 
            this.txtImageName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtImageName.Location = new System.Drawing.Point(261, 0);
            this.txtImageName.Name = "txtImageName";
            this.txtImageName.Size = new System.Drawing.Size(188, 20);
            this.txtImageName.TabIndex = 3;
            // 
            // cmbImage
            // 
            this.cmbImage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbImage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbImage.FormattingEnabled = true;
            this.cmbImage.Location = new System.Drawing.Point(54, 0);
            this.cmbImage.Name = "cmbImage";
            this.cmbImage.Size = new System.Drawing.Size(155, 21);
            this.cmbImage.TabIndex = 2;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.button1);
            this.tabPage3.Controls.Add(this.chkFindSelect);
            this.tabPage3.Controls.Add(this.txtFilter);
            this.tabPage3.Controls.Add(this.listBox1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(501, 516);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Transcript";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(363, 15);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 20);
            this.button1.TabIndex = 3;
            this.button1.Text = "Locate";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // chkFindSelect
            // 
            this.chkFindSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkFindSelect.AutoSize = true;
            this.chkFindSelect.Checked = true;
            this.chkFindSelect.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFindSelect.Location = new System.Drawing.Point(261, 17);
            this.chkFindSelect.Name = "chkFindSelect";
            this.chkFindSelect.Size = new System.Drawing.Size(67, 17);
            this.chkFindSelect.TabIndex = 2;
            this.chkFindSelect.Text = "Highlight";
            this.chkFindSelect.UseVisualStyleBackColor = true;
            this.chkFindSelect.CheckedChanged += new System.EventHandler(this.chkFindSelect_CheckedChanged);
            // 
            // txtFilter
            // 
            this.txtFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilter.Location = new System.Drawing.Point(12, 15);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(243, 20);
            this.txtFilter.TabIndex = 1;
            this.txtFilter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilter_KeyPress);
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.ContextMenuStrip = this.contextMenuStrip2;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 41);
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox1.Size = new System.Drawing.Size(476, 459);
            this.listBox1.TabIndex = 0;
            this.listBox1.DoubleClick += new System.EventHandler(this.listBox1_DoubleClick);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyImageTimestampToolStripMenuItem,
            this.copyLyricsTimestampToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(201, 48);
            // 
            // copyImageTimestampToolStripMenuItem
            // 
            this.copyImageTimestampToolStripMenuItem.Name = "copyImageTimestampToolStripMenuItem";
            this.copyImageTimestampToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.copyImageTimestampToolStripMenuItem.Text = "Copy Image Timestamp";
            this.copyImageTimestampToolStripMenuItem.Click += new System.EventHandler(this.copyImageTimestampToolStripMenuItem_Click);
            // 
            // copyLyricsTimestampToolStripMenuItem
            // 
            this.copyLyricsTimestampToolStripMenuItem.Name = "copyLyricsTimestampToolStripMenuItem";
            this.copyLyricsTimestampToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.copyLyricsTimestampToolStripMenuItem.Text = "Copy Lyrics Timestamp";
            this.copyLyricsTimestampToolStripMenuItem.Click += new System.EventHandler(this.copyLyricsTimestampToolStripMenuItem_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.treeView1);
            this.tabPage4.Controls.Add(this.txtAllTranscriptsFilter);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(501, 516);
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
            this.treeView1.Size = new System.Drawing.Size(476, 465);
            this.treeView1.TabIndex = 3;
            this.treeView1.DoubleClick += new System.EventHandler(this.treeView1_DoubleClick);
            // 
            // txtAllTranscriptsFilter
            // 
            this.txtAllTranscriptsFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAllTranscriptsFilter.Location = new System.Drawing.Point(12, 13);
            this.txtAllTranscriptsFilter.Name = "txtAllTranscriptsFilter";
            this.txtAllTranscriptsFilter.Size = new System.Drawing.Size(476, 20);
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
            this.tabPage1.Size = new System.Drawing.Size(501, 516);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Chapters";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(250, 19);
            this.toolStripStatusLabel1.Spring = true;
            // 
            // lblNext
            // 
            this.lblNext.Name = "lblNext";
            this.lblNext.Size = new System.Drawing.Size(32, 19);
            this.lblNext.Text = "Next";
            // 
            // pnlPointer
            // 
            this.pnlPointer.BackColor = System.Drawing.Color.Red;
            this.pnlPointer.Location = new System.Drawing.Point(166, 179);
            this.pnlPointer.Name = "pnlPointer";
            this.pnlPointer.Size = new System.Drawing.Size(5, 5);
            this.pnlPointer.TabIndex = 6;
            this.pnlPointer.Visible = false;
            // 
            // chkShowPointer
            // 
            this.chkShowPointer.AutoSize = true;
            this.chkShowPointer.Location = new System.Drawing.Point(134, 3);
            this.chkShowPointer.Name = "chkShowPointer";
            this.chkShowPointer.Size = new System.Drawing.Size(88, 17);
            this.chkShowPointer.TabIndex = 5;
            this.chkShowPointer.Text = "Show pointer";
            this.chkShowPointer.UseVisualStyleBackColor = true;
            this.chkShowPointer.CheckedChanged += new System.EventHandler(this.chkShowPointer_CheckedChanged);
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
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.contextMenuStrip2.ResumeLayout(false);
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
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem reloadImagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel lblImageTime;
        private System.Windows.Forms.CheckBox chkFindSelect;
        private System.Windows.Forms.ToolStripStatusLabel lblLirycsTime;
        private System.Windows.Forms.ToolStripStatusLabel lblTopmost;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem copyImageTimestampToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyLyricsTimestampToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jumpToNextImageToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button cmdSetImageName;
        private System.Windows.Forms.TextBox txtImageName;
        private System.Windows.Forms.ComboBox cmbImage;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ToolStripMenuItem copyImagesAsLyricsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAllImagesToolStripMenuItem;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button cmdSetNextSlideTime;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lblNext;
        private System.Windows.Forms.Panel pnlPointer;
        private System.Windows.Forms.CheckBox chkShowPointer;
    }
}