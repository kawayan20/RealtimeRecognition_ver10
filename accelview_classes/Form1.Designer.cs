namespace accelview_classes
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.labelConnect = new System.Windows.Forms.Label();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.comboBoxCOMS = new System.Windows.Forms.ComboBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.serialPort2 = new System.IO.Ports.SerialPort(this.components);
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.learning0 = new System.Windows.Forms.Button();
            this.learning2 = new System.Windows.Forms.Button();
            this.complete = new System.Windows.Forms.Button();
            this.recognitionlabel = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.learning1 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.saveF = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.statusStrip1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 725);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 18, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1531, 35);
            this.statusStrip1.TabIndex = 22;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 30);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.ActiveLinkColor = System.Drawing.Color.Black;
            this.toolStripStatusLabel2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.toolStripStatusLabel2.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Bold);
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(228, 30);
            this.toolStripStatusLabel2.Text = "Sampling Frequency";
            // 
            // buttonStart
            // 
            this.buttonStart.BackColor = System.Drawing.SystemColors.Desktop;
            this.buttonStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStart.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonStart.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonStart.Location = new System.Drawing.Point(450, 29);
            this.buttonStart.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 40);
            this.buttonStart.TabIndex = 0;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = false;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.BackColor = System.Drawing.SystemColors.Desktop;
            this.buttonStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStop.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonStop.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonStop.Location = new System.Drawing.Point(522, 29);
            this.buttonStop.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(75, 40);
            this.buttonStop.TabIndex = 1;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = false;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.BackColor = System.Drawing.SystemColors.Desktop;
            this.buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSave.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonSave.Location = new System.Drawing.Point(595, 29);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 40);
            this.buttonSave.TabIndex = 2;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // labelConnect
            // 
            this.labelConnect.AutoSize = true;
            this.labelConnect.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Bold);
            this.labelConnect.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelConnect.Location = new System.Drawing.Point(26, 27);
            this.labelConnect.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelConnect.Name = "labelConnect";
            this.labelConnect.Size = new System.Drawing.Size(225, 45);
            this.labelConnect.TabIndex = 3;
            this.labelConnect.Text = "Unconnected";
            // 
            // serialPort1
            // 
            this.serialPort1.BaudRate = 250000;
            this.serialPort1.PortName = "COM4";
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // comboBoxCOMS
            // 
            this.comboBoxCOMS.BackColor = System.Drawing.SystemColors.MenuText;
            this.comboBoxCOMS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxCOMS.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.comboBoxCOMS.ForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.comboBoxCOMS.FormattingEnabled = true;
            this.comboBoxCOMS.Location = new System.Drawing.Point(274, 32);
            this.comboBoxCOMS.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.comboBoxCOMS.Name = "comboBoxCOMS";
            this.comboBoxCOMS.Size = new System.Drawing.Size(160, 31);
            this.comboBoxCOMS.TabIndex = 16;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileName = "test.csv";
            this.saveFileDialog1.Filter = "csvファイル(.csv)|*.csv|全てのファイル(*.*)|*.*";
            // 
            // serialPort2
            // 
            this.serialPort2.BaudRate = 19200;
            this.serialPort2.DtrEnable = true;
            // 
            // trackBar1
            // 
            this.trackBar1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.trackBar1.LargeChange = 50;
            this.trackBar1.Location = new System.Drawing.Point(33, 95);
            this.trackBar1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.trackBar1.Maximum = 500;
            this.trackBar1.Minimum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar1.Size = new System.Drawing.Size(56, 624);
            this.trackBar1.TabIndex = 23;
            this.trackBar1.Value = 1;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.MenuText;
            this.pictureBox2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBox2.Location = new System.Drawing.Point(112, 95);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(1380, 624);
            this.pictureBox2.TabIndex = 38;
            this.pictureBox2.TabStop = false;
            // 
            // learning0
            // 
            this.learning0.BackColor = System.Drawing.SystemColors.WindowText;
            this.learning0.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.learning0.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.learning0.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.learning0.Location = new System.Drawing.Point(938, 27);
            this.learning0.Name = "learning0";
            this.learning0.Size = new System.Drawing.Size(90, 40);
            this.learning0.TabIndex = 39;
            this.learning0.Text = "Class 0";
            this.learning0.UseVisualStyleBackColor = false;
            this.learning0.Click += new System.EventHandler(this.learning0_Click);
            // 
            // learning2
            // 
            this.learning2.BackColor = System.Drawing.SystemColors.WindowText;
            this.learning2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.learning2.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.learning2.Location = new System.Drawing.Point(1118, 27);
            this.learning2.Name = "learning2";
            this.learning2.Size = new System.Drawing.Size(90, 40);
            this.learning2.TabIndex = 41;
            this.learning2.Text = "Class 2";
            this.learning2.UseVisualStyleBackColor = false;
            this.learning2.Click += new System.EventHandler(this.learning2_Click);
            // 
            // complete
            // 
            this.complete.BackColor = System.Drawing.SystemColors.ControlText;
            this.complete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.complete.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.complete.Location = new System.Drawing.Point(1217, 27);
            this.complete.Name = "complete";
            this.complete.Size = new System.Drawing.Size(129, 40);
            this.complete.TabIndex = 42;
            this.complete.Text = "Recognition";
            this.complete.UseVisualStyleBackColor = false;
            this.complete.Click += new System.EventHandler(this.complete_Click);
            // 
            // recognitionlabel
            // 
            this.recognitionlabel.AutoSize = true;
            this.recognitionlabel.Font = new System.Drawing.Font("メイリオ", 19F, System.Drawing.FontStyle.Bold);
            this.recognitionlabel.Location = new System.Drawing.Point(1361, 27);
            this.recognitionlabel.Name = "recognitionlabel";
            this.recognitionlabel.Size = new System.Drawing.Size(127, 48);
            this.recognitionlabel.TabIndex = 43;
            this.recognitionlabel.Text = "Result";
            // 
            // comboBox1
            // 
            this.comboBox1.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox1.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.comboBox1.ForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(766, 31);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(160, 31);
            this.comboBox1.TabIndex = 44;
            // 
            // learning1
            // 
            this.learning1.BackColor = System.Drawing.SystemColors.WindowText;
            this.learning1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.learning1.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.learning1.Location = new System.Drawing.Point(1028, 27);
            this.learning1.Name = "learning1";
            this.learning1.Size = new System.Drawing.Size(90, 40);
            this.learning1.TabIndex = 40;
            this.learning1.Text = "Class 1";
            this.learning1.UseVisualStyleBackColor = false;
            this.learning1.Click += new System.EventHandler(this.learning1_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Location = new System.Drawing.Point(1461, 730);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(14, 14);
            this.button1.TabIndex = 45;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(357, 730);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(178, 30);
            this.label2.TabIndex = 47;
            this.label2.Text = "Window Size = ";
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(1481, 730);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(14, 14);
            this.button2.TabIndex = 48;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // saveF
            // 
            this.saveF.BackColor = System.Drawing.SystemColors.Desktop;
            this.saveF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveF.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.saveF.ForeColor = System.Drawing.Color.White;
            this.saveF.Location = new System.Drawing.Point(740, 37);
            this.saveF.Name = "saveF";
            this.saveF.Size = new System.Drawing.Size(20, 21);
            this.saveF.TabIndex = 49;
            this.saveF.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.saveF.UseVisualStyleBackColor = false;
            this.saveF.Click += new System.EventHandler(this.saveF_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1531, 760);
            this.Controls.Add(this.saveF);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.recognitionlabel);
            this.Controls.Add(this.complete);
            this.Controls.Add(this.learning2);
            this.Controls.Add(this.learning1);
            this.Controls.Add(this.learning0);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.comboBoxCOMS);
            this.Controls.Add(this.labelConnect);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonStart);
            this.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Form1";
            this.Opacity = 0.8D;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RealTimeRecognition";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label labelConnect;
        private System.Windows.Forms.ComboBox comboBoxCOMS;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.IO.Ports.SerialPort serialPort2;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button learning0;
        private System.Windows.Forms.Button learning2;
        private System.Windows.Forms.Button complete;
        private System.Windows.Forms.Label recognitionlabel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button learning1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button saveF;
        public System.IO.Ports.SerialPort serialPort1;
    }
}

