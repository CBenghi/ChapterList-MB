using Accord.Imaging;
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

        readonly BlobCounterBase differenceBlobFounder;

        public PreProcess()
        {
            InitializeComponent();

            differenceBlobFounder = new BlobCounter
            {
                // set filtering options
                FilterBlobs = true,
                MinWidth = 6,
                MinHeight = 6,
                ObjectsOrder = ObjectsOrder.XY
            };
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
            MoveToRelativeImage(0);
            UpdateImages();
        }

        private int UpdateImages()
        {
            var delta = repository.Images[currentImage + 1].TimeStampMilliseconds
                - repository.Images[currentImage].TimeStampMilliseconds;
            TimeSpan t = new TimeSpan(0, 0, 0, 0, delta);

            Bitmap b0 = getB(repository.Images[currentImage].file.FullName);
            lblCurr.Text = repository.Images[currentImage].file.Name + " " + t.ToString();
            lblCurr.ForeColor = t.TotalMilliseconds <= 5000
                ? System.Drawing.Color.Red
                : System.Drawing.Color.Black;

            Bitmap b1 = getB(repository.Images[currentImage + 1].file.FullName);
            lblNext.Text = repository.Images[currentImage + 1].file.Name;

            var ret = UpdateDifference(b0, b1);

            var size = (b0.Width * b0.Height) + 1.0;
            var perc = 100 * ret / size;

            lblPosition.Text = $"#{currentImage + 1} of {repository.Images.Count()} - Delta: {perc:0.0}%, {ret} px ";

            imgLeft.Image = b0;
            imgRight.Image = b1;
            return ret;
        }

        private int UpdateDifference(Bitmap b0, Bitmap b1)
        {
            Difference d = new Difference(b0);

            try
            {
                var unmmanagedDiff = Accord.Imaging.UnmanagedImage.FromManagedImage(d.Apply(b1));

                Add a = null;
                Multiply m = null;
                int i = (int)nudEnhanceDiff.Value;
                while (i-- > 0)
                {
                    if (a == null)
                        a = new Add(unmmanagedDiff);
                    if (m == null)
                        m = new Multiply(unmmanagedDiff);
                    a.ApplyInPlace(unmmanagedDiff);
                    // m.ApplyInPlace(unmmanagedDiff);
                }

                //else if (i == 2)
                //{
                //    Bitmap basegrey = new Bitmap(unmmanagedDiff.Width, unmmanagedDiff.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                //    Graphics graphics = Graphics.FromImage(basegrey as System.Drawing.Image);
                //    int intensity = 255;
                //    graphics.Clear(System.Drawing.Color.FromArgb(255, intensity, intensity, intensity));
                //    m = new Multiply(basegrey);
                //    m.ApplyInPlace(unmmanagedDiff);
                //}

                differenceBlobFounder.ProcessImage(unmmanagedDiff);

                Blob[] blobs = differenceBlobFounder.GetObjectsInformation();
                var totArea = blobs.Sum(x => x.Area);


                imgBig.Image = unmmanagedDiff.ToManagedImage();
                return totArea;
            }
            catch (Exception)
            {
                imgBig.Image = null;
                return b0.Width * b0.Height;
            }
            
           
        }

        private Bitmap getB(string path)
        {
            var bytes = File.ReadAllBytes(path);
            var ms = new MemoryStream(bytes);
            var img = System.Drawing.Image.FromStream(ms);
            return (Bitmap)img;
        }

        private void cmdPrev_Click(object sender, EventArgs e)
        {
            MoveToRelativeImage(-1);
        }

        private void MoveToRelativeImage(int delta)
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
            MoveToRelativeImage(1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var fname = repository.Images[currentImage].file;
                FileSystem.DeleteFile(fname.FullName, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                repository.ReloadImages();
                MoveToRelativeImage(0);
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
            MoveToRelativeImage(0);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                var fname = repository.Images[currentImage+1].file;
                FileSystem.DeleteFile(fname.FullName, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                repository.ReloadImages();
                MoveToRelativeImage(0);
            }
            catch (Exception)
            {

            }
        }

        private void nudEnhanceDiff_ValueChanged(object sender, EventArgs e)
        {
            UpdateImages();
        }
    }
}
