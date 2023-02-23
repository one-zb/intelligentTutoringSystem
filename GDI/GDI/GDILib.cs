using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;


namespace GDI
{
    /// <summary>
    /// 图形库：画图各种方法
    /// </summary>
    public class GDILib
    {
        //以下所有方法中A，B两点均为相同坐标两点
        //定义三角形顶点
        static Point X1;
        static Point X2;
        static Point X3;

        // 通过点的名字 来查找该点的坐标
        Dictionary<string, Point> nameToPoint = new Dictionary<string, Point>();

        public Point getX1()
        {
            return X1;
        }

        

        private Graphics g = null;
        public GDILib(Graphics gg)
        {
            g = gg;
            // 这里修改界面图片的底色
            g.Clear(System.Drawing.Color.Beige);
            //g.Clear(System.Drawing.Color.White);
        }

        /// <summary>
        /// 化学实验
        /// </summary>

        // 化学
        // 二氧化硫制备实验图 测试通过
        public void DrawSO2()
        {
            if(g != null)
            {
                List<string> gdiGraph = new List<string>() { "二氧化硫制备实验反应装置", "广口瓶", "玻璃管", "广口瓶", "玻璃管", "广口瓶", "玻璃管", "广口瓶", "玻璃管", "石棉网", "酒精灯", "分液漏斗", "锥形瓶", "铁架台" };
                //"二氧化硫制备实验反应装置""烧杯", "玻璃管", "石棉网", "酒精灯", "铁架台", "锥形瓶", "玻璃管", "广口瓶", "玻璃管", "广口瓶", "玻璃管", "分液漏斗", "反应瓶", "铁架台"
                //"广口瓶","玻璃管", "广口瓶", "玻璃管", "广口瓶", "玻璃管", "广口瓶", "玻璃管", "石棉网", "酒精灯", "分液漏斗", "锥形瓶", "铁架台" 
                GDIGraphGeneration gdiGeneration = new GDIGraphGeneration();
                gdiGeneration.Draw(g, gdiGraph);
            }
        }

        // 化学 画铁架台加烧瓶
        public void DrawIronSupportFlask()
        {
            if(g != null)
            {
                IronSupport_Flask iff2 = new IronSupport_Flask(g, 300, 300);
            }
        }

        // 化学 
        public void DrawBottle()
        {
            if (g != null)
            {
                Bottle bottle = new Bottle(g, 250, 250, 10);
            }
        }





        /// <summary>
        /// 数学 初中平面几何
        /// </summary>
        /// 


        // 在已知三角形ABC中任取一点 0 测试通过
        public void DrawFreePointInTrangle(string A, string B, string C, string O)
        {
            if(g != null)
            {
                Point pointA = nameToPoint[A];
                Point pointB = nameToPoint[B];
                Point pointC = nameToPoint[C];

                //三角形顶点
                Vector3 AA = new Vector3(pointA.X, pointA.Y, 0);
                Vector3 BB = new Vector3(pointB.X, pointB.Y, 0);
                Vector3 CC = new Vector3(pointC.Y, pointC.Y, 0);

                //边向量
                Vector3 e1 = BB - AA;
                Vector3 e2 = CC - AA;

                //随机数生成器
                Random random = new Random();

                //随机坐标
                float x = (float)random.NextDouble();
                float y = (float)random.NextDouble();

                //反射处理
                if (x + y > 1)
                {
                    x = 1 - x;
                    y = 1 - y;
                }

                //随机点
                Vector3 p = x * e1 + y * e2 + AA;
                Point freePoint = new Point((int)p.X,(int)p.Y);

                // 拿到要画的点
                string text = O;
                Font textFont = new Font("宋体", 12);
                SolidBrush textBrush = new SolidBrush(Color.Black);
                // 显示字母
                g.FillEllipse(Brushes.Black, freePoint.X, freePoint.Y, 4, 4);
                g.DrawString(text, textFont, textBrush, freePoint.X, freePoint.Y + 10);

            }
        }
        


        // 在已知线段上AB随机取一个点0 测试通过
        public void DrawFreePoint(string A, string B, string O)
        {
            if(g != null)
            {
                // 拿到这两点 AB坐标
                Point start = nameToPoint[A];
                Point end = nameToPoint[B];
                ;
                // 随机数 r
                Random random = new Random();
                double r = random.NextDouble();
                Point freePoint = new Point();
                freePoint.X = (int)(start.X + (int)(end.X - start.X) * r);
                freePoint.Y = (int)(start.Y + (int)(end.Y - start.Y) * r);

                // 拿到要画的点
                string text = O;
                Font textFont = new Font("宋体", 12);
                SolidBrush textBrush = new SolidBrush(Color.Black);
                // 显示字母
                g.FillEllipse(Brushes.Black, freePoint.X, freePoint.Y, 4, 4);
                g.DrawString(text, textFont, textBrush, freePoint.X, freePoint.Y + 10);
            }
        }

        // 画三角形的∠ABC的角平分线 B点是开始点，两边分别是BA BC 测试通过
        public void DrawAngleBisector(string A, string B, string C, string O)
        {
            if(g != null)
            {
                Point start = nameToPoint[B];
                Point end1 = nameToPoint[A];
                Point end2 = nameToPoint[C];

                // 创建画笔
                Pen pen = new Pen(Color.Black);
                // 计算两条边的夹角（弧度）
                double angle = Math.Atan2(end2.Y - start.Y, end2.X - start.X) - Math.Atan2(end1.Y - start.Y, end1.X - start.X);
                // 计算角平分线与Y轴正方向的夹角（弧度）
                double bisectorAngle = Math.Atan2(end1.Y - start.Y, end1.X - start.X) + angle / 2;
                // 计算角平分线上的一个点的坐标（假设Y坐标差为50）
                int dy = 50;
                int dx = (int)(dy / Math.Tan(bisectorAngle));

                Point bisectorPoint = new Point();
                if (start.Y < end1.Y && start.Y < end2.Y)
                {
                    // 如果这个起始点是三角形的顶点，也就是离X轴最近的点
                    bisectorPoint = new Point(start.X + dx, start.Y + dy);
                } else
                {
                    bisectorPoint = new Point(start.X - dx, start.Y - dy);
                }
               



                //// 定义两个向量a和b，分别是五边形第一条边和第二条边
                //double ax = end1.X - start.X;
                //double ay = end1.Y - start.Y;
                //double bx = end2.X - end1.X;
                //double by = end2.Y - end1.Y;

                //// 计算两个向量的夹角（弧度制）
                //double angle = Math.Acos((ax * bx + ay * by) / (Math.Sqrt(ax * ax + ay * ay) * Math.Sqrt(bx * bx + by * by)));

                //// 计算角平分线的方向（弧度制）
                //double bisectorAngle = angle / 2;

                //// 定义角平分线的长度（像素）
                //int length = 100;

                //// 计算角平分线的终点坐标
                //int x2 = (int)(end1.X - length * Math.Cos(bisectorAngle));
                //int y2 = (int)(end1.Y + length * Math.Sin(bisectorAngle));
                //Point bisectorPoint = new Point(x2, y2);

                //// 调用Graphics.DrawLine方法，画出角平分线
                //g.DrawLine(pen, end1, bisectorPoint);

                // 拿到要画的点
                string text = O;
                Font textFont = new Font("宋体", 12);
                SolidBrush textBrush = new SolidBrush(Color.Black);
                // 显示字母
                g.FillEllipse(Brushes.Black, bisectorPoint.X, bisectorPoint.Y, 4, 4);
                g.DrawString(text, textFont, textBrush, bisectorPoint.X, bisectorPoint.Y + 10);
                // 画出角平分线
                g.DrawLine(pen, start, bisectorPoint);


            }
        }

        // 在一个正方形ABCD或者矩形内任取一个点O  测试通过
        public void DrawFreePoint(string A, string B, string C, string D, string O)
        {
            if(g != null)
            {
                // 拿来装A B C D点坐标
                Point pointA = new Point();
                Point pointB = new Point();
                Point pointC = new Point();
                Point pointD = new Point();
                Point pointO = new Point();

                pointA = nameToPoint[A];
                pointB = nameToPoint[B];
                pointC = nameToPoint[C];

                Random rand = new Random();
                pointO.X = rand.Next(pointA.X, pointB.X);
                pointO.Y = rand.Next(pointA.Y, pointC.Y);

                // 拿到要画的点
                string text = O;
                Font textFont = new Font("宋体", 12);
                SolidBrush textBrush = new SolidBrush(Color.Black);
                // 显示字母
                g.FillEllipse(Brushes.Black, pointO.X, pointO.Y, 4, 4);
                g.DrawString(text, textFont, textBrush, pointO.X, pointO.Y + 10);


            }
        }
        // 画一个不规则的四边形 比如凸四边形ABCD 测试通过
        public void DrawTuSquare(string A, string B, string C, string D)
        {
            if (g != null)
            {
                // 构造四个点
                Point A1 = new Point(150, 110);
                Point A2 = new Point(450, 50);
                Point A3 = new Point(150, 300);
                Point A4 = new Point(450, 300);

                // 加入词典 把左边
                nameToPoint.Add(A, A1);
                nameToPoint.Add(B, A2);
                nameToPoint.Add(C, A3);
                nameToPoint.Add(D, A4);

                // 画点
                FreePoint(A);
                FreePoint(B);
                FreePoint(C);
                FreePoint(D);

                // 连接点
                DrawLineAB(A, B);
                DrawLineAB(B, D);
                DrawLineAB(D, C);
                DrawLineAB(C, A);
            }
        }

        //在已知的一个四边形中，在其中的AB一条边的外边画一个点C 测试通过
        public void DrawPointOutSide(string A, string B, string C)
        {
            // 要画的这个点是距离这条边的中点位置50个单位远
            if (g != null)
            {

                // 拿来装A B两点坐标
                Point pointA = new Point();
                Point pointB = new Point();
                // C是用来存放中点
                Point pointC = new Point();

                pointA = nameToPoint[A];
                pointB = nameToPoint[B];
                pointC.X = (pointA.X + pointB.X) / 2 + 50;
                pointC.Y = (pointA.Y + pointB.Y) / 2;
                // 要显示在画布上的中点 都要加入坐标字典中
                nameToPoint.Add(C, pointC);

                // 拿到要画交点
                string text = C;
                Font textFont = new Font("宋体", 12);
                SolidBrush textBrush = new SolidBrush(Color.Black);
                // 显示字母
                g.FillEllipse(Brushes.Black,pointC.X, pointC.Y, 4, 4);
                g.DrawString(text, textFont, textBrush, pointC.X, pointC.Y + 10);

            }
        }

        // 显示三角形ABC的垂心 O   测试通过
        public void DrawVerticalCenter(string A, string B, string C, string O)
        {
            if(g != null)
            {

                // 拿来装A B C两点坐标
                Point pointA = new Point();
                Point pointB = new Point();
                Point pointC = new Point();

                foreach (string key in nameToPoint.Keys)
                {
                    if (key.Equals(A))
                    {
                        // 如果用户要连接的线段两个点 AB 匹配到了 已经存在的点 就拿出来该点坐标
                        pointA = nameToPoint[key];

                    }

                    if (key.Equals(B))
                    {
                        pointB = nameToPoint[key];
                    }

                    if (key.Equals(C))
                    {
                        pointC = nameToPoint[key];
                    }
                }
                // 先做一条边的垂线
                // 先拿到一条边的垂足
                Point D = getFoot(pointB, pointC, pointA);
                Point E = getFoot(pointA, pointC, pointB);

                // 再计算两条垂线的交点 就是垂心
                Point center = GetIntersection(pointA, D, pointB, E);
                // 要显示在画布上的点 都要加入坐标字典中
                nameToPoint.Add(O, center);


                // 拿到要画交点
                string text = O;
                Font textFont = new Font("宋体", 12);
                SolidBrush textBrush = new SolidBrush(Color.Black);
                // 显示字母
                g.FillEllipse(Brushes.Black, center.X, center.Y, 4, 4);
                g.DrawString(text, textFont, textBrush, center.X, center.Y + 10);


            }
        }

        // 显示三角形ABC的外心 P  测试通过
        public void DrawOutCenter(string A, string B, string C, string P)
        {

            if(g != null)
            {

                // 拿来装A B C两点坐标
                Point pointA = new Point();
                Point pointB = new Point();
                Point pointC = new Point();

                foreach (string key in nameToPoint.Keys)
                {
                    if (key.Equals(A))
                    {
                        // 如果用户要连接的线段两个点 AB 匹配到了 已经存在的点 就拿出来该点坐标
                        pointA = nameToPoint[key];

                    }

                    if (key.Equals(B))
                    {
                        pointB = nameToPoint[key];
                    }

                    if (key.Equals(C))
                    {
                        pointC = nameToPoint[key];
                    }
                }
                double R = 0;
                Point center; 
                GetTriangleExcenterRadius(pointA, pointB, pointC, out R, out center);

                // 把外心加入坐标字典中
                nameToPoint.Add(P, center);

                // 拿到要画交点
                string text = P;
                Font textFont = new Font("宋体", 12);
                SolidBrush textBrush = new SolidBrush(Color.Black);
                // 显示字母
                g.FillEllipse(Brushes.Black, center.X, center.Y, 4, 4);
                g.DrawString(text, textFont, textBrush, center.X, center.Y + 10);


            }
        }

        // 显示 两条已知直线的交点 已知两条直线 AB是一条直线 CD是一条直线 测试通过
        public void DrawInsertPoint(string A, string B, string C, string D, string F)
        {
            if(g != null)
            {
                // 两条直线的 四个点坐标 
                Point pointA = new Point();
                Point pointB = new Point();
                Point pointC = new Point();
                Point pointD = new Point();
                foreach (string key in nameToPoint.Keys)
                {
                    if (key.Equals(A))
                    {
                        // 把画布已经存在三个点坐标拿出来
                        pointA = nameToPoint[key];

                    }

                    if (key.Equals(B))
                    {
                        pointB = nameToPoint[key];
                    }

                    if (key.Equals(C))
                    {
                        pointC = nameToPoint[key];
                    }

                    if (key.Equals(D))
                    {
                        pointD = nameToPoint[key];
                    }
                }

                // 计算交点
                Point point = GetIntersection(pointA, pointB, pointC, pointD);
                // 出现新的点 要加入到字典中
                nameToPoint.Add(F, point);

                // 拿到要画交点
                string text = F;
                Font textFont = new Font("宋体", 12);
                SolidBrush textBrush = new SolidBrush(Color.Black);
                // 显示字母
                g.FillEllipse(Brushes.Black, point.X, point.Y, 4, 4);
                g.DrawString(text, textFont, textBrush, point.X, point.Y + 10);

            }
        }


        // 计算已知两条直线的交点 测试通过
        private Point GetIntersection(Point lineFirstStar, Point lineFirstEnd, Point lineSecondStar, Point lineSecondEnd)
        {
            
            float a = 0, b = 0;
            int state = 0;
            if (lineFirstStar.X != lineFirstEnd.X)
            {
                a = (lineFirstEnd.Y - lineFirstStar.Y) / (lineFirstEnd.X - lineFirstStar.X);
                state |= 1;
            }
            if (lineSecondStar.X != lineSecondEnd.X)
            {
                b = (float)(lineSecondEnd.Y - lineSecondStar.Y) / (float)(lineSecondEnd.X - lineSecondStar.X);
                state |= 2;
            }
            switch (state)
            {
                case 0: //L1与L2都平行Y轴
                    {
                        if (lineFirstStar.X == lineSecondStar.X)
                        {
                            //throw new Exception("两条直线互相重合，且平行于Y轴，无法计算交点。");
                            return new Point(0, 0);
                        }
                        else
                        {
                            //throw new Exception("两条直线互相平行，且平行于Y轴，无法计算交点。");
                            return new Point(0, 0);
                        }
                    }
                case 1: //L1存在斜率, L2平行Y轴
                    {
                        float x = lineSecondStar.X;
                        float y = (lineFirstStar.X - x) * (-a) + lineFirstStar.Y;
                        return new Point((int)x, (int)y);
                    }
                case 2: //L1 平行Y轴，L2存在斜率
                    {
                        float x = lineFirstStar.X;
                        //网上有相似代码的，这一处是错误的。你可以对比case 1 的逻辑 进行分析
                        //源code:lineSecondStar * x + lineSecondStar * lineSecondStar.X + p3.Y;
                        float y = (lineSecondStar.X - x) * (-b) + lineSecondStar.Y;
                        return new Point((int)x, (int)y);
                    }
                case 3: //L1，L2都存在斜率
                    {
                        if (a == b)
                        {
                            // throw new Exception("两条直线平行或重合，无法计算交点。");
                            return new Point(0, 0);
                        }
                        float x = (a * lineFirstStar.X - b * lineSecondStar.X - lineFirstStar.Y + lineSecondStar.Y) / (a - b);
                        float y = a * x - a * lineFirstStar.X + lineFirstStar.Y;
                        return new Point((int)x, (int)y);
                    }
            }
            // throw new Exception("不可能发生的情况");
            return new Point(0, 0);
        }



        // 构造一个已知三角形的外接圆 测试通过
        public void DrawOutCircle(string A, string B, string C)
        {
            if(g != null)
            {
                // 拿来装三角形三个点
                Point pointA = new Point();
                Point pointB = new Point();
                Point pointC = new Point();

                // 外心圆 圆心坐标
                Point center = new Point();
                // 外心圆半径
                double R = 0;

                foreach (string key in nameToPoint.Keys)
                {
                    if (key.Equals(A))
                    {
                        // 把画布已经存在三个点坐标拿出来
                        pointA = nameToPoint[key];

                    }

                    if (key.Equals(B))
                    {
                        pointB = nameToPoint[key];
                    }

                    if (key.Equals(C))
                    {
                        pointC = nameToPoint[key];
                    }
                }

                // 调用获取圆心和半径的方法
                GetTriangleExcenterRadius(pointA, pointB, pointC, out R, out center);

                //  g.DrawEllipse(p, 50, 50, 200, 200);
                //这里50,50指椭圆的边框的左上角的 X ，Y坐标，200,200指椭圆的宽和高。
                Pen pen = new Pen(Color.Black);
                g.DrawEllipse(pen, (float)(center.X - R), (float)(center.Y - R), (float)(2 * R), (float)(2 * R));


            }
        }

        // 已知一个三角形求外心 测试通过
        private void GetTriangleExcenterRadius(Point A, Point B, Point C, out double R, out Point center)
        {
            //same point
            //if (A == B && A == C)
            //{
            //    R = 0;
            //    center = A;
            //    return;
            //}
            double x1 = A.X, x2 = B.X, x3 = C.X, y1 = A.Y, y2 = B.Y, y3 = C.Y;
            double C1 = Math.Pow(x1, 2) + Math.Pow(y1, 2) - Math.Pow(x2, 2) - Math.Pow(y2, 2);
            double C2 = Math.Pow(x2, 2) + Math.Pow(y2, 2) - Math.Pow(x3, 2) - Math.Pow(y3, 2);
            double centery = (C1 * (x2 - x3) - C2 * (x1 - x2)) / (2 * (y1 - y2) * (x2 - x3) - 2 * (y2 - y3) * (x1 - x2));
            double centerx = (C1 - 2 * centery * (y1 - y2)) / (2 * (x1 - x2));
            center = new Point((int)centerx, (int)centery);
            R = GetDistance(A, center);
        }
        // 外心圆半径 测试通过
        private double GetDistance(Point A, Point B)
        {
            return Math.Sqrt(Math.Pow((A.X - B.X), 2) + Math.Pow((A.Y - B.Y), 2));
        }


        // 直接构造任意一个三角形  这里定义成一个等边三角形 示意图 
        // 只要识别是三角形 就直接给用户呈现一个等边三角形
        public void DrwaTrangle(string A, string B, string C)
        {
            if(g != null)
            {
                Point A1 = new Point(370, 80);
                Point A2 = new Point(200, 340);
                Point A3 = new Point(450, 340);
                nameToPoint.Add(A, A1);
                nameToPoint.Add(B, A2);
                nameToPoint.Add(C, A3);
                FreePoint(A);
                FreePoint(B);
                FreePoint(C);
                DrawLineAB(A, B);
                DrawLineAB(B, C);
                DrawLineAB(A, C);


                
            }
        }


        // 画一个矩形 可以代表任意四边形 测试通过
        public void DrawRectangle(string A, string B, string C, string D)
        {
            if(g != null)
            {
                // 构造四个点
                Point A1 = new Point(150, 110);
                Point A2 = new Point(450, 110);
                Point A3 = new Point(150, 300);
                Point A4 = new Point(450, 300);

                // 加入词典 把左边
                nameToPoint.Add(A, A1);
                nameToPoint.Add(B, A2);
                nameToPoint.Add(C, A3);
                nameToPoint.Add(D, A4);

                // 画点
                FreePoint(A);
                FreePoint(B);
                FreePoint(C);
                FreePoint(D);

                // 连接点
                DrawLineAB(A, B);
                DrawLineAB(B, D);
                DrawLineAB(D, C);
                DrawLineAB(C, A);
            }
        }

        // 画一个正方形 测试通过
        public void DrawSquare(string A, string B, string C, string D)
        {
            if (g != null)
            {
                // 构造四个点
                Point A1 = new Point(150, 90);
                Point A2 = new Point(450, 90);
                Point A3 = new Point(150, 390);
                Point A4 = new Point(450, 390);

                // 加入词典 把左边
                nameToPoint.Add(A, A1);
                nameToPoint.Add(B, A2);
                nameToPoint.Add(C, A3);
                nameToPoint.Add(D, A4);

                // 画点
                FreePoint(A);
                FreePoint(B);
                FreePoint(C);
                FreePoint(D);

                // 连接点
                DrawLineAB(A, B);
                DrawLineAB(B, D);
                DrawLineAB(D, C);
                DrawLineAB(C, A);
            }
        }


        // 画一个平行四边形 测试通过
        public void DrawParaSquare(string A, string B, string C, string D)
        {
            if (g != null)
            {
                // 构造四个点
                Point A1 = new Point(190, 90);
                Point A2 = new Point(490, 90);
                Point A3 = new Point(150, 300);
                Point A4 = new Point(450, 300);

                // 加入词典 把左边
                nameToPoint.Add(A, A1);
                nameToPoint.Add(B, A2);
                nameToPoint.Add(C, A3);
                nameToPoint.Add(D, A4);

                // 画点
                FreePoint(A);
                FreePoint(B);
                FreePoint(C);
                FreePoint(D);

                // 连接点
                DrawLineAB(A, B);
                DrawLineAB(B, D);
                DrawLineAB(D, C);
                DrawLineAB(C, A);
            }
        }



        // 随机画一个点 已测试通过
        // 并且在点旁边 标注了字母
        // 如果画布已经存在点，那么就会用画布上已经存在的点坐标
        // 如果没有 那么就随机构造点
        public void FreePoint(string pointName)
        {
            if(g != null)
            {

                // 拿到要画的名字 A
                string text = pointName;
                Font textFont = new Font("宋体", 12);
                SolidBrush textBrush = new SolidBrush(Color.Black);

                // 首先判断 画布上 有没有存在点 
                if (nameToPoint.Count == 0)
                {
                    // 随机一个点
                    Random rand = new Random();
                    // 给定区间的的任意点
                    Point randPoint = new Point(rand.Next(Test.test.pictureBox1.Width), rand.Next(Test.test.pictureBox1.Height));
                    // 把该点 加入词典 注意每次得到一个新点 要记住加到词典里
                    nameToPoint.Add(text, randPoint);
                    // 画一个点 参数为点的坐标 加点的宽度 和 高度
                    g.FillEllipse(Brushes.Black, randPoint.X, randPoint.Y, 4, 4);
                    // 显示字母
                    g.DrawString(text, textFont, textBrush, randPoint.X, randPoint.Y + 10);
                } else
                {

                    // 如果已经有点了 但是只有字典里有坐标 还没有显示字母和点在画布上 那么就把画布上点 赋给这个点
                    if (nameToPoint.ContainsKey(pointName))
                    {
                        foreach (string key in nameToPoint.Keys)
                        {
                            if (key.Equals(pointName))
                            {
                                // 在几何里查找点的坐标 比如要画A点 那么就在A点 取出该点坐标
                                Point curPoint = nameToPoint[key];
                                g.FillEllipse(Brushes.Black, curPoint.X, curPoint.Y, 4, 4);
                                g.DrawString(text, textFont, textBrush, curPoint.X - 10, curPoint.Y + 5);
                                break;
                            }
                        }
                    } else
                    {
                        // 如果这个点不在字典里 也就是说这个要画点是个新的点 那么就随机给个点
                        // 这个时候属于 画布已经有一个点了  但是还要画一个新点
                        // 随机一个点
                        Random rand = new Random();
                        Point randPoint = new Point(rand.Next(Test.test.pictureBox1.Width), rand.Next(Test.test.pictureBox1.Height));
                        // 把该点 加入词典 注意每次得到一个新点 要记住加到词典里
                        nameToPoint.Add(text, randPoint);
                        // 画一个点 参数为点的坐标 加点的宽度 和 高度
                        g.FillEllipse(Brushes.Black, randPoint.X, randPoint.Y, 4, 4);
                        // 显示字母
                        g.DrawString(text, textFont, textBrush, randPoint.X, randPoint.Y + 10);
                    }

                    

                }

               

 
                
              
                
            }
        }




        // 画线段 传进来两个点 AB 并且把这个两个点显示出来 已测试通过
        // 前提是画布上已经画好了两个点 AB 
        public void DrawLineAB(string A, string B)
        {
            if (g != null)
            {

                Point pointA = new Point();
                Point pointB = new Point();
                Pen pen = new Pen(Color.Black);

                // 首先判断用户传进来的点的名字 然后拿到字典里 名字对应的坐标 然后再连接起来
                foreach (string key in nameToPoint.Keys)
                {
                    if(key.Equals(A))
                    {
                        // 如果用户要连接的线段两个点 AB 匹配到了 已经存在的点 就拿出来该点坐标
                        pointA = nameToPoint[key];

                    }
                    
                    if(key.Equals(B))
                    {
                        pointB = nameToPoint[key];
                    }
                }
                
                //X1 = new Point(100, 50);
                //X2 = new Point(200, 50);

                g.DrawLine(pen, pointA, pointB);
                String text1 = A;
                String text2 = B;
                Font textFont1 = new Font("宋体", 12);
                Font textFont2 = new Font("宋体", 12);
                SolidBrush textBrush = new SolidBrush(Color.Black);
                
                // 因为该方法 是建立在画布上已存在的点 才来画线段
                // 那么因为在画点的方法里，已经标注了点的名字  所以这里可以不显示
                
                //g.DrawString(text1, textFont1, textBrush, pointA.X - 10, pointA.Y - 15);
                //g.DrawString(text2, textFont2, textBrush, pointB.X - 10, pointB.Y);
            }
        }


        // 测试通过
        // 画平行线 AB // CD 这个点是自己定义好的
        public void DrawParaLine(string A, string B, string C, string D)
        {
            if(g != null)
            {
                // 构造四个点
                Point A1 = new Point(190, 90);
                Point A2 = new Point(490, 90);
                Point A3 = new Point(150, 300);
                Point A4 = new Point(450, 300);


                // 加入词典 把左边
                nameToPoint.Add(A, A1);
                nameToPoint.Add(B, A2);
                nameToPoint.Add(C, A3);
                nameToPoint.Add(D, A4);


                // 画点
                FreePoint(A);
                FreePoint(B);
                FreePoint(C);
                FreePoint(D);

                // 连接边
                DrawLineAB(A, B);
                DrawLineAB(D, C);
            }
        }

        

        // 测试通过
        // 画垂线 AB垂直CD  即在AB上做一条垂直线 C 是直线外一点
        // 前提是一直一条直线和直线外一点 做已知直线的垂线
        public void DrawVertical(string A, string B, string C, string D)
        {
            if(g != null)
            {

                // 拿来装A B C两点坐标
                Point pointA = new Point();
                Point pointB = new Point();
                Point pointC = new Point();
                // 首先判断用户传进来的点的名字 然后拿到字典里 名字对应的坐标 然后再连接起来
                // 这一步是 是取到 AB两点坐标 我们是要画AB垂直CD 
                foreach (string key in nameToPoint.Keys)
                {
                    if (key.Equals(A))
                    {
                        // 如果用户要连接的线段两个点 AB 匹配到了 已经存在的点 就拿出来该点坐标
                        pointA = nameToPoint[key];

                    }

                    if (key.Equals(B))
                    {
                        pointB = nameToPoint[key];
                    }

                    if (key.Equals(C))
                    {
                        pointC = nameToPoint[key];
                    }
                }

                // 调用求垂足的坐标 D
                Point pointD = new Point();
                pointD = getFoot(pointA, pointB, pointC);

                // 画出垂足点并且显示字母
                // 拿到要画的名字 D
                string text = D;
                Font textFont = new Font("宋体", 12);
                SolidBrush textBrush = new SolidBrush(Color.Black);
                // 参数为点的坐标 加点的宽度 和 高度
                g.FillEllipse(Brushes.Black, pointD.X, pointD.Y, 4, 4);
                // 显示字母
                g.DrawString(text, textFont, textBrush, pointD.X, pointD.Y + 10);



                //画布上新产生的点 要记得加入字典
                nameToPoint.Add(D, pointD);
                // 再连接垂线
                DrawLineAB(C, D);

               




            }
        }

        // 测试通过
        // 求点到线段垂足  P1  P2是已知线段两端点 p3是线段外一点
        private Point getFoot(Point p1, Point p2, Point p3)
        {
            Point foot = new Point();

            float dx = p1.X - p2.X;
            float dy = p1.Y - p2.Y;

            float u = (p3.X - p1.X) * dx + (p3.Y - p1.Y) * dy;
            u /= dx * dx + dy * dy;

            foot.X = (int)(p1.X + u * dx);
            foot.Y = (int)(p1.Y + u * dy);

            float d = Math.Abs((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.X - p2.Y));
            float d1 = Math.Abs((p1.X - foot.X) * (p1.X - foot.X) + (p1.Y - foot.Y) * (p1.X - foot.Y));
            float d2 = Math.Abs((p2.X - foot.X) * (p2.X - foot.X) + (p2.Y - foot.Y) * (p2.X - foot.Y));

            //if (d1 > d || d2 > d)
            //{
            //    if (d1 > d2) return p2;
            //    else return p1;
            //}

            return foot;
        }




        //画已知AB线段的中点 E点 并且显示E字母 测试通过
        public void DrawMidPoint(string pointA, string pointB, string midPoint)
        {
            if (g != null)
            {
                //定义两点
                //X1 = new Point(100, 50);
                //X2 = new Point(200, 50);

                Point point1 = new Point();
                Point point2 = new Point();

                foreach (string key in nameToPoint.Keys)
                {
                 
                    
                        // 例如说 AB中点D  经过处理来到这里 就会 ABD 
                        // 那么就先匹配 AB 这两个在画布上的坐标拿出来 然后再画中点D
                        if(key.Equals(pointA))
                        {
                            point1 = nameToPoint[key];
                        }

                        if(key.Equals(pointB))
                        {
                            point2 = nameToPoint[key];
                        }
                    
                }

                //根据两点坐标计算中点E坐标
                Point E = new Point((point1.X + point2.X) / 2, (point1.Y + point2.Y) / 2);
                // 也要把这个点加入字典中 表示画布上已经画出来了
                nameToPoint.Add(midPoint, E);


                //画中点E
                //g.FillEllipse(Brushes.Black, E.X, E.Y, 4, 4);

                //显示中点字母
                string text = midPoint;
                Font textFont = new Font("宋体", 12);
                SolidBrush textBrush = new SolidBrush(Color.Black);
                g.DrawString(text, textFont, textBrush, E.X, E.Y - 20);

            }
        }












        //废弃
        //画线段AC，AC线段长度等于已知线段AB
        public void DrawEqualLineAC()
        {
            if (g != null)
            {
                //已知A,B两点坐标
                X1 = new Point(100, 50);
                X2 = new Point(200, 50);

                //计算AB线段长度
                double dx = X1.X - X2.X;
                double dy = X1.Y - X2.Y;
                double distance = Math.Sqrt(dx * dx + dy * dy);

                double cx = (Math.Sqrt(5 / 4 * (distance * distance - 100 * 100 - 50 * 50) + 100 * 100)) + 100;
                double cy = cx / 2;


                //C点坐标
                Point C = new Point((int)cx, (int)cy);


                //连接AC
                Pen pen = new Pen(Color.Black);
                g.DrawLine(pen, X1, C);


                //给线段AC端点显示字母 
                String text2 = "C";
                Font textFont1 = new Font("宋体", 12);
                Font textFont2 = new Font("宋体", 12);
                SolidBrush textBrush = new SolidBrush(Color.Black);
                g.DrawString(text2, textFont2, textBrush, C.X, C.Y);
            }
        }


        // 废弃
        //画线段AC的中点 F点 并且显示F字母
        public void DrawMidPointF()
        {
            if (g != null)
            {
                //已知A,B两点坐标
                X1 = new Point(100, 50);
                X2 = new Point(200, 50);

                //计算AB线段长度
                double dx = X1.X - X2.X;
                double dy = X1.Y - X2.Y;
                double distance = Math.Sqrt(dx * dx + dy * dy);

                double cx = (Math.Sqrt(5 / 4 * (distance * distance - 100 * 100 - 50 * 50) + 100 * 100)) + 100;
                double cy = cx / 2;


                //C点坐标
                Point C = new Point((int)cx, (int)cy);



                //F点坐标
                Point F = new Point((X1.X + C.X) / 2, (X1.Y + C.Y) / 2);

                //画中点
                Pen pen = new Pen(Color.Black);
                //g.FillEllipse(Brushes.Black, F.X, F.Y, 4, 4);



                //给线段AC端点显示字母 
                String text = "F";
                Font textFont = new Font("宋体", 12);
                SolidBrush textBrush = new SolidBrush(Color.Black);
                g.DrawString(text, textFont, textBrush, F.X, F.Y + 10);
            }
        }

        //废弃
        //连接线段CE 
        public void DrawLineCE()
        {
           if(g != null)
            {
                //拿到C点坐标
                //已知A,B两点坐标
                X1 = new Point(100, 50);
                X2 = new Point(200, 50);

                //计算AB线段长度
                double dx = X1.X - X2.X;
                double dy = X1.Y - X2.Y;
                double distance = Math.Sqrt(dx * dx + dy * dy);

                double cx = (Math.Sqrt(5 / 4 * (distance * distance - 100 * 100 - 50 * 50) + 100 * 100)) + 100;
                double cy = cx / 2;


                //C点坐标
                Point C = new Point((int)cx, (int)cy);

                //拿到E点坐标
                //根据两点坐标计算中点E坐标
                Point E = new Point((X1.X + X2.X) / 2, (X1.Y + X2.Y) / 2);

                //画线段CE
                Pen pen = new Pen(Color.Black);
                g.DrawLine(pen, C.X, C.Y, E.X, E.Y);
            }

        }

        //废弃
        //连接线段BF
        public void DrawLineBF()
        {
            if (g != null)
            {
      

                //拿到F点坐标
                //已知A,B两点坐标
                X1 = new Point(100, 50);
                X2 = new Point(200, 50);

                //计算AB线段长度
                double dx = X1.X - X2.X;
                double dy = X1.Y - X2.Y;
                double distance = Math.Sqrt(dx * dx + dy * dy);

                double cx = (Math.Sqrt(5 / 4 * (distance * distance - 100 * 100 - 50 * 50) + 100 * 100)) + 100;
                double cy = cx / 2;


                //C点坐标
                Point C = new Point((int)cx, (int)cy);

                //F点坐标
                Point F = new Point((X1.X + C.X) / 2, (X1.Y + C.Y) / 2);



                //画线段CE
                Pen pen = new Pen(Color.Black);
                g.DrawLine(pen, X2.X, X2.Y, F.X, F.Y);
            }

        }


        //废弃
        //画AB两点，两点坐标已定义
        public void DrawPoint()
        {
            if (g != null)
            {

                g.FillEllipse(Brushes.Black, 100, 50, 4, 4);
                g.FillEllipse(Brushes.Black, 200, 50, 4, 4);
            }
        }

    }
}
