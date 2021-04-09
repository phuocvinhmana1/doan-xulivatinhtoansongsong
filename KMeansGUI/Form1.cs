using KMeansProject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using System.Diagnostics;

namespace KMeansGUI
{
    public partial class Form1 : Form
    {
        int demne = 0;
        int row = 0;
        int demdong;
        int doithay = 0;
        DateTime times;
        DateTime timee;
        DateTime time;
        // khai bao lop Kmeans
        private KMeans objKMeans;
        // khoi tao list chua cac diem
        private List<double[]> dataSetList;
        private List<double[]> dataSetListTamCum;
        List<diem> listdiem;
        List<tamdiem> listamdiem;
        List<tamdiemcu> listtamdiemcu;
        List<tamdiemcu2> listtamdiemcu2;
        //khai bao class backgroundworker trong c#, dung de xu li hinh anh
        private BackgroundWorker objBackgroundWorker;
        //khai bao lop KmeansEventArgs
        KMeansEventArgs kmeansEA;
        string maumoi = "";
        bool thaydoi = true;

        public Form1()
        {
            InitializeComponent();
         
        }


         Color[] mausac = {Color.Red,Color.Blue,Color.Black, Color.Gold, Color.Green, Color.Orange};

        
   
        
        private void button1_Click(object sender, EventArgs e)
        {
            picImage.Refresh();

            var watch = System.Diagnostics.Stopwatch.StartNew();



                Random objRandom = new Random();
                dataSetList = new List<double[]>();

           
                
                for (int i = 0; i < int.Parse(textBox5.Text); i++)
                {
                    // khoi tao mang 1 chieu point voi 2 phan tu ung voi toa do x va y
                    double[] point = new double[2];

                    for (int j = 0; j < 2; j++)
                    {

                        if (j == 0)  // neu j==0 la x
                        {
                            // tao ngau nhien toa do x theo chieu dai cua picturebox
                            point[j] = Misc.taoDiemNgauNhien(objRandom, 0, 500);
                        }
                        else  // nguoc lai la y
                        {
                            // tao ngau nhien toa do y theo chieu cao cua picturebox
                            point[j] = Misc.taoDiemNgauNhien(objRandom, 0, 500);
                        }

                    }
                    // them diem vua tao vao list diem

                    dataSetList.Add(point);
                }
                // goi ham khoi tao 2 tham so o class Kmeans voi 2 tham so lan luot la so cum, va khoang cach
                objKMeans = new KMeans((int)numericUpDown2.Value, new TinhKhoangCach(), int.Parse(textBox4.Text));

      
                objBackgroundWorker = new BackgroundWorker();
                objBackgroundWorker.WorkerReportsProgress = true; 
               
                objBackgroundWorker.DoWork += ObjBackgroundWorker_DoWork;

               
                objBackgroundWorker.RunWorkerCompleted += ObjBackgroundWorker_RunWorkerCompleted;

             
                objBackgroundWorker.ProgressChanged += ObjBackgroundWorker_ProgressChanged;

                
                objBackgroundWorker.RunWorkerAsync(dataSetList.ToArray());

                watch.Stop();

                var elapsedMs = watch.Elapsed.TotalSeconds;
                //MessageBox.Show(elapsedMs.ToString());
                textBox1.Text = elapsedMs.ToString();
               
            
                
            
        }

        private void ObjBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            kmeansEA = e.UserState as KMeansEventArgs;
            if (kmeansEA != null)
            {
                foreach (TamCum tamcum in kmeansEA.tamCumList)
                {
                   // textBox3.Text = tamcum.ToString();
                    picImage.Invalidate();
                    //Thread.Sleep(1000);
                }
            }
        }

        private void ObjBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //TamCum[] result = e.Result as TamCum[];
          //  MessageBox.Show("Ket thuc!");
            //button1.Enabled = true;
        }

        private void ObjBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {

            // tao 1 luong moi thuc thi cong viec chinh
            // e.argument luu tham so nhan boi runwokerasync
            double[][] inputDataset = e.Argument as double[][];
            objKMeans.UpdateProgress += (x, y) =>
            {
                objBackgroundWorker.ReportProgress(0, y);
            };
            e.Result = objKMeans.Run(inputDataset); // e.result luu ket qua, report lai
        }

        private void picImage_Paint(object sender, PaintEventArgs e)
        {
            if (kmeansEA == null || kmeansEA.tamCumList == null) return;


            if (checkBox1.Checked)
            {
                foreach (TamCum centroid in kmeansEA.tamCumList)
                    centroid.DrawMe(e);
            }
            

            if (kmeansEA.Dataset == null) return;


            if (checkBox1.Checked)
            {
                Graphics g = e.Graphics;

                foreach (double[] point in kmeansEA.Dataset)
                {
                    g.DrawEllipse(new Pen(Color.Gray, 2.0f), (float)point[0], (float)point[1], 10, 10);
                }
            }
            //MessageBox.Show("123");
        }

        public Color _color;


        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();

            // ham do thoi gian thuc thi chuong trinh
            var watch = System.Diagnostics.Stopwatch.StartNew();

            // khai bao cac list luu gia tri cac diem va tam diem

            listdiem = new List<diem>();
            listamdiem = new List<tamdiem>();
            listtamdiemcu = new List<tamdiemcu>();
            listtamdiemcu2 = new List<tamdiemcu2>();
            
            Random objRandom = new Random();
      


            // tao diem ngau nhien them vao list diem

            for (int i = 0; i < int.Parse(textBox5.Text); i++)
            {
               
                double[] point = new double[2];

                for (int j = 0; j < 2; j++)
                {

                    if (j == 0)  
                    {
                      
                    
                        point[j] = Misc.taoDiemNgauNhien(objRandom, 0, 500);
                    }
                    else  
                    {
               
              
                        point[j] = Misc.taoDiemNgauNhien(objRandom, 0, 500);
                    }              

                }             
               
               
                diem d = new diem(point[0],point[1],0,Color.Gray.Name.ToString());
                listdiem.Add(d); // them diem vao list diem
            
            }




            // tao tam cum ngau nhien them vao list tam cum

            for (int i = 0; i < numericUpDown2.Value; i++)
            {

                double x = 0;
                double y = 0;
                x = Misc.taoDiemNgauNhien(objRandom, 0, 500);
                y = Misc.taoDiemNgauNhien(objRandom, 0, 500);
                Color mau = mausac[i];
                tamdiem td = new tamdiem(x, y, mau.Name.ToString(), 0,0);
      
                listamdiem.Add(td);
             
            }






            for (int i3 = 0; i3 < 5; i3++)
            {
                // tinh khoang cach giua cac diem den tam cum va doi mau cac diem giong nhu mau tam cum gan nhat
                timDiemMinDenTamCum(listdiem, listamdiem);

                // tinh trung binh khoang cach cac diem den tam cum va di chuyen tam cum

                for (int i = 0; i < listamdiem.Count; i++)
                {
                    listamdiem[i].Dem = 0;
                    listamdiem[i].X = 0;
                    listamdiem[i].Y = 0;

                }

                for (int i1 = 0; i1 < listdiem.Count; i1++)
                {

                    for (int i = 0; i < listamdiem.Count; i++)
                    {

                        if (listdiem[i1].Mau == listamdiem[i].Mausac)
                        {

                            listamdiem[i].X = listamdiem[i].X + listdiem[i1].X;
                            listamdiem[i].Y = listamdiem[i].Y + listdiem[i1].Y;
                            listamdiem[i].Dem++;
                        }
                    }

                }

                for (int i = 0; i < listamdiem.Count; i++)
                {
                    // MessageBox.Show("chia cho: "+ listamdiem[i].Kc.ToString());
                    listamdiem[i].X = listamdiem[i].X / listamdiem[i].Dem;
                    listamdiem[i].Y = listamdiem[i].Y / listamdiem[i].Dem;
                }

                //Thread.Sleep(int.Parse(textBox4.Text));
            }



                    if (checkBox1.Checked)
                    {
                        veDiem(listdiem, maumoi);
                    }


                    // ve lai tam cum

                    if (checkBox1.Checked)
                    {
                        veTamCum(listamdiem, int.Parse(numericUpDown2.Value.ToString()));
                    }
                    demne++;
                
         
           
                watch.Stop();
                var elapsedMs = watch.Elapsed.TotalSeconds;
                //MessageBox.Show(elapsedMs.ToString());
                textBox2.Text = elapsedMs.ToString();
                double chenhlech = double.Parse(textBox1.Text) - double.Parse(textBox2.Text);


                if (row == 0)
                {
                    dataGridView1.Rows.Add("0", textBox5.Text, numericUpDown2.Value, textBox1.Text, textBox2.Text, chenhlech.ToString());
                    row = row + 1;
                }
                else
                {
                    dataGridView1.Rows.Add(row, textBox5.Text, numericUpDown2.Value, textBox1.Text, textBox2.Text, chenhlech.ToString());
                    row++;

                }


        }

        public void timDiemMinDenTamCum(List<diem> listdiem, List<tamdiem> listamdiem)
        {
            for (int i1 = 0; i1 < listdiem.Count; i1++)
            {

                for (int i = 0; i < listamdiem.Count; i++)
                {

                    listamdiem[i].Kc = tinhkc2(listdiem[i1].X, listdiem[i1].Y, listamdiem[i].X, listamdiem[i].Y);
                }

                // tim gia tri nho nhat cua tung diem voi tam cum

                double min = listamdiem[0].Kc;
                int dem = 0;
                for(int i = 0; i < listamdiem.Count; i ++)
                {
                    if(min > listamdiem[i].Kc)
                    {
                        min = listamdiem[i].Kc;
                        dem = i;
                    }
                }

                maumoi = listamdiem[dem].Mausac;

                listdiem[i1].Mau = maumoi;   

            }
        }


        public void veDiem(List<diem> listdiem, string mau)
        {

            int sodiem = int.Parse(textBox5.Text);

            for (int i = 0; i < sodiem; i++)
            {

                double x = 0;
                double y = 0;
                x = listdiem[i].X;
                y = listdiem[i].Y;

                Graphics g1 = pictureBox1.CreateGraphics();
                g1.FillEllipse(new SolidBrush(Color.FromName(listdiem[i].Mau)), (float)x, (float)y, 10, 10);
                //Thread.Sleep(int.Parse(textBox4.Text));

            }

        }

        public void veTamCum(List<tamdiem> listtamdiem, int k)
        {
            int socum = int.Parse(numericUpDown2.Value.ToString());

            for (int i = 0; i < k; i++)
            {

                double x = 0;
                double y = 0;
                x = listtamdiem[i].X;
                y = listtamdiem[i].Y;

                Graphics g1 = pictureBox1.CreateGraphics();
                g1.FillEllipse(new SolidBrush(mausac[i]), (float)x, (float)y, 15, 15);
              //  Thread.Sleep(int.Parse(textBox4.Text));

            }
 
        }



        public double tinhkc2(double x, double y, double tamx, double tamy)  // 1,2   3,2
        {
            //double kq = 0;

            return Math.Sqrt(Math.Pow(x - tamx,2) + Math.Pow(y - tamy,2));
        }

        public double tinhkc(double[] diem1, double[] diem2)  // 1,2   3,2
        {
            double ketqua = 0;
            for (int i = 0; i < 2; i++)
            {
                ketqua = ketqua + Math.Pow(diem1[i] - diem2[i], 2);
            }
            return Math.Sqrt(ketqua); // tra ve khoang cach giua diem1 va diem2
        }
            

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("123");
        }
            

        
    }
}
