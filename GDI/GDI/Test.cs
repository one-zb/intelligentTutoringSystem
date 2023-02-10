using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace GDI
{
    public partial class Test : Form
    {
        //bool b = false;

        //这行代码和下面的构造方法作用：可以让其他普通类调用这个窗口的控件或者事件
        public static Test test;
        public Test()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            test = this;
        }
        
        //定义为类的成员，是为了在窗体加载时就初始化该对象，所以不要放在单击事件里去创建该对象
        //不要放在单击事件里去创该对象
        //达到窗体一旦加载，自定义的命令就已经全部加入字典
        CMDMatch match = new CMDMatch();


        //刷新画图控件
        //public void refresh()
        //{
        //    b = true;
        //    pictureBox1.Refresh();
        //}

        private void button1_Click(object sender, EventArgs e)
        {

            //配对用户输入命令,也就是当我们点击鼠标画图
            //才会去语义网络图中去寻找有没有包含图的节点，然后才开始调用生图的方法
            //match.GraphMethod();

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {


            //if (b == false) return;

            //实例画图形库
            //GDILib myGDI = new GDILib(e.Graphics); 
            //myGDI.drawLine();


            //Graphics g = e.Graphics;
            //Pen pen = new Pen(Color.Black);

            //画端点
            //g.FillEllipse(Brushes.Red, 100, 50, 4, 4);
            //g.FillEllipse(Brushes.Red, 200, 50, 4, 4);

            //画直线
            //g.DrawLine(pen, new Point(100, 50), new Point(200, 50));


            //给直线两端标字母
            //String text = "A";
            //Font textFont = new Font("宋体", 16);
            //SolidBrush textBrush = new SolidBrush(Color.Black);
            //g.DrawString(text, textFont, textBrush, 90, 50);

            //释放资源
            //pen.Dispose();
            //g.Dispose();
        }

        private void Test_Load(object sender, EventArgs e)
        {

        }
    }
}
