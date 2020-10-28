namespace SyncView
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.lblTopmost = new System.Windows.Forms.ToolStripStatusLabel();
			this.titleArtistStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.chaptersCountStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.lblImageTime = new System.Windows.Forms.ToolStripStatusLabel();
			this.lblLirycsTime = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.lblNext = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage5 = new System.Windows.Forms.TabPage();
			this.lstBookmarks = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.transcriptsContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.copyTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyBareTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyAbsoluteTimestampToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyImageTimestampToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyLyricsTimestampToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.skipToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.SearchTreeImageList = new System.Windows.Forms.ImageList(this.components);
			this.panel3 = new System.Windows.Forms.Panel();
			this.button1 = new System.Windows.Forms.Button();
			this.chkSingleAudioShowImages = new System.Windows.Forms.CheckBox();
			this.txtFilter = new System.Windows.Forms.TextBox();
			this.chkSingleAudioShowTranscripts = new System.Windows.Forms.CheckBox();
			this.chkHiglightTranscript = new System.Windows.Forms.CheckBox();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.chkSearchI = new System.Windows.Forms.CheckBox();
			this.chkSearchT = new System.Windows.Forms.CheckBox();
			this.btnSearch = new System.Windows.Forms.Button();
			this.allReposSearchResults = new System.Windows.Forms.TreeView();
			this.txtAllTranscriptsFilter = new System.Windows.Forms.TextBox();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.pnlPointer = new SyncView.RoundPanel();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.imageContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.jumpToNextImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.reloadImagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openInExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.fileExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.paintNetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.openImageManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.copyImagesAsLyricsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.createHTMLFromImagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveAllImagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.deleteThisImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.panel2 = new System.Windows.Forms.Panel();
			this.cmdPointer = new System.Windows.Forms.Button();
			this.cmdSetNextSlideTime = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.button5 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.cmdSetImageName = new System.Windows.Forms.Button();
			this.txtImageName = new System.Windows.Forms.TextBox();
			this.cmbImage = new System.Windows.Forms.ComboBox();
			this.statusStrip1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage5.SuspendLayout();
			this.transcriptsContextMenu.SuspendLayout();
			this.panel3.SuspendLayout();
			this.tabPage4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.imageContextMenu.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusStrip1
			// 
			this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblTopmost,
            this.titleArtistStatusLabel,
            this.chaptersCountStatusLabel,
            this.lblImageTime,
            this.lblLirycsTime,
            this.toolStripStatusLabel1,
            this.lblNext});
			this.statusStrip1.Location = new System.Drawing.Point(0, 771);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(1001, 24);
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
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(742, 19);
			this.toolStripStatusLabel1.Spring = true;
			// 
			// lblNext
			// 
			this.lblNext.Name = "lblNext";
			this.lblNext.Size = new System.Drawing.Size(32, 19);
			this.lblNext.Text = "Next";
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage5);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(369, 771);
			this.tabControl1.TabIndex = 10;
			// 
			// tabPage5
			// 
			this.tabPage5.Controls.Add(this.lstBookmarks);
			this.tabPage5.Controls.Add(this.panel3);
			this.tabPage5.Location = new System.Drawing.Point(4, 22);
			this.tabPage5.Name = "tabPage5";
			this.tabPage5.Size = new System.Drawing.Size(361, 745);
			this.tabPage5.TabIndex = 4;
			this.tabPage5.Text = "Lezione";
			this.tabPage5.UseVisualStyleBackColor = true;
			// 
			// lstBookmarks
			// 
			this.lstBookmarks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
			this.lstBookmarks.ContextMenuStrip = this.transcriptsContextMenu;
			this.lstBookmarks.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstBookmarks.HideSelection = false;
			this.lstBookmarks.Location = new System.Drawing.Point(0, 100);
			this.lstBookmarks.Name = "lstBookmarks";
			this.lstBookmarks.Size = new System.Drawing.Size(361, 645);
			this.lstBookmarks.SmallImageList = this.SearchTreeImageList;
			this.lstBookmarks.TabIndex = 4;
			this.lstBookmarks.UseCompatibleStateImageBehavior = false;
			this.lstBookmarks.View = System.Windows.Forms.View.Details;
			this.lstBookmarks.DoubleClick += new System.EventHandler(this.listBox1_DoubleClick);
			this.lstBookmarks.Resize += new System.EventHandler(this.lstBookmarks_Resize);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Bookmarks";
			this.columnHeader1.Width = 337;
			// 
			// transcriptsContextMenu
			// 
			this.transcriptsContextMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.transcriptsContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyTextToolStripMenuItem,
            this.copyBareTextToolStripMenuItem,
            this.copyAbsoluteTimestampToolStripMenuItem,
            this.copyImageTimestampToolStripMenuItem,
            this.copyLyricsTimestampToolStripMenuItem,
            this.skipToolStripMenuItem});
			this.transcriptsContextMenu.Name = "contextMenuStrip2";
			this.transcriptsContextMenu.Size = new System.Drawing.Size(213, 136);
			// 
			// copyTextToolStripMenuItem
			// 
			this.copyTextToolStripMenuItem.Name = "copyTextToolStripMenuItem";
			this.copyTextToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
			this.copyTextToolStripMenuItem.Text = "Copy text";
			this.copyTextToolStripMenuItem.Click += new System.EventHandler(this.copyTextToolStripMenuItem_Click);
			// 
			// copyBareTextToolStripMenuItem
			// 
			this.copyBareTextToolStripMenuItem.Name = "copyBareTextToolStripMenuItem";
			this.copyBareTextToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
			this.copyBareTextToolStripMenuItem.Text = "Copy bare text";
			this.copyBareTextToolStripMenuItem.Click += new System.EventHandler(this.copyBareTextToolStripMenuItem_Click);
			// 
			// copyAbsoluteTimestampToolStripMenuItem
			// 
			this.copyAbsoluteTimestampToolStripMenuItem.Name = "copyAbsoluteTimestampToolStripMenuItem";
			this.copyAbsoluteTimestampToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
			this.copyAbsoluteTimestampToolStripMenuItem.Text = "Copy absolute Timestamp";
			this.copyAbsoluteTimestampToolStripMenuItem.Click += new System.EventHandler(this.copyAbsoluteTimestampToolStripMenuItem_Click);
			// 
			// copyImageTimestampToolStripMenuItem
			// 
			this.copyImageTimestampToolStripMenuItem.Name = "copyImageTimestampToolStripMenuItem";
			this.copyImageTimestampToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
			this.copyImageTimestampToolStripMenuItem.Text = "Copy Image Timestamp";
			this.copyImageTimestampToolStripMenuItem.Click += new System.EventHandler(this.copyImageTimestampToolStripMenuItem_Click);
			// 
			// copyLyricsTimestampToolStripMenuItem
			// 
			this.copyLyricsTimestampToolStripMenuItem.Name = "copyLyricsTimestampToolStripMenuItem";
			this.copyLyricsTimestampToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
			this.copyLyricsTimestampToolStripMenuItem.Text = "Copy Lyrics Timestamp";
			this.copyLyricsTimestampToolStripMenuItem.Click += new System.EventHandler(this.copyLyricsTimestampToolStripMenuItem_Click);
			// 
			// skipToolStripMenuItem
			// 
			this.skipToolStripMenuItem.Name = "skipToolStripMenuItem";
			this.skipToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
			this.skipToolStripMenuItem.Text = "Skip";
			this.skipToolStripMenuItem.Click += new System.EventHandler(this.skipToolStripMenuItem_Click);
			// 
			// SearchTreeImageList
			// 
			this.SearchTreeImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("SearchTreeImageList.ImageStream")));
			this.SearchTreeImageList.TransparentColor = System.Drawing.Color.Transparent;
			this.SearchTreeImageList.Images.SetKeyName(0, "Sound.png");
			this.SearchTreeImageList.Images.SetKeyName(1, "Image.png");
			this.SearchTreeImageList.Images.SetKeyName(2, "TextSmall.png");
			this.SearchTreeImageList.Images.SetKeyName(3, "Skip.png");
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.button1);
			this.panel3.Controls.Add(this.chkSingleAudioShowImages);
			this.panel3.Controls.Add(this.txtFilter);
			this.panel3.Controls.Add(this.chkSingleAudioShowTranscripts);
			this.panel3.Controls.Add(this.chkHiglightTranscript);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel3.Location = new System.Drawing.Point(0, 0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(361, 100);
			this.panel3.TabIndex = 10;
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Location = new System.Drawing.Point(273, 5);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 20);
			this.button1.TabIndex = 7;
			this.button1.Text = "Locate";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.btnLocate_Click);
			// 
			// chkSingleAudioShowImages
			// 
			this.chkSingleAudioShowImages.AutoSize = true;
			this.chkSingleAudioShowImages.Checked = true;
			this.chkSingleAudioShowImages.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkSingleAudioShowImages.Location = new System.Drawing.Point(10, 53);
			this.chkSingleAudioShowImages.Name = "chkSingleAudioShowImages";
			this.chkSingleAudioShowImages.Size = new System.Drawing.Size(60, 17);
			this.chkSingleAudioShowImages.TabIndex = 9;
			this.chkSingleAudioShowImages.Text = "Images";
			this.chkSingleAudioShowImages.UseVisualStyleBackColor = true;
			this.chkSingleAudioShowImages.CheckedChanged += new System.EventHandler(this.chkSingleAudioShow_CheckedChanged);
			// 
			// txtFilter
			// 
			this.txtFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtFilter.Location = new System.Drawing.Point(10, 5);
			this.txtFilter.Name = "txtFilter";
			this.txtFilter.Size = new System.Drawing.Size(258, 20);
			this.txtFilter.TabIndex = 5;
			this.txtFilter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilter_KeyPress);
			// 
			// chkSingleAudioShowTranscripts
			// 
			this.chkSingleAudioShowTranscripts.AutoSize = true;
			this.chkSingleAudioShowTranscripts.Checked = true;
			this.chkSingleAudioShowTranscripts.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkSingleAudioShowTranscripts.Location = new System.Drawing.Point(10, 75);
			this.chkSingleAudioShowTranscripts.Name = "chkSingleAudioShowTranscripts";
			this.chkSingleAudioShowTranscripts.Size = new System.Drawing.Size(78, 17);
			this.chkSingleAudioShowTranscripts.TabIndex = 8;
			this.chkSingleAudioShowTranscripts.Text = "Transcripts";
			this.chkSingleAudioShowTranscripts.UseVisualStyleBackColor = true;
			this.chkSingleAudioShowTranscripts.CheckedChanged += new System.EventHandler(this.chkSingleAudioShow_CheckedChanged);
			// 
			// chkHiglightTranscript
			// 
			this.chkHiglightTranscript.AutoSize = true;
			this.chkHiglightTranscript.Checked = true;
			this.chkHiglightTranscript.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkHiglightTranscript.Location = new System.Drawing.Point(10, 31);
			this.chkHiglightTranscript.Name = "chkHiglightTranscript";
			this.chkHiglightTranscript.Size = new System.Drawing.Size(67, 17);
			this.chkHiglightTranscript.TabIndex = 6;
			this.chkHiglightTranscript.Text = "Highlight";
			this.chkHiglightTranscript.UseVisualStyleBackColor = true;
			this.chkHiglightTranscript.CheckedChanged += new System.EventHandler(this.chkFindSelect_CheckedChanged);
			// 
			// tabPage4
			// 
			this.tabPage4.Controls.Add(this.chkSearchI);
			this.tabPage4.Controls.Add(this.chkSearchT);
			this.tabPage4.Controls.Add(this.btnSearch);
			this.tabPage4.Controls.Add(this.allReposSearchResults);
			this.tabPage4.Controls.Add(this.txtAllTranscriptsFilter);
			this.tabPage4.Location = new System.Drawing.Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Size = new System.Drawing.Size(361, 745);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "Collezione";
			this.tabPage4.UseVisualStyleBackColor = true;
			// 
			// chkSearchI
			// 
			this.chkSearchI.AutoSize = true;
			this.chkSearchI.Checked = true;
			this.chkSearchI.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkSearchI.Location = new System.Drawing.Point(96, 39);
			this.chkSearchI.Name = "chkSearchI";
			this.chkSearchI.Size = new System.Drawing.Size(60, 17);
			this.chkSearchI.TabIndex = 6;
			this.chkSearchI.Text = "Images";
			this.chkSearchI.UseVisualStyleBackColor = true;
			// 
			// chkSearchT
			// 
			this.chkSearchT.AutoSize = true;
			this.chkSearchT.Checked = true;
			this.chkSearchT.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkSearchT.Location = new System.Drawing.Point(12, 39);
			this.chkSearchT.Name = "chkSearchT";
			this.chkSearchT.Size = new System.Drawing.Size(78, 17);
			this.chkSearchT.TabIndex = 5;
			this.chkSearchT.Text = "Transcripts";
			this.chkSearchT.UseVisualStyleBackColor = true;
			// 
			// btnSearch
			// 
			this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSearch.Location = new System.Drawing.Point(255, 11);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(98, 23);
			this.btnSearch.TabIndex = 4;
			this.btnSearch.Text = "Search";
			this.btnSearch.UseVisualStyleBackColor = true;
			this.btnSearch.Click += new System.EventHandler(this.button4_Click);
			// 
			// allReposSearchResults
			// 
			this.allReposSearchResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.allReposSearchResults.FullRowSelect = true;
			this.allReposSearchResults.ImageIndex = 0;
			this.allReposSearchResults.ImageList = this.SearchTreeImageList;
			this.allReposSearchResults.Location = new System.Drawing.Point(12, 68);
			this.allReposSearchResults.Name = "allReposSearchResults";
			this.allReposSearchResults.SelectedImageIndex = 0;
			this.allReposSearchResults.Size = new System.Drawing.Size(341, 650);
			this.allReposSearchResults.TabIndex = 3;
			this.allReposSearchResults.DoubleClick += new System.EventHandler(this.treeView1_DoubleClick);
			// 
			// txtAllTranscriptsFilter
			// 
			this.txtAllTranscriptsFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtAllTranscriptsFilter.Location = new System.Drawing.Point(12, 13);
			this.txtAllTranscriptsFilter.Name = "txtAllTranscriptsFilter";
			this.txtAllTranscriptsFilter.Size = new System.Drawing.Size(237, 20);
			this.txtAllTranscriptsFilter.TabIndex = 2;
			this.txtAllTranscriptsFilter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.pnlPointer);
			this.splitContainer1.Panel1.Controls.Add(this.pictureBox1);
			this.splitContainer1.Panel1.Controls.Add(this.panel2);
			this.splitContainer1.Panel1.Controls.Add(this.panel1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
			this.splitContainer1.Size = new System.Drawing.Size(1001, 771);
			this.splitContainer1.SplitterDistance = 628;
			this.splitContainer1.TabIndex = 8;
			// 
			// pnlPointer
			// 
			this.pnlPointer.BackColor = System.Drawing.Color.Red;
			this.pnlPointer.Location = new System.Drawing.Point(0, 0);
			this.pnlPointer.Name = "pnlPointer";
			this.pnlPointer.Size = new System.Drawing.Size(11, 11);
			this.pnlPointer.TabIndex = 10;
			// 
			// pictureBox1
			// 
			this.pictureBox1.ContextMenuStrip = this.imageContextMenu;
			this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBox1.Location = new System.Drawing.Point(0, 21);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(628, 729);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 7;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
			this.pictureBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.pictureBox1_DragDrop);
			this.pictureBox1.DragOver += new System.Windows.Forms.DragEventHandler(this.pictureBox1_DragOver);
			this.pictureBox1.DoubleClick += new System.EventHandler(this.pictureBox1_DoubleClick);
			// 
			// imageContextMenu
			// 
			this.imageContextMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.imageContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.jumpToNextImageToolStripMenuItem,
            this.reloadImagesToolStripMenuItem,
            this.copyImageToolStripMenuItem,
            this.openInExplorerToolStripMenuItem,
            this.toolStripSeparator3,
            this.openImageManagerToolStripMenuItem,
            this.toolStripSeparator1,
            this.copyImagesAsLyricsToolStripMenuItem,
            this.createHTMLFromImagesToolStripMenuItem,
            this.saveAllImagesToolStripMenuItem,
            this.toolStripSeparator2,
            this.deleteThisImageToolStripMenuItem});
			this.imageContextMenu.Name = "contextMenuStrip1";
			this.imageContextMenu.Size = new System.Drawing.Size(216, 220);
			// 
			// jumpToNextImageToolStripMenuItem
			// 
			this.jumpToNextImageToolStripMenuItem.Name = "jumpToNextImageToolStripMenuItem";
			this.jumpToNextImageToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
			this.jumpToNextImageToolStripMenuItem.Text = "Jump to next image";
			this.jumpToNextImageToolStripMenuItem.Click += new System.EventHandler(this.jumpToNextImageToolStripMenuItem_Click);
			// 
			// reloadImagesToolStripMenuItem
			// 
			this.reloadImagesToolStripMenuItem.Name = "reloadImagesToolStripMenuItem";
			this.reloadImagesToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
			this.reloadImagesToolStripMenuItem.Text = "Reload images";
			this.reloadImagesToolStripMenuItem.Click += new System.EventHandler(this.reloadImagesToolStripMenuItem_Click);
			// 
			// copyImageToolStripMenuItem
			// 
			this.copyImageToolStripMenuItem.Name = "copyImageToolStripMenuItem";
			this.copyImageToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
			this.copyImageToolStripMenuItem.Text = "Copy Image to Clipboard";
			this.copyImageToolStripMenuItem.Click += new System.EventHandler(this.copyImageToolStripMenuItem_Click);
			// 
			// openInExplorerToolStripMenuItem
			// 
			this.openInExplorerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileExplorerToolStripMenuItem,
            this.paintNetToolStripMenuItem});
			this.openInExplorerToolStripMenuItem.Name = "openInExplorerToolStripMenuItem";
			this.openInExplorerToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
			this.openInExplorerToolStripMenuItem.Text = "Open in";
			// 
			// fileExplorerToolStripMenuItem
			// 
			this.fileExplorerToolStripMenuItem.Name = "fileExplorerToolStripMenuItem";
			this.fileExplorerToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
			this.fileExplorerToolStripMenuItem.Text = "File explorer";
			this.fileExplorerToolStripMenuItem.Click += new System.EventHandler(this.fileExplorerToolStripMenuItem_Click);
			// 
			// paintNetToolStripMenuItem
			// 
			this.paintNetToolStripMenuItem.Name = "paintNetToolStripMenuItem";
			this.paintNetToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
			this.paintNetToolStripMenuItem.Text = "Paint Net";
			this.paintNetToolStripMenuItem.Click += new System.EventHandler(this.paintNetToolStripMenuItem_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(212, 6);
			// 
			// openImageManagerToolStripMenuItem
			// 
			this.openImageManagerToolStripMenuItem.Name = "openImageManagerToolStripMenuItem";
			this.openImageManagerToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
			this.openImageManagerToolStripMenuItem.Text = "Open image manager";
			this.openImageManagerToolStripMenuItem.Click += new System.EventHandler(this.openImageManagerToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(212, 6);
			// 
			// copyImagesAsLyricsToolStripMenuItem
			// 
			this.copyImagesAsLyricsToolStripMenuItem.Name = "copyImagesAsLyricsToolStripMenuItem";
			this.copyImagesAsLyricsToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
			this.copyImagesAsLyricsToolStripMenuItem.Text = "Copy all images as lyrics";
			this.copyImagesAsLyricsToolStripMenuItem.Click += new System.EventHandler(this.copyImagesAsLyricsToolStripMenuItem_Click);
			// 
			// createHTMLFromImagesToolStripMenuItem
			// 
			this.createHTMLFromImagesToolStripMenuItem.Name = "createHTMLFromImagesToolStripMenuItem";
			this.createHTMLFromImagesToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
			this.createHTMLFromImagesToolStripMenuItem.Text = "Create HTML From images";
			this.createHTMLFromImagesToolStripMenuItem.Click += new System.EventHandler(this.createHTMLFromImagesToolStripMenuItem_Click);
			// 
			// saveAllImagesToolStripMenuItem
			// 
			this.saveAllImagesToolStripMenuItem.Name = "saveAllImagesToolStripMenuItem";
			this.saveAllImagesToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
			this.saveAllImagesToolStripMenuItem.Text = "Save all images";
			this.saveAllImagesToolStripMenuItem.Visible = false;
			this.saveAllImagesToolStripMenuItem.Click += new System.EventHandler(this.saveAllImagesToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(212, 6);
			// 
			// deleteThisImageToolStripMenuItem
			// 
			this.deleteThisImageToolStripMenuItem.Name = "deleteThisImageToolStripMenuItem";
			this.deleteThisImageToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
			this.deleteThisImageToolStripMenuItem.Text = "Delete this image";
			this.deleteThisImageToolStripMenuItem.Click += new System.EventHandler(this.deleteThisImageToolStripMenuItem_Click);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.cmdPointer);
			this.panel2.Controls.Add(this.cmdSetNextSlideTime);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(0, 750);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(628, 21);
			this.panel2.TabIndex = 9;
			// 
			// cmdPointer
			// 
			this.cmdPointer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdPointer.Location = new System.Drawing.Point(566, 1);
			this.cmdPointer.Name = "cmdPointer";
			this.cmdPointer.Size = new System.Drawing.Size(60, 20);
			this.cmdPointer.TabIndex = 5;
			this.cmdPointer.Text = "Pointer";
			this.cmdPointer.UseVisualStyleBackColor = true;
			this.cmdPointer.Click += new System.EventHandler(this.cmdPointer_Click);
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
			this.panel1.Controls.Add(this.button5);
			this.panel1.Controls.Add(this.button3);
			this.panel1.Controls.Add(this.button2);
			this.panel1.Controls.Add(this.cmdSetImageName);
			this.panel1.Controls.Add(this.txtImageName);
			this.panel1.Controls.Add(this.cmbImage);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(628, 21);
			this.panel1.TabIndex = 8;
			// 
			// button5
			// 
			this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button5.Location = new System.Drawing.Point(254, 0);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(40, 20);
			this.button5.TabIndex = 7;
			this.button5.Text = "=>";
			this.button5.UseVisualStyleBackColor = true;
			this.button5.Click += new System.EventHandler(this.button5_Click);
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
			this.button2.Location = new System.Drawing.Point(207, 0);
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
			this.cmdSetImageName.Location = new System.Drawing.Point(586, 1);
			this.cmdSetImageName.Name = "cmdSetImageName";
			this.cmdSetImageName.Size = new System.Drawing.Size(40, 20);
			this.cmdSetImageName.TabIndex = 4;
			this.cmdSetImageName.Text = "Set";
			this.cmdSetImageName.UseVisualStyleBackColor = true;
			this.cmdSetImageName.MouseUp += new System.Windows.Forms.MouseEventHandler(this.cmdSetImageName_MouseUp);
			// 
			// txtImageName
			// 
			this.txtImageName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtImageName.Location = new System.Drawing.Point(299, 0);
			this.txtImageName.Name = "txtImageName";
			this.txtImageName.Size = new System.Drawing.Size(280, 20);
			this.txtImageName.TabIndex = 3;
			this.txtImageName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtImageName_KeyPress);
			// 
			// cmbImage
			// 
			this.cmbImage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cmbImage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbImage.FormattingEnabled = true;
			this.cmbImage.Location = new System.Drawing.Point(54, 0);
			this.cmbImage.Name = "cmbImage";
			this.cmbImage.Size = new System.Drawing.Size(147, 21);
			this.cmbImage.TabIndex = 2;
			// 
			// MainForm
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1001, 795);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.statusStrip1);
			this.KeyPreview = true;
			this.MinimumSize = new System.Drawing.Size(323, 344);
			this.Name = "MainForm";
			this.Text = "SyncView";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
			this.DragOver += new System.Windows.Forms.DragEventHandler(this.MainForm_DragOver);
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainForm_KeyPress);
			this.Resize += new System.EventHandler(this.MainForm_Resize);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.tabPage5.ResumeLayout(false);
			this.transcriptsContextMenu.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.tabPage4.ResumeLayout(false);
			this.tabPage4.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.imageContextMenu.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel titleArtistStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel chaptersCountStatusLabel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TextBox txtAllTranscriptsFilter;
        private System.Windows.Forms.TreeView allReposSearchResults;
        private System.Windows.Forms.ContextMenuStrip imageContextMenu;
        private System.Windows.Forms.ToolStripMenuItem reloadImagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel lblImageTime;
        private System.Windows.Forms.ToolStripStatusLabel lblLirycsTime;
        private System.Windows.Forms.ToolStripStatusLabel lblTopmost;
        private System.Windows.Forms.ContextMenuStrip transcriptsContextMenu;
        private System.Windows.Forms.ToolStripMenuItem copyImageTimestampToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyLyricsTimestampToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jumpToNextImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyImagesAsLyricsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAllImagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lblNext;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private RoundPanel pnlPointer;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button cmdPointer;
        private System.Windows.Forms.Button cmdSetNextSlideTime;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button cmdSetImageName;
        private System.Windows.Forms.TextBox txtImageName;
        private System.Windows.Forms.ComboBox cmbImage;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox chkHiglightTranscript;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.ListView lstBookmarks;
        private System.Windows.Forms.ToolStripMenuItem deleteThisImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyBareTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyAbsoluteTimestampToolStripMenuItem;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ToolStripMenuItem openInExplorerToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ImageList SearchTreeImageList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ToolStripMenuItem openImageManagerToolStripMenuItem;
        private System.Windows.Forms.CheckBox chkSearchI;
        private System.Windows.Forms.CheckBox chkSearchT;
        private System.Windows.Forms.ToolStripMenuItem fileExplorerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem paintNetToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.CheckBox chkSingleAudioShowImages;
        private System.Windows.Forms.CheckBox chkSingleAudioShowTranscripts;
        private System.Windows.Forms.ToolStripMenuItem createHTMLFromImagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem skipToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyImageToolStripMenuItem;
        private System.Windows.Forms.Panel panel3;
    }
}