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

        // 化学仪器类


        // 铁架台
        [CMD("铁架台")]
        static public void DrawIronSupport(int flag)
        {


            myGDI.DrawIronSupport();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }

        // 烧杯
        [CMD("烧杯")]
        static public void DrawBeaker(int flag)
        {


            myGDI.DrawBeaker();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }

        // 广口瓶
        [CMD("广口瓶")]
        static public void DrawBottle(int flag)
        {


            myGDI.DrawBottle();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }

        }

        // 烧瓶
        [CMD("烧瓶")]
        static public void DrawFlask(int flag)
        {


            myGDI.DrawFlask();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }

        // 试管
        [CMD("试管")]
        static public void DrawTestTube(int flag)
        {


            myGDI.DrawTestTube();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }

        // 酒精灯 测试通过
        [CMD("alcoholamp", "酒精灯")]
        static public void DrawAlcoholLamp(int flag)
        {


            myGDI.DrawAlcoholLamp();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }

        // 漏斗
        [CMD("漏斗")]
        static public void DrawFunnel(int flag)
        {


            myGDI.DrawFunnel();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }


        // U型管
        [CMD("u型管")]
        static public void DrawUTube(int flag)
        {


            myGDI.DrawUTube();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }

        // 水槽
        [CMD("水槽")]
        static public void DrawSink(int flag)
        {


            myGDI.DrawSink();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }

        // 表面皿
        [CMD("表面皿")]
        static public void DrawWatchGlass(int flag)
        {


            myGDI.DrawWatchGlass();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }

        // 量筒 
        [CMD("量筒")]
        static public void DrawVolumetricCylinder(int flag)
        {
            myGDI.DrawVolumetricCylinder();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }

        // 玻璃棒
        [CMD("玻璃棒")]
        static public void DrawGlassRod(int flag)
        {
            myGDI.DrawGlassRod();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }

        // 细口瓶
        [CMD("细口瓶")]
        static public void DrawNarrowNeckedBottle(int flag)
        {
            myGDI.DrawNarrowNeckedBottle();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }

        // 燃烧匙
        [CMD("燃烧匙")]
        static public void DrawCombustionSpoon(int flag)
        {
            myGDI.DrawCombustionSpoon();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }


        // 胶头滴管
        [CMD("胶头滴管")]
        static public void DrawDropper(int flag)
        {
            myGDI.DrawDropper();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }

        // 三口烧瓶
        [CMD("三口烧瓶")]
        static public void DrawThreeNeckedFlask(int flag)
        {
            myGDI.DrawThreeNeckedFlask ();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }

        // 玻璃管
        [CMD("玻璃管")]
        static public void DrawGlassTube(int flag)
        {
            myGDI.DrawGlassTube();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }

        // 石棉网
        [CMD("石棉网")]
        static public void DrawAsbestosNet(int flag)
        {
            myGDI.DrawAsbestosNet();
            if (flag == 1)
            { 
                bmp.Save(imagePath);
            }
        }

        // 化学类 二氧化硫制备实验图 测试通过
        [CMD("so", "二氧化硫制备")]
        static public void DrawCreateSO2Experiment(int flag)
        {

            myGDI.DrawSO2();
            if(flag == 1)
            {
                // flag 为1表示这是最后一个命令，绘制的图形可以保存为图片， 因为界面显示图形是读取图片文件。
                bmp.Save(imagePath);
            }
            
        }


        // 化学类 铁架台反应瓶 测试通过
        [CMD("ironsupportflask", "铁架台反应瓶")]
        static public void DrawIronSupportFlask(int flag)
        {

            myGDI.DrawIronSupportFlask();
        }

       


       

       


        
        // 物理电学部分

        // 电流表
        [CMD("电流表")]
        static public void DrawAmmeter(int flag)
        {
            myGDI.DrawAmmeter();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }

        // 电压表
        [CMD("电压表")]
        static public void DrawVoltmeter(int flag)
        {
            myGDI.DrawVoltmeter();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }

        // 电动机
        [CMD("电动机")]
        static public void DrawMotor(int flag)
        {
            myGDI.DrawMotor();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }


        // 电阻
        [CMD("电阻")]
        static public void DrawResistance(int flag)
        {
            myGDI.DrawResistance();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }

        // 滑动变阻器
        [CMD("滑动变阻器")]
        static public void DrawSlidingRheostat(int flag)
        {
            myGDI.DrawResistance();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }

        // 灯泡
        [CMD("灯泡")]
        static public void DrawBulb(int flag)
        {
            myGDI.DrawBulb();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }

        // 开关
        [CMD("开关")]
        static public void DrawSwitch(int flag)
        {
            myGDI.DrawSwitch();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }

        // 电源
        [CMD("电源")]
        static public void DrawPower(int flag)
        {
            myGDI.DrawPower();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }

        // 电容
        [CMD("电容")]
        static public void DrawCapacitor(int flag)
        {
            myGDI.DrawCapacitor();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }

        // 铃铛
        [CMD("铃铛")]
        static public void DrawBell(int flag)
        {
            myGDI.DrawBell();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }

        // 二极管
        [CMD("二极管")]
        static public void DrawDiode(int flag)
        {
            myGDI.DrawDiode();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }

        // 三极管
        [CMD("三极管")]
        static public void DrawTriode(int flag)
        {
            myGDI.DrawTriode();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }


        // 接地极
        [CMD("地极")]
        static public void DrawGround(int flag)
        {
            myGDI.DrawGround();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }

        // 扬声器
        [CMD("扬声器")]
        static public void DrawSpeaker(int flag)
        {
            myGDI.DrawSpeaker();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }

        // 倒相放大器 非门
        [CMD("非门")]
        static public void DrawNotGate(int flag)
        {
            myGDI.DrawNotGate();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }

        // 与门
        [CMD("与门")]
        static public void DrawAndGate(int flag)
        {
            myGDI.DrawAndGate();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }

        // 或门
        [CMD("或门")]
        static public void DrawOrGate(int flag)
        {
            myGDI.DrawOrGate();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }

        // 与非门
        [CMD("与非门")]
        static public void DrawNAndGate(int flag)
        {
            myGDI.DrawNAndGate();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }

        // 或非门
        [CMD("或非门")]
        static public void DrawNOrGate(int flag)
        {
            myGDI.DrawNOrGate();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }

        // 异或门
        [CMD("异或门")]
        static public void DrawXOrGate(int flag)
        {
            myGDI.DrawXOrGate();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }

        // 运算放大器
        [CMD("运算放大器")]
        static public void DrawOperationalAmplifier(int flag)
        {
            myGDI.DrawOperationalAmplifier();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }

        // 信号源
        [CMD("信号源")]
        static public void DrawSignalSource(int flag)
        {
            myGDI.DrawSignalSource();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }

        // 石英晶体振荡器
        [CMD("石英晶体振荡器")]
        static public void DrawCrystalOscillator(int flag)
        {
            myGDI.DrawCrystalOscillator();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }

        // 保险丝
        [CMD("保险丝")]
        static public void DrawFuse(int flag)
        {
            myGDI.DrawFuse();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }

        // 报警器
        [CMD("报警器")]
        static public void DrawAlarmSystem(int flag)
        {
            myGDI.DrawAlarmSystem();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }

        // 电感器
        [CMD("电感器")]
        static public void DrawInductor(int flag)
        {
            myGDI.DrawInductor();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }

        // 电阻箱
        [CMD("电阻箱")]
        static public void ResistanceBox(int flag)
        {
            myGDI.DrawResistanceBox();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }


        // 电阻丝
        [CMD("电阻丝")]
        static public void ResistanceWire(int flag)
        {
            myGDI.DrawResistanceWire();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }



        // 物理力学部分


        // 游标卡尺 
        [CMD("游标卡尺")]
        static public void DrawVernierCaliper(int flag)
        {
            myGDI.DrawVernierCaliper();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }


        //  弹簧测力计
        [CMD("弹簧测力计")]
        static public void DrawSpringDynamometer(int flag)
        {
            myGDI.DrawSpringDynamometer();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }


        //  直尺
        [CMD("直尺")]
        static public void DrawRuler(int flag)
        {
            myGDI.DrawRuler();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }

        //  桌子
        [CMD("桌子")]
        static public void DrawDesk(int flag)
        {
            myGDI.DrawDesk();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }

        //  小球
        [CMD("小球")]
        static public void DrawBall(int flag)
        {
            myGDI.DrawBall();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }

        // 木块
        [CMD("木块")]
        static public void DrawBlock(int flag)
        {
            myGDI.DrawBlock();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }

        // 小车
        [CMD("小车")]
        static public void DrawCar(int flag)
        {
            myGDI.DrawCar();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }

        // 凹透镜
        [CMD("凹透镜")]
        static public void DrawConcavelens(int flag)
        {
            myGDI.DrawConcavelens();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }


        // 凸透镜
        [CMD("凸透镜")]
        static public void DrawConvexlens(int flag)
        {
            myGDI.DrawConvexlens();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }



        // 数学


        // 数学 立体几何 圆柱体 测试通过
        [CMD("cylinder", "圆柱")]
        static public void DrawyClinder(int flag)
        {


            myGDI.DrawyClinder();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }

        }

        // 棱锥 测试通过
        [CMD("pyramid", "棱锥")]
        static public void DrawPyramid(int flag)
        {
            myGDI.DrawPyramid();
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }




        // 延长AB到0点 测试通过
        [CMD("extendline", "延长")]
        static public void DrawExtendLine(string A, string B, string O, int flag)
        {

            myGDI.DrawExtendLine(A, B, O);
            if (flag == 1)
            {

                bmp.Save(imagePath);
            }
        }




        // 在已知三角形ABC中，以AB为边画一个正方形 E F顺序是在图中从左到右的 TODO
        [CMD("squarewithtrangle")]
        static public void DrawSquareWithline(string A, string B, string C, string E, string F, int flag)
        {

            myGDI.DrawSquareWithline(A, B, C, E, F);
            if (flag == 1)
            {

                bmp.Save(imagePath);
            }
        }



        // 在已知三角形ABC中任取一点O 测试通过
        [CMD("tranglefreepoint", "三角形内任取一点")]
        static public void DrawFreePointInTrangle(string A, string B, string C, string O, int flag)
        {

            myGDI.DrawFreePointInTrangle(A, B, C, O);
            if (flag == 1)
            {

                bmp.Save(imagePath);
            }
        }





        // 在已知线段AB上随机取一个点0 测试通过
        [CMD("linefreepoint", "线段上任取一点")]
        static public void DrawFreePoint(string A, string B, string O, int flag)
        {

            myGDI.DrawFreePoint(A, B, O);
            if (flag == 1)
            {

                bmp.Save(imagePath);
            }
        }




        // 画一个角ABC的角平分线BO 测试通过
        [CMD("anglebisector", "角平分线")]
        static public void DrawAngleBisector(string A, string B, string C, string O, int flag)
        {

            myGDI.DrawAngleBisector(A, B, C, O);
            if (flag == 1)
            {

                bmp.Save(imagePath);
            }
        }


        // 在一个正方形ABCD或者矩形内任取一个点O 测试通过
        [CMD("insidepoint","四边形里任取一点")]
        static public void DrawFreePoint(string A, string B, string C, string D, string O, int flag)
        {

            myGDI.DrawFreePoint(A, B, C, D, O);
            if (flag == 1)
            {

                bmp.Save(imagePath);
            }
        }



        // 画一个任意凸四边形ABCD 测试通过
        [CMD("tusquare", "凸四边形")]
        static public void DrawTuSquare(string A, string B, string C, string D, int flag)
        {

            myGDI.DrawTuSquare(A, B, C, D);
            if (flag == 1)
            {

                bmp.Save(imagePath);
            }
        }

        // 在已知的一个四边形中，在其中的AB一条边的外边画一个点C 测试通过
        [CMD("outsidepoint", "线外任取一点")]
        static public void DrawPointOutSide(string A, string B, string C, int flag)
        {

            myGDI.DrawPointOutSide(A, B, C);
            if (flag == 1)
            {

                bmp.Save(imagePath);
            }
        }

         

        // 显示三角形ABC外心 D 测试通过
        [CMD("outcenter","三角形外心")]
        static public void DrawOutCenter(string A, string B, string C, string D, int flag)
        {

            myGDI.DrawOutCenter(A, B, C, D);
            if (flag == 1)
            {
                
                bmp.Save(imagePath);
            }
        }

        // 显示三角形ABC垂心 D 测试通过
        [CMD("verticalcenter", "三角形垂心")]
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
        [CMD("trangle", "三角形")]
        static public void DrawTrangle (string A, string B, string C, int flag)
        {

            myGDI.DrwaTrangle(A, B, C);
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }

        // 测试通过 画一个矩形 或者 任意四边形
        [CMD("rectangle", "矩形")]
        static public void DrawRectangle(string A, string B, string C, string D, int flag)
        {

            myGDI.DrawRectangle(A, B, C, D);
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }

        // 测试通过 画一个正方形
        [CMD("square", "正方形")]
        static public void DrawSquare(string A, string B, string C, string D, int flag)
        {

            myGDI.DrawSquare(A, B, C, D);
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }
        }

        // 画一个平行四边形  测试通过
        [CMD("parasquare", "平行四边形")]
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
        [CMD("边AB", "边", "segment", "连接")]
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
        [CMD("freepoint", "随机点")]
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
        [CMD("midpoint", "中点")]
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
        [CMD("paraline", "平行")]
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
        [CMD("vertical", "垂直")]
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
        [CMD("outcircle", "外接圆")]
        static public void DrawOutCircle(string A, string B, string C, string O, int flag)
        {

            //myGDI = new GDILib(Test.test.pictureBox1.CreateGraphics());
            myGDI.DrawOutCircle(A, B, C, O);
            if (flag == 1)
            {
                bmp.Save(imagePath);
            }

        }


        // 画已知两条直线的交点 并且显示交点 测试通过
        [CMD("insertpoint", "相交")]
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
