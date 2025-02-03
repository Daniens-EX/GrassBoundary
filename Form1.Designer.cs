namespace grassBED
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Portselect = new System.Windows.Forms.ComboBox();
            this.Camselect = new System.Windows.Forms.ComboBox();
            this.BtnConnect = new System.Windows.Forms.Button();
            this.groupBoxNames = new System.Windows.Forms.GroupBox();
            this.BtnAutostart = new System.Windows.Forms.Button();
            this.videobox = new System.Windows.Forms.PictureBox();
            this.BtnRefresh = new System.Windows.Forms.Button();
            this.Receivebox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.imgprocessresult = new System.Windows.Forms.PictureBox();
            this.Hhisoutput = new System.Windows.Forms.PictureBox();
            this.Vhisoutput = new System.Windows.Forms.PictureBox();
            this.histogramanalysisbox = new System.Windows.Forms.RichTextBox();
            this.groupBoxNames.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.videobox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgprocessresult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Hhisoutput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Vhisoutput)).BeginInit();
            this.SuspendLayout();
            // 
            // Portselect
            // 
            this.Portselect.FormattingEnabled = true;
            this.Portselect.Location = new System.Drawing.Point(12, 60);
            this.Portselect.Name = "Portselect";
            this.Portselect.Size = new System.Drawing.Size(248, 28);
            this.Portselect.TabIndex = 0;
            // 
            // Camselect
            // 
            this.Camselect.FormattingEnabled = true;
            this.Camselect.Location = new System.Drawing.Point(12, 26);
            this.Camselect.Name = "Camselect";
            this.Camselect.Size = new System.Drawing.Size(248, 28);
            this.Camselect.TabIndex = 1;
            // 
            // BtnConnect
            // 
            this.BtnConnect.Location = new System.Drawing.Point(284, 20);
            this.BtnConnect.Name = "BtnConnect";
            this.BtnConnect.Size = new System.Drawing.Size(144, 59);
            this.BtnConnect.TabIndex = 2;
            this.BtnConnect.Text = "Connect";
            this.BtnConnect.UseVisualStyleBackColor = true;
            this.BtnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
            // 
            // groupBoxNames
            // 
            this.groupBoxNames.Controls.Add(this.Camselect);
            this.groupBoxNames.Controls.Add(this.Portselect);
            this.groupBoxNames.Location = new System.Drawing.Point(12, 12);
            this.groupBoxNames.Name = "groupBoxNames";
            this.groupBoxNames.Size = new System.Drawing.Size(266, 112);
            this.groupBoxNames.TabIndex = 3;
            this.groupBoxNames.TabStop = false;
            this.groupBoxNames.Text = "Devices Setup";
            // 
            // BtnAutostart
            // 
            this.BtnAutostart.Location = new System.Drawing.Point(470, 20);
            this.BtnAutostart.Name = "BtnAutostart";
            this.BtnAutostart.Size = new System.Drawing.Size(144, 104);
            this.BtnAutostart.TabIndex = 4;
            this.BtnAutostart.Text = "Start Auto-capture";
            this.BtnAutostart.UseVisualStyleBackColor = true;
            this.BtnAutostart.Click += new System.EventHandler(this.BtnAutostart_Click);
            // 
            // videobox
            // 
            this.videobox.Location = new System.Drawing.Point(12, 150);
            this.videobox.Name = "videobox";
            this.videobox.Size = new System.Drawing.Size(619, 425);
            this.videobox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.videobox.TabIndex = 5;
            this.videobox.TabStop = false;
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.Location = new System.Drawing.Point(284, 85);
            this.BtnRefresh.Name = "BtnRefresh";
            this.BtnRefresh.Size = new System.Drawing.Size(144, 39);
            this.BtnRefresh.TabIndex = 6;
            this.BtnRefresh.Text = "Refresh";
            this.BtnRefresh.UseVisualStyleBackColor = true;
            this.BtnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // Receivebox
            // 
            this.Receivebox.Location = new System.Drawing.Point(12, 602);
            this.Receivebox.Name = "Receivebox";
            this.Receivebox.Size = new System.Drawing.Size(619, 262);
            this.Receivebox.TabIndex = 7;
            this.Receivebox.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 127);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "Video Output:";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // imgprocessresult
            // 
            this.imgprocessresult.Location = new System.Drawing.Point(744, 150);
            this.imgprocessresult.Name = "imgprocessresult";
            this.imgprocessresult.Size = new System.Drawing.Size(482, 332);
            this.imgprocessresult.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgprocessresult.TabIndex = 9;
            this.imgprocessresult.TabStop = false;
            // 
            // Hhisoutput
            // 
            this.Hhisoutput.Location = new System.Drawing.Point(744, 515);
            this.Hhisoutput.Name = "Hhisoutput";
            this.Hhisoutput.Size = new System.Drawing.Size(482, 332);
            this.Hhisoutput.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Hhisoutput.TabIndex = 10;
            this.Hhisoutput.TabStop = false;
            // 
            // Vhisoutput
            // 
            this.Vhisoutput.Location = new System.Drawing.Point(1274, 150);
            this.Vhisoutput.Name = "Vhisoutput";
            this.Vhisoutput.Size = new System.Drawing.Size(482, 332);
            this.Vhisoutput.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Vhisoutput.TabIndex = 11;
            this.Vhisoutput.TabStop = false;
            // 
            // histogramanalysisbox
            // 
            this.histogramanalysisbox.Location = new System.Drawing.Point(1274, 515);
            this.histogramanalysisbox.Name = "histogramanalysisbox";
            this.histogramanalysisbox.Size = new System.Drawing.Size(482, 332);
            this.histogramanalysisbox.TabIndex = 12;
            this.histogramanalysisbox.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1794, 876);
            this.Controls.Add(this.histogramanalysisbox);
            this.Controls.Add(this.Vhisoutput);
            this.Controls.Add(this.Hhisoutput);
            this.Controls.Add(this.imgprocessresult);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Receivebox);
            this.Controls.Add(this.BtnRefresh);
            this.Controls.Add(this.videobox);
            this.Controls.Add(this.BtnAutostart);
            this.Controls.Add(this.groupBoxNames);
            this.Controls.Add(this.BtnConnect);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBoxNames.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.videobox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgprocessresult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Hhisoutput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Vhisoutput)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComboBox Portselect;
        private ComboBox Camselect;
        private Button BtnConnect;
        private GroupBox groupBoxNames;
        private Button BtnAutostart;
        private PictureBox videobox;
        private Button BtnRefresh;
        private RichTextBox Receivebox;
        private Label label1;
        private System.Windows.Forms.Timer timer1;
        private PictureBox imgprocessresult;
        private PictureBox Hhisoutput;
        private PictureBox Vhisoutput;
        private RichTextBox histogramanalysisbox;
    }
}