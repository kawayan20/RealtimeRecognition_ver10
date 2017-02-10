using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using Accord.Math;
using Accord.Neuro;
using Accord.Neuro.Networks;
using AForge.Neuro.Learning;
using Accord.Statistics.Kernels;
using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;
using Accord.Neuro.ActivationFunctions;
using Accord.MachineLearning;
using Accord.MachineLearning.DecisionTrees;
using Accord.MachineLearning.DecisionTrees.Learning;
using System.Diagnostics;
using Signal_Process2;
using System.Timers;






namespace accelview_classes
{
    public partial class Form1 : Form
    {
        Data alldata;
        #region メンバ変数
        public Form2 Form2Obj; //子フォーム
        public Form3 Form3Obj;

        //form2描画用データ格納リスト
        List<List<double>> form2List = new List<List<double>>();

        SensorData sensorData;
        //bool openFlag;
        delegate void setfocus();
        Color[] colors;
        PictureBox[] Pbox;

        //フィルタ用
        private float currentOrientationValues_x = 0.0f;
	    private float currentOrientationValues_y = 0.0f;
        private float currentOrientationValues_z = 0.0f;
			
        private float currentAccelerationValues_x = 0.0f;
        private float currentAccelerationValues_y = 0.0f;
        private float currentAccelerationValues_z = 0.0f;

        private float old_x = 0.0f;
        private float old_y = 0.0f;
        private float old_z = 0.0f;

        List<double> Accel = new List<double>();
        List<int> timelist = new List<int>();     

        //FFTに使用するデータ数 2のn乗
        private const int fft_num = 1024;
        double[] AccelXYZ = new double[fft_num];

        //FFT結果
        //List<double> FFTaccel = new List<double>();
        //DFT fft = new DFT(fft_num, DFT.Window.Hanning);
        DFT fft = new DFT(fft_num, DFT.Window.NoWindow);
        FFTresult fftresult;

        //学習用inputとoutput
        double[][] inputs = new double[3][];
        int[] outputs = {0, 
                         1, 
                         2};
        double[] featX = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };


        DeepBeliefNetwork network;

        double[][] outputs2 = new double[3][]; 
        //認識フラグ
        Boolean flag = false;

        System.Timers.Timer mytimer = new System.Timers.Timer();

        //有線（arduino）加速度センサのコマンド用
        byte[] stopmessage = new byte[3];


        Pen Fpen;
        Pen pensfft;
        Pen pensff2t;


        #endregion
        #region メソッド
        public Form1()
        {
            //hello git
            InitializeComponent();
        }
        #region 初期化処理
        private void Form1_Load(object sender, EventArgs e)
        {
            Form2Obj = new Form2(); //子フォーム生成
            Form2Obj.Form1Obj = this; // 親ﾌｫｰﾑ情報を子に設定

            Form3Obj = new Form3(); //子フォーム生成

            form2List.Add(new List<double>()); //form2List[0]
            form2List.Add(new List<double>()); //form2List[1]
            form2List.Add(new List<double>()); //form2List[2]

            Pbox = new PictureBox[3];
            Pbox[0] = Form2Obj.pictureBox1;
            Pbox[1] = Form2Obj.pictureBox3;
            Pbox[2] = Form2Obj.pictureBox4;

            pictureBox2.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox2_Paint);

            Pbox[0].Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            Pbox[1].Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox3_Paint);
            Pbox[2].Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox4_Paint);

            Form3Obj.pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);

            //マウスカーソルがボタン上にきた時の色
            learning0.FlatAppearance.MouseOverBackColor = Color.DimGray;
            learning1.FlatAppearance.MouseOverBackColor = Color.DimGray;
            learning2.FlatAppearance.MouseOverBackColor = Color.DimGray;
            complete.FlatAppearance.MouseOverBackColor = Color.DimGray;
            buttonStart.FlatAppearance.MouseOverBackColor = Color.DimGray;
            buttonSave.FlatAppearance.MouseOverBackColor = Color.DimGray;
            buttonStop.FlatAppearance.MouseOverBackColor = Color.DimGray;

            //有線(arduino)加速度線の場合のコマンド用
            stopmessage[0] = 65;
            stopmessage[1] = 66;
            stopmessage[2] = 67;



            //for (int i = 0; i < fft_num; i++)
            //{
            //    Accel.Add(0);
            //}

            outputs2[0] = new double[] { -1, -1 };
            outputs2[1] = new double[] { 1, 1 };
            outputs2[2] = new double[] { 1, -1 };
            //Debug.WriteLine(outputs2[2][0]);
            //for (int i = 0; i < 3; i++)
            //{
            //    inputs[i] = new double[9];
            //}

            //利用可能なシリアルポート名の配列を取得する
            string[] PortList = SerialPort.GetPortNames();
            //機械学習のアルゴリズム選択
            string[] ML_Algorithm = { "k-NN", "C4.5", "SVM", "DeepLearning"};
            //コンボボックスの中身を消去
            comboBoxCOMS.Items.Clear();
            comboBox1.Items.Clear();
            //シリアルポート名をコンボボックスにセットする
            foreach (string name in PortList)
            {
                comboBoxCOMS.Items.Add(name);
                //comboBox1.Items.Add(name);
            }
            foreach (string name in ML_Algorithm)
            {
                comboBox1.Items.Add(name);
                //comboBox1.Items.Add(name);
            }

            //COMが1個以上あれば1番目を選択状態にしておく
            if (comboBoxCOMS.Items.Count > 0 && comboBoxCOMS.Items.Count < 3)
            {
                comboBoxCOMS.SelectedIndex = 0;
            }
            else if(comboBoxCOMS.Items.Count >= 3)
            {
                comboBoxCOMS.SelectedIndex = 2;
            }
            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }

            comboBoxCOMS.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.Clear();
            //-----------------------------------------
            //クラスのインスタンス生成
            //各変数の初期化
            sensorData = new SensorData();
            //-----------------------------------------
            colors = new Color[] { Color.YellowGreen, Color.DarkTurquoise, Color.PowderBlue, Color.Orange, Color.LightSeaGreen, Color.Turquoise };

            //描画用ペン
            Fpen = new Pen(new SolidBrush(colors[5]), 2);
            pensfft = new Pen(new SolidBrush(colors[5]), 3);
            pensff2t = new Pen(new SolidBrush(colors[3]), 3);

            mytimer.Elapsed += new ElapsedEventHandler(OnElapsed_TimersTimer);
            mytimer.Interval = 100;

            ////描画のちらつき防止
            //ダブルバッファ
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            label2.Text = "Window Size = " + fft_num.ToString();
        }
        //画面の表示を初期化する
        private void Clear()
        {
            //textBoxX.Text = "";
            //textBoxY.Text = "";
            //textBoxZ.Text = "";
            //textBoxGX.Text = "";
            //textBoxGY.Text = "";
            //textBoxGZ.Text = "";
        }
        #endregion

        #region シリアル通信関係
        //シリアルデータ取得
        //データを受け取ったらDataConvertクラスでデータをAccel型に変換ののち、sensorDataオブジェクトにデータを投げる
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //labelReceived.Text = "Received Data="+serialPort1.ReadByte
            byte[] readdata = new byte[15];
            while (serialPort1.BytesToRead > 4)
            {
                //読んだデータが8個以上なら
                //頭から3バイトが<0x73><0x65><0x6E><0x62>になっていればそれ以降のバイトをデータとして取得
                readdata[0] = (byte)serialPort1.ReadByte();//1バイト読む
                if (readdata[0] == 0x73)
                {
                    //先頭が0x61なら
                    readdata[1] = (byte)serialPort1.ReadByte();//1バイト読む
                    if (readdata[1] == 0x65)
                    {
                        //2番目が0x67なら
                        readdata[2] = (byte)serialPort1.ReadByte();//1バイト読む
                        if (readdata[2] == 0x6E)
                        {
                            readdata[3] = (byte)serialPort1.ReadByte();
                            //3番目が0x62なら
                            if (readdata[3] == 0x62)
                            //4番目が0x62なら
                            {
                                //ここまできたら先頭の4バイトはsenbであるので
                                //以下、時刻（4バイト）、加速度（xyz各2バイト）、角速度（xyz各2バイト）、終端（1バイト（0xC1））となる
                                for (int i = 4; i < 15; i++)
                                {
                                    readdata[i] = (byte)serialPort1.ReadByte();
                                }
                                if (readdata[14] == 0xc1)
                                {
                                    //-------------------------------------------------------
                                    //クラスのインスタンス作成＆メソッド呼び出し
                                    //終端が0xc1ならAccelDataを作成してSensorDataに追加
                                    AccelData acd = new AccelData(readdata, dataType.accel);
                                    sensorData.pushData(acd);

                                    //------------------------------------------------------
                                    //以下画面への描画関連の処理
                                    Invoke((setfocus)delegate()
                                    {
                                        //テキストボックスを加速度の値で埋めるメソッド
                                        //this.TextFill(this.sensorData.LastData);
                                        //
                                        //this.ProcessingByReceive();
                                    //this.myFFT();
                                        this.ProcessingByReceive();

                                        //再描画メソッドを呼び出す
                                        //pictureBox2.Invalidate();
                                    });
                                   // break;
                                }
                                //else
                                //{
                                //    continue;
                                //}
                            }
                        }
                    }
                }
            }
        }
        

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (comboBoxCOMS.Text != "COM5")
            {
                serialPort1.BaudRate = 115200;
            }

            Mystart();
            mytimer.Start();

            serialPort1.Write(stopmessage, 1, 2);
            labelConnect.Text = "Connected";
        }

        private void ButtonStop_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                if (comboBoxCOMS.Text != "COM5")
                {
                    labelConnect.Text = "Disconnected";
                    //シリアルポートが開いているなら
                    //切断する処理を行う
                    serialPort1.Write("stop senb \n");
                    //serialPort1.Close();

                    //serialPort1.Close();
                    //fft.FFT(AccelXYZ).Save();

                }
                else
                {
                    labelConnect.Text = "Disconnected";
                    serialPort1.Write(stopmessage, 0, 1);
                }

            }
        }
        #endregion

        #region 認識とグラフ描画

        private void DrawGraphs2(Graphics g, int ratio)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;


            int w = pictureBox2.Width;
            int h = pictureBox2.Height;

            //myFFT();

            if (flag)
            {
                //double[] featX = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                double[] testdata = new double[23];
                for (int i = 0; i < testdata.Length; i++)
                {
                    testdata[i] = 0;
                }

                double fftMax = 0.0;
                double max_fre = 62.0;
                double max_fre_temp = 62.0;

                for (int i = 62; i < 512; i++)
                {
                    if (fftMax < fftresult.GetData(i).data.Abs)
                    {
                        fftMax = fftresult.GetData(i).data.Abs;
                        max_fre = max_fre_temp;
                    }
                    max_fre_temp += 1.0;
                }
                testdata[22] = max_fre;

                for (int i = 62; i < 82; i++)
                {
                    testdata[0] += fftresult.GetData(i).data.Abs;
                }
                for (int i = 82; i < 103; i++)
                {
                    testdata[1] += fftresult.GetData(i).data.Abs;
                }
                for (int i = 103; i < 123; i++)
                {
                    testdata[2] += fftresult.GetData(i).data.Abs;
                }
                for (int i = 123; i < 144; i++)
                {
                    testdata[3] += fftresult.GetData(i).data.Abs;
                }
                for (int i = 144; i < 164; i++)
                {
                    testdata[4] += fftresult.GetData(i).data.Abs;
                }
                for (int i = 164; i < 185; i++)
                {
                    testdata[5] += fftresult.GetData(i).data.Abs;
                }
                for (int i = 185; i < 205; i++)
                {
                    testdata[6] += fftresult.GetData(i).data.Abs;
                }
                for (int i = 205; i < 226; i++)
                {
                    testdata[7] += fftresult.GetData(i).data.Abs;
                }
                for (int i = 226; i < 246; i++)
                {
                    testdata[8] += fftresult.GetData(i).data.Abs;
                }
                for (int i = 246; i < 267; i++)
                {
                    testdata[9] += fftresult.GetData(i).data.Abs;
                }
                for (int i = 267; i < 286; i++)
                {
                    testdata[10] += fftresult.GetData(i).data.Abs;
                }
                for (int i = 286; i < 307; i++)
                {
                    testdata[11] += fftresult.GetData(i).data.Abs;
                }
                for (int i = 307; i < 327; i++)
                {
                    testdata[12] += fftresult.GetData(i).data.Abs;
                }
                for (int i = 327; i < 348; i++)
                {
                    testdata[13] += fftresult.GetData(i).data.Abs;
                }
                for (int i = 348; i < 368; i++)
                {
                    testdata[14] += fftresult.GetData(i).data.Abs;
                }
                for (int i = 368; i < 389; i++)
                {
                    testdata[15] += fftresult.GetData(i).data.Abs;
                }
                for (int i = 389; i < 409; i++)
                {
                    testdata[16] += fftresult.GetData(i).data.Abs;
                }
                for (int i = 409; i < 430; i++)
                {
                    testdata[17] += fftresult.GetData(i).data.Abs;
                }
                for (int i = 430; i < 450; i++)
                {
                    testdata[18] += fftresult.GetData(i).data.Abs;
                }
                for (int i = 450; i < 471; i++)
                {
                    testdata[19] += fftresult.GetData(i).data.Abs;
                }
                for (int i = 471; i < 491; i++)
                {
                    testdata[20] += fftresult.GetData(i).data.Abs;
                }
                for (int i = 491; i < 512; i++)
                {
                    testdata[21] += fftresult.GetData(i).data.Abs;
                }
                
                //Debug.WriteLine(inputs[0][2]);
                //アルゴリズム別の認識
                if (comboBox1.SelectedItem.ToString() == "k-NN")
                {
                    KNearestNeighbors knn = new KNearestNeighbors(k: 1, classes: 3, inputs: inputs, outputs: outputs);
                    //int answer = knn.Compute(testdata); // answer will be 2.
                    //Debug.WriteLine("array ", inputs[0]);
                    int answer = knn.Compute(testdata); // answer will be 2.
                    //結果表示
                    recognitionlabel.Text ="class " + answer.ToString();
                    ChangebuttonColor(answer);
                }
                else if (comboBox1.SelectedItem.ToString() == "C4.5")
                {
                    DecisionTree tree = new DecisionTree(
                           inputs: new List<DecisionVariable>
                    {
                        DecisionVariable.Continuous("0"),
                        DecisionVariable.Continuous("1"),
                        DecisionVariable.Continuous("2"),
                        DecisionVariable.Continuous("3"),
                        DecisionVariable.Continuous("4"),
                        DecisionVariable.Continuous("5"),
                        DecisionVariable.Continuous("6"),
                        DecisionVariable.Continuous("7"),
                        DecisionVariable.Continuous("8"),
                        DecisionVariable.Continuous("9"),
                        DecisionVariable.Continuous("10"),
                        DecisionVariable.Continuous("11"),
                        DecisionVariable.Continuous("12"),
                        DecisionVariable.Continuous("13"),
                        DecisionVariable.Continuous("14"),
                        DecisionVariable.Continuous("15"),
                        DecisionVariable.Continuous("16"),
                        DecisionVariable.Continuous("17"),
                        DecisionVariable.Continuous("18"),
                        DecisionVariable.Continuous("19"),
                        DecisionVariable.Continuous("20"),
                        DecisionVariable.Continuous("21"),
                        DecisionVariable.Continuous("22")
                    },
                           classes: 3);

                    C45Learning teacher = new C45Learning(tree);
                    double error = teacher.Run(inputs, outputs);

                    double[][] test = new double[1][];
                    //test[0] = new double{ testdata[0], testdata[1], testdata[2], testdata[3], testdata[4], testdata[5], testdata[6], testdata[7], testdata[8] };
                    test[0] = new double[23];
                    testdata.CopyTo(test[0], 0);
                    int[] answer = test.Apply(tree.Compute);

                    recognitionlabel.Text ="class " + answer[0].ToString();
                    ChangebuttonColor(answer[0]);

                }
                else if (comboBox1.SelectedItem.ToString() == "SVM")
                {
                    IKernel kernel = new Linear();
                    var machine = new MulticlassSupportVectorMachine(inputs: 23, kernel: kernel, classes: 3);
                    var teacher = new MulticlassSupportVectorLearning(machine, inputs, outputs);
                    teacher.Algorithm = (svm, classInputs, classOutputs, i, j) => new SequentialMinimalOptimization(svm, classInputs, classOutputs);
                    double error = teacher.Run();
                    int answer = machine.Compute(new double[] { testdata[0], testdata[1], testdata[2], testdata[3], testdata[4], testdata[5], testdata[6], testdata[7], testdata[8], testdata[9], testdata[10], testdata[11], testdata[12], testdata[13], testdata[14], testdata[15], testdata[16], testdata[17], testdata[18], testdata[19], testdata[20], testdata[21], testdata[22] });

                    recognitionlabel.Text = "class " + answer.ToString();
                    ChangebuttonColor(answer);
                }
                else if (comboBox1.SelectedItem.ToString() == "DeepLearning")
                {
                    double[] answer = network.Compute(new double[] { testdata[0], testdata[1], testdata[2], testdata[3], testdata[4], testdata[5], testdata[6], testdata[7], testdata[8], testdata[9], testdata[10], testdata[11], testdata[12], testdata[13], testdata[14], testdata[15], testdata[16], testdata[17], testdata[18], testdata[19], testdata[20], testdata[21], testdata[22] });

                    ////  一番確率の高いクラスのインデックスを得る
                     
                    //answer.Max(out imax);
                    //if (answer[0] < 1) imax = 0;
                    //else if (answer[0] >= 1 && answer[0] < 2) imax = 1;
                    //else if (answer[0] >= 2) imax = 2;
                    //recognitionlabel.Text = "class " + imax.ToString();
                    if (answer[0] < 0) answer[0] = -1;
                    else answer[0] = 1;
                    if (answer[1] < 0) answer[1] = -1;
                    else answer[1] = 1;

                    Debug.WriteLine("0. "+answer[0]);
                    Debug.WriteLine("1. "+answer[1]);

                    if (answer[0] == -1 && answer[1] == -1)
                    {
                        int imax = 0; ChangebuttonColor(imax); recognitionlabel.Text = imax.ToString();
                    }
                    else if (answer[0] == 1 && answer[1] == 1)
                    {
                        int imax = 1; ChangebuttonColor(imax); recognitionlabel.Text = imax.ToString();
                    }
                    else if (answer[0] == 1 && answer[1] == -1)
                    {
                        int imax = 2; ChangebuttonColor(imax); recognitionlabel.Text = imax.ToString();
                    }
                    else { recognitionlabel.Text = "error"; }
                }
            }

            if (fftresult != null)
            {
                for (int i = 5; i < fftresult.Length - 1; i++)
                {
                    if (i > 62 && i < 512)
                    {
                        Fpen = pensff2t;
                    }
                    else
                    {
                        Fpen = pensfft;
                    }
                    int fft_preY = AdjustY_ftt((int)((fftresult.GetData(i - 2).data.Abs+ fftresult.GetData(i - 1).data.Abs+fftresult.GetData(i).data.Abs)/3), h, ratio);
                    int fft_Y = AdjustY_ftt((int)((fftresult.GetData(i - 1).data.Abs + fftresult.GetData(i).data.Abs + fftresult.GetData(i+1).data.Abs) / 3), h, ratio);
                    g.DrawLine(Fpen, new Point((i - 1) * w / fftresult.Length, fft_preY), new Point(i * w / fftresult.Length, fft_Y));
                    //(fftresult.GetData(i - 1).data.Abs + fftresult.GetData(i).data.Abs + fftresult.GetData(i + 1).data.Abs) / 3;
                }
            }
            //g.Dispose();
        }

        private int AdjustY_ftt(int y, int h, int ratio)
        {
            //int result = h / 2 - y;
            return (h - 10) - y / ratio;
        }


        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            int ratio = (trackBar1.Value - trackBar1.Minimum) * (1 - 200) / (trackBar1.Maximum - trackBar1.Minimum) + 200;
            this.DrawGraphs2(e.Graphics, ratio);
        }


        #region その他データ受信毎に実行されるメソッド
        private void ProcessingByReceive()
        {
            toolStripStatusLabel2.Text = "frequency = " + sensorData.CurrentFreq.ToString("####") + "Hz";
            //Debug.WriteLine(sensorData.CurrentFreq);

            //double[] means = Statistics.Mean(sensorData.AllData);
            //textBoxDataView.Text += "加速度(x,y,z),角速度(x,y,z)=";
            //for (int i = 0; i < means.Length; i++)
            //{
            //    textBoxDataView.Text += means[i].ToString("0.0") + ",";
            //}
            //textBoxDataView.Text += "\n";
        }
        #endregion
        #endregion

        #region 保存関係
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            CalcFeature(featX);

            alldata = new Data();
            int time = 0;
            int rtime = 0;
            double t = 1.0;
            double t1 = 1.0;
            double t2 = 1.0;
            TMP temp = new TMP(time, rtime, t, t1, t2);
            alldata.PushData(new TMP(time, rtime, t, t1, t2));
            alldata.PushData(temp);

            int num = alldata.D[0].Time;
            //alldata.D[0].Time = 0;

            if (serialPort1.IsOpen)
            {
                if (comboBoxCOMS.Text != "COM5")
                {
                    labelConnect.Text = "Disconnected";
                    //シリアルポートが開いているなら
                    //切断する処理を行う
                    serialPort1.Write("stop senb \n");
                    //serialPort1.Close();

                    //serialPort1.Close();
                    //fft.FFT(AccelXYZ).Save();

                }
                else
                {
                    labelConnect.Text = "Disconnected";
                    serialPort1.Write(stopmessage, 0, 1);
                }

                if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Encoding enc = Encoding.GetEncoding("Shift_JIS");
                    //
                    using (StreamWriter sw = new StreamWriter(saveFileDialog1.FileName, false, enc))
                    {
                        for (int i = 0; i < sensorData.AllData.Count; i++)
                        {
                            sw.WriteLine(sensorData.AllData[i].CSVFormat(sensorData.AllData[0].D));
                        }
                    }
                }

            }
            
        }
        #endregion

        #region 加速度センサコマンド
        public void Mystart()
        {

            labelConnect.Text = "接続中...";
            string cmd = "senb +000000000 2 1 0 \n";
            if (!serialPort1.IsOpen)
            {
                //シリアルポートが開いていないなら
                serialPort1.Close();
                serialPort1.PortName = comboBoxCOMS.SelectedItem.ToString();
                serialPort1.Open();
                //labelConnect.Text = "接続";

                //センサ時間設定
                serialPort1.Write("sett 000000000 \n");

                //加速度と角速度をstopされるまで出力する
                serialPort1.Write(cmd);
            }
            else
            {
                //labelConnect.Text = "接続";
                serialPort1.Write(cmd);
                //加速度と角速度をstopされるまで出力する
                // serialPort1.Write(cmd);
            }
        }
        #endregion

        #region sensorData to FFT用のデータ配列 & FFT
        private FFTresult MyFFT()
        {
            //SensorDatanの最後のデータを取得--------------------------------
            AccelData[] drawnData2 = sensorData.ExtractData(fft_num).ToArray();

            //FFT用データリストにコピー
            if (drawnData2.Length == fft_num)
            {

                for (int i = 0; i < drawnData2.Length; i++ )
                {
                    //ローパスフィルタで重力値抽出
                    currentOrientationValues_x = drawnData2[i].ReturnByNumber(0) * 0.1f + currentOrientationValues_x * (1.0f - 0.1f);
                    currentOrientationValues_y = drawnData2[i].ReturnByNumber(1) * 0.1f + currentOrientationValues_y * (1.0f - 0.1f);
                    currentOrientationValues_z = drawnData2[i].ReturnByNumber(2) * 0.1f + currentOrientationValues_z * (1.0f - 0.1f);

                    //重力値除去
                    currentAccelerationValues_x = drawnData2[i].ReturnByNumber(0) - currentOrientationValues_x;
                    currentAccelerationValues_y = drawnData2[i].ReturnByNumber(1) - currentOrientationValues_y;
                    currentAccelerationValues_z = drawnData2[i].ReturnByNumber(2) - currentOrientationValues_z;

                    //ベクトル値計算
                    float dx = currentAccelerationValues_x - old_x;
                    float dy = currentAccelerationValues_y - old_y;
                    float dz = currentAccelerationValues_z - old_z;
                    //float dx = drawnData2[0].ReturnByNumber(0);
                    //float dy = drawnData2[0].ReturnByNumber(1);
                    //float dz = drawnData2[0].ReturnByNumber(2);

                    double vectorSize = Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2) + Math.Pow(dz, 2));

                        //Accel.RemoveAt(0);
                        Accel.Add(vectorSize);


                    // 状態更新
                    //vectorSize_old = vectorSize;
                    old_x = currentAccelerationValues_x;
                    old_y = currentAccelerationValues_y;
                    old_z = currentAccelerationValues_z;
                }


                //FFT用double配列に変形
                AccelXYZ = Accel.ToArray();
                Accel.Clear();
                //System.Diagnostics.Debug.WriteLine(AccelXYZ[0]);


            }
            fftresult = fft.FFT(AccelXYZ);
      
            return fftresult;
        }
        #endregion

        #region 学習と特徴量抽出
        //class 0 学習
        private void Learning0_Click(object sender, EventArgs e)
        {
            //learningbutton.FlatStyle = FlatStyle.Flat;
            //learning0.FlatAppearance.BorderColor = colors[3];
            form2List[0].Clear();
            //myFFT();
            for (int i = 0; i < fftresult.Length; i++)
            {
                form2List[0].Add(fftresult.GetData(i).data.Abs);
            }

            //double[] featX = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            CalcFeature(featX);
            //inputs[0] = featX;
            //Debug.WriteLine(featX[0]);
            inputs[0] = new double[] { featX[0], featX[1], featX[2], featX[3], featX[4], featX[5], featX[6], featX[7], featX[8], featX[9], featX[10], featX[11], featX[12], featX[13], featX[14], featX[15], featX[16], featX[17], featX[18], featX[19], featX[20], featX[21], featX[22] };
        }
        //class 1 学習
        private void Learning1_Click(object sender, EventArgs e)
        {
            //learning1.FlatAppearance.BorderColor = colors[3]; 
            form2List[1].Clear();
            //myFFT();
            for (int i = 0; i < fftresult.Length; i++)
            {
                form2List[1].Add(fftresult.GetData(i).data.Abs);
            }

            //double[] featX = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            CalcFeature(featX);
            //inputs[1] = featX;
            inputs[1] = new double[] { featX[0], featX[1], featX[2], featX[3], featX[4], featX[5], featX[6], featX[7], featX[8], featX[9], featX[10], featX[11], featX[12], featX[13], featX[14], featX[15], featX[16], featX[17], featX[18], featX[19], featX[20], featX[21], featX[22] };
        }
        //class 2 学習
        private void Learning2_Click(object sender, EventArgs e)
        {
            //learning2.FlatAppearance.BorderColor = colors[3]; 
            form2List[2].Clear();
            //myFFT();
            for (int i = 0; i < fftresult.Length; i++)
            {
                form2List[2].Add(fftresult.GetData(i).data.Abs);
            }

           // double[] featX = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
            CalcFeature(featX);   
            //inputs[2] = featX;
            inputs[2] = new double[] { featX[0], featX[1], featX[2], featX[3], featX[4], featX[5], featX[6], featX[7], featX[8], featX[9], featX[10], featX[11], featX[12], featX[13], featX[14], featX[15], featX[16], featX[17], featX[18], featX[19], featX[20], featX[21], featX[22] };
            //Debug.WriteLine(featX[2]);
        }
        //特徴量抽出
        private double[] CalcFeature(double[] featX)
        {
            double fftMax = 0.0;
            double max_fre = 62.0;
            double max_fre_temp = 62.0;

            for(int i = 0; i < featX.Length; i++)
            {
                featX[i] = 0;
            }

            for (int i = 62; i < 512; i++)
            {
                if (fftMax < fftresult.GetData(i).data.Abs)
                {
                    fftMax = fftresult.GetData(i).data.Abs;
                    max_fre = max_fre_temp;
                }
                max_fre_temp += 1.0;
            }
            featX[22] = max_fre;

            for (int i = 62; i < 82; i++)
            {
                featX[0] += fftresult.GetData(i).data.Abs;
            }
            for (int i = 82; i < 103; i++)
            {
                featX[1] += fftresult.GetData(i).data.Abs;
            }
            for (int i = 103; i < 123; i++)
            {
                featX[2] += fftresult.GetData(i).data.Abs;
            }
            for (int i = 123; i < 144; i++)
            {
                featX[3] += fftresult.GetData(i).data.Abs;
            }
            for (int i = 144; i < 164; i++)
            {
                featX[4] += fftresult.GetData(i).data.Abs;
            }
            for (int i = 164; i < 185; i++)
            {
                featX[5] += fftresult.GetData(i).data.Abs;
            }
            for (int i = 185; i < 205; i++)
            {
                featX[6] += fftresult.GetData(i).data.Abs;
            }
            for (int i = 205; i < 226; i++)
            {
                featX[7] += fftresult.GetData(i).data.Abs;
            }
            for (int i = 226; i < 246; i++)
            {
                featX[8] += fftresult.GetData(i).data.Abs;
            }
            for (int i = 246; i < 267; i++)
            {
                featX[9] += fftresult.GetData(i).data.Abs;
            }
            for (int i = 267; i < 286; i++)
            {
                featX[10] += fftresult.GetData(i).data.Abs;
            }
            for (int i = 286; i < 307; i++)
            {
                featX[11] += fftresult.GetData(i).data.Abs;
            }
            for (int i = 307; i < 327; i++)
            {
                featX[12] += fftresult.GetData(i).data.Abs;
            } 
            for (int i = 327; i < 348; i++)
            {
                featX[13] += fftresult.GetData(i).data.Abs;
            }
            for (int i = 348; i < 368; i++)
            {
                featX[14] += fftresult.GetData(i).data.Abs;
            }
            for (int i = 368; i < 389; i++)
            {
                featX[15] += fftresult.GetData(i).data.Abs;
            }
            for (int i = 389; i < 409; i++)
            {
                featX[16] += fftresult.GetData(i).data.Abs;
            }
            for (int i = 409; i < 430; i++)
            {
                featX[17] += fftresult.GetData(i).data.Abs;
            }
            for (int i = 430; i < 450; i++)
            {
                featX[18] += fftresult.GetData(i).data.Abs;
            }
            for (int i = 450; i < 471; i++)
            {
                featX[19] += fftresult.GetData(i).data.Abs;
            }
            for (int i = 471; i < 491; i++)
            {
                featX[20] += fftresult.GetData(i).data.Abs;
            }
            for (int i = 491; i < 512; i++)
            {
                featX[21] += fftresult.GetData(i).data.Abs;
            }
            return featX;
        }
        #endregion

        #region 認識開始ボタンとDeepLearning用の学習
        private void Complete_Click(object sender, EventArgs e)
        {
            flag = true;
            if (comboBox1.SelectedItem.ToString() == "DeepLearning")
            {
                ////DBNの生成
                //network = new DeepBeliefNetwork(
                //    inputsCount: inputs.Length,         // 入力層の次元
                //    hiddenNeurons: new int[] { 1 }); // 隠れ層と出力層の次元
                network = new DeepBeliefNetwork(
                     new GaussianFunction(),          // 活性化関数の指定
                     inputsCount: 9,                  // 入力層の次元
                     hiddenNeurons: new int[] { 4, 2 }); // 隠れ層と出力層の次元

                // ネットワークの重みをガウス分布で初期化する
                new GaussianWeights(network).Randomize();
                network.UpdateVisibleWeights();

                // DBNの学習アルゴリズムの生成  5000回繰り返し入力
                var teacher = new BackPropagationLearning(network);
                for (int i = 0; i < 1000; i++)
                    teacher.RunEpoch(inputs, outputs2);

                // 重みの更新
                network.UpdateVisibleWeights();
            }
        }
        #endregion

        #region Form2のグラフ描画
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            int ratio = (Form2Obj.trackBar2.Value - Form2Obj.trackBar2.Minimum) * (1 - 200) / (Form2Obj.trackBar2.Maximum - Form2Obj.trackBar2.Minimum) + 200;
            this.Form2_DrawGraph(e.Graphics, ratio, 0, form2List[0]);
        }
        private void pictureBox3_Paint(object sender, PaintEventArgs e)
        {
            int ratio = (Form2Obj.trackBar2.Value - Form2Obj.trackBar2.Minimum) * (1 - 200) / (Form2Obj.trackBar2.Maximum - Form2Obj.trackBar2.Minimum) + 200;
            this.Form2_DrawGraph(e.Graphics, ratio, 1, form2List[1]);
        }
        private void pictureBox4_Paint(object sender, PaintEventArgs e)
        {
            int ratio = (Form2Obj.trackBar2.Value - Form2Obj.trackBar2.Minimum) * (1 - 200) / (Form2Obj.trackBar2.Maximum - Form2Obj.trackBar2.Minimum) + 200;
            this.Form2_DrawGraph(e.Graphics, ratio, 2, form2List[2]);
        }
        private void Form2_DrawGraph(Graphics g, int ratio, int k, List<double> list)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Pen Fpen = new Pen(new SolidBrush(colors[5]), 2);
            Pen pensfft = new Pen(new SolidBrush(colors[5]), 3);
            Pen pensff2t = new Pen(new SolidBrush(colors[3]), 3);

            int w = Pbox[k].Width;
            int h = Pbox[k].Height;

            for (int i = 1; i < list.Count; i++)
            {
                if (i > 62 && i < 512)
                {
                    Fpen = pensff2t;
                }
                else
                {
                    Fpen = pensfft;
                }
                int fft_preY = AdjustY_ftt((int)list[i - 1], h, ratio);
                int fft_Y = AdjustY_ftt((int)list[i], h, ratio);
                g.DrawLine(Fpen, new Point((i - 1) * w / list.Count, fft_preY), new Point(i * w / list.Count, fft_Y));
            }
        }
        #endregion

        #endregion

        #region Form3グラフ描画
        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            int ratio = (Form3Obj.trackBar.Value - Form3Obj.trackBar.Minimum) * (1 - 200) / (Form3Obj.trackBar.Maximum - Form3Obj.trackBar.Minimum) + 200;
            this.Form3_DrawGraph(e.Graphics, ratio, 2, form2List);
        }
        private void Form3_DrawGraph(Graphics g, int ratio, int k, List<List<double>> list)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Pen[] pen = new Pen[3]; 
            pen[0] = new Pen(new SolidBrush(colors[0]), 3);
            pen[1] = new Pen(new SolidBrush(colors[1]), 3);
            pen[2] = new Pen(new SolidBrush(colors[3]), 3);

            int w = Form3Obj.pictureBox.Width;
            int h = Form3Obj.pictureBox.Height;

            for (int j = 0; j < list.Count; j++)
            {
                for (int i = 62; i < list[j].Count; i++)
                {
                    int fft_preY = AdjustY_ftt((int)list[j][i - 1], h, ratio);
                    int fft_Y = AdjustY_ftt((int)list[j][i], h, ratio);
                    g.DrawLine(pen[j], new Point((i - 62) * w / (list[j].Count-62), fft_preY), new Point((i-61) * w / (list[j].Count-62), fft_Y));
                }
            }
        }

        #endregion

        public void ChangebuttonColor(int answer)
        {
            learning0.ForeColor = Color.White;
            learning1.ForeColor = Color.White;
            learning2.ForeColor = Color.White;

            learning0.FlatAppearance.BorderColor = Color.White;
            learning1.FlatAppearance.BorderColor = Color.White;
            learning2.FlatAppearance.BorderColor = Color.White;

            if (answer == 0) { learning0.ForeColor = colors[3]; learning0.FlatAppearance.BorderColor = colors[3]; }
            else if (answer == 1) { learning1.ForeColor = colors[3]; learning1.FlatAppearance.BorderColor = colors[3]; }
            else if (answer == 2) { learning2.ForeColor = colors[3]; learning2.FlatAppearance.BorderColor = colors[3]; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2Obj.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Form3Obj.Show();
        }


        private void OnElapsed_TimersTimer(object sender, ElapsedEventArgs e)
        {
            this.MyFFT();
            pictureBox2.Invalidate();
        }

        private void SaveF_Click(object sender, EventArgs e)
        {
            this.saveFileDialog1.Filter = "csv(*.csv)|*.csv";
            DialogResult dr1 = saveFileDialog1.ShowDialog();
            saveFileDialog1.OverwritePrompt = false;

            if (dr1 == System.Windows.Forms.DialogResult.OK)
            {
                Encoding enc = Encoding.GetEncoding("Shift_JIS");
                StreamWriter writer1 = new StreamWriter(saveFileDialog1.FileName, true, enc);
                writer1.Write("{0},{1},{2},{3},{4},{5},{6}, {7},{8},{9},{10},{11},{12},{13}, {14}, {15}, {16},{17},{18}, {19}, {20}, {21}, {22}",

              featX[0], featX[1], featX[2], featX[3], featX[4], featX[5], featX[6], featX[7], featX[8], featX[9], featX[10], featX[11], featX[12], featX[13], featX[14], featX[15], featX[16], featX[17], featX[18], featX[19], featX[20], featX[21], featX[22]);

                writer1.WriteLine("");

                writer1.Close();
            }
        }

    }
        
}
