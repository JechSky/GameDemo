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

namespace wftest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Panel p;

        object obj = new object();
        Random ran = new Random();
        public static List<int> selects = new List<int>();
        List<string> imgUrls = new List<string>()
        {
            "images//1.png","images//2.png","images//3.png","images//4.png","images//5.png"
        };

        private void Form1_Load(object sender, EventArgs e)
        {
            Common.formW = this.Width;
            Common.formH = this.Height;
            this.BackColor = Color.Pink;
            //p = new Panel() { Width = this.Width, Height = this.Height, BackColor = Color.Red };
            //this.Controls.Add(p);
            CreatePicFun();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                lock (obj)
                {
                    if (Common.listPics.Count > 4)
                    {
                        MessageBox.Show("当前最多5个，请消除后再添加！");
                    }
                    else
                    {
                        CreatePicFun();
                    }

                }
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }

        int CreateRandom(List<int> selects,int rNum=-1)
        {
            try
            {
                lock (obj)
                {
                    int ranNum = 0;
                    if (rNum == -1)
                    {
                        ranNum = ran.Next(0, imgUrls.Count);
                    }
                    else
                    {
                        if (rNum == 0 || rNum == 1)
                        {
                            ranNum = ran.Next(rNum + 1, imgUrls.Count);
                        }
                        else
                        {
                            ranNum = ran.Next(0, rNum);
                        }
                    }
                    if (selects != null && selects.Contains(ranNum))
                    {
                        return CreateRandom(selects, ranNum);
                    }
                    else
                    {
                        if (selects == null || selects.Count < imgUrls.Count)
                        {
                            selects.Add(ranNum);
                        }
                        return ranNum;
                    }
                }

            }
            catch (Exception ex)
            {
                throw;
            }
           
        }

        void CreatePicFun()
        {
            int top = ran.Next(0, Common.formH - 240);
            int left = ran.Next(0, Common.formW - 230);
            int selectNum = CreateRandom(selects);
            CreatePicobj(imgUrls[selectNum], selectNum, top, left);
        }

        void CreatePicobj(string imgUrl, int id, int top = 5, int left = 5)
        {
            lock (obj)
            {
                MyPicBox mypic = new MyPicBox(this) { picId = id.ToString(), BackgroundImage = Image.FromFile(imgUrl), Top = top, Left = left };
                //mypic.BackColor = Color.Transparent;
                //mypic.Parent = this;
                //mypic.PerformLayout();
                //List<MyPicBox> lists = Common.listPics.Where(r => r.picId != mypic.picId).ToList();
                //if (lists.Count>0)
                //{
                //    for (int i = 0; i < lists.Count; i++)
                //    {
                //        mypic.Parent = lists[i];
                //    }
                //}

                //p.Controls.Add(mypic);
                this.Controls.Add(mypic);
                Common.listPics.Add(mypic);
            }
        }


    }
}
