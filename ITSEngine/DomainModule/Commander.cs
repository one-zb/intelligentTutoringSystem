using GDI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;


namespace ITS.DomainModule
{
    /// <summary>
    /// 这个是命令类，供用户调用
    /// </summary>
    public class Commander
    {


        // 这里是GDI 作为启动项目的测试，图形显示在Test窗口上
        //static GDILib myGDI = new GDILib(Test.test.pictureBox1.CreateGraphics());

        static readonly Bitmap bmp = new Bitmap(1920, 1080);

        static readonly Graphics graph = Graphics.FromImage(bmp);
        static GDILib myGDI = new GDILib(graph);

        //static DateTime dt = DateTime.Now;
        //static string directory = @"C:\Users\Administrator\Desktop\老师项目\image\test.png";
        //const string fileName ="test.png";
        const string imagePath = @"C:\Users\10114\Desktop\老师项目\image\test.png";

       





        //static public void SetBitmap(Bitmap bitmap)
        //{
        //    bmp = bitmap;
        //    graph = Graphics.FromImage(bmp);
        //    myGDI = new GDILib(graph);
        //}


        // 存储图片路径
        static public string[] GetImagePath()
        {


            //Test.test.pictureBox1.Image.Save(imagePath);
            //bmp.Save(imagePath);
            return new string[] { imagePath };
        }
        

        // 测试
        [CMD("s", "sh", "h", "hi")]
        static public int SayHi()
        {
            Console.WriteLine("Hello world!");
            return 0;
        }

        // 测试
        [CMD("i", "ip", "ipad")]
        static public int Ipad()
        {
            Console.WriteLine("i am a iapd!");
            return 0;
        }


        // 化学类 二氧化硫制备实验图 测试通过
        [CMD("sulfurdioxideexperiment", "so")]
        static public void DrawSO2(int flag)
        {

            myGDI.DrawSO2();
            if(flag == 1)
            {
                bmp.Save(imagePath);
            }
        }


        // 化学类 铁架台 测试通过
        [CMD("ironsupportflask")]
        static public void DrawDrawIronSupportFlask()
        {

            myGDI.DrawIronSupportFlask();
        }

        // 化学  瓶子 测试通过
        
        [CMD("bottle")]
        static public void DrawBottle(int flag)
        {

            myGDI.DrawBottle();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }

            //graph.Flush();

        }





        // 显示三角形ABC外心 D 测试通过
        [CMD("outcenter")]
        static public void DrawOutCenter(string A, string B, string C, string D)
        {

            myGDI.DrawOutCenter(A, B, C, D);
        }

        // 显示三角形ABC垂心 D 测试通过
        [CMD("verticalcenter")]
        static public void DrawVerticalCenter(string A, string B, string C, string D)
        {
            myGDI.DrawVerticalCenter(A, B, C, D);
        }

        // 当提取到一个三角形ABC的时候 可以直接创建一个三角形 示意图为等边三角形 不精确 
        // 只要识别是画三角形 就直接用这示意图
        // 测试通过
        [CMD("trangle" )]
        static public void DrawTrangle (string A, string B, string C)
        {

            myGDI.DrwaTrangle(A, B, C);
        }

        // 测试通过 画一个矩形 或者 任意四边形
        [CMD("rectangle")]
        static public void DrawRectangle(string A, string B, string C, string D)
        {

            myGDI.DrawRectangle(A, B, C, D);
        }

        // 测试通过 画一个正方形
        [CMD("square")]
        static public void DrawSquare(string A, string B, string C, string D)
        {

            myGDI.DrawSquare(A, B, C, D);
        }

        // 画一个平行四边形  测试通过
        [CMD("parasquare")]
        static public void DrawParaSquare(string A, string B, string C, string D)
        {

            myGDI.DrawParaSquare(A, B, C, D);
        }



        //画线段AB       
        //[CMD("segmentAB", "边AB", "边")]
        //static public void DrawLineAB()
        //{
        //    //Test.test.refresh();
        //    GDILib myGDI = new GDILib(Test.test.pictureBox1.CreateGraphics()); 
        //    myGDI.DrawLineAB("O", "M");
        //    Console.WriteLine(myGDI.getX1());

        //}

        //带参数的重载画线段 传进来字母 初始化 画线段AB 测试已通过
        [CMD("边AB", "边", "segment")]
        static public void DrawLineAB(string A, string B)
        {
            //myGDI = new GDILib(Test.test.pictureBox1.CreateGraphics());
       
            myGDI.DrawLineAB(A, B);
        }


    


        //随机画一个点 测试已通过
        [CMD("freepoint")]
        static public void FreePoint(string pointName)
        {
             
            //myGDI = new GDILib(Test.test.pictureBox1.CreateGraphics());
            myGDI.FreePoint(pointName);

        }


        // 在上一个命令画出的线段中 画一个中点  也就是说 画布上已经存在一个线段 然后在这个线段上画中点
        //ABD 表示在AB上画中点
        //测试已通过 
        [CMD("midpoint")]
        static public void DrawMidPoint(string pointA, string pointB,string midPoint)
        {
            
            //myGDI = new GDILib(Test.test.pictureBox1.CreateGraphics());
            myGDI.DrawMidPoint(pointA, pointB, midPoint);

        }


        // 画平行线 AB // CD  测试通过 
        [CMD("paraline")]
        static public void DrawParaLine(string A, string B, string C, string D)
        {

            //myGDI = new GDILib(Test.test.pictureBox1.CreateGraphics());
            myGDI.DrawParaLine(A, B, C, D);

        }

        // 画AB垂直CD  AB是画布上已知直线 C 是已知直线外一点  垂足为D 这个顺序不能变
        // 测试通过
        [CMD("vertical")]
        static public void DrawVertical(string A, string B, string C, string D)
        {

            //myGDI = new GDILib(Test.test.pictureBox1.CreateGraphics());
            myGDI.DrawVertical(A, B, C, D);

        }


        // 画已知三角形的外接圆 测试通过
        [CMD("outcircle")]
        static public void DrawOutCircle(string A, string B, string C)
        {

            //myGDI = new GDILib(Test.test.pictureBox1.CreateGraphics());
            myGDI.DrawOutCircle(A, B, C);

        }


        // 画已知两条直线的交点 并且显示交点
        [CMD("insertpoint")]
        static public void DrawInsertPoint(string A, string B, string C, string D, string F)
        {

            //myGDI = new GDILib(Test.test.pictureBox1.CreateGraphics());
            myGDI.DrawInsertPoint(A, B, C, D, F);

        }



















        //画已知AB线段相等的线段 AC
        [CMD("equalABAC")]
        static public void DrawEqualLineAC()
        {
            
            //GDILib myGDI = new GDILib(Test.test.pictureBox1.CreateGraphics());
            //myGDI.DrawEqualLineAC();

        }


        //画已知AC两点的中点F
        [CMD("midF")]
        static public void DrawMidPointF()
        {

            //GDILib myGDI = new GDILib(Test.test.pictureBox1.CreateGraphics());
            //myGDI.DrawMidPointF();

        }


        //连接CE
        [CMD("segmentCE")]
        static public void DrawLine1()
        {

            //GDILib myGDI = new GDILib(Test.test.pictureBox1.CreateGraphics());
            //myGDI.DrawLineCE();

        }

        //连接BF
        [CMD("segmentBF")]
        static public void DrawLine2()
        {

            //GDILib myGDI = new GDILib(Test.test.pictureBox1.CreateGraphics());
            //myGDI.DrawLineBF();

        }

    }
}
