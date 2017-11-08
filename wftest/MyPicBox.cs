using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wftest
{
    public class MyPicBox: PictureBox
    {
        public string picId = "";
        bool isDown = false;
        Point pointOld = new Point();
        Form form;
        public MyPicBox(Form form)
        {
            this.Width = 200;
            this.Height = 200;
            this.MouseDown += MyPicBox_MouseDown;
            this.MouseMove += MyPicBox_MouseMove;
            this.MouseUp += MyPicBox_MouseUp;
            this.Paint += MyPicBox_Paint;
            this.form = form;
        }

        private void MyPicBox_Paint(object sender, PaintEventArgs e)
        {
            //this.BackColor = Color.Transparent;
            //this.Parent = this.form;

            //foreach (Control C in this.form.Controls)
            //{
            //    if (C is PictureBox)
            //    {
            //        PictureBox L = (PictureBox)C;

            //        L.Visible = true;
            //        ImageAttributes attrib = new ImageAttributes();
            //        Color color = Color.Transparent;
            //        attrib.SetColorKey(color, color);
            //        e.Graphics.DrawImage(L.BackgroundImage, new Rectangle(L.Left, L.Top, L.Width, L.Height), 0, 0, L.Width, L.Height, GraphicsUnit.Pixel, attrib);

            //    }
            //}

        }

        private void MyPicBox_MouseDown(object sender, MouseEventArgs e)
        {
            MyPicBox cur = Common.listPics.Where(r => r.picId.Equals(this.picId)).FirstOrDefault();
            if (cur != null)
            {
                cur.isDown = true;
                cur.pointOld = e.Location;
            }
        }

        private void MyPicBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MyPicBox cur = Common.listPics.Where(r => r.picId.Equals(this.picId)).FirstOrDefault();
                if (cur != null && cur.isDown)
                {
                    int incressW = e.Location.X - cur.pointOld.X;
                    int incressH = e.Location.Y - cur.pointOld.Y;
                    cur.Location = new Point(cur.Left + incressW, cur.Top + incressH);

                    Fix(cur);

                    if (Common.listPics.Count > 1)
                    {
                        #region MyRegion
                        //Control moveIn = form.GetChildAtPoint(e.Location);
                        //if (moveIn != null)
                        //{
                        //    if (moveIn is MyPicBox)
                        //    {
                        //        MyPicBox mb = moveIn as MyPicBox;
                        //        Common.listPics.Remove(mb);
                        //        Form1.selects.Remove(int.Parse(mb.picId));
                        //        form.Controls.Remove(moveIn);
                        //        mb.Dispose();
                        //    }
                        //}
                        #endregion

                        cur.PerformLayout();

                        MyPicBox moveIn = hasPoint(cur.PointToScreen(cur.Location), cur);
                        if (moveIn != null && !string.IsNullOrEmpty(moveIn.picId) && moveIn.picId != cur.picId)
                        {
                            Form1.selects.Remove(int.Parse(moveIn.picId));
                            Common.listPics.Remove(moveIn);
                            form.Controls.Remove(moveIn);
                            moveIn.Dispose();
                        }

                    }

                }

            }

        }

        private void MyPicBox_MouseUp(object sender, MouseEventArgs e)
        {
            MyPicBox cur = Common.listPics.Where(r => r.isDown && r.picId.Equals(this.picId)).FirstOrDefault();
            if (cur != null)
            {
                isDown = false;
            }
        }
        
        MyPicBox hasPoint(Point point,MyPicBox pic)
        {
            List<MyPicBox> lists = Common.listPics.Where(r => r.picId != pic.picId).ToList();
            for (int i = 0; i < lists.Count; i++)
            {
               
                List<Point> points = new List<Point>();

                for (int j = 0; j <= 100; j++)
                {
                    for (int k = 0; k <= 100; k++)
                    {
                        points.Add(lists[i].PointToScreen(new Point(lists[i].Location.X + j, lists[i].Location.Y + k)));
                    }

                }
                if (points.Contains(point))
                {
                    return lists[i];
                }
                   
            }
            return null;
        }

        void Fix(MyPicBox pic)
        {
            if(pic.Left<=0)
            {
                pic.Left = 0;
            }
            else if(pic.Left>= Common.formW-pic.Width - 15)
            {
                pic.Left = Common.formW - pic.Width - 15;
            }
            if(pic.Top<=0)
            {
                pic.Top = 0;
            }
            else if (pic.Top >= Common.formH - pic.Height-35)
            {
                pic.Top = Common.formH - pic.Height - 35;
            }


        }


    }

}
