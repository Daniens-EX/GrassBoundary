using AForge.Imaging.Filters;
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Diagnostics;
using System.IO.Ports;
using AForge.Imaging;
using Emgu.CV.Structure;
using Emgu.CV;

namespace grassBED
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            updateserial();
            updatecam();
            BtnAutostart.Enabled = false;
            Receivebox.Clear();
        }


        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;
        Bitmap video;
        private SerialPort ThisPort = new SerialPort();
        delegate void SetTextCallback(string text);
        private static System.Windows.Forms.Timer? actimer;

        private void updateserial()
        {
            Portselect.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                Portselect.Items.Add(port);
            }
            CheckForIllegalCrossThreadCalls = false;
        }

        private void updatecam()
        {
            Camselect.Items.Clear();
            // Get a list of available video devices
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (videoDevices.Count > 0)
            {
                // Add the available video devices to the ComboBox control
                for (int i = 0; i < videoDevices.Count; i++)
                {
                    Camselect.Items.Add(videoDevices[i].Name);
                }
            }
        }

        private void serialconnect()
        {
            bool error = false;

            // Check if all settings have been selected

            if (Portselect.SelectedIndex != -1)
            {
                //if yes than Set The Port's settings
                ThisPort.PortName = Portselect.Text;
                ThisPort.BaudRate = 9600;
                ThisPort.Parity = 0;
                ThisPort.DataBits = 8;

                try
                {
                    //Open Port
                    ThisPort.Open();
                    ThisPort.DataReceived += SerialPortDataReceived;  //Check for received data. When there is data in the receive buffer,
                                                                      //it will raise this event, we need to subscribe to it to know when there is data
                }
                catch (UnauthorizedAccessException) { error = true; }
                catch (System.IO.IOException) { error = true; }
                catch (ArgumentException) { error = true; }

                if (error) MessageBox.Show(this, "Could not open the COM port. Most likely it is already in use, has been removed, or is unavailable.", "COM Port unavailable", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }
            else
            {
                MessageBox.Show("Please select an available serial port", "Serial Port Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }
            //if the port is open, Change the Connect button to disconnect, enable the send button.
            //and disable the groupBox to prevent changing configuration of an open port.
            if (ThisPort.IsOpen)
            {
                BtnConnect.Text = "Disconnect";
                BtnAutostart.Enabled = true;
                groupBoxNames.Enabled = false;
                BtnRefresh.Enabled = false;
            }
        }

        private void serialdisconnect()
        {
            ThisPort.Close();
            BtnConnect.Text = "Connect";
            BtnAutostart.Enabled = false;
            groupBoxNames.Enabled = true;
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            if (Camselect.SelectedIndex!=-1 && Portselect.SelectedIndex!=-1)
            {
                if(ThisPort.IsOpen)
                {
                    serialdisconnect();
                    CloseCam();
                    BtnAutostart.Enabled = false;
                }
                else
                {
                    serialconnect();
                    OpenCam();
                    BtnAutostart.Enabled = true;
                }
            }
            else
            {
                MessageBox.Show("Please select available camera and serial port for establishing connection.", "Null Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            updatecam();
            updateserial();
        }

        private void SerialPortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var serialPort = (SerialPort)sender;
            var data = serialPort.ReadExisting();
            SetText(data);
        }

        private void SetText(string text)
        {
            //invokeRequired required compares the thread ID of the calling thread to the thread of the creating thread.
            // if these threads are different, it returns true
            if (this.Receivebox.InvokeRequired)
            {
                Receivebox.ForeColor = Color.Blue;    //write text data in blue colour
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.Receivebox.AppendText(text);
            }
        }

        private void OpenCam()
        {
            // Start video capture
            videoSource = new VideoCaptureDevice(videoDevices[Camselect.SelectedIndex].MonikerString);
            videoSource.NewFrame += VideoSource_NewFrame;
            videoSource.Start();
        }

        private void CloseCam()
        {
            // Stop video capture and dispose the video source
            videoSource.SignalToStop();
            videoSource.WaitForStop();
            videoSource = null;
            videobox.Image = null;
        }

        private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap video = (Bitmap)eventArgs.Frame.Clone();
            videobox.Image = video;
        }

        private void BtnAutostart_Click(object sender, EventArgs e)
        {
            if (BtnAutostart.Text== "Start Auto-capture")
            {
                gpsshow();
                imgcapture();
                imgprocess();
                AutoclickON();
                BtnConnect.Enabled = false;
            }
            else
            {
                AutoclickOFF();
                BtnConnect.Enabled = true;
            }
        }

        private void AutoclickON()
        {
            BtnAutostart.Text = "Stop Auto-capture";
            actimer = new System.Windows.Forms.Timer();
            actimer.Interval = 1000;
            actimer.Tick += new EventHandler(timer1_Tick);
            actimer.Enabled = true;
            actimer.Start();
        }

        private void AutoclickOFF()
        {
            BtnAutostart.Text = "Start Auto-capture";
            actimer.Stop();
            actimer.Enabled = false;
            actimer.Dispose();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            gpsshow();
            imgcapture();
            imgprocess();
            actimer.Stop();
            actimer.Dispose();
            actimer.Start();
        }

        private void gpsshow()
        {
            bool error = false;
            ThisPort.Write("GPS");
        }

        private void imgcapture()
        {
            string today = DateTime.Now.ToString("dd-MM-yyyy");
            string subfolderPath = Path.Combine(@"C:\Users\daite\Desktop\Test pictures", today);
            if(!Directory.Exists(subfolderPath))
            {
                Directory.CreateDirectory(subfolderPath);
            }
            string filecurrent = "Captured " + DateTime.Now.ToString("HH-mm-ss") + ".jpg";
            string filepath = Path.Combine(subfolderPath, filecurrent);
            Bitmap image = (Bitmap)videobox.Image;
            image.Save(filepath, System.Drawing.Imaging.ImageFormat.Jpeg);
            imgprocessresult.Image= image;
            CloseCam();
        }

        private void imgprocess()
        {
            Grayimage();
            histogramanalysis();
            OpenCam();
        }

        private void Grayimage()
        {
            Bitmap image = (Bitmap)imgprocessresult.Image;
            // create filter
            HSLFiltering filter = new HSLFiltering();
            // set color ranges to keep
            filter.Hue = new IntRange(66, 148);
            filter.Saturation = new AForge.Range(0.3f, 1);
            filter.Luminance = new AForge.Range(0.1f, 1);
            // apply the filter
            filter.ApplyInPlace(image);

            // create grayscale filter(BT709)
            Grayscale filter1 = new Grayscale(0.2125, 0.7154, 0.0721);
            // apply the filter
            Bitmap R1image = filter1.Apply(image);
            // create filter

            OtsuThreshold filter2 = new OtsuThreshold();
            // apply the filter
            filter2.ApplyInPlace(R1image);
            // create and configure the filter

            FillHoles filter3 = new FillHoles();
            filter3.MaxHoleHeight = 1200;
            filter3.MaxHoleWidth = 1200;
            filter3.CoupledSizeFiltering = false;
            // apply the filter
            Bitmap R2image = filter3.Apply(R1image);
            imgprocessresult.Image = R2image;
        }

        private void histogramanalysis()
        {
            Bitmap image = (Bitmap)imgprocessresult.Image;
            // collect horizontal statistics
            HorizontalIntensityStatistics his = new HorizontalIntensityStatistics(image);
            // get gray histogram (for grayscale image)
            AForge.Math.Histogram hhistogram = his.Gray;
            // output some histogram's information
            string histoText = "H.Mean = " + hhistogram.Mean + Environment.NewLine;
            histoText += "H.Min = " + hhistogram.Min + Environment.NewLine;
            histoText += "H.Max = " + hhistogram.Max + Environment.NewLine;
            histoText += "H.StdDev = " + hhistogram.StdDev + Environment.NewLine;
            histoText += "H.Median = " + hhistogram.Median + Environment.NewLine;


            var img1 = new Emgu.CV.Image<Gray, byte>(hhistogram.Values.Length, 255, new Gray(255));
            for (int i = 0; i < hhistogram.Values.Length; i++)
            {
                // CvInvoke.Line(img, new System.Drawing.Point(i, 255), new System.Drawing.Point(i, 255 - (int)(histogram.Values[i] / histogram.Max)), new MCvScalar(255, 255, 255));
                if ((int)(hhistogram.Values[i] / hhistogram.Max) > 50)
                { CvInvoke.Line(img1, new System.Drawing.Point(i, 255), new System.Drawing.Point(i, (int)(hhistogram.Values[i] / hhistogram.Max)), new MCvScalar(0, 0, 0)); }
                else
                {
                    CvInvoke.Line(img1, new System.Drawing.Point(i, 0), new System.Drawing.Point(i, 255), new MCvScalar(0, 0, 0));
                }
            }

            img1.Save("C:\\Users\\daite\\Desktop\\Test pictures\\horizontaltransferpoint.jpg");
            Hhisoutput.Image = AForge.Imaging.Image.FromFile("C:\\Users\\daite\\Desktop\\Test pictures\\horizontaltransferpoint.jpg");

           /* string folderpath = Path.Combine(@"C:\Users\daite\Desktop\Test pictures", DateTime.Now.ToString("dd-MM-yyyy"));
            string horizontalhissave = "Horizontal analysis of " + DateTime.Now.ToString("HH-mm-ss") + ".jpg";
            string hispathsave = Path.Combine(folderpath, horizontalhissave);
            Bitmap hisanalysis = (Bitmap)Hhisoutput.Image;
            hisanalysis.Save(hispathsave);
            */

            // collect vertical statistics
            VerticalIntensityStatistics vis = new VerticalIntensityStatistics(image);
            // get gray histogram (for grayscale image)
            AForge.Math.Histogram vhistogram = vis.Gray;
            // output some histogram's information

            histoText += Environment.NewLine;
            histoText += "V.Mean = " + vhistogram.Mean + Environment.NewLine;
            histoText += "V.Min = " + vhistogram.Min + Environment.NewLine;
            histoText += "V.Max = " + vhistogram.Max + Environment.NewLine;
            histoText += "V.StdDev = " + vhistogram.StdDev + Environment.NewLine;
            histoText += "V.Median = " + vhistogram.Median + Environment.NewLine;

            histogramanalysisbox.Text = histoText;

            var img2 = new Emgu.CV.Image<Gray, byte>(vhistogram.Values.Length, 255, new Gray(255));
            for (int j = 0; j < vhistogram.Values.Length; j++)
            {
                // CvInvoke.Line(img, new System.Drawing.Point(i, 255), new System.Drawing.Point(i, 255 - (int)(histogram.Values[i] / histogram.Max)), new MCvScalar(255, 255, 255));
                if ((int)(vhistogram.Values[j] / vhistogram.Max) > 50)
                { CvInvoke.Line(img2, new System.Drawing.Point(j, 255), new System.Drawing.Point(j, (int)(vhistogram.Values[j] / vhistogram.Max)), new MCvScalar(0, 0, 0)); }
                else
                {
                    CvInvoke.Line(img2, new System.Drawing.Point(j, 0), new System.Drawing.Point(j, 255), new MCvScalar(0, 0, 0));
                }
            }

            img2.Save("C:\\Users\\daite\\Desktop\\Test pictures\\verticaltransferpoint.jpg");
            Vhisoutput.Image = AForge.Imaging.Image.FromFile("C:\\Users\\daite\\Desktop\\Test pictures\\verticaltransferpoint.jpg");

            /*string verticalhissave = "Vertical analysis of " + DateTime.Now.ToString("HH-mm-ss") + ".jpg";
            string vispathsave = Path.Combine(folderpath, verticalhissave);
            Bitmap visanalysis = (Bitmap)Vhisoutput.Image;
            //visanalysis.Save(vispathsave);*/
        }
    }
}