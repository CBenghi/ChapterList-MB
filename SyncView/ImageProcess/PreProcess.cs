using Accord.Imaging.Filters;
using ChapterListMB.SyncView;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace PresentationGrab
{
    public partial class PreProcess : Form
    {
        private SyncViewRepository repository;

        public PreProcess()
        {
            InitializeComponent();
        }

        internal SyncViewRepository Repository
        {
            get => repository; 
            set
            {
                repository = value;
            }
        }
        int currentImage = 0;

        private void PreProcess_Load(object sender, EventArgs e)
        {
            if (repository == null)
                return;
            currentImage = repository.Images.LastObjectPolled;
            Move(0);
            UpdateImages();
        }

        private void UpdateImages()
        {
            var delta = repository.Images[currentImage+1].TimeStampMilliseconds
                - repository.Images[currentImage].TimeStampMilliseconds;
            TimeSpan t = new TimeSpan(0, 0, 0, 0, delta);
            

            Bitmap b0 = getB(repository.Images[currentImage].file.FullName);
            lblCurr.Text = repository.Images[currentImage].file.Name + " " + t.ToString();
            lblCurr.ForeColor = t.TotalMilliseconds <= 5000
                ? System.Drawing.Color.Red
                : System.Drawing.Color.Black;

            Bitmap b1 = getB(repository.Images[currentImage + 1].file.FullName);
            lblNext.Text = repository.Images[currentImage + 1].file.Name;

            UpdateDifference(b0, b1);

            imgLeft.Image = b0;
            imgRight.Image = b1;
            
        }

        private void UpdateDifference(Bitmap b0, Bitmap b1)
        {
            Difference d = new Difference(b0);
            var diff = d.Apply(b1);
            var unm = Accord.Imaging.UnmanagedImage.FromManagedImage(diff);

            if (chkEnhanceDiff.Checked)
            {
                Add a = new Add(unm);
                Multiply m = new Multiply(unm);
             
                a.ApplyInPlace(unm);

                a.ApplyInPlace(unm);
            }
            imgBig.Image = unm.ToManagedImage();
        }

        private Bitmap getB(string path)
        {
            var bytes = File.ReadAllBytes(path);
            var ms = new MemoryStream(bytes);
            var img = Image.FromStream(ms);
            return (Bitmap)img;
        }

        private void cmdPrev_Click(object sender, EventArgs e)
        {
            Move(-1);
        }

        private void Move(int delta)
        {
            var t = currentImage + delta;
            if (t < 0)
            {
                t = 0;
            }
            else if (t >= repository.Images.Count() - 1)
            {
                t = repository.Images.Count() - 2;
            }
            currentImage = t;
            UpdateImages();
        }

        private void cmdNext_Click(object sender, EventArgs e)
        {
            Move(1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var fname = repository.Images[currentImage].file;
                FileSystem.DeleteFile(fname.FullName, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                repository.ReloadImages();
                Move(0);
            }
            catch (Exception)
            {

            }

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var fileRetainName = repository.Images[currentImage].file;
            var fileRetainImage = repository.Images[currentImage + 1].file;
            var fullRetainName = fileRetainName.FullName;
            FileSystem.DeleteFile(fullRetainName, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
            File.Move(fileRetainImage.FullName, fullRetainName);
            repository.ReloadImages();
            Move(0);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                var fname = repository.Images[currentImage+1].file;
                FileSystem.DeleteFile(fname.FullName, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                repository.ReloadImages();
                Move(0);
            }
            catch (Exception)
            {

            }
        }

        private void chkEnhanceDiff_CheckedChanged(object sender, EventArgs e)
        {
            UpdateImages();
        }
    }
}
