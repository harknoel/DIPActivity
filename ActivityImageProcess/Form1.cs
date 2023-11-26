namespace ActivityImageProcess
{
    public partial class Form1 : Form
    {

        Bitmap loaded;
        Bitmap processed;
        Bitmap imageB, imageA, colorgreen;
        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "PNG Image|*.png|JPEG Image|*.jpg|All Files|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                loaded = new Bitmap(ofd.FileName);
                pictureBox1.Image = loaded;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (loaded == null || processed == null)
                {
                    throw new NullReferenceException();
                }
                saveFileDialog1.Filter = "PNG Image|*.png|JPEG Image|*.jpg|All Files|*.*";
                saveFileDialog1.ShowDialog();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Please select an image.");
            }
        }

        private void saveFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            processed.Save(saveFileDialog1.FileName);
        }
        private void basicCopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Color pixel;
                processed = new Bitmap(loaded.Width, loaded.Height);

                for (int x = 0; x < loaded.Width; x++)
                {
                    for (int y = 0; y < loaded.Height; y++)
                    {
                        pixel = loaded.GetPixel(x, y);
                        processed.SetPixel(x, y, pixel);
                    }
                }
                pictureBox2.Image = processed;
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Please select an image.");
            }


        }

        private void greyscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Color pixel;
                int gray;
                processed = new Bitmap(loaded.Width, loaded.Height);

                for (int x = 0; x < loaded.Width; x++)
                {
                    for (int y = 0; y < loaded.Height; y++)
                    {
                        pixel = loaded.GetPixel(x, y);
                        gray = (byte)((pixel.R + pixel.G + pixel.B) / 3);
                        processed.SetPixel(x, y, Color.FromArgb(gray, gray, gray));
                    }
                }
                pictureBox2.Image = processed;
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Please select an image.");
            }
        }

        private void colorInversionToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                Color pixel;
                processed = new Bitmap(loaded.Width, loaded.Height);

                for (int x = 0; x < loaded.Width; x++)
                {
                    for (int y = 0; y < loaded.Height; y++)
                    {
                        pixel = loaded.GetPixel(x, y);
                        processed.SetPixel(x, y, Color.FromArgb(255 - pixel.R, 255 - pixel.G, 255 - pixel.B));
                    }
                }

                pictureBox2.Image = processed;
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Please select an image.");
            }

        }

        private void histogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Color pixel;
                int gray;
                processed = new Bitmap(loaded.Width, loaded.Height);

                for (int x = 0; x < loaded.Width; x++)
                {
                    for (int y = 0; y < loaded.Height; y++)
                    {
                        pixel = loaded.GetPixel(x, y);
                        gray = (byte)((pixel.R + pixel.G + pixel.B) / 3);
                        processed.SetPixel(x, y, Color.FromArgb(gray, gray, gray));
                    }
                }

                Color sample;
                int[] histdata = new int[256];
                for (int x = 0; x < loaded.Width; x++)
                {
                    for (int y = 0; y < loaded.Height; y++)
                    {
                        sample = loaded.GetPixel(x, y);
                        histdata[sample.R]++;
                    }
                }


                // Make the color of the Histogram
                int COLOR_WIDTH = 256;
                int HEIGHT = 800;

                Bitmap histogram = new Bitmap(COLOR_WIDTH, HEIGHT);

                for (int x = 0; x < COLOR_WIDTH; x++)
                {
                    for (int y = 0; y < HEIGHT; y++)
                    {
                        histogram.SetPixel(x, y, Color.White);
                    }
                }

                // Plot the histogram using the data
                int _HEIGHT = HEIGHT;
                int factor = 5;

                for (int x = 0; x < 256; x++)
                {
                    HEIGHT = Math.Min(histdata[x] / factor, _HEIGHT);
                    for (int y = 0; y < HEIGHT; y++)
                    {
                        histogram.SetPixel(x, 799 - y, Color.Black);
                    }
                }

                pictureBox2.Image = histogram;

            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Please select an image.");
            }

        }

        private void sepiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                processed = new Bitmap(loaded.Width, loaded.Height);

                for (int x = 0; x < loaded.Width; x++)
                {
                    for (int y = 0; y < loaded.Height; y++)
                    {
                        Color originalPixel = loaded.GetPixel(x, y);

                        // Calculate sepia values
                        int tr = (int)(0.393 * originalPixel.R + 0.769 * originalPixel.G + 0.189 * originalPixel.B);
                        int tg = (int)(0.349 * originalPixel.R + 0.686 * originalPixel.G + 0.168 * originalPixel.B);
                        int tb = (int)(0.272 * originalPixel.R + 0.534 * originalPixel.G + 0.131 * originalPixel.B);

                        // Clamp values to be in the valid 0-255 range
                        int sepiaR = Math.Min(255, tr);
                        int sepiaG = Math.Min(255, tg);
                        int sepiaB = Math.Min(255, tb);

                        processed.SetPixel(x, y, Color.FromArgb(sepiaR, sepiaG, sepiaB));
                    }
                }
                pictureBox2.Image = processed;
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Please select an image.");
            }

        }

        private void openFileDialog2_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            imageB = new Bitmap(openFileDialog2.FileName);
        }

        private void openFileDialog3_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            imageA = new Bitmap(openFileDialog3.FileName);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog2.ShowDialog();
            pictureBox3.Image = imageB;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog3.ShowDialog();
            pictureBox4.Image = imageA;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Bitmap resultImage = new Bitmap(imageB.Width, imageA.Height);
            Color mygreen = Color.FromArgb(0, 0, 255);
            int greygreen = (mygreen.R + mygreen.G + mygreen.B) / 3;
            int threshold = 5;

            for (int i = 0; i < imageB.Width; i++)
            {
                for (int j = 0; j < imageB.Height; j++)
                {
                    Color pixel = imageB.GetPixel(i, j);
                    Color backpixel = imageA.GetPixel(i, j);
                    int grey = (pixel.R + pixel.G + pixel.B) / 3;
                    int subtractvalue = Math.Abs(grey - greygreen);
                    if (subtractvalue < threshold)
                        resultImage.SetPixel(i, j, backpixel);
                    else
                        resultImage.SetPixel(i, j, pixel);
                }
            }
            pictureBox5.Image = resultImage;
        }
    }
}