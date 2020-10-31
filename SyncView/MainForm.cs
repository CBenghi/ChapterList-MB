using ChapterListMB;
using ChapterListMB.SyncView;
using PresentationGrab;
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

            UpdateTrackDelegate = UpdateTrackMethod;
            
            SetTimeDelegate = SetCurrentTime;

            irProperty = pictureBox1.GetType().GetProperty("ImageRectangle", BindingFlags.GetProperty | BindingFlags.NonPublic | BindingFlags.Instance);

            this.TopMost = false;
            UpdateTopMostText();
        }

        int CurrentMilli = -1;
        int lastTranscriptLocate = -1;

        // TIMER BASED, not called from musicBee
        public void SetCurrentTime(int playerPosition)
        {
            // lblPos.Text = playerPosition.ToString();
            if (repo != null)
            {
                // status bar
                lblImageTime.Text = SyncViewRepository.GetImagesTimestamp(playerPosition);
                lblLirycsTime.Text = SyncViewRepository.GetLyricsTimestamp(playerPosition);
                lblNext.Text = $"- {(repo.Images.NextObjectTime - playerPosition ) / 1000} sec.";

                // update transcript current
                var diffmilli = Math.Abs(playerPosition - lastTranscriptLocate);
                if (diffmilli > 500)
                {
                    LocateTranscript();
                }
                CurrentMilli = playerPosition;
                
                // update image current
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
                if (requestBookMarkLocate)
                    BookmarkLocate();

                // update pointer current
                var pointerIndex = repo.Pointers.GetIndex(playerPosition);
                if (pointerIndex != -1)
                {
                    SetPointer(repo.Pointers[pointerIndex]);
                }
            }
        }

        int lastCurrent = -1;

        private void LocateTranscript()
        {
            bool doRefresh = false;
            if (lstBookmarks.Items.Count == 0)
                return;
            var now = GetCurrentTranscriptIndex();
            if (lastCurrent == now)
                return;

            if (lstBookmarks.Items[now].ImageIndex == 3) // this is a skip
            {
                DoSkipCommand(lstBookmarks.Items[now].Tag as Bookmark);
            }

            if (lastCurrent != -1 && lstBookmarks.Items.Count > lastCurrent)
            {
                if (lstBookmarks.Items[lastCurrent].ImageIndex != 0)
                    doRefresh = true;
                var index = (int)((lstBookmarks.Items[lastCurrent].Tag as Bookmark)?.Type);
                lstBookmarks.Items[lastCurrent].ImageIndex = index;
            }
            else
            {
                doRefresh = true;
            }
            lastCurrent = now;
            try
            {
                lstBookmarks.Items[now].ImageIndex = 0;
            }
            catch (Exception)
            {

            }
            if (doRefresh)
                lstBookmarks.Refresh();
        }

        private void DoSkipCommand(Bookmark item)
        {
            if (item == null)
                return;
            var position = item.GetSkipIndex();
            if (position != -1)
            {
                RequestPlayerTime(position);
            }
            else
                JumpToNextImage();
            
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
            populateBookmarks();

            this.Text = "SyncView - " + repo.mediaFileName;
            
            titleArtistStatusLabel.Text = $"{Track.NowPlayingTrackInfo.Artist} – {Track.NowPlayingTrackInfo.Title}";
            if (Track.ChapterList.NumChapters == 0)
                chaptersCountStatusLabel.Text = "No Chapters";
        }

        private void populateBookmarks()
        {
            //StackTrace st = new StackTrace();
            //var frms = st.GetFrames().Select(x => x.GetMethod()).Where(m => m.Module.Assembly == Assembly.GetExecutingAssembly()).Select(f => f.Name);
            //Debug.WriteLine("  Calling from: " + string.Join(", ", frms));
            var bks = new List<Bookmark>();           
            if (!chkHiglightTranscript.Checked)
            {
                if (chkSingleAudioShowTranscripts.Checked)
                    bks.AddRange(repo.GetTranscriptBookmarks(txtFilter.Text));
                if (chkSingleAudioShowImages.Checked)
                    bks.AddRange(repo.GetImagesBookmarks(txtFilter.Text));
                setBookmarks(bks);
            }
            else
            {
                if (chkSingleAudioShowTranscripts.Checked)
                    bks.AddRange(repo.GetTranscriptBookmarks(""));
                if (chkSingleAudioShowImages.Checked)
                    bks.AddRange(repo.GetImagesBookmarks(""));

                setBookmarks(bks);
                highlightBookmarks();
            }
        }

        private void setBookmarks(List<Bookmark> bks)
        {
            bks.Sort();

            lstBookmarks.Items.Clear();
            foreach (var item in bks)
            {
                var newLvi = new ListViewItem();
                newLvi.Tag = item;
                newLvi.Text = item.Text;
                newLvi.ImageIndex = (int)item.Type;
                lstBookmarks.Items.Add(newLvi);
            }
        }

        private void highlightBookmarks()
        {
            lstBookmarks.SelectedItems.Clear();
            if (txtFilter.Text == "")
                return;
            for (int i = lstBookmarks.Items.Count-1; i >= 0; i--)
            {
                if (SyncViewRepository.DefaultTextMatch(txtFilter.Text, lstBookmarks.Items[i].Text))
                {
                    lstBookmarks.SelectedIndices.Add(i);
                }
            }
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
            lstBookmarks.Refresh();
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                RefreshSearch();
            }
        }

        private void RefreshSearch()
        {
            if (chkHiglightTranscript.Checked == true)
            {
                highlightBookmarks();
                lstBookmarks.Focus();
            }
            else
                populateBookmarks();
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            var snd = sender as ListView;
            if (snd == null)
                return;
            var txt = snd.SelectedItems[0].Tag as Bookmark;
            if (txt == null)
                return;
            RequestPlayerTime(txt.Timing);
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
            var m = Regexes.regexUnnamedAbsolutePosition.Match(sought);
            if (m.Success)
            {
                // find a particular position in the repo.
                var s = new Session(m.Groups["L"].Value, m.Groups["P"].Value);
                var mediaFile = s.GetAudioFile(repo);
                var mill = SyncViewRepository.GetMilli(m.Groups["position"].Value + " ");
                AudioJumpTo(mediaFile, mill);
                tabControl1.SelectedIndex = 0;
                return;
            }
            searchAll(sought);
        }

        private void searchAll(string sought)
        {
            allReposSearchResults.Nodes.Clear();

            // creates the list first (images and text)

            List<Bookmark> bookmarks = new List<Bookmark>();
            if (chkSearchT.Checked)
            {
                foreach (var trsfile in repo.GetTranscripts())
                {
                    var curSession = Session.FromTranscriptFile(trsfile.Name);
                    var thisList = SyncViewRepository.GetLyricsText(trsfile, sought, false).ToList();
                    foreach (var transcrriptLine in thisList)
                    {
                        Bookmark b = new Bookmark();
                        b.session = curSession;
                        b.Text = transcrriptLine;
                        b.Timing = SyncViewRepository.GetMilli(transcrriptLine);
                        b.Type = Bookmark.SourceType.Transcript;
                        bookmarks.Add(b);
                    }
                }
            }
            if (chkSearchI.Checked)
            {
                bookmarks.AddRange(repo.FindImagesAllRepos(sought));
            }

            // then sort and show.
            //
            bookmarks.Sort();
            Session sortSession = null;
            TreeNode n = null;
            foreach (var item in bookmarks)
            {
                if (!item.session.Equals(sortSession))
                {
                    n = new TreeNode();
                    n.Text = item.session.GetMediaTitle(repo);
                    if (n.Text == "")
                        n.Text = item.session.ToString();
                    n.Tag = item.session;
                    n.ImageIndex = 0;
                    allReposSearchResults.Nodes.Add(n);
                    sortSession = item.session;
                }
                TreeNode sub = new TreeNode();
                sub.Text = item.Text;
                sub.Tag = item;
                if (item.Type == Bookmark.SourceType.Image)
                    sub.ImageIndex = 1;
                else
                    sub.ImageIndex = 2;
                n.Nodes.Add(sub);
            }
            allReposSearchResults.ExpandAll();
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            var snd = sender as TreeView;
            if (snd == null)
                return;
            var txt = snd.SelectedNode.Text;
            if (string.IsNullOrEmpty(txt))
                return;
            
            if (snd.SelectedNode.Tag is Bookmark b)
            {
                var f = b.session.GetAudioFile(repo);
                AudioJumpTo(f, b.Timing);
                if (chkSwitch.Checked)
                    tabControl1.SelectedIndex = 0;
            }
            else if (snd.SelectedNode.Tag is Session s)
            {
                var f = s.GetAudioFile(repo);
                AudioJumpTo(f, 0);
                if (chkSwitch.Checked)
                    tabControl1.SelectedIndex = 0;
            }
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
            repo.Save();

            repo.ReloadImages();
            cmbImage.Items.Clear();
            cmbImage.Items.AddRange(repo.Images.ToArray());
            populateBookmarks();
            requestBookMarkLocate = true;
        }

        private void lblImageTime_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(lblImageTime.Text + " - ");
            lblImageTime.Text = "Clip";
        }

        private void chkFindSelect_CheckedChanged(object sender, EventArgs e)
        {
            populateBookmarks();
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

        private void btnLocate_Click(object sender, EventArgs e)
        {
            BookmarkLocate();
        }

        private void BookmarkLocate()
        {
            if (lstBookmarks.Items.Count == 0)
                return;
            int last = GetCurrentTranscriptIndex() - 3;
            last = Math.Max(last, 0);
            
            lstBookmarks.TopItem = lstBookmarks.Items[last];
            requestBookMarkLocate = false;
        }

        private int GetCurrentTranscriptIndex()
        {
            var last = 0;
            for (int i = lstBookmarks.Items.Count - 1; i > 0; i--)
            {
                if (!(lstBookmarks.Items[i].Tag is Bookmark bk))
                    continue;
                if (bk.Timing < CurrentMilli)   
                {
                    last = i;
                    break;
                }
            }
            return last;
        }

        private void copyImageTimestampToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var bk = lstBookmarks.SelectedItems[0].Tag as Bookmark;
            if (bk == null)
                return;
            int milli = bk.Timing;
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
            var bk = lstBookmarks.SelectedItems[0].Tag as Bookmark;
            if (bk == null)
                return "";

            var milli = bk.Timing;
            var ret = "";
            if (milli != -1)
            {
                ret = SyncViewRepository.GetLyricsTimestamp(milli);
            }
            return ret;
        }

        private void jumpToNextImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            JumpToNextImage();
        }

        private void JumpToNextImage()
        {
            var next = repo.Images.NextObjectTime;
            if (next == -1 || next == int.MaxValue)
                return;
            RequestPlayerTime(next);
            requestBookMarkLocate = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var img = cmbImage.SelectedItem as ImageInfo;
            if (img == null)
                return;
            if (img.TimeStampMilliseconds == -1)
                return;
            RequestPlayerTime(img.TimeStampMilliseconds);
            requestBookMarkLocate = true;
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
            cmbImage.DropDownWidth = pictureBox1.Width - cmbImage.Left;
        }

        int pSize = 21;

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

        private string SelectedTranscriptText(bool bare = false)
        {
            StringBuilder sb = new StringBuilder();
            foreach (ListViewItem item in lstBookmarks.SelectedItems)
            {
                if (bare)
                {
                    Bookmark b = item.Tag as Bookmark;
                    sb.AppendLine(SyncViewRepository.regexGetLyricsTime.Replace(b.Text, ""));
                }
                else
                {
                    sb.AppendLine(item.Text);
                }
            }
            var AllText = sb.ToString();
            return AllText;
        }

        private void copyBareTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(SelectedTranscriptText(true));
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
            
            var b = Bookmark.FromImageFile(new FileInfo(first));
            if (b!= null)
            {
                AudioJumpTo(b);
            }
        }

        private void AudioJumpTo(Bookmark b)
        {
            AudioJumpTo(b.session.GetAudioFile(repo), b.Timing);
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
            
            // todo: use bookmark instead
            var s = Session.FromImageFile(new FileInfo(first));
            if (s != null)
            {
                var mediaFile = s.GetAudioFile(repo);
                var mill = ImageInfo.GetMillisecondsFromFileName(new FileInfo(first));
                AudioJumpTo(mediaFile, mill);
            }
        }

        private void cmdSetImageName_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                jumpToNextImageToolStripMenuItem_Click(null, null);
                return;
            }
            SaveImageName();
        }

        bool requestBookMarkLocate = true;

        private void SaveImageName()
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
                populateBookmarks();
                requestBookMarkLocate = true;             
            }
        }

        private void lstBookmarks_Resize(object sender, EventArgs e)
        {
            lstBookmarks.Columns[0].Width = lstBookmarks.Width - 16;
        }

        private void txtImageName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                SaveImageName();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var t = cmbImage.SelectedItem as ImageInfo;
            if (t == null)
                return;
            txtImageName.Text = t.GetName();
        }

        private void openImageManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PreProcess p = new PreProcess();
            p.Repository = this.repo;
            p.ShowDialog();
            reloadImagesToolStripMenuItem_Click(null, null);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            MainForm_Resize(null, null); // sets the combo width
        }

        private void fileExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var im = repo.GetLastImage();

            if (im == null || !im.Exists)
            {
                return;
            }
            // combine the arguments together
            // it doesn't matter if there is a space after ','
            string argument = $"/select, \"{im.FullName}\"";
            Process.Start("explorer.exe", argument);
        }

        private void paintNetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var im = repo.GetLastImage();

            if (im == null || !im.Exists)
            {
                return;
            }
            // combine the arguments together
            // it doesn't matter if there is a space after ','
            string argument = $"\"{im.FullName}\"";
            Process.Start(@"C:\Program Files\paint.net\PaintDotNet.exe", argument);
        }

        private void chkSingleAudioShow_CheckedChanged(object sender, EventArgs e)
        {
            RefreshSearch();
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            JumpToNextImage();
        }

        private void createHTMLFromImagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var imgp = repo.GetImagePath();

            var fi = new FileInfo(repo.mediaFileName);
            var pageName = Path.ChangeExtension(fi.Name, "html");
            var FullPage = new FileInfo(Path.Combine(imgp.FullName, pageName));

            var style = @"
<style>
.cropped {
    width: 1800px; /* width of container */
    height: 920px; /* height of container */
    overflow: hidden;
    /* border: 1px solid white; */
	margin: 30px;
}

.cropped img {
	/* top, right, bottom, left */
    margin: -95px 0px 0px -80px;
}
</style>
";


            using (var p = FullPage.CreateText())
            {
                p.WriteLine("<!DOCTYPE html>");
                p.WriteLine("<html>");

                p.WriteLine("<head>");
                p.WriteLine($"<title>AFC - {repo.mediaFileName}</title>");
                p.WriteLine($"{style}");

                p.WriteLine("</head>");

                p.WriteLine("<body>");
                foreach (var image in repo.Images)
                {
                    p.WriteLine($"<h1>{image.GetName()}</h1>");
                    p.WriteLine($"<div class=\"cropped\"><img src=\"{image.file.Name}\" /></div>");
                }
                
                p.WriteLine("</body>");
                p.WriteLine("</html>");
            }
        }

        private void skipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sel1 = lstBookmarks.SelectedItems.Cast<ListViewItem>().ToList();
            var sel2 = sel1.Select(x => x.Tag as Bookmark);
            var min = sel2.Min(x => x.Timing);
            var max = sel2.Max(x => x.Timing);
            repo.AddSkip(min, max);
            populateBookmarks();
            BookmarkLocate();
        }

        private void copyImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetImage(pictureBox1.Image);
        }

		private void openTranscriptFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
            var t = repo.GetAssociatedLyricsFile();
            if (!t.Exists)
                return;
            Process.Start(t.FullName);
        }
	}
}
