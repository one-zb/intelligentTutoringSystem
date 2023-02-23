using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace GDI
{
    /// <summary>
    /// 这个是命令类，供用户调用
    /// </summary>
    public class Commander
    {


        // 这里是GDI 作为启动项目的测试，图形显示在Test窗口上
        //static GDILib myGDI = new GDILib(Test.test.pictureBox1.CreateGraphics());

        // 在这里修改参数，可以调整图片显示在界面的清晰度，尽量跟OutputTextBox类中image参数接近
        // 这里设为600 400 image.width设为400 是清晰图片，
        static readonly Bitmap bmp = new Bitmap(600, 500);

        static readonly Graphics graph = Graphics.FromImage(bmp);
        static GDILib myGDI = new GDILib(graph);

        const string imagePath = @"C:\Users\10114\Desktop\大论文\20221204老师项目\老师项目\image\test.png";


        // 存储图片路径
        static public string[] GetImagePath()
        {

            //Test.test.pictureBox1.Image.Save(imagePath);
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
        [CMD("sulfur dioxide experiment")]
        static public void DrawCreateSO2Experiment(int flag)
        {

            myGDI.DrawSO2();
            if(flag == 1)
            {
                // flag 为1表示这是最后一个命令，绘制的图形可以保存为图片， 因为界面显示图形是读取图片文件。
                bmp.Save(imagePath);
            }
            
        }


        // 化学类 铁架台 测试通过
        [CMD("ironsupportflask")]
        static public void DrawDrawIronSupportFlask(int flag)
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

        }




        // 在已知三角形ABC中任取一点O 测试通过
        [CMD("tranglefreepoint")]
        static public void DrawFreePointInTrangle(string A, string B, string C, string O, int flag)
        {

            myGDI.DrawFreePointInTrangle(A, B, C, O);
            if (flag == 1)
            {

                bmp.Save(imagePath);
            }
        }





        // 在已知线段AB上随机取一个点0 测试通过
        [CMD("linefreepoint")]
        static public void DrawFreePoint(string A, string B, string O, int flag)
        {

            myGDI.DrawFreePoint(A, B, O);
            if (flag == 1)
            {

                bmp.Save(imagePath);
            }
        }




        // 画一个角ABC的角平分线BO 测试通过
        [CMD("anglebisector")]
        static public void DrawAngleBisector(string A, string B, string C, string O, int flag)
        {

            myGDI.DrawAngleBisector(A, B, C, O);
            if (flag == 1)
            {

                bmp.Save(imagePath);
            }
        }


        // 在一个正方形ABCD或者矩形内任取一个点O 测试通过
        [CMD("insidepoint")]
        static public void DrawFreePoint(string A, string B, string C, string D, string O, int flag)
        {

            myGDI.DrawFreePoint(A, B, C, D, O);
            if (flag == 1)
            {

                bmp.Save(imagePath);
            }
        }



        // 画一个任意凸四边形ABCD 测试通过
        [CMD("tusquare")]
        static public void DrawTuSquare(string A, string B, string C, string D, int flag)
        {

            myGDI.DrawTuSquare(A, B, C, D);
            if (flag == 1)
            {

                bmp.Save(imagePath);
            }
        }

        // 在已知的一个四边形中，在其中的AB一条边的外边画一个点C 测试通过
        [CMD("outsidepoint")]
        static public void DrawPointOutSide(string A, string B, string C, int flag)
        {

            myGDI.DrawPointOutSide(A, B, C);
            if (flag == 1)
            {

                bmp.Save(imagePath);
            }
        }

        // 






        // 显示三角形ABC外心 D 测试通过
        [CMD("outcenter")]
        static public void DrawOutCenter(string A, string B, string C, string D, int flag)
        {

            myGDI.DrawOutCenter(A, B, C, D);
            if (flag == 1)
            {
                
                bmp.Save(imagePath);
            }
        }

        // 显示三角形ABC垂心 D 测试通过
        [CMD("verticalcenter")]
        static public void DrawVerticalCenter(string A, string B, string C, string D, int flag)
        {
            myGDI.DrawVerticalCenter(A, B, C, D);
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }

        // 当提取到一个三角形ABC的时候 可以直接创建一个三角形 示意图为等边三角形 不精确 
        // 只要识别是画三角形 就直接用这示意图
        // 测试通过
        [CMD("trangle" )]
        static public void DrawTrangle (string A, string B, string C, int flag)
        {

            myGDI.DrwaTrangle(A, B, C);
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }

        // 测试通过 画一个矩形 或者 任意四边形
        [CMD("rectangle")]
        static public void DrawRectangle(string A, string B, string C, string D, int flag)
        {

            myGDI.DrawRectangle(A, B, C, D);
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }

        // 测试通过 画一个正方形
        [CMD("square")]
        static public void DrawSquare(string A, string B, string C, string D, int flag)
        {

            myGDI.DrawSquare(A, B, C, D);
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }

        // 画一个平行四边形  测试通过
        [CMD("parasquare")]
        static public void DrawParaSquare(string A, string B, string C, string D, int flag)
        {

            myGDI.DrawParaSquare(A, B, C, D);
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
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
        static public void DrawLineAB(string A, string B, int flag)
        {
            //myGDI = new GDILib(Test.test.pictureBox1.CreateGraphics());
       
            myGDI.DrawLineAB(A, B);
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }


    


        //随机画一个点 测试已通过
        [CMD("freepoint")]
        static public void DrawFreePoint(string pointName, int flag)
        {
             
            //myGDI = new GDILib(Test.test.pictureBox1.CreateGraphics());
            myGDI.FreePoint(pointName);
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }

        }


        // 在上一个命令画出的线段中 画一个中点  也就是说 画布上已经存在一个线段 然后在这个线段上画中点
        //ABD 表示在AB上画中点
        //测试已通过 
        [CMD("midpoint")]
        static public void DrawMidPoint(string pointA, string pointB,string midPoint, int flag)
        {
            
            //myGDI = new GDILib(Test.test.pictureBox1.CreateGraphics());
            myGDI.DrawMidPoint(pointA, pointB, midPoint);
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }

        }


        // 画平行线 AB // CD  测试通过 
        [CMD("paraline")]
        static public void DrawParaLine(string A, string B, string C, string D, int flag)
        {

            //myGDI = new GDILib(Test.test.pictureBox1.CreateGraphics());
            myGDI.DrawParaLine(A, B, C, D);
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }

        }

        // 画AB垂直CD  AB是画布上已知直线 C 是已知直线外一点  垂足为D 这个顺序不能变
        // 测试通过
        [CMD("vertical")]
        static public void DrawVertical(string A, string B, string C, string D, int flag)
        {

            //myGDI = new GDILib(Test.test.pictureBox1.CreateGraphics());
            myGDI.DrawVertical(A, B, C, D);
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }

        }


        // 画已知三角形的外接圆 测试通过
        [CMD("outcircle")]
        static public void DrawOutCircle(string A, string B, string C, int flag)
        {

            //myGDI = new GDILib(Test.test.pictureBox1.CreateGraphics());
            myGDI.DrawOutCircle(A, B, C);
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }

        }


        // 画已知两条直线的交点 并且显示交点 测试通过
        [CMD("insertpoint")]
        static public void DrawInsertPoint(string A, string B, string C, string D, string F, int flag)
        {

            //myGDI = new GDILib(Test.test.pictureBox1.CreateGraphics());
            myGDI.DrawInsertPoint(A, B, C, D, F);
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }

        }



    }
}
