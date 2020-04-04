using ChapterListMB;
using ChapterListMB.SyncView;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace SyncView
{
    public partial class MainForm : Form
    {
        private Track Track { get; set; }
        private Chapter CurrentChapter { get; set; }

        private readonly BindingSource _chapterListBindingSource = new BindingSource();
        private readonly DataGridViewCellStyle _defaultCellStyleA;  // White row style
        private readonly DataGridViewCellStyle _defaultCellStyleB;  // Tan row style
        
        public delegate void UpdateTrack(Track track);
        public delegate void UpdateChapterList();
        public delegate void SetCurrentChapter(Chapter chapter);

        public UpdateTrack UpdateTrackDelegate;
        public UpdateChapterList UpdateChapterListDelegate;
        public SetCurrentChapter SetCurrentChapterDelegate;

        public delegate void SetTime(int time);
        public SetTime SetTimeDelegate;
        SyncViewRepository repo;

        PropertyInfo irProperty; // used to access scaled image rectangle

        public MainForm()
        {
            InitializeComponent();

            chaptersDGV.DataSource = _chapterListBindingSource;
            chaptersDGV.AutoGenerateColumns = false;
            // Copy default cell styles
            _defaultCellStyleA = chaptersDGV.DefaultCellStyle;
            _defaultCellStyleB = chaptersDGV.AlternatingRowsDefaultCellStyle;

            comboBoxNewChapterName.SelectedIndex = 0;

            UpdateTrackDelegate = UpdateTrackMethod;
            UpdateChapterListDelegate = UpdateFirstColumn;
            SetCurrentChapterDelegate = SetCurrentChapterMethod;
            SetTimeDelegate = SetCurrentTime;

            irProperty = pictureBox1.GetType().GetProperty("ImageRectangle", BindingFlags.GetProperty | BindingFlags.NonPublic | BindingFlags.Instance);

            this.TopMost = false;
            UpdateTopMostText();
        }

        int CurrentMilli = -1;

        public void SetCurrentTime(int playerPosition)
        {
            // lblPos.Text = playerPosition.ToString();
            if (repo != null)
            {
                lblImageTime.Text = SyncViewRepository.GetImagesTimestamp(playerPosition);
                lblLirycsTime.Text = SyncViewRepository.GetLyricsTimestamp(playerPosition);
                lblNext.Text = $"- {(repo.Images.NextObjectTime - playerPosition ) / 1000} sec.";
                CurrentMilli = playerPosition;
                
                // load image
                var imageIndex = repo.Images.GetIndex(playerPosition);
                if (imageIndex != -1)
                {
                    // lblPos.Text = repo.GetiImageFile(imageIndex).ToString();
                    pictureBox1.ImageLocation = repo.Images[imageIndex].file.FullName;
                    pnlPointer.Size = new Size(0, 0);
                    try
                    {
                        cmbImage.SelectedIndex = imageIndex;
                    }
                    catch (Exception)
                    {

                    }
                }
                var pointerIndex = repo.Pointers.GetIndex(playerPosition);
                if (pointerIndex != -1)
                {
                    SetPointer(repo.Pointers[pointerIndex]);
                }
            }
        }

        int lastPointerTime = -1;
        PointerCoordinates lastPC = null;

        private void SetPointer(PointerCoordinates pointerCoordinates)
        {
            lastPointerTime = CurrentMilli;
            lastPC = pointerCoordinates;
            if (pictureBox1.Image == null)
                return;
            if (pointerCoordinates == null)
            {
                pnlPointer.Size = new Size(0, 0);
                return;
            }

            if (pnlPointer.Size.Width == 0)
                pnlPointer.Size = new Size(pSize, pSize);

            // Debug.WriteLine(pictureBox1.Image.PhysicalDimension.Width);
            Rectangle rectangle = (Rectangle)irProperty.GetValue(pictureBox1, null);
            var ratio = (double) rectangle.Width / pictureBox1.Image.PhysicalDimension.Width;
            var px = pictureBox1.Location.X + rectangle.X + pointerCoordinates.X * ratio;
            var py = pictureBox1.Location.Y + rectangle.Y + pointerCoordinates.Y * ratio;
            pnlPointer.Location = new Point((int)px - (pSize/2 + 1), (int)py - (pSize / 2 + 1));
            pnlPointer.Refresh();
        }

        public void UpdateTrackMethod(Track track)
        {
            cmbImage.Items.Clear();
            pictureBox1.Image = null;
            Track = track;
            repo = new SyncViewRepository(track);
            if (repo.Images != null)
                cmbImage.Items.AddRange(repo.Images.ToArray());
            setLirics();

            this.Text = "SyncView - " + repo.mediaFileName;
            
            _chapterListBindingSource.DataSource = Track.ChapterList.Chapters;

            ClearFirstColumn();

            titleArtistStatusLabel.Text = $"{Track.NowPlayingTrackInfo.Artist} – {Track.NowPlayingTrackInfo.Title}";
            if (Track.ChapterList.NumChapters == 0)
                chaptersCountStatusLabel.Text = "No Chapters";
        }

        private void setLirics()
        {
            listBox1.Items.Clear();
            if (chkFindSelect.Checked == false)
                listBox1.Items.AddRange(repo.GetLyricsText(txtFilter.Text).ToArray());
            else
            {
                listBox1.Items.AddRange(repo.GetLyricsText("").ToArray());
                filterLirics();
            }
        }

        private void filterLirics()
        {
            listBox1.SelectedItems.Clear();
            if (txtFilter.Text == "")
                return;
            for (int i = listBox1.Items.Count-1; i >= 0; i--)
            {
                if (CultureInfo.CurrentCulture.CompareInfo.IndexOf(listBox1.Items[i].ToString(), txtFilter.Text, CompareOptions.IgnoreCase) >= 0)
                {
                    listBox1.SelectedIndices.Add(i);
                }
            }
        }

        public void UpdateFirstColumn()
        {
            foreach (DataGridViewRow row in chaptersDGV.Rows)
            {
                if (row.Index == RepeatSection.A?.ChapterNumber - 1)
                {   // Repeat chapter A image
                    SetReplayImage("black", row);
                }
                else if (row.Index == RepeatSection.B?.ChapterNumber - 1)
                {   // Repeat chapter B image
                    SetReplayImage("gray", row);
                }
                else if (row.Index == CurrentChapter?.ChapterNumber - 1)
                {   // Current chapter playhead
                    SetCurrentChapterImage();
                }
                else
                {   // Empty image for blank cell
                    row.Cells[0].Value = new Bitmap(16,16);
                }
            }
        }

        public void ClearFirstColumn()
        {   // Blanks out first current chapter column
            for (int i = 0; i < chaptersDGV.RowCount; i++)
            {
                chaptersDGV.Rows[i].Cells[0].Value = new Bitmap(16, 16);
            }
        }

        public void SetCurrentChapterMethod(Chapter chapter)
        {
            CurrentChapter = chapter;
            chaptersCountStatusLabel.Text = $"{CurrentChapter.ChapterNumber}/{Track.ChapterList.NumChapters} – {CurrentChapter.Title}";

            SetRowColors();
            UpdateFirstColumn();
        }

        internal void SetPlaying(bool v)
        {
            throw new NotImplementedException();
        }

        private void SetRowColors()
        {
            foreach (var dgvRow in chaptersDGV.Rows) // Reset row colors to defaults
            {
                DataGridViewRow row = (DataGridViewRow) dgvRow;
                row.DefaultCellStyle = (row.Index%2) == 0 ? _defaultCellStyleA : _defaultCellStyleB;
                row.Cells[2].Style = (row.Index%2) == 0 ? _defaultCellStyleA : _defaultCellStyleB;
            }
            var selectedStyle = new DataGridViewCellStyle
            {
                BackColor = Properties.Settings.Default.HighlightColor,
                SelectionBackColor = Properties.Settings.Default.HighlightBackgroundColor
            };
            chaptersDGV.Rows[CurrentChapter.ChapterNumber - 1].DefaultCellStyle = selectedStyle;

            var boldStyle = new DataGridViewCellStyle(selectedStyle);   // Bold the current chapter title text
            boldStyle.Font = new Font(FontFamily.GenericSansSerif, 8.25f, FontStyle.Bold);
            chaptersDGV.Rows[CurrentChapter.ChapterNumber - 1].Cells[2].Style = boldStyle;
        }

        private Chapter GetSelectedDGVChapter()
        {
            if (chaptersDGV.SelectedRows.Contains(chaptersDGV.Rows[0]))
            {
                MessageBox.Show("Cannot remove or change position of first chapter.");
                return null;
            }
            if (chaptersDGV.SelectedRows.Count == 0 || chaptersDGV.SelectedRows.Count > 1)
            {
                MessageBox.Show("Invalid chapter selection.");
                return null;
            }
            return (Chapter)chaptersDGV.SelectedRows[0].DataBoundItem;
        }
        private void addChapterButton_Click(object sender, EventArgs e)
        {
            string newChapterName = comboBoxNewChapterName.Text == "<Default>"
                ? string.Empty
                : comboBoxNewChapterName.Text;  // Uses text from center combo box for new chapter name
            OnAddChapterButtonClickedRouted(newChapterName);
        }
        
        private void removeChapterButton_Click(object sender, EventArgs e)
        {
            var chapterToRemove = GetSelectedDGVChapter();
            if (chapterToRemove != null)
                OnRemoveChapterButtonClickedRouted(chapterToRemove);
        }
        private void shiftPositionBackButton_Click(object sender, EventArgs e)
        {
            var chapterToShiftBack = GetSelectedDGVChapter();
            if (chapterToShiftBack == null)
            {
                return;
            }

            int? newPosition = chapterToShiftBack.Position -
                               (ModifierKeys == Keys.Shift
                                   ? (int)Properties.Settings.Default.ChapterPositionShiftValue.TotalMilliseconds*4
                                   : (int)Properties.Settings.Default.ChapterPositionShiftValue.TotalMilliseconds);
            if (newPosition < 1) newPosition = 1;
            OnChangeChapterRequested(chapterToShiftBack, null, newPosition);
            chaptersDGV.UpdateCellValue(1, chapterToShiftBack.ChapterNumber - 1);
        }

       

        private void shiftPositionFwdButton_Click(object sender, EventArgs e)
        {
            var chapterToShiftForwards = GetSelectedDGVChapter();
            if (chapterToShiftForwards == null)
            {
                return;
            }
            int? newPosition = chapterToShiftForwards.Position +
                               (ModifierKeys == Keys.Shift
                                   ? (int)Properties.Settings.Default.ChapterPositionShiftValue.TotalMilliseconds*4
                                   : (int)Properties.Settings.Default.ChapterPositionShiftValue.TotalMilliseconds);
            if (newPosition > Track.NowPlayingTrackInfo.Duration.TotalMilliseconds)
                newPosition = (int)Track.NowPlayingTrackInfo.Duration.TotalMilliseconds;
            OnChangeChapterRequested(chapterToShiftForwards, null, newPosition);
            chaptersDGV.UpdateCellValue(1, chapterToShiftForwards.ChapterNumber - 1);
        }
        private void chaptersDGV_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {   // Requests to set player position to chapter position.
            if (e.RowIndex >= 0 && e.RowIndex < Track.ChapterList.NumChapters)
            {
                Chapter chapt = ((List<Chapter>) _chapterListBindingSource.DataSource)[e.RowIndex];
                RequestPlayerTime(chapt.Position);
            }
        }
        private void chaptersDGV_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {   // Changes chapter title from editbox
            Track.ChapterList.SaveChaptersToFile();
        }

        public event EventHandler<int> RequestPlayToggleEvent;

        protected virtual void RequestPlayToggle(int value)
        {
            RequestPlayToggleEvent?.Invoke(this, value);
        }


        public event EventHandler<FileInfo> RequestPlayFileEvent;
        protected virtual void RequestPlayNow(FileInfo file)
        {
            RequestPlayFileEvent?.Invoke(this, file);
        }

        public event EventHandler<int> RequestPositionEvent;
        protected virtual void RequestPlayerTime(int position)
        {   
            RequestPositionEvent?.Invoke(this, position);
        }

        public event EventHandler<string> AddChapterButtonClickedRouted;
        protected virtual void OnAddChapterButtonClickedRouted(string newChapterName)
        {
            AddChapterButtonClickedRouted?.Invoke(this, newChapterName);
            _chapterListBindingSource.ResetBindings(false);
        }

        public event EventHandler<Chapter> RemoveChapterButtonClickedRouted;
        protected virtual void OnRemoveChapterButtonClickedRouted(Chapter e)
        {
            RemoveChapterButtonClickedRouted?.Invoke(this, e);
            _chapterListBindingSource.ResetBindings(false);
        }

        public event EventHandler<ChapterChangeEventArgs> ChangeChapterRequested;
        protected virtual void OnChangeChapterRequested(Chapter c, string newTitle, int?  newPosition)
        {
            var chapterChange = !newPosition.HasValue ? 
                new ChapterChangeEventArgs(c, newTitle, c.Position) : 
                new ChapterChangeEventArgs(c, c.Title, newPosition.Value);
            ChangeChapterRequested?.Invoke(this, chapterChange);
        }
        private void OnChaptersDgvCellClick(object sender, DataGridViewCellEventArgs e)
        {   // A-B Repeating
            if((e.ColumnIndex == 0) )
            {
                Chapter toRepeat = (Chapter) chaptersDGV.Rows[e.RowIndex].DataBoundItem;
                RepeatSection.ReceiveChapter(toRepeat, Track.NowPlayingTrackInfo.Duration);
                UpdateFirstColumn();
                SetCurrentChapterImage();
            }
        }

        private void OnChaptersDgvSelectionChanged(object sender, EventArgs e)
        {   
            
            if (chaptersDGV.Rows.Count > 1) // Don't call SCCPI until after current chapter has changed.
            {
                if (CurrentChapter == null || chaptersDGV.SelectedRows.Count < 1) return;
                DataGridViewRow selectedChapterRow = chaptersDGV.SelectedRows[0];
                if (selectedChapterRow.Index == CurrentChapter.ChapterNumber- 1)
                {
                    SetCurrentChapterImage();
                }
                else
                {
                    UpdateFirstColumn();
                }
            }
        }

        private void SetCurrentChapterImage()
        {
            DataGridViewRow selectedRow = chaptersDGV.SelectedRows[0];
            if (selectedRow.Index == CurrentChapter.ChapterNumber - 1)
            {   // Current chapter is selected
                if (selectedRow.Index == RepeatSection.A?.ChapterNumber - 1
                    || selectedRow.Index == RepeatSection.B?.ChapterNumber - 1)
                {
                    SetReplayImage("white", selectedRow);
                }
                else
                {
                    SetPlayheadImage("white");
                }
            }
            else
            {   // Other chapter is selected
                SetPlayheadImage("black");
            }
        }

        private void SetReplayImage(string color, DataGridViewRow row)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            Stream myPlayheadStream = asm.GetManifestResourceStream($"ChapterListMB.Resources.replaychapter-{color}.png");
            row.Cells[0].Value = Image.FromStream(myPlayheadStream);
        }

        private void SetPlayheadImage(string color)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            Stream myPlayheadStream = asm.GetManifestResourceStream($"ChapterListMB.Resources.activechapter-{color}.png");
            chaptersDGV.Rows[CurrentChapter.ChapterNumber - 1].Cells[0].Value = Image.FromStream(myPlayheadStream);
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                if (chkFindSelect.Checked == true)
                {
                    filterLirics();
                }
                else
                    setLirics();
            }
        }

        

        

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            var snd = sender as ListBox;
            if (snd == null)
                return;
            var txt = snd.SelectedItem.ToString();

            var mill = SyncViewRepository.GetMilli(txt);

            RequestPlayerTime(mill);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                SearchOrLocate();
            }
        }

        private void SearchOrLocate()
        {
            var sought = txtAllTranscriptsFilter.Text;
            // L1P1[042:22.200]
            Regex r = new Regex(@"L(?<L>\d+)P(?<P>\d+)(?<position>\[.+])");
            var m = r.Match(sought);
            if (m.Success)
            {
                var mediaFile = SyncViewRepository.AudioFromLP(m.Groups["L"].Value, m.Groups["P"].Value);
                var mill = SyncViewRepository.GetMilli(m.Groups["position"].Value + " ");
                AudioJumpTo(mediaFile, mill);
            }
            searchAllTranscripts(sought);
        }

        private void searchAllTranscripts(string sought)
        {
            treeView1.Nodes.Clear();
            foreach (var trsfile in SyncViewRepository.GetTranscripts())
            {
                var thisList = SyncViewRepository.GetLyricsText(trsfile, sought).ToList();
                if (thisList.Any())
                {
                    var node = new TreeNode()
                    {
                        Text = trsfile.Name
                    };

                    foreach (var transcrriptLine in thisList)
                    {
                        var subnode = new TreeNode()
                        {
                            Text = transcrriptLine
                        };
                        node.Nodes.Add(subnode);
                    }
                    treeView1.Nodes.Add(node);
                }
            }
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            var snd = sender as TreeView;
            if (snd == null)
                return;
            var txt = snd.SelectedNode.Text;
            if (string.IsNullOrEmpty(txt))
                return;

            var folderNode = snd.SelectedNode;
            var mill = SyncViewRepository.GetMilli(txt);
            if (mill != -1)
            {
                folderNode = snd.SelectedNode.Parent;
            }

            txt = folderNode.Text;
            var f = repo.AudioFromTrascriptName(txt);
            AudioJumpTo(f, mill);
        }

        private void AudioJumpTo(FileInfo mediaFile, int mill)
        {
            if (mediaFile != null && mediaFile.FullName != repo.mediaFileName)
            {
                RequestPlayNow(mediaFile);
            }
            if (mill != -1)
            {
                RequestPlayerTime(mill);
            }
        }

        private void reloadImagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            repo.ReloadImages();
            cmbImage.Items.Clear();
            cmbImage.Items.AddRange(repo.Images.ToArray());
        }

        private void lblImageTime_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(lblImageTime.Text + " - ");
            lblImageTime.Text = "Clip";
        }

        private void chkFindSelect_CheckedChanged(object sender, EventArgs e)
        {
            setLirics();
        }

        private void lblLyricsTime_Click(object sender, EventArgs e)
        {
            this.TopMost = !this.TopMost;
            UpdateTopMostText();
        }

        private void UpdateTopMostText()
        {
            if (this.TopMost)
                lblTopmost.Text = "Is topmost";
            else
                lblTopmost.Text = "Not topmost";
        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(lblLirycsTime.Text);
            lblLirycsTime.Text = "Clip";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var last = 0;
            for (int i = listBox1.Items.Count-1; i > 0; i--)
            {
                var itemMilli = SyncViewRepository.GetMilli(listBox1.Items[i].ToString());
                if (itemMilli < CurrentMilli)
                {
                    last = i;
                    break;
                }
            }
            listBox1.SelectedItems.Clear();
            listBox1.SelectedIndex = listBox1.Items.Count - 1; // scroll selected element to first position?
            listBox1.SelectedIndex = last;
            listBox1.Refresh();
        }

        private void copyImageTimestampToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var txt = listBox1.SelectedItem.ToString();
            if (string.IsNullOrEmpty(txt))
                return;
            int milli = SyncViewRepository.GetMilli(txt);
            if (milli != -1)
                Clipboard.SetText(SyncViewRepository.GetImagesTimestamp(milli) + " - ");
        }

        private void copyLyricsTimestampToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ret = GetSelectedTransriptTime();
            if (ret != "")
                Clipboard.SetText(ret);
        }

        private string GetSelectedTransriptTime()
        {
            var txt = listBox1.SelectedItem.ToString();
            if (string.IsNullOrEmpty(txt))
                return "";
            int milli = SyncViewRepository.GetMilli(txt);
            var ret = "";
            if (milli != -1)
            {
                ret = SyncViewRepository.GetLyricsTimestamp(milli);
            }
            return ret;
        }

        private void jumpToNextImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var next = repo.Images.NextObjectTime;
            if (next == -1 || next == int.MaxValue) 
                return;
            RequestPlayerTime(next);
        }

        private void cmdSetImageName_Click(object sender, EventArgs e)
        {
            if (txtImageName.Text.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            {
                txtImageName.Text = "INVALID - " + txtImageName.Text;
                return;
            }
            var success = repo.TrySetImageName(txtImageName.Text);
            if (success)
            {
                cmbImage.Items.Clear();
                cmbImage.Items.AddRange(repo.Images.ToArray());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var img = cmbImage.SelectedItem as ImageInfo;
            if (img == null)
                return;
            if (img.TimeStampMilliseconds == -1)
                return;
            RequestPlayerTime(img.TimeStampMilliseconds);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RequestPlayToggle(0);
        }

        private void copyImagesAsLyricsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var image in repo.Images)
            {
                sb.AppendLine(SyncViewRepository.GetLyricsTimestamp(image.TimeStampMilliseconds) + " " + image.GetName());
            }
            Clipboard.SetText(sb.ToString());
        }

        private void saveAllImagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var image in repo.Images)
            {
                var name = image.GetName();
                image.TrySetImageName(name);
                // sb.AppendLine(SyncViewRepository.GetLyricsTimestamp(image.computedTimeStampMilliseconds) + " " + image.GetName());
            }
            reloadImagesToolStripMenuItem_Click(null, null);
        }

        private void cmdSetNextSlideTime_Click(object sender, EventArgs e)
        {
            int thisIndex = cmbImage.SelectedIndex + 1;
            if (repo.Images.Count() <= thisIndex)
                return;
            var nextImage = repo.Images[thisIndex];
            nextImage.TrySetImageTime(CurrentMilli);

            repo.ReloadImages();
            reloadImagesToolStripMenuItem_Click(null, null);

        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (lastPC != null)
                SetPointer(lastPC);
        }

        int pSize = 5;

        private void cmdPointer_Click(object sender, EventArgs e)
        {
            if (pSize == 5)
                pSize = 11;
            else if (pSize == 11)
                pSize = 21;
            else
                pSize = 5;
            pnlPointer.Size = new Size(pSize, pSize);
            SetPointer(lastPC);
        }

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            var control = ActiveControl;
            var container = control as IContainerControl;
            while (container != null)
            {
                control = container.ActiveControl;
                container = control as IContainerControl;
            }

            if (control.GetType() == typeof(TextBox))
            {
                e.Handled = false;
                return;
            }
            if (e.KeyChar == ' ')
            {
                RequestPlayToggle(0);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            RequestPlayToggle(0);
        }

        private void deleteThisImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var success = repo.DeleteCurrentImage();
            if (success)
            {
                cmbImage.Items.Clear();
                cmbImage.Items.AddRange(repo.Images.ToArray());
            }
        }

        private void copyTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string AllText = SelectedTranscriptText();
            Clipboard.SetText(AllText);

        }

        private string SelectedTranscriptText()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in listBox1.SelectedItems)
            {
                sb.AppendLine(item.ToString());
            }
            var AllText = sb.ToString();
            return AllText;
        }

        private void copyBareTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string AllText = SelectedTranscriptText();
            Clipboard.SetText(SyncViewRepository.regexGetLyricsTime.Replace(AllText, ""));
        }

        private void copyAbsoluteTimestampToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var t = repo.GetLectureCode() + GetSelectedTransriptTime();
            t = t.Trim();
            if (!string.IsNullOrEmpty(t))
                Clipboard.SetText(t);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SearchOrLocate();
        }

        private void pictureBox1_DragDrop(object sender, DragEventArgs e)
        {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            var first = files.FirstOrDefault();
            if (first == null)
                return;

            // C:\Data\Work\Esame Stato\SupportingMedia\L04\P01\6627 - Orientamento - Serra solare apribile.png
            Regex r = new Regex(@"\\L(?<L>\d+)\\(?<P>\d+)\\(?<t>\d+) -");
            var m = r.Match(first);
            if (m.Success)
            {
                var mediaFile = SyncViewRepository.AudioFromLP(m.Groups["L"].Value, m.Groups["P"].Value);
                var mill = ImageInfo.GetMillisecondsFromFileName(new FileInfo(first));
                AudioJumpTo(mediaFile, mill);
            }
        }

        private void MainForm_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
        }

        private void pictureBox1_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            var first = files.FirstOrDefault();
            if (first == null)
                return;

            // C:\Data\Work\Esame Stato\SupportingMedia\L04\P01\6627 - Orientamento - Serra solare apribile.png
           
            Regex r = new Regex(@"\\L(?<L>\d+)\\P(?<P>\d+)\\(?<t>\d+) -");
            var m = r.Match(first);
            if (m.Success)
            {
                var mediaFile = SyncViewRepository.AudioFromLP(m.Groups["L"].Value, m.Groups["P"].Value);
                var mill = ImageInfo.GetMillisecondsFromFileName(new FileInfo(first));
                AudioJumpTo(mediaFile, mill);
            }
        }

        private void openInExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var im = repo.GetLastImage();
            
            if (!im.Exists)
            {
                return;
            }
            // combine the arguments together
            // it doesn't matter if there is a space after ','
            string argument = "/select, \"" + im.FullName + "\"";

            Process.Start("explorer.exe", argument);
        }
    }
}
