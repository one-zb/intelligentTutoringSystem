using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;


namespace GDI
{
    public partial class Form1 : Form
    {
        //Bitmap bitmap;
        //Graphics graphics;
        //Image image;
        //Pen pen = new Pen(Brushes.Black);
        public Form1()
        {
            InitializeComponent();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //bitmap = new Bitmap(500, 500);
            //graphics = Graphics.FromImage(bitmap);
            //Gdi gdi = new Gdi();
            //float dx = graphics.DpiX / 25.4f;
            //float dy = graphics.DpiY / 25.4f;
            //单个图元
            //gdi.Arc(g:graphics,b:bitmap,startAngle:30,endAngle:120);
            //gdi.Line();
            //gdi.Triangle();
            //gdi.Rectangle();
            //gdi.Pie();
            //gdi.Convexlens(x1: 250, y1: 10);
            //gdi.Concavelens(x1: 250, y1: 10);
            //gdi.Bulb(x1:250,y1:10);
            //gdi.Ammeter(x1: 250, y1: 250);
            //gdi.Voltmeter(x1: 250, y1: 250);
            //gdi.Car(x1: 250, y1: 250);
            //gdi.Block(x1: 100, y1: 300);
            //gdi.Desk(x1: 100, y1: 200);
            //gdi.Arrow();
            //gdi.Str(str:"F",size:15);
            //gdi.Ruler(g:graphics,b:bitmap,x1: 10, y1: 50, width: 70, height: 10);
            //gdi.SpringDynamometer(x1: 40, y1: 30, width: 25, height: 70,scale:18);
            //gdi.Motor(graphics, bitmap, 250, 250);
            //gdi.Resistance(graphics, bitmap);
            //gdi.SlidingRheostat(graphics, bitmap, 250, 250,scale:0.7f);
            //gdi.Switch(graphics, bitmap);
            //gdi.Power(graphics, bitmap);
            //多个物体组成
            //gdi.Desk(graphics, bitmap, 200, 200);
            //gdi.Block(graphics, bitmap, 250, 190, 150, 10);
            //gdi.Block(graphics, bitmap, 350, 175, 15, 15);
            //gdi.Arrow(graphics, bitmap, 400, 195, 450, 195);
            //gdi.Str(graphics, bitmap, "F", 450, 195, 15);
            //gdi.Str(graphics, bitmap, "m", 350, 155, 15);
            //gdi.Str(graphics, bitmap, "M", 230, 180, 15);
            //gdi.Ruler(graphics, bitmap, 10, 10, 100, 10);
            /// <summary>
            ///电路图
            /// </summary>
            //gdi.Power(graphics, bitmap, 200, 100);
            //gdi.Switch(graphics, bitmap, 300, 150,angle:90,size:8);
            //gdi.Bulb(graphics, bitmap, 200, 150);
            //gdi.Bulb(graphics, bitmap, 170, 200);
            //gdi.Switch(graphics, bitmap, 240, 200,size:8);

            //image = bitmap;
            //image.Save(@"C:\Users\99629\Documents\Visual Studio 2012\Projects\GDI\GDI\Picture\CircuitDiagram.png", System.Drawing.Imaging.ImageFormat.Png);
            //pictureBox1.Image = bitmap;
          
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
           

        }
    }
}
