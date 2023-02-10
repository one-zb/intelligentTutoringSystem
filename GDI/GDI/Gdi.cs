using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace GDI
{
    //基础图形
    public class Line : BasicGraphics
    {
        public PointF p1;//连接点1
        public PointF p2;//连接点2
        Pen p = new Pen(Brushes.Black);
        public Line(Graphics g, float x1, float y1, float x2, float y2, float angle = 0, bool isSolid = true) 
        {
            if (isSolid)
                p.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;//恢复实线
            else
                p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;//虚线
            this.x =(x1 + x2) / 2;
            this.y =(y1 + y2) / 2;
            p1 = new PointF(x1, y1);
            p2 = new PointF(x2, y2); 
            graphics = g;
            Rotate(angle);
            p1 = calcNewPoint(p1, this.centerPoint, angle);
            p2 = calcNewPoint(p2, this.centerPoint, angle);
            graphics.DrawLine(p, p1, p2);
        }
        public void LineWithArrow(string str1 = "", string str2 = "", string str3 = "") 
        {
            System.Drawing.Drawing2D.AdjustableArrowCap lineCap = new System.Drawing.Drawing2D.AdjustableArrowCap(6, 6, true);
            p.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;//定义线尾的样式为箭头
            p.CustomEndCap = lineCap;
            Str(graphics, str1, p1.X - 20, p1.Y - 10);
            Str(graphics, str2, p1.X + Math.Abs(p1.X - p2.X) / 2, p1.Y - 20);
            Str(graphics, str3, p1.X + Math.Abs(p1.X - p2.X), p1.Y - 10);
            graphics.DrawLine(p, p1, p2);
        }
    }
    public class Arc : BasicGraphics
    {
        public Arc(Graphics g , float x1, float y1, float width, float height, float startAngle, float endAngle)
        {
            this.x = x1+width/2;
            this.y = y1+height/2;
            
            SizeF size2 = new SizeF(width, height);
            PointF p = new PointF(x1, y1);
            RectangleF rec2 = new RectangleF(p, size2);
            graphics = g;
            graphics.DrawArc(pen, rec2, startAngle, endAngle);
        }
    }
    public class Triangle : BasicGraphics
    {
        //innerAngle内部角的度数可以设置直角或者任意角度，e为边的长度
        //（x1,y1）为第一个点位置，第二个点位置默认为（x1 + e * size / 10, y1）
        //angle为对整个图形旋转的角度
        public Triangle(Graphics g, float x1, float y1, float innerAngle = 90, float e = 40,float angle = 0)
        {
            this.x = x1;
            this.y = y1;
            graphics = g;
            Rotate(angle);
            PointF[] points = { new PointF(x1, y1), calcNewPoint(new PointF(x1 + e, y1), this.centerPoint, -innerAngle), new PointF(x1 + e, y1) };
            graphics.DrawPolygon(pen, points);
        }
        public Triangle(Graphics g, float x1, float y1, float x2, float y2, float x3, float y3,float angle = 0) 
        {
            this.x = x1;
            this.y = y1;
            graphics = g;
            Rotate(angle);
            PointF[] points = { new PointF(x1, y1), new PointF(x2, y2), new PointF(x3, y3) };
            graphics.DrawPolygon(pen, points);
        }
    }
    public class Axis : BasicGraphics 
    {
        float width;
        float height;
        float span;
        float scaleAxis;
        float startPoint=0,spanNum=1,grids=1;
        public Axis(Graphics g,float x1,float y1,int scale=20,float size=10) 
        {
            width = size * 20;
            height = size * 20;
            scaleAxis = scale;
            this.x = x1;
            this.y = y1;
            graphics = g;
            Arrow(graphics, x1, y1, x1 + width, y1);
            Arrow(graphics, x1, y1, x1, y1 - height);
            DrawMultLines(x1, y1,width*0.9f,height*0.9f,scale);
            DrawMultLines(x1, y1, width * 0.9f, height * 0.9f, scale,false);
          
        }
        //画方格线
        private void DrawMultLines( float x, float y, float width,float height,int scale,bool isVertical=true) 
        {
            span=0;
            int k = 0;
            if (isVertical)
            {
                while (k != scale)
                {
                    k += 1;
                    span +=  width / scale;
                    graphics.DrawLine(pen, x + span, y , x + span, y - height);
                }
            }
            else 
            {
                while (k != scale)
                {
                    k += 1;
                    span += height / scale;
                    graphics.DrawLine(pen, x , y - span, x +  width, y - span);
                }
            }
        }
        //刻度标注
        public void Scale(float startNums,float spanNums,int gridNums,string str,bool isVertical=false)
        {
            //startNums开始的刻度数,spanNums每个刻度相隔的数值，gridNums相隔多少个格子
            float xOffset = x;
            float yOffset = y;
            float offset = 0;
            startPoint=startNums;
            spanNum=spanNums;
            grids=gridNums;
            StringFormat strfmt = new StringFormat();
            strfmt.Alignment = StringAlignment.Center;
            if(isVertical)
                 Str(graphics, str, x+5, y - height);
            else
                Str(graphics, str, x + width, y);
            for (int i = 0; i <= scaleAxis; i++)
            {
                if (i % gridNums == 0)
                {
                    graphics.DrawString((startNums).ToString(), new Font("宋体", 12), Brushes.Black,
                        new PointF(xOffset + (isVertical ? -6 : offset), yOffset + (isVertical ? -offset-6 : 0)), strfmt);
                    startNums += spanNums;
                }
                offset += width*0.9f / scaleAxis;
            }
        }
        public void DrawPoint(float x1,float y1,int mode=0,float size=10) 
        {
            float offset, offsetX, offsetY;
            offset=width * 0.9f / scaleAxis;
            offsetX =x+offset * grids * ((x1 - startPoint) / spanNum)/2;
            offsetY =y-offset * grids * ((y1 - startPoint) / spanNum);
            Points point1 = new Points(graphics, offsetX, offsetY, mode, size);
        }
        public void DrawLine(float x1, float y1, float x2, float y2) 
        {
            float offset, X1, Y1, X2, Y2;
            offset = width * 0.9f / scaleAxis;
            X1 = x + offset * grids * ((x1 - startPoint) / spanNum) / 2;
            Y1 = y - offset * grids * ((y1 - startPoint) / spanNum);
            X2 = x + offset * grids * ((x2 - startPoint) / spanNum) / 2;
            Y2 = y - offset * grids * ((y2 - startPoint) / spanNum);
            graphics.DrawLine(pen, X1, Y1, X2, Y2);
        }
    }
    //数学图形
    public class Cube : BasicGraphics //立方体
    {
        float width, height, lenght;
        PointF A, B, C, D, E, F,G,H;
        public Cube(Graphics g, float x1, float y1,float w=0, float h=0,float l=0,float size = 10, float angle = 0) 
        {//x1,y1为立方体中心点，w,h,l分别对应立方体的增加多少的宽，高，长，size为整体尺寸倍数，angle为围绕中心点顺时针旋转的角度
            width = size * 5 + w;
            height = size * 5 + h;
            lenght = size * 5 + l;
            A = new PointF(x1 - width / 2, y1 - height / 2);
            B = new PointF(x1 + width / 2, y1 - height / 2);
            C = new PointF(x1 + width / 2, y1 + height / 2);
            D = new PointF(x1 - width / 2, y1 + height / 2);
            PointF[] pf1={A,B,C,D};//前面的四方形
            E = new PointF(x1 + width / 2+0.707f*lenght, y1 + height / 2-0.707f*lenght);
            F = new PointF(x1 + width / 2 + 0.707f * lenght, y1 + height / 2 - 0.707f * lenght-height);
            G = new PointF(x1 + width / 2 + 0.707f * lenght-width, y1 + height / 2 - 0.707f * lenght - height);
            H = new PointF(x1 + width / 2 + 0.707f * lenght - width, y1 + height / 2 - 0.707f * lenght);
            PointF[] pf2 = { A, G, F, E, C, B };
            this.x = x1;
            this.y = y1;
            graphics = g;
            Rotate(angle);
            graphics.DrawPolygon(pen, pf1);
            graphics.DrawPolygon(pen, pf2);
            graphics.DrawLine(pen, B, F);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            graphics.DrawLine(pen, G, H);
            graphics.DrawLine(pen, E, H);
            graphics.DrawLine(pen, D, H);
        }
    }
    public class Cylinder : BasicGraphics //圆柱体
    {
        float width, height;
        public Cylinder(Graphics g, float x1, float y1, float w = 0, float h = 0, float size = 10, float angle = 0) 
        {  //w,h为增加圆柱体的底的宽度的单位(在原来的基础上增加的量)/增加高度的量
            width = size*5+w;
            height = size*5+h;
            this.x = x1;
            this.y = y1;
            graphics = g;
            Rotate(angle);
            graphics.DrawEllipse(pen, x1, y1, width, width/2);
            graphics.DrawEllipse(pen, x1, y1+height, width, width / 2);
            graphics.DrawLine(pen, x1, y1 + width / 4, x1, y1 + height + width / 4);
            graphics.DrawLine(pen, x1+width, y1 + width / 4, x1+width, y1 + height + width / 4);
        }
    }
    public class Quadrilateral : BasicGraphics //四方形(平行四边形/梯形/倒梯形)
    {
         public Quadrilateral(Graphics g,float x1,float y1,float innerAngle=45,float size=10,float angle=0) 
        {
            this.x =x1;
            this.y =y1;
            float scale;
            scale = size * 5;
            graphics = g;
            Rotate(angle);
            PointF[] points = { new PointF(x1, y1), calcNewPoint(new PointF(x1 + scale, y1), this.centerPoint, -innerAngle), calcNewPoint(new PointF(x1, y1), new PointF(x1 +  scale, y1),180-innerAngle) , new PointF(x1 +scale, y1)};
            graphics.DrawPolygon(pen, points);
        }
        //根据四个点(依照给的点顺序依次连)来画
         public Quadrilateral(Graphics g, float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4, float angle = 0) 
         {
             this.x = x1;
             this.y = y1;
             graphics = g;
             Rotate(angle);
             PointF[] points = { 
                                   new PointF(x1, y1), 
                                   new PointF(x2, y2), 
                                   new PointF(x3, y3), 
                                   new PointF(x4, y4), 
                               };
             graphics.DrawPolygon(pen, points);
         }
    }
    public class Colne : BasicGraphics //圆锥
    {
        float width, height;
        public Colne(Graphics g, float x1, float y1, float w = 0, float h = 0, float size = 10, float angle = 0) 
        {  //w,h为增加圆柱体的底的宽度的单位(在原来的基础上增加的量)/增加高度的量
            width = size*5+w;
            height = size*5+h;
            this.x = x1;
            this.y = y1;
            graphics = g;
            Rotate(angle);
            graphics.DrawEllipse(pen, x1, y1, width, width/2);
            graphics.DrawLine(pen, x1, y1 + width / 4, x1 + width / 2, y1 - height);
            graphics.DrawLine(pen, x1+width, y1 + width / 4, x1 + width / 2, y1 - height);
        }
    }
    public class Circle : BasicGraphics //圆
    {
        public Circle(Graphics g, float x1, float y1,float radius=10)
        {
            this.x = x1 + radius;
            this.y = y1 + radius;
            graphics = g;
            graphics.DrawEllipse(pen, x1, y1, radius*2, radius * 2);
        }
    }
    public class Ellipse : BasicGraphics //椭圆
    {
        public Ellipse(Graphics g, float x1, float y1, float a,float b) 
        {
            this.x = x1 + a;
            this.y = y1 + b;
            graphics = g;
            graphics.DrawEllipse(pen, x1, y1, 2 * a, 2 * b);
        }
    }
    public class Points : BasicGraphics  //点
    {
        public Points(Graphics g, float x1, float y1,int mode=0,float size=10) 
        {
            float length = 7 * size / 10;
            this.x = x1;
            this.y = y1;
            graphics = g;
            switch (mode)
            {
                case 0: graphics.FillEllipse(Brushes.Black, x1 - length/2, y1 - length/2, length, length);//圆点
                    break;
                case 1: graphics.FillRectangle(Brushes.Black, x1 - length/2, y1 -length/2, length, length);//矩形点
                    break;
                case 2://三角形
                     PointF[] points = { 
                                   new PointF(x1, y1-0.67f* length), 
                                   new PointF(x1-0.5f*length, y1+0.33f*length), 
                                   new PointF(x1+0.5f*length, y1+0.33f*length)
                               };
                     graphics.FillPolygon(Brushes.Black, points);
                    break;
                case 3://六角形
                     PointF[] points1 = { 
                                   new PointF(x1, y1-0.67f* length), 
                                   new PointF(x1-0.5f*length, y1+0.33f*length), 
                                   new PointF(x1+0.5f*length, y1+0.33f*length)
                               };
                     PointF[] points2 = { 
                                   new PointF(x1, y1+0.67f* length), 
                                   new PointF(x1-0.5f*length, y1-0.33f*length), 
                                   new PointF(x1+0.5f*length, y1-0.33f*length)
                               };
                     graphics.FillPolygon(Brushes.Black, points1);
                     graphics.FillPolygon(Brushes.Black, points2);
                    break;
                default:
                    throw new IndexOutOfRangeException("mode参数为0到3");
                   
            }
        }
    }
    public class Curve : BasicGraphics  //曲线(正弦/余弦)
    {
        //length表示用多少单位像素代表 2pi
        public Curve(Graphics g, float x1, float y1,int mode=0,float size=10)
        {
            float length = 100*size/10;
            float floatPI = (float)Math.PI;//float类型的pi
            float scale=(0.5f*length)/floatPI;//单位化
            float pi = scale * floatPI;//转换后的pi
            this.x = x1;
            this.y = y1;
            graphics = g;
            PointF[] pointC={};
            switch (mode)
            {
                case 0://正弦曲线
                    PointF[] points ={
                              new PointF(x1,y1),
                              new PointF(x1+pi/4,y1-(float)Math.Sin(floatPI/4)*scale),
                              new PointF(x1+pi/2,y1-(float)Math.Sin(floatPI/2)*scale),
                              new PointF(x1+pi*3/4,y1-(float)Math.Sin(floatPI*3/4)*scale),
                              new PointF(x1+pi,y1),
                              new PointF(x1+pi*5/4,y1-(float)Math.Sin(floatPI*5/4)*scale),
                              new PointF(x1+pi*3/2,y1+(float)Math.Sin(floatPI/2)*scale),
                              new PointF(x1+pi*7/4,y1-(float)Math.Sin(floatPI*7/4)*scale),
                              new PointF(x1+pi*2,y1),
                          };
                    pointC = points;
                    break;
                case 1://余弦曲线
                    PointF[] points1 ={
                              new PointF(x1,y1-(float)Math.Cos(0)*scale),
                              new PointF(x1+pi/4,y1-(float)Math.Cos(floatPI/4)*scale),
                              new PointF(x1+pi/2,y1),
                              new PointF(x1+pi*3/4,y1-(float)Math.Cos(floatPI*3/4)*scale),
                              new PointF(x1+pi,y1-(float)Math.Cos(floatPI)*scale),
                              new PointF(x1+pi*5/4,y1-(float)Math.Cos(floatPI*5/4)*scale),
                              new PointF(x1+pi*3/2,y1),
                              new PointF(x1+pi*7/4,y1-(float)Math.Cos(floatPI*7/4)*scale),
                              new PointF(x1+pi*2,y1-(float)Math.Cos(floatPI*2)*scale),
                          };
                    pointC = points1;
                    break;
                default:
                    break;
            }
            graphics.DrawCurve(pen, pointC);
        }
    }
    public class Sphere : BasicGraphics  //球体
    {
        public Sphere(Graphics g,float x1,float y1,float radius=40)
        {
            this.x = x1 + radius;
            this.y = y1 + radius;
            graphics = g;
            graphics.DrawEllipse(pen, x1, y1, radius * 2, radius * 2);
            graphics.DrawArc(pen, x1, y1 + radius * 2 / 3, radius * 2, radius * 2 / 3, 0, 180);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            graphics.DrawArc(pen, x1, y1 + radius * 2 / 3, radius * 2, radius * 2 / 3, 180, 180);
        }
    }
    public class Pyramid : BasicGraphics //棱锥
    {
        float width, height;
        public Pyramid(Graphics g, float x1, float y1,int mode=0,  float w = 0, float h = 0,float size = 10, float angle = 0) 
        {
            width = size * 7 + w;
            height = size * 7 + h;
            this.x = x1;
            this.y = y1;
            graphics = g;
            Rotate(angle); 
            if (mode==0)//三棱锥
            {
                PointF[] points = { new PointF(x1, y1 - height * 2 / 3), new PointF(x1 - 0.577f * width, y1 + height / 3), new PointF(x1 + 0.577f * width, y1 + height / 3) };
                graphics.DrawPolygon(pen, points);
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                PointF c = new PointF(x1, y1);
                graphics.DrawLine(pen, c, points[0]);
                graphics.DrawLine(pen, c, points[1]);
                graphics.DrawLine(pen, c, points[2]);
            }
            else if (mode==1)//四棱锥
            {
                PointF[] points = { new PointF(x1, y1), 
                                    new PointF(x1 - 0.577f * width, y1 + height), 
                                    new PointF(x1 + 0.277f * width, y1 + height),
                                    new PointF(x1 + 0.677f * width, y1 + (height) -0.346f*width),
                                    new PointF(x1, y1),
                                    new PointF(x1 + 0.277f * width, y1 + height)
                                  };
                graphics.DrawPolygon(pen, points);
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                PointF c = new PointF(x1 - 0.177f * width, y1 + height - 0.346f * width);
                graphics.DrawLine(pen, c, points[0]);
                graphics.DrawLine(pen, c, points[1]);
                graphics.DrawLine(pen, c, points[3]);
            }
            else if (mode == 2)//五棱锥
            {
                PointF[] points = { new PointF(x1, y1), 
                                    new PointF(x1 - 0.577f * width, y1 + height), 
                                    new PointF(x1 - 0.077f * width, y1 + height*1.3f),
                                    new PointF(x1 + 0.577f * width, y1 + height),
                                    new PointF(x1, y1),
                                    new PointF(x1 - 0.077f * width, y1 + height*1.3f)
                                  };
                graphics.DrawPolygon(pen, points);
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                PointF c1 = new PointF(x1 - 0.177f * width, y1 + height * 0.7f);
                PointF c2 = new PointF(x1 + 0.177f * width, y1 + height * 0.7f);
                graphics.DrawLine(pen, c1, points[0]);
                graphics.DrawLine(pen, c1, c2);
                graphics.DrawLine(pen, c1, points[1]);
                graphics.DrawLine(pen, c2, points[0]);
                graphics.DrawLine(pen, c2, points[3]);
            }
            else 
            {
                throw new ArgumentNullException("mode参数只为0，1，2");
            }
        }
    }
    public class Angle : BasicGraphics   //角
    {
        float width;
        public Angle(Graphics g, float x1, float y1, float angle=90, float size = 10, float rotateAngle = 0) 
        {  //w,h为增加圆柱体的底的宽度的单位(在原来的基础上增加的量)/增加高度的量
            this.x = x1;
            this.y = y1;
            width = size * 10;
            graphics = g;
            Rotate(rotateAngle);
            graphics.DrawLine(pen, x1, y1, x1 + width, y1);
            var m = new System.Drawing.Drawing2D.Matrix();
            m.RotateAt(-angle, centerPoint);
            graphics.Transform = m;
            graphics.DrawLine(pen, x1, y1, x1 + width, y1);
            graphics.DrawArc(pen, x1 - width / 5, y1 - width / 5, width * 2 / 5, width * 2 / 5, 0, angle);
            m.RotateAt(angle/2, centerPoint);
            graphics.Transform = m;
            Str(g, angle.ToString() + "°", x1+width/4, y1-size,size*1.5f);
            Points p = new Points(g, x1, y1);
        }
    }
    public class Prism : BasicGraphics   //棱柱
    {
        float width, height;
        public Prism(Graphics g, float x1, float y1, int mode = 0, float w = 0, float h = 0, float size = 10, float angle = 0) 
         {
             width = size * 7 + w;
             height = size * 7 + h;
             this.x = x1;
             this.y = y1;
             graphics = g;
             Rotate(angle);
             if (mode == 0)//三棱柱
             {
                 PointF[] points = { new PointF(x1, y1), //起点为上三角形的三点中位置为中的点
                                     new PointF(x1 - 0.577f * width, y1 - height / 3), 
                                     new PointF(x1 + 0.4f * width, y1 - height / 3),
                                     new PointF(x1 , y1),
                                     new PointF(x1 , y1 + height),
                                     new PointF(x1 + 0.4f * width, y1 + height*2 / 3),
                                     new PointF(x1 + 0.4f * width, y1 - height / 3),
                                     new PointF(x1 - 0.577f * width, y1 - height / 3),
                                     new PointF(x1 - 0.577f * width, y1 + height*2 / 3),
                                     new PointF(x1 , y1 + height),
                                   };
                 graphics.DrawPolygon(pen, points);
                 pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                 graphics.DrawLine(pen, points[8], points[5]);
             }
             else if (mode == 1)//四棱柱
             {
                 PointF[] points = { new PointF(x1, y1), //起点为上四边形中左下角的顶点
                                     new PointF(x1 , y1 + height), 
                                     new PointF(x1 +  0.6f*width, y1+height),
                                     new PointF(x1 +  0.6f*width, y1),
                                     new PointF(x1 , y1),
                                     new PointF(x1 + 0.3f * width, y1 - width*0.477f),
                                     new PointF(x1 + 0.9f * width, y1 - width*0.477f),
                                     new PointF(x1 + 0.6f * width, y1),
                                     new PointF(x1 +  0.6f*width, y1+height),
                                     new PointF(x1 + 0.9f * width, y1 + height-width*0.477f),
                                     new PointF(x1 + 0.9f * width, y1 - width*0.477f),
                                     new PointF(x1 + 0.6f * width, y1)
                                   };
                 graphics.DrawPolygon(pen, points);
                 pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                 PointF c = new PointF(x1+0.3f*width, y1 + height-width*0.477f);
                 graphics.DrawLine(pen, c, points[1]);
                 graphics.DrawLine(pen, c, points[5]);
                 graphics.DrawLine(pen, c, points[9]);
             }
             else if (mode == 2)//五棱柱
             {
                 PointF[] points = { new PointF(x1, y1), //起点为面前四边形中左上角的顶点
                                     new PointF(x1 , y1 + height), 
                                     new PointF(x1 +  0.6f*width, y1+height),
                                     new PointF(x1 +  0.6f*width, y1),
                                     new PointF(x1 , y1),
                                     new PointF(x1 - 0.2f * width, y1 - width*0.4f),
                                     new PointF(x1 + 0.3f * width, y1 - width*0.8f),
                                     new PointF(x1 + 0.8f * width, y1-width*0.4f),
                                     new PointF(x1 +  0.6f*width, y1),
                                     new PointF(x1 +  0.6f*width, y1+height),
                                     new PointF(x1 +  0.8f*width, y1+height-width*0.4f),
                                     new PointF(x1 + 0.8f * width, y1-width*0.4f),
                                     new PointF(x1 +  0.6f*width, y1),
                                     new PointF(x1 , y1),
                                     new PointF(x1 , y1 + height), 
                                     new PointF(x1-0.2f*width , y1 + height-width*0.4f), 
                                     new PointF(x1 - 0.2f * width, y1 - width*0.4f)
                                   };
                 graphics.DrawPolygon(pen, points);
                 pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                 PointF c = new PointF(x1 + 0.3f * width, y1 - width * 0.8f+height);
                 graphics.DrawLine(pen, c, points[6]);
                 graphics.DrawLine(pen, c, points[10]);
                 graphics.DrawLine(pen, c, points[15]);
             }
             else
             {
                 throw new ArgumentNullException("mode参数只为0，1，2");
             }
         }
    }
    //物理力学
    public class Convexlens : PhysicalMechanics//凸透镜
    {
        private float width;
        private float height;
        public Convexlens(Graphics g,float x1, float y1,float size,float angle=0)
        {
            width = size;
            height = size * 6;
            this.x = x1 + width / 2; 
            this.y = y1+height/2;
            SizeF s = new SizeF(width, height);
            PointF p = new PointF(x1, y1);
            RectangleF rec2 = new RectangleF(p, s);
            RectangleF rec = new RectangleF(x1 + width / 2, y1 + height / 2, 3, 3);
            graphics = g;
            Rotate(angle);
            graphics.DrawEllipse(pen, rec2);
            graphics.FillEllipse(Brushes.Black, rec);//光心
        }
    }
    public class Concavelens : PhysicalMechanics //凹透镜
    {
        private Line l1;
        private Line l2;
        private Arc a1;
        private Arc a2;
        private float width;
        private float height;
        public Concavelens(Graphics g ,float x1 , float y1,float size,float angle=0)
        {
            this.graphics = g;
            width=size;
            height=size*6;
            this.x = x1 + width / 2;
            this.y = y1 + height / 2;
            Rotate(angle);
            l1 = new Line(g, x1, y1, x1 + width, y1);
            l2 = new Line(g, x1, y1 + height, x1 + width, y1 + height);
            a1 = new Arc(g, x1 - 3*size/10, y1, width / 2, height, -90, 180);
            a2 = new Arc(g, x1 + 4*size/5, y1, width / 2, height, 90, 180);
            RectangleF rec = new RectangleF(x1 + width / 2, y1 + height / 2, 3, 3);
            graphics.FillEllipse(Brushes.Black, rec);
        }
    }
    public class Car : PhysicalMechanics //小车
    {
        private float width;
        private float height;
        public Car(Graphics g,float x1 ,float y1,float size,float angle=0)
        {
            width = size*3;
            height = size*3/2;
            this.x = x1+width/2;
            this.y = y1+height/2;
            SizeF s = new SizeF(width / 5, width / 5);
            RectangleF rec2 = new RectangleF(new PointF(x1, y1 + height), s);
            RectangleF rec3 = new RectangleF(new PointF(x1 + width - width / 5, y1 + height), s);
            graphics = g;
            Rotate(angle);
            graphics.DrawRectangle(pen, x1, y1, width, height);
            graphics.DrawEllipse(pen, rec2);
            graphics.DrawEllipse(pen, rec3);
        }
    } 
    public class Block : PhysicalMechanics //木块
    {
        private float width;
        private float height;
        public Block(Graphics g ,float x1, float y1,float size,float angle=0)
        {
            width = size*3;
            height =size*3/2;
            this.x = x1 + width / 2;
            this.y = y1 + height / 2;
            graphics = g;
            Rotate(angle);
            graphics.DrawRectangle(pen, x1, y1, width, height);
        }
    }
    public class Ball : PhysicalMechanics //小球
    {
        private float width;
        private float height;
        public Ball(Graphics g,float x1,float y1, float size, float angle = 0)
        {
            width=2 * size;
            height=2 * size;
            this.x = x1 + width / 2;
            this.y = y1 + height / 2;
            RectangleF rec = new RectangleF(x1 - size, y1 - size,width,height);
            graphics = g;
            Rotate(angle);
            graphics.DrawEllipse(pen, rec);
        }
    }
    public class Desk : PhysicalMechanics //桌子
    {
        private float width;
        private float height;
        public Desk(Graphics g,int x1, int y1,float size,float angle=0)
        {
            width = size*8;
            height = size*4;
            this.x = x1 + width / 2;
            this.y = y1 + height / 2;
            SizeF size1 = new SizeF(width, height / 5);
            SizeF size2 = new SizeF(width / 30, height * 7 / 10);
            SizeF size3 = new SizeF(width / 30, height * 7 / 10);
            RectangleF[] rec = new RectangleF[3];
            RectangleF rec1 = new RectangleF(new PointF(x1, y1), size1);
            RectangleF rec2 = new RectangleF(new PointF(x1 + width / 10, y1 + height / 5), size2);
            RectangleF rec3 = new RectangleF(new PointF(x1 + width * 8 / 10, y1 + height / 5), size3);
            rec[0] = rec1;
            rec[1] = rec2;
            rec[2] = rec3;
           
            graphics = g;
            Rotate(angle);
            graphics.DrawRectangles(pen, rec);
        }
    } 
    public class Ruler : PhysicalMechanics //直尺
    {
        private float width;
        private float height;
        public Ruler(Graphics g,float x1, float y1,float size , int angle = 0,bool isDown=false)
        {
            width = size * 10;
            height = size;
            this.x = x1 + width/2;
            this.y = y1 + height/2;
            graphics = g;
            Rotate(x, y, angle);
            graphics.DrawPolygon(pen, new PointF[] 
            {
              new PointF(x1,y1),
              new PointF(x1+ width, y1),
              new PointF(x1 + width, y1 + height),
              new PointF(x1,y1 + height)
            });
            ScaleMark(graphics, x1 + 1, y1+(isDown==false?0:height), width-size/2, height, 100,isDown);
        }
    } 
    public class SpringDynamometer : PhysicalMechanics //弹簧测力计
    {
        public PointF p1;//连接点1
        private Line l1;
        private Line l2;
        private Arc a;
        private float width;
        private float height;
        public SpringDynamometer(Graphics g,float x1,float y1,float size,float num=3,float angle=0)
        {
            if (num < 0 || num > 5) throw new ArgumentOutOfRangeException("num 只能为0到5之间");
            width = size*5;
            height = 15*size;
            this.x = x1 + width / 2;
            this.y = y1 + height / 2;
            graphics = g;
            Rotate(angle);
            float recx = (width * 3 / 7 + x1);
            float recy = ((height * 1 / 6) + y1);
            float recwidth = (width / 7); ;
            float recheight = (height * 4.8f / 6);
            p1 = calcNewPoint(new PointF(recx + recwidth / 2, recy + recheight + size*2), this.centerPoint, angle);
            graphics.DrawPolygon(pen, new PointF[] 
            {
               new PointF(x1,y1),
               new PointF(x1+ width, y1),
               new PointF(x1 + width, y1 + height),
               new PointF(x1,y1 + height)
            });
            graphics.DrawRectangle(pen, recx,recy,recwidth,recheight);//内部小矩形
            ScaleMarkV(graphics, recx + recwidth,recy,recwidth, recheight-size/2, 50,size:size); //右边刻度
            ScaleMarkV(graphics, recx, recy, recwidth, recheight - size / 2, 50, true,size: size);//左边刻度
            Str(graphics, "gf",recx + recwidth, recy-size,size);
            Str(graphics, "N", recx-recwidth,recy-size,size);
            //底部直线
           l1=new Line(graphics,recx + recwidth / 2, recy + recheight, recx + recwidth / 2, recy + recheight + size);
           l2 = new Line(graphics, recx,recy + ((recheight - size / 2) / 50)*(num*10+1), recx + recwidth, recy + ((recheight - size / 2) / 50)*(num*10+1));//中间指示条
           a=new Arc(graphics, recx + (recwidth / 2)-size/2, recy + recheight + size, size,  size, 0, 270);//钩子
        }
    }
    public class VernierCaliper : PhysicalMechanics //游标卡尺
    {
        private float width;
        private float height;
        private float Num;
        public VernierCaliper(Graphics g,float x1, float y1,float size ,float num=0,bool isSimple=false, int angle = 0)
        {
            width = size * 10;
            height = size;
            this.x = x1 + width/2;
            this.y = y1 + height/2;
            Num = num*(width-size/2)/100;
            graphics = g;
            Rotate(x, y, angle);
            //游标卡尺主体
            graphics.DrawPolygon(pen, new PointF[] 
                {
                  new PointF(x1,y1),//矩形左上角
                  new PointF(x1+ width, y1),
                  new PointF(x1 + width, y1 + height),
                  new PointF(x1,y1 + height),
                 isSimple==false?new PointF(x1-size/10,y1+height-size/10):new PointF(x1,y1 + height),
                  isSimple==false?new PointF(x1-size/3,y1+height-size/10):new PointF(x1,y1 + height),
                 isSimple==false? new PointF(x1-size/3,y1+height+size*6/10):new PointF(x1,y1 + height),
                  isSimple==false?new PointF(x1-size/10,y1+height+size*6/10):new PointF(x1,y1 + height),
                 isSimple==false? new PointF(x1-size/10,y1+height-size/10+size*2):new PointF(x1,y1 + height),//顶点
                 isSimple==false? new PointF(x1-size/2,y1+height-size/3+size*2):new PointF(x1,y1 + height),
                 isSimple==false? new PointF(x1-size/1.3f,y1+height+size*5/10):new PointF(x1,y1 + height),
                  isSimple==false?new PointF(x1-size/1.3f,y1):new PointF(x1,y1 + height),
                 isSimple==false? new PointF(x1-size/3,y1):new PointF(x1,y1 + height),
                  isSimple==false?new PointF(x1-size/3,y1-size/4):new PointF(x1,y1 + height),
                 isSimple==false? new PointF(x1-size/3-size/10,y1-size/4):new PointF(x1,y1 + height),
                  isSimple==false?new PointF(x1-size/3-size/10,y1-size/4-size/1.5f):new PointF(x1,y1 + height),//顶点
                });
            //下面游标
            graphics.DrawPolygon(pen, new PointF[] 
                {
                  new PointF(x1+1+Num,y1+height),//起点
                  new PointF(x1+ width/2+Num, y1+height),
                  new PointF(x1 + width/2+Num, y1 + height+size*6/10), 
                   isSimple==false?new PointF(x1 +size/2+Num, y1 + height+size*6/10):new PointF(x1+1+Num,y1 + height+size*6/10),
                  isSimple==false? new PointF(x1 + size*3/10+Num, y1+height-size/3+size*2):new PointF(x1+1+Num,y1 + height+size*6/10),
                   isSimple==false?new PointF(x1-size/10+Num,y1 + height+size*9/10+size):new PointF(x1+1+Num,y1 + height+size*6/10),//顶点
                   isSimple==false?new PointF(x1-size/10+Num,y1 + height+size*6/10):new PointF(x1+1+Num,y1 + height+size*6/10),
                  new PointF(x1+1+Num,y1 + height+size*6/10),
                });
            if (!isSimple)
            {   
                //上面附件
                graphics.DrawPolygon(pen, new PointF[] 
                {
                      new PointF(x1+ width/2+Num, y1),//起点
                      new PointF(x1+ width/2+Num, y1-size/8),
                      new PointF(x1-size/3-size/10+Num-size/10, y1-size/8),
                      new PointF(x1-size/3-size/10+Num-size/10, y1-size/4),
                      new PointF(x1-size/3-size/10+Num, y1-size/4),
                      new PointF(x1-size/3-size/10+Num,y1-size/4-size/1.5f),//顶点
                      new PointF(x1-size/1.3f+Num,y1),
                });
            }
            ScaleMark(graphics, x1 + 1, y1+height, width-size/2, height, 100,true);
            Scale(graphics,Num+ x1 + 1, y1 + height, (width - size / 2)*49/100, height, 50);
            Str(graphics, "0.02mm", x1 + width / 2 + Num-size/2, y1 + height+size*6/10-size/8,10*size/80);
            Str(graphics, "cm", x1 + width-size+Num, y1+height/4, size / 4);
        }
        protected void Scale(Graphics gra, float x1, float y1, float width, float height, int scale, bool isDown = false, float size = 9)
        {
            float xOffset = x1;
            float yOffset = y1;
            float offset = 0;
            StringFormat strfmt = new StringFormat();
            strfmt.Alignment = StringAlignment.Center;
            for (int i = 0; i <= scale; i++)
            {
                offset += width / scale;
                if (i % 5 == 0)
                {
                    gra.DrawLine(pen,
                       new PointF(xOffset + offset, yOffset),
                       new PointF(xOffset + offset, yOffset + (isDown == false ? (10+height/8) : (10-height/8))));

                    graphics.DrawString(((i /5)==10?0:i/5).ToString(), new Font("宋体", size), Brushes.Black,
                        new PointF(xOffset + offset, yOffset + (isDown == false ? (+10+height/8) : (-20-height/8))),
                         strfmt);
                }
                
                else
                {
                    graphics.DrawLine(pen,
                     new PointF(xOffset + offset, yOffset),
                     new PointF(xOffset + offset, yOffset + (isDown == false ? (+5+height/10) : (-5-height/10))));
                }
            }
        }
    }
    //化学部分 单个图元
    public class IronSupport : ChemistryGdi //铁架台
    {
        public PointF p1;
        private float width;
        private float height;
        public IronSupport(Graphics g, float x1, float y1, float nums=0.5f,float size=10,float angle = 0)
        {
            width = size * 8*1.2f;
            height = size * 16*1.2f;
            this.x = x1;
            this.y = y1;
            PointF cP1 = new PointF(x - width / 70+height/4, y - height - height / 40 + height * nums);
            PointF cP2 = new PointF(x - width / 70 + height / 4, y - height - height / 40 + height * (nums + 0.34f));
            PointF cP3 = new PointF(x+width/2,y);
            connectPoints.Add(cP1);
            connectPoints.Add(cP2);
            connectPoints.Add(cP3);
            graphics = g ;
            Rotate(angle);
            p1 = calcNewPoint(new PointF(x - width / 70+height*2/5, y - height - height / 40 + height * nums+width/20), this.centerPoint, angle);
            graphics.DrawRectangle(pen, x, y, width, height / 16);//底盘
            graphics.DrawRectangle(pen, x, y + height / 16, width / 5, height / 30);
            graphics.DrawRectangle(pen, x + width * 4 / 5, y + height / 16, width / 5, height / 30);
            graphics.DrawRectangle(pen, x + size / 4, y - height / 40, width / 7, height / 40);
            graphics.DrawRectangle(pen, x + size / 4 + width / 70, y - height - height / 40, width / 15, height);//竖杆
            graphics.FillRectangle(Brushes.Black, x - width / 70, y - height - height / 40 + height * nums, height / 6f, width / 20);//上横杆
            graphics.FillEllipse(Brushes.Black,x - width / 20 + height / 6f, y - height - height / 25 + height * nums, height / 5f, width / 10);
            graphics.FillRectangle(Brushes.Black, x - width / 70, y - height - height / 40 + height * (nums+0.34f), height / 8, width / 20);//下横杆
            graphics.FillRectangle(Brushes.Black, x - width / 70+height/8, y - height - height / 35 + height * (nums + 0.34f), height / 3.5f, width / 15);
            graphics.FillRectangle(Brushes.Gray, x+2, y - height - height / 23 + height * nums, height / 20, width / 8);
            graphics.FillRectangle(Brushes.Gray, x + 2, y - height - height / 23 + height * (nums+0.34f), height / 20, width / 8);
        }
    }
    public class Beaker : ChemistryGdi 
    {
        private float width;
        private float height;
        private float radius;
        public Beaker(Graphics g,float x1,float y1, float size=10, float angle = 0)
        {
            size = size * 0.9f;
            width=2 * size;
            height=2 * size;
            radius=size;
            this.x = x1 ;
            this.y = y1 ;
            PointF cP1 = new PointF(x, y - radius * 3.5f);
            PointF cP2 = new PointF(x, y - radius * 3.5f);
            connectPoints.Add(cP1);
            connectPoints.Add( cP2);
            graphics = g;
            Rotate(angle);
            graphics.DrawLine(pen,x1 - width / 2, y1, x1 + width / 2, y1);
            graphics.DrawArc(pen, x1 - width / 2 - radius, y1 - 2 * radius, 2*radius,2*radius,90,90);//左底盘弯角
            graphics.DrawArc(pen, x1 + width / 2 - radius, y1 - 2 * radius, 2 * radius, 2 * radius, 0, 90);//右底盘弯角
            graphics.DrawLine(pen, x1 - width / 2 - radius, y1 - radius, x1 - width / 2 - radius, y1 - radius * 5.5f);
            graphics.DrawLine(pen, x1 + width / 2 + radius, y1 - radius, x1 + width / 2 + radius, y1 - radius * 4.5f);
            graphics.DrawArc(pen, x1 + width / 2 + radius, y1 - 5.5f * radius, 2* radius, 2* radius, 180, 90);//右弯角
            graphics.DrawLine(pen, x1 - width / 2 - radius, y1 - radius * 5.5f, x1 - width * 0.6f - radius * 1.3f, y1 - radius * 6f);
            graphics.DrawEllipse(pen, x1 - width * 0.6f -radius*1.3f, y1 - radius * 6f-height/10, width*2.5f, height/5);
            graphics.DrawLine(pen, x1 + width * 1.9f - radius * 1.3f, y1 - radius * 6f, x1 + width / 2 + 2 * radius, y1 - 5.5f * radius);
        }
        public void DrawLiquid() 
        {
            float x, y, span;
            x = centerPoint.X - width / 2;
            y = centerPoint.Y - (height + 3*radius) * 0.6f;
            span = 10f;
            graphics.DrawLine(pen, x - radius, y, x + width + radius, y);
            ShowLiquid(x - radius, y + span, x + width + radius, y + span);
            ShowLiquid(x - radius*0.3f, y + span*2, x + width + radius, y + span*2);
        }
    }  //烧杯          
    public class Bottle : ChemistryGdi //广口瓶
    {
        private float width;
        private float height;
        private float radius;
        public Bottle(Graphics g,float x1,float y1, float size=10, float angle = 0)
        {
            width=2 * size;
            height=2 * size;
            radius=size;
            this.x = x1 ;
            this.y = y1 ;
            PointF cP1 = new PointF(x1 - width / 7f, y1 - 5f * radius);
            PointF cP2 = new PointF(x1 - width /20f, y1 - 5f * radius);
            connectPoints.Add(cP1);
            connectPoints.Add(cP2);
            graphics = g;
            Rotate(angle);
            graphics.DrawLine(pen,x1 - width / 2, y1, x1 + width / 2, y1);
            graphics.DrawArc(pen, x1 - width / 2 - radius, y1 - 2 * radius, 2*radius,2*radius,90,90);//左底盘弯角
            graphics.DrawArc(pen, x1 + width / 2 - radius, y1 - 2 * radius, 2 * radius, 2 * radius, 0, 90);//右底盘弯角
            graphics.DrawLine(pen, x1 - width / 2 - radius, y1 - radius, x1 - width / 2 - radius, y1 - radius * 4.5f);
            graphics.DrawLine(pen, x1 + width / 2 + radius, y1 - radius, x1 + width / 2 + radius, y1 - radius * 4.5f);
            graphics.DrawArc(pen, x1 + width / 2 - radius, y1 - 5.5f * radius, 2* radius, 2* radius, 0, -90);//右弯角
            graphics.DrawArc(pen, x1 - width / 2 - radius, y1 - 5.5f * radius, 2 * radius, 2 * radius, 180, 90);//左弯角
            graphics.DrawLine(pen, x1 - width / 2, y1 - 5.5f * radius, x1 - width / 2, y1 - 6.5f * radius);//左边直线
            graphics.DrawLine(pen, x1 + width / 2, y1 - 5.5f * radius, x1 + width / 2, y1 - 6.5f * radius);//右边直线
            graphics.FillRectangle(Brushes.Black, x1 - width / 2, y1 - 7f * radius, width ,radius);
        }
        public void DrawLiquid()
        {
            float x, y, span;
            x = centerPoint.X - width / 2;
            y = centerPoint.Y - (height + 3 * radius) * 0.6f;
            span = 10f; 
            graphics.DrawLine(pen, x - radius, y, x + width + radius, y);
            ShowLiquid(x - radius, y + span, x + width + radius, y + span);
            ShowLiquid(x - radius * 0.3f, y + span * 2, x + width + radius, y + span * 2);
        }
    }          
    public class Flask : ChemistryGdi //烧瓶  4种
    {
        private float width;
        private float height;
        private float radius;
        public Flask(Graphics g,float x1,float y1,int mode=0, float size=10,float angle = 0)
        {
            if (mode==3)//锥形瓶
            {
                width = 3 * size;
                height = 3 * size;
                radius = size;
                this.x = x1;
                this.y = y1;
                PointF cp1 = new PointF(x1+2, y1 - 5.5f * radius);
                PointF cp2 = new PointF(x1 +4, y1 - 5.5f * radius);
                connectPoints.Add(cp1);
                connectPoints.Add( cp2 );
              
                graphics = g;
                Rotate(angle);
                graphics.DrawLine(pen, x1 - width / 2, y1, x1 + width / 2, y1);
                graphics.DrawArc(pen, x1 - width / 2 - radius, y1 - 2 * radius, 2 * radius, 2 * radius, 90, 90);//左底盘弯角
                graphics.DrawArc(pen, x1 + width / 2 - radius, y1 - 2 * radius, 2 * radius, 2 * radius, 0, 90);//右底盘弯角
                graphics.DrawLine(pen, x1 - width / 2 - radius, y1 - radius, x1 - width / 3, y1 - radius * 5.5f);//左边斜线
                graphics.DrawLine(pen, x1 + width / 2 + radius, y1 - radius, x1 + width / 3, y1 - radius * 5.5f);//右边斜线
                graphics.DrawLine(pen, x1 - width / 3, y1 - 5.5f * radius, x1 - width / 3, y1 - 7.5f * radius);//左边直线
                graphics.DrawLine(pen, x1 + width / 3, y1 - 5.5f * radius, x1 + width / 3, y1 - 7.5f * radius);//右边直线
                graphics.FillRectangle(Brushes.Black,x1 - width / 3, y1 - 8f * radius, width * 2 / 3, radius);
            }
            else
            {
                width = 2.5f * size;
                height = 2.5f * size;
                radius = width;
                this.x = x1;
                this.y = y1;
                PointF cp1 = new PointF(x1 + width / 2 - size, y1 - 1.732f * width - width * 0.75f);
                PointF cp2 = new PointF(x1 + width / 2 - size+2, y1 - 1.732f * width - width * 0.75f);
                connectPoints.Add(cp1);
                connectPoints.Add(cp2);
                graphics = g;
                Rotate(angle);
                if (mode == 0)
                {
                    graphics.DrawLine(pen, x1 - width / 2, y1, x1 + width / 2, y1);//平底烧瓶
                }
                else if (mode == 1)
                {
                    graphics.DrawArc(pen, x1 - radius, y1 - 1.732f * radius / 2 - radius, 2 * radius, 2 * radius, 30, 90);//圆底烧瓶
                }
                else if (mode == 2)
                {
                    graphics.DrawArc(pen, x1 - radius, y1 - 1.732f * radius / 2 - radius, 2 * radius, 2 * radius, 30, 90);//圆底蒸馏烧瓶
                    graphics.DrawLine(pen, x1 + width / 2 - size / 2, y1 - 1.732f * width - width * 0.9f, x1 + width, y1 - 1.732f * width - width * 0.75f);
                    graphics.DrawLine(pen, x1 + width / 2 - size / 2, y1 - 1.732f * width - width * 0.75f, x1 + width, y1 - 1.732f * width - width * 0.6f);
                    graphics.DrawLine(pen, x1 + width, y1 - 1.732f * width - width * 0.75f, x1 + width, y1 - 1.732f * width - width * 0.6f);
                }
                else
                {
                    throw new ArgumentNullException("mode参数只为0，1, 2, 3");
                }
                graphics.DrawArc(pen, x1 - radius, y1 - 1.732f * radius / 2 - radius, 2 * radius, 2 * radius, -60, 120);//右边弯边
                graphics.DrawArc(pen, x1 - radius, y1 - 1.732f * radius / 2 - radius, 2 * radius, 2 * radius, 120, 120);//左边弯边
                graphics.DrawLine(pen, x1 + width / 2, y1 - 1.732f * width, x1 + width / 2 - size / 2, y1 - 1.732f * width);
                graphics.DrawLine(pen, x1 - width / 2, y1 - 1.732f * width, x1 - width / 2 + size / 2, y1 - 1.732f * width);
                graphics.DrawLine(pen, x1 + width / 2 - size / 2, y1 - 1.732f * width, x1 + width / 2 - size / 2, y1 - 1.732f * width - width * 1.5f);
                graphics.DrawLine(pen, x1 - width / 2 + size / 2, y1 - 1.732f * width, x1 - width / 2 + size / 2, y1 - 1.732f * width - width * 1.5f);
                graphics.FillRectangle(Brushes.Black, x1 - width / 2 + size / 2, y1 - 1.732f * width - width * 1.5f - size * 0.35f, width - size, (width - size) / 2);
            }
        }
        public void DrawLiquid()//液体效果
        {
            float x, y, span;
            x = centerPoint.X - width / 2;
            y = centerPoint.Y - (height + 3 * radius) * 0.6f;
            span = 10f;
            graphics.DrawLine(pen, x - radius*0.3f, y+radius, x + width + radius*0.3f, y+radius);
            ShowLiquid(x - radius*0.3f, y + span+radius, x + width + radius*0.3f, y + span+radius);
            ShowLiquid(x - radius * 0.3f, y + span * 2+radius, x + width + radius*0.3f, y + span * 2+radius);
        }
    }
    public class TestTube : ChemistryGdi    //试管
    {
        private float width;
        private float height;
        private float radius;
        public TestTube(Graphics g, float x1, float y1, float size = 10, float angle = 0)
        {
            width = size*0.3f;
            height =size*0.3f;
            radius = size;
            this.x = x1;
            this.y = y1;
            PointF cP1 = new PointF(x, y-radius*6f);
            PointF cP2 = new PointF(x+2, y - radius * 6f);
            connectPoints.Add(cP1);
            connectPoints.Add( cP2 );
            graphics = g;
            Rotate(angle);
            graphics.DrawLine(pen, x1 - width / 2, y1, x1 + width / 2, y1);
            graphics.DrawArc(pen, x1 - width / 2 - radius, y1 - 2 * radius, 2 * radius, 2 * radius, 90, 90);//左底盘弯角
            graphics.DrawArc(pen, x1 + width / 2 - radius, y1 - 2 * radius, 2 * radius, 2 * radius, 0, 90);//右底盘弯角
            graphics.DrawLine(pen, x1 - width / 2 - radius, y1 - radius, x1 - width / 2 - radius, y1 - radius * 7.5f);
            graphics.DrawLine(pen, x1 + width / 2 + radius, y1 - radius, x1 + width / 2 + radius, y1 - radius * 7.5f);
            graphics.FillRectangle(Brushes.Black, x1 - width / 2 -radius, y1 - 8f * radius, width+radius*2f, radius);
        }
        public void DrawLiquid()
        {
            float x, y, span;
            x = centerPoint.X - width / 2;
            y = centerPoint.Y - (height + 3 * radius) * 0.6f;
            span = 10f;
            graphics.DrawLine(pen, x - radius * 0.3f, y, x + width + radius * 0.3f, y);
            ShowLiquid(x - radius * 0.3f, y + span , x + width + radius * 0.3f, y + span);
            ShowLiquid(x - radius * 0.3f, y + span * 2, x + width + radius * 0.3f, y + span * 2);
        }
    }
    public class AlcoholLamp : ChemistryGdi   //酒精灯
    {
        private float width;
        private float height;
        private float radius;
        public AlcoholLamp(Graphics g,float x1,float y1, float size=10, float angle = 0)
        {
            width=4.5f * size;
            height=4.5f * size;
            radius=size;
            this.x = x1 ;
            this.y = y1 ;
            PointF cp1 = new PointF(x1, y1);
            PointF cp2 = new PointF(x1, y1);
            connectPoints.Add(cp1);
            connectPoints.Add( cp2 );
            graphics = g;
            Rotate(angle);
            graphics.DrawLine(pen,x1 - width / 2, y1, x1 + width / 2, y1);
            graphics.DrawLine(pen, x1 - width / 2, y1,x1-width/2+size/2,y1-size/2);//底部左斜线
            graphics.DrawLine(pen, x1 + width / 2, y1, x1 + width / 2 - size / 2, y1 - size / 2);//底部右斜线
            graphics.DrawLine(pen, x1 - width / 2 + size / 2, y1 - size / 2, x1 + width / 2 - size / 2, y1 - size / 2);
            graphics.DrawRectangle(pen, x1 - width / 2 + size / 2, y1 - size*3/4, width - size, size / 4);
            graphics.DrawLine(pen, x1 - width / 2 + size / 2, y1 - size * 3 / 4, x1 - width*3/4, y1 - size * 3f);//左边斜线
            graphics.DrawLine(pen, x1 + width / 2 - size / 2, y1 - size * 3 / 4, x1 + width*3/4, y1 - size * 3f);//右边斜线
            graphics.DrawLine(pen, x1 - width *3 / 4, y1 - size * 3f, x1 - size*3 / 4, y - size * 5f);//左边斜
            graphics.DrawLine(pen, x1 + width * 3 / 4, y1 - size * 3f, x1 + size * 3 / 4, y - size * 5f);//右边斜
            graphics.DrawLine(pen, x1 - size * 3 / 4, y - size * 5f, x1 - size * 3 / 4, y - size * 6f);
            graphics.DrawLine(pen, x1 + size * 3 / 4, y - size * 5f, x1 + size * 3 / 4, y - size * 6f);
            graphics.DrawEllipse(pen, x1 - size * 3 / 4, y1 - 6.5f * radius, size*1.5f, size);
        }
        public void DrawLiquid()
        {
            float x, y, span;
            x = centerPoint.X - width / 2;
            y = centerPoint.Y - (height + 3 * radius) * 0.6f;
            span = 10f;
            graphics.DrawLine(pen, x - radius * 0.3f, y + radius, x + width + radius * 0.3f, y + radius);
            ShowLiquid(x - radius * 0.3f, y + span + radius, x + width + radius * 0.3f, y + span + radius);
            ShowLiquid(x - radius * 0.3f, y + span * 2 + radius, x + width + radius * 0.3f, y + span * 2 + radius);
        }
    }
    public class Funnel : ChemistryGdi  //漏斗 3种
    {
        private float width;
        private float height;
        private float radius;
        public Funnel(Graphics g,float x0,float y0,int mode=0, float size=10, float angle = 0)
        {
            width=size;
            height=size*8;
            radius=size*5;
            float x1 = x0-3;
            float y1 = y0 - 35;
            this.x = x1 ;//中心点为长管子顶部中心
            this.y = y1 ;
            PointF cp1 = new PointF(x1, y1);
            PointF cp2 = new PointF(x1, y1);
            connectPoints.Add(cp1);
            connectPoints.Add(cp2);
            graphics = g;
            Rotate(angle);
            graphics.DrawLine(pen,x1 - width / 3, y1, x1 - width / 3, y1+height+size/2);
            graphics.DrawLine(pen, x1 + width / 3, y1, x1 + width / 3, y1 + height);
            graphics.DrawLine(pen, x1 - width / 3, y1 + height + size / 2, x1 + width / 3, y1 + height);
            if (mode == 0) //普通漏斗
            {
                graphics.DrawLine(pen, x1 - width / 3, y1, x1 - width * 3, y1 - radius / 2);
                graphics.DrawLine(pen, x1 + width / 3, y1, x1 + width * 3, y1 - radius / 2);
                graphics.DrawLine(pen, x1 - width * 3, y1 - radius / 2, x1 + width * 3, y1 - radius / 2);
            }
            else if (mode == 1)//分液漏斗
            {
                graphics.DrawArc(pen, x1 - width*3 / 2, y1 - width*3, width*3, width*3, 75, -330);
                graphics.DrawLine(pen, x1 - width / 2f, y1 - width * 3, x1 - width / 2f, y1 - width * 3.3f);
                graphics.DrawLine(pen, x1 + width / 2f, y1 - width * 3, x1 + width / 2f, y1 - width * 3.3f);
                graphics.DrawRectangle(pen, x1 - width*0.75f, y1 - width * 3.3f-width/4, width*1.5f, width/4);
                graphics.DrawLine(pen, x1 - width * 0.15f, y1 - width * 3.3f - width / 4, x1 - width * 0.15f, y1 - width * 3.3f - width / 2);
                graphics.DrawLine(pen, x1 + width * 0.15f, y1 - width * 3.3f - width / 4, x1 + width * 0.15f, y1 - width * 3.3f - width / 2);
                graphics.DrawArc(pen, x1 - width/2, y1 - width * 3.3f - width / 2 - width, width, width, 75, -330);
                graphics.DrawRectangle(pen, x1 - width*0.75f, y1, width * 1.5f, width/2);
                graphics.DrawArc(pen, x1 - width * 0.75f-width/2, y1-width/6f, width/2, width/1.3f, 15, -330);
            }
            else if (mode == 2)//长颈漏斗
            {
                graphics.DrawArc(pen, x1 - width * 3 / 2, y1 - width * 3, width * 3, width * 3, 75, -135);//左边圆弧
                graphics.DrawArc(pen, x1 - width * 3 / 2, y1 - width * 3, width * 3, width * 3, 105, 135);//右边圆弧
                graphics.DrawLine(pen, x1 + width * 3 / 4, y1 - width *2.8f , x1 + width*1.2f, y1 - width * 3.3f);//右边斜线
                graphics.DrawLine(pen, x1 - width * 3 / 4, y1 - width * 2.8f, x1 - width * 1.2f, y1 - width * 3.3f);//左边斜线
                graphics.DrawEllipse(pen, x1 - width * 1.2f, y1 - width * 3.3f - radius*0.07f, width * 2.4f, 0.14f*radius);
            }
            else 
            {
                throw new ArgumentNullException("mode参数只为0，1，2");
            }
        }
        public void DrawLiquid()
        {
            float x, y, span;
            x = centerPoint.X - width / 2;
            y = centerPoint.Y - (height + 3 * radius) * 0.6f;
            span = 4f;
            graphics.DrawLine(pen, x - radius * 0.2f, y + radius*2.5f, x + width + radius * 0.2f, y + radius*2.5f);
            ShowLiquid(x - radius * 0.2f, y + span + radius*2.5f, x + width + radius * 0.2f, y + span + radius*2.5f);
            ShowLiquid(x - radius * 0.1f, y + span * 2 + radius*2.5f, x + width + radius * 0.1f, y + span * 2 + radius*2.5f);
        }
    }
    public class U_Tube : ChemistryGdi //U型管
    {
        private float width;
        private float height;
        private float span;
        public U_Tube(Graphics g,float x1,float y1, float size, float angle = 0)
        {
            width=6 * size;
            height=6 * size;
            span=size;
            this.x = x1 ;
            this.y = y1 ;
            graphics = g;
            Rotate(angle);
            graphics.DrawArc(pen, x1 - width / 2, y1 - height, width, height, 0, 180);
            graphics.DrawArc(pen, x1 - width / 2+span, y1 - height+span, width-2*span, height-2*span, 0, 180);
            PointF[] pointsRight={new PointF(x1 + width / 2-span, y1 -height/2),new PointF(x1 + width / 2-span, y1 -height*1.3f),new PointF(x1 + width / 2, y1 -height*1.3f),new PointF(x1 + width / 2, y1 - height/2)};
            PointF[] pointsLeft = { new PointF(x1 - width / 2 + span, y1 - height / 2), new PointF(x1 - width / 2 + span, y1 - height*1.3f), new PointF(x1 - width / 2, y1 - height*1.3f), new PointF(x1 - width / 2, y1 - height/2) };
            graphics.DrawLines(pen, pointsRight);
            graphics.DrawLines(pen, pointsLeft);
        }
    }          //todo
    public class Sink : ChemistryGdi  //水槽               //todo
    {
        private float width;
        private float height;
        private float radius;
        public Sink(Graphics g,float x1,float y1, float size, float angle = 0)
        {
            width=8 * size;
            height=4 * size;
            radius=size;
            this.x = x1 ;
            this.y = y1 ;
            graphics = g;
            Rotate(angle);
            graphics.DrawEllipse(pen, x1 - width / 2, y1 - height / 2, width, height/2);
            graphics.DrawArc(pen, x1 -width*0.6f, y1-height*0.45f, width*1.2f, height / 2, 0, 180);
            graphics.DrawLine(pen, x1 - width * 0.6f, y1 - height * 0.2f, x1 - width * 0.6f, y1 - height);
            graphics.DrawLine(pen, x1 + width * 0.6f, y1 - height * 0.2f, x1 + width * 0.6f, y1 - height);
            graphics.DrawArc(pen, x1 - width * 0.6f, y1 - height * 1.25f, width * 1.2f, height / 2, 0, 360);
            graphics.DrawArc(pen, x1 - width * 0.5f, y1 - height * 1.15f, width , height / 3, 0, 360);
        }
    }                      
    public class WatchGlass : ChemistryGdi //表面皿
    {
        private float width;
        private float height;
        private float radius;
        public WatchGlass(Graphics g,float x1,float y1, float size, float angle = 0)
        {
            width=6 * size;
            height=4 * size;
            radius=size;
            this.x = x1 ;
            this.y = y1 ;
            graphics = g;
            Rotate(angle);
            graphics.DrawArc(pen, x - width / 2, y - height, width, height, 0, 180);
            graphics.DrawEllipse(pen, x - width / 2, y - height*2/3, width, height/3);
        }
    }     //todo
    public class VolumetricCylinder : ChemistryGdi //量筒
    {
        private float width;
        private float height;
        private float span;
        private float heightScale;
        public VolumetricCylinder(Graphics g,float x1,float y1, float size,int ml=50, float angle = 0)//ml 参数位量筒的容量
        {
            width=size*4;
            height= size/2;
            heightScale=25.5f;
            span=size;
            this.x = x1 ;
            this.y = y1 ;
            graphics = g;
            Rotate(angle);
            graphics.DrawRectangle(pen, x1 - width / 2, y1 - height / 2, width, height);
            graphics.DrawLine(pen, x1 - width / 2, y1 - height / 2, x1 - width / 2+span/2, y1 - height*1.5f);
            graphics.DrawLine(pen, x1 + width / 2, y1 - height / 2, x1 + width / 2 - span / 2, y1 - height * 1.5f);
            graphics.DrawLine(pen, x1 - width / 2 + span / 2, y1 - height * 1.5f, x1 + width / 2 - span / 2, y1 - height * 1.5f);
            graphics.DrawLine(pen, x1 - width / 2 + span, y1 - height * 1.5f, x1 - width / 2 + span, y1 - height * heightScale);//左边直线
            graphics.DrawLine(pen, x1 + width / 2 - span, y1 - height * 1.5f, x1 + width / 2 - span, y1 - height * heightScale);//右边直线
            graphics.DrawEllipse(pen, x1 - width / 2 + span, y1 - height * heightScale-height/2, width - span*2, height);
            Scale(graphics, x1 - width / 2 + span, y1 - height * (heightScale-5f), width - 2 * span, height * (heightScale - 7.2f), ml,size:size);
            Str(graphics, "ml", x1 - width / 2 + span, y1 - height * heightScale+size,size);
        }
        public void Scale(Graphics gra, float x1, float y1, float width, float height, int scale, bool isLeft = false, float size = 9)
        {
            float xOffset = x1;
            float yOffset = y1;
            float offset = 0;
            StringFormat strfmt = new StringFormat();
            strfmt.Alignment = StringAlignment.Center;
            scale = scale / 2;
            for (int i = 0; i <= scale; i++)
            {
                offset += height / scale;
                if (i % 5 == 0)
                {
                    gra.DrawLine(pen,
                       new PointF(xOffset, yOffset + offset),
                       new PointF(xOffset + (isLeft == false ? (10) : (-10)), yOffset + offset));
                    graphics.DrawString((2*i).ToString(), new Font("宋体", size / 2), Brushes.Black,
                        new PointF(xOffset + (isLeft == false ? (15) : (-20)) + height / scale, yOffset+height - offset + height / scale),
                         strfmt);
                }
                else 
                {
                    graphics.DrawLine(pen,
                       new PointF(xOffset, yOffset + offset),
                       new PointF(xOffset + (isLeft == false ? (7) : (-7)), yOffset + offset));
                }
            }
        }
    }  //todo
    public class GlassRod : ChemistryGdi //玻璃棒
    {
        private float width;
        private float height;
        public GlassRod(Graphics g,float x1,float y1, float size=10,float angle = 0)
        {
            width=size;
            height=size*12;
            this.x = x1 ;
            this.y = y1 ;
            PointF cP1 = new PointF(x, y+height/3);
            connectPoints.Add( cP1 );
            graphics = g;
            Rotate(angle);
            graphics.DrawLine(pen,x1 - width / 3, y1-height/2, x1 - width / 3, y1+height/2);
            graphics.DrawLine(pen, x1 + width / 3, y1-height/2, x1 + width / 3, y1 + height/2);
            graphics.DrawArc(pen, x1 - width / 3, y1 - height / 2 - width / 3, width*2 / 3, width*2 / 3, 180, 180);
            graphics.DrawArc(pen, x1 - width / 3, y1 + height / 2 - width / 3, width*2 / 3, width*2 / 3, 0, 180);
        }
    }           
    public class NarrowNeckedBottle : ChemistryGdi //细口瓶
    {
        private float width;
        private float height;
        private float radius;
        public NarrowNeckedBottle(Graphics g,float x1,float y1, float size=10, float angle = 0)
        {
            width=2 * size;
            height=2 * size;
            radius=size;
            PointF cP1 = new PointF(x1 - width / 6.5f, y1 - 5f * radius);
            connectPoints.Add(cP1);
            this.x = x1 ;
            this.y = y1 ;
            graphics = g;
            Rotate(angle);
            graphics.DrawLine(pen,x1 - width / 2, y1, x1 + width / 2, y1);
            graphics.DrawArc(pen, x1 - width / 2 - radius, y1 - 2 * radius, 2*radius,2*radius,90,90);//左底盘弯角
            graphics.DrawArc(pen, x1 + width / 2 - radius, y1 - 2 * radius, 2 * radius, 2 * radius, 0, 90);//右底盘弯角
            graphics.DrawLine(pen, x1 - width / 2 - radius, y1 - radius, x1 - width / 2 - radius, y1 - radius * 4.5f);
            graphics.DrawLine(pen, x1 + width / 2 + radius, y1 - radius, x1 + width / 2 + radius, y1 - radius * 4.5f);
            graphics.DrawArc(pen, x1 + width / 2 - radius, y1 - 5.5f * radius, 2* radius, 2* radius, 0, -100);//右弯角
            graphics.DrawArc(pen, x1 - width / 2 - radius, y1 - 5.5f * radius, 2 * radius, 2 * radius, 180, 100);//左弯角
            graphics.DrawLine(pen, x1 - width / 2+0.15f*size, y1 - 5.5f * radius, x1 - width / 2+0.15f*size, y1 - 7f * radius);//左边直线
            graphics.DrawLine(pen, x1 + width / 2-0.15f*size, y1 - 5.5f * radius, x1 + width / 2-0.15f*size, y1 - 7f * radius);//右边直线
            graphics.FillRectangle(Brushes.Black, x1 - width / 2 + 0.15f * size, y1 - 7.5f * radius, width - 0.3f * size, radius);
        }
        public void DrawLiquid()
        {
            float x, y, span;
            x = centerPoint.X - width / 2;
            y = centerPoint.Y - (height + 3 * radius) * 0.6f;
            span = 10f;
            graphics.DrawLine(pen, x - radius, y, x + width + radius, y);
            ShowLiquid(x - radius, y + span, x + width + radius, y + span);
            ShowLiquid(x - radius * 0.3f, y + span * 2, x + width + radius, y + span * 2);
        }
    }  
    public class CombustionSpoon : ChemistryGdi //燃烧匙
    {
        private float width;
        private float height;
        public CombustionSpoon(Graphics g,float x1,float y1, float size=10,float angle = 0,bool isFlip=false)
        {
            width=size*2;
            height=size*12;
            this.x = x1 ;
            this.y = y1 ;
            graphics = g;
            Rotate(angle);
            graphics.DrawLine(pen,x1, y1, x1, y1-height);
            graphics.DrawLine(pen, x1, y1, x1 +(isFlip?-width:width), y1);
            graphics.DrawArc(pen, x1+(isFlip?-width:0), y1-width/2, width, width, 0, 180);
        }
    }     //todo
    public class Dropper : ChemistryGdi //胶头滴管
    {
        private float width;
        private float height;
        private float radius;
        public Dropper(Graphics g,float x1,float y1, float size=10,float angle = 0)
        {
            width = size;
            height=size*2;
            radius = size;
            this.x = x1 ;//中心为胶头中心点
            this.y = y1 ;
            graphics = g;
            Rotate(angle);
            graphics.DrawArc(pen, x1 - width / 2, y1 - height / 2, width, height, 60, -300);//胶头
            graphics.DrawLine(pen, x1 - width*0.4f, y1 + height * 0.33f, x1 + width*0.4f, y1 + height * 0.33f);//横线
            graphics.DrawLine(pen, x1 - width * 0.4f, y1 + height * 0.33f, x1 - width * 0.4f, y1 + height*3);//左边直线
            graphics.DrawLine(pen,  x1 + width*0.4f, y1 + height * 0.33f, x1 + width * 0.4f, y1 + height*3);//右边直线
            graphics.DrawLine(pen, x1 - width * 0.4f, y1 + height * 3, x1 - width * 0.2f, y1 + height * 3.5f);//左边斜线
            graphics.DrawLine(pen, x1 + width * 0.4f, y1 + height * 3, x1 + width * 0.2f, y1 + height * 3.5f);//右边斜线
            graphics.DrawLine(pen, x1 - width * 0.2f, y1 + height * 3.5f, x1 - width * 0.2f, y1 + height * 4f);//滴管头部左边直线
            graphics.DrawLine(pen, x1 + width * 0.2f, y1 + height * 3.5f, x1 + width * 0.2f, y1 + height * 3.9f);//滴管头部右边直线
            graphics.DrawLine(pen, x1 - width * 0.2f, y1 + height * 4f, x1 + width * 0.2f, y1 + height * 3.9f);

        }
    }          //todo
    public class ThreeNeckedFlask : ChemistryGdi //三口烧瓶
    {
        private float radius;
        private float width;
        private float height;
        public ThreeNeckedFlask(Graphics g, float x1, float y1, float size = 10, float angle = 0) 
        {
            width = size;
            height = size*2;
            radius = size*4;
            this.x = x1;//圆环中心
            this.y = y1;
            graphics = g;
            Rotate(angle);
            graphics.DrawArc(pen, x1 - radius, y1 - radius, 2 * radius, 2 * radius, -30, 240);
            graphics.DrawArc(pen, x1 - radius, y1 - radius, 2 * radius, 2 * radius, 230, 20);
            graphics.DrawArc(pen, x1 - radius, y1 - radius, 2 * radius, 2 * radius, -70, 20);
            //左边
            graphics.DrawLine(pen, x1 - 1.72f * radius/2, y1 - radius / 2, x1 - 1.72f * radius/2, y1 - radius);
            graphics.DrawLine(pen, x1 - 0.6427f * radius, y1 - radius * 0.766f, x1 - 0.6427f * radius, y1 - radius);
            graphics.DrawEllipse(pen, x1 - 1.72f * radius / 2, y1 - radius - size / 4, 0.2173f * radius, size / 2);
            //右边
            graphics.DrawLine(pen, x1 + 1.72f * radius / 2, y1 - radius / 2, x1 + 1.72f * radius / 2, y1 - radius);
            graphics.DrawLine(pen, x1 + 0.6427f * radius, y1 - radius * 0.766f, x1 + 0.6427f * radius, y1 - radius);
            graphics.DrawEllipse(pen, x1 + 1.72f * radius / 2 - 0.2173f * radius, y1 - radius - size / 4, 0.2173f * radius, size / 2);
            //中间
            graphics.DrawLine(pen, x1 + 0.342f * radius, y1 - 0.9397f * radius, x1 + 0.342f * radius, y1 - 1.2f * radius);
            graphics.DrawLine(pen, x1 - 0.342f * radius, y1 - 0.9397f * radius, x1 - 0.342f * radius, y1 - 1.2f * radius);
            graphics.DrawEllipse(pen, x1 - 0.342f * radius, y1 - 1.2f * radius - size / 4, 0.684f * radius, size / 2);
        }
    }  //todo
    public class GlassTube : ChemistryGdi //玻璃管
    {
        private float width;
        private float heightTube;
        private float height;
        public GlassTube(Graphics g, float x1, float y1, int mode = 0, float size = 10, float angle = 0)
        {
            width=size*7;
            heightTube = size/2;
            height=size*12;
            this.x = x1 ;
            this.y = y1 ;
            PointF cP1 = new PointF(x - width / 2 - heightTube, y + (heightTube / 2 + height * 0.4f)/2);
            PointF cP2 = mode == 0 ? new PointF(x + width / 2 + heightTube, y + (heightTube + height) / 4.5f) : new PointF(x + width / 2 + heightTube, y + (heightTube + height) / 1.8f);
            connectPoints.Add( cP1);
            connectPoints.Add(cP2);
            graphics = g;
            Rotate(angle);
            graphics.DrawLine(pen, x - width / 2 - heightTube/2, y - heightTube / 2, x + width / 2 + heightTube/2, y - heightTube/2);//上
            graphics.DrawLine(pen, x - width / 2, y + heightTube / 2, x + width / 2, y + heightTube / 2);                        //下
            graphics.DrawLine(pen, x - width / 2 - heightTube, y , x - width / 2 - heightTube, y + heightTube / 2+height*0.4f); //左左
            graphics.DrawLine(pen, x - width / 2, y + heightTube / 2, x - width / 2, y + heightTube / 2+height*0.4f); //左
            graphics.DrawArc(pen, x - width / 2 - heightTube, y - heightTube / 2, heightTube, heightTube, 180, 90);
            graphics.DrawArc(pen, x + width / 2, y - heightTube / 2, heightTube, heightTube, 270, 90);
            if (mode==0)
            {
               graphics.DrawLine(pen, x + width / 2, y + heightTube / 2, x + width / 2, y + heightTube / 2 + height*0.6f);//左
                graphics.DrawLine(pen, x + width / 2 + heightTube, y , x + width / 2 + heightTube, y + heightTube / 2 + height*0.6f);//右
            }
            else
            {

                graphics.DrawLine(pen, x + width / 2, y + heightTube / 2, x + width / 2, y + heightTube / 2 + height * 0.95f);
                graphics.DrawLine(pen, x + width / 2 + heightTube, y - heightTube / 2, x + width / 2 + heightTube, y + heightTube / 2 + height * 0.95f);
            }
        }
    }
    public class AsbestosNet : ChemistryGdi //石棉网
    {
        private float width;
        private float radius;
        public AsbestosNet(Graphics g,float x1,float y1, float size=10, float angle = 0)
        {
            width=6 * size;
            radius=size;
            this.x = x1 ;
            this.y = y1 ;
            PointF cP1 = new PointF(x, y);
            connectPoints.Add(cP1);
            graphics = g;
            Rotate(angle);
            graphics.FillEllipse(Brushes.Brown, x1 - width / 2, y1 - radius/4, width ,radius/2);
        }
    }         
    //化学部分 组合图元  整体旋转和缩放问题未解决
    //从上到下 从左到右添加联接点
    public class IronSupport_Flask : ChemistryGdi  //铁架台 反应瓶
    {
        public IronSupport_Flask(Graphics g, float x1, float y1, int mode1=0,float size=10,float angle = 0)
        {
            List<PointF> I = GDIAuxiliary.GetInstance().gdiDic["IronSupport"];
            float xAux = x1 - I[0].X;
            float yAux = y1 - I[0].Y;
            //Flask flask = new Flask(g, x1, y1, mode, size);
            //IronSupport ironSupport = new IronSupport(g, flask.connectPoints[0].X - f[0].X, flask.connectPoints[0].Y - f[0].Y, 0.3f, size);
            List<PointF> f = GDIAuxiliary.GetInstance().gdiDic["Flask"];
            IronSupport ironSupport = new IronSupport(g, xAux, yAux, 0.3f, size);
            Flask flask = new Flask(g, ironSupport.connectPoints[0].X - f[0].X, ironSupport.connectPoints[0].Y - f[0].Y, mode1, size);
            this.x = (ironSupport.centerPoint.X + flask.centerPoint.X) / 2;
            this.y = (ironSupport.centerPoint.Y + flask.centerPoint.Y) / 2;
            connectPointsDic.Add("Flask", new List<PointF>() { flask.connectPoints[0], flask.connectPoints[1]});
            connectPointsDic.Add("IronSupport", new List<PointF>() {ironSupport.connectPoints[1],ironSupport.connectPoints[2] });
        }
    }
    public class IronSupport_Flask_Funnel : ChemistryGdi //铁架台 反应瓶 漏斗
    {
        public IronSupport_Flask_Funnel(Graphics g, float x1, float y1, int mode1 = 0,int mode2=1, float size = 10, float angle = 0)
        {
            List<PointF> I = GDIAuxiliary.GetInstance().gdiDic["IronSupport"];
            List<PointF> f = GDIAuxiliary.GetInstance().gdiDic["Flask"];
            float xAux = x1 - I[0].X;
            float yAux = y1 - I[0].Y + f[0].Y / 2.2f;
            IronSupport ironSupport = new IronSupport(g, xAux, yAux, 0.3f,size);
            Flask flask = new Flask(g, ironSupport.connectPoints[0].X - f[0].X, ironSupport.connectPoints[0].Y - f[0].Y, mode1, size);
            Funnel funnel = new Funnel(g, flask.connectPoints[0].X, flask.connectPoints[0].Y,mode2,size);
            this.x = (ironSupport.centerPoint.X + flask.centerPoint.X+funnel.centerPoint.X) / 3;
            this.y = (ironSupport.centerPoint.Y + flask.centerPoint.Y+funnel.centerPoint.Y) / 3;
            connectPointsDic.Add("Flask", new List<PointF>() { flask.connectPoints[0], flask.connectPoints[1] });
            connectPointsDic.Add("IronSupport", new List<PointF>() { ironSupport.connectPoints[1], ironSupport.connectPoints[2] });
        }
    }//铁架台 反应瓶 漏斗
    public class IronSupport_Flask_AlcoholLamp : ChemistryGdi //铁架台 反应瓶 酒精灯
    {
         public IronSupport_Flask_AlcoholLamp(Graphics g, float x1, float y1, int mode1 = 0,bool isLiquid=false, float size = 10, float angle = 0)
        {
            List<PointF> I = GDIAuxiliary.GetInstance().gdiDic["IronSupport"];
            List<PointF> f = GDIAuxiliary.GetInstance().gdiDic["Flask"];
            float xAux = x1 - I[0].X;
            float yAux = y1 - I[0].Y+f[0].Y/2.2f;
            IronSupport ironSupport = new IronSupport(g, xAux, yAux, 0.3f,size);
            Flask flask = new Flask(g,ironSupport.connectPoints[0].X-f[0].X,ironSupport.connectPoints[0].Y-f[0].Y, mode1,size);
            AlcoholLamp alcoholLamp = new AlcoholLamp(g, ironSupport.connectPoints[2].X, ironSupport.connectPoints[2].Y,size);
            this.x = (ironSupport.centerPoint.X + flask.centerPoint.X ) / 2;
            this.y = (ironSupport.centerPoint.Y + flask.centerPoint.Y ) / 2;
            connectPointsDic.Add("Flask", new List<PointF>() { flask.connectPoints[0], flask.connectPoints[1] });
            connectPointsDic.Add("IronSupport", new List<PointF>() { ironSupport.connectPoints[1]});
            if (isLiquid)
            {
                flask.DrawLiquid();
                alcoholLamp.DrawLiquid();
            }
        }
    }//铁架台 反应瓶 酒精灯
    public class IronSupport_Flask_AlcoholLamp_AsbestosNet : ChemistryGdi //铁架台 反应瓶 酒精灯 石棉网
    {
        public IronSupport_Flask_AlcoholLamp_AsbestosNet(Graphics g, float x1, float y1, int mode1 = 0,bool isLiquid=false, float size = 10, float angle = 0)
        {
            List<PointF> I = GDIAuxiliary.GetInstance().gdiDic["IronSupport"];
            List<PointF> f = GDIAuxiliary.GetInstance().gdiDic["Flask"];
            float xAux = x1 - I[0].X;
            float yAux = y1 - I[0].Y+f[0].Y/2.2f;
            IronSupport ironSupport = new IronSupport(g, xAux, yAux, 0.3f,size);
            Flask flask = new Flask(g,ironSupport.connectPoints[0].X-f[0].X,ironSupport.connectPoints[0].Y-f[0].Y, mode1,size);
            AsbestosNet asbestosNet = new AsbestosNet(g, ironSupport.connectPoints[1].X, ironSupport.connectPoints[1].Y,size);
            AlcoholLamp alcoholLamp = new AlcoholLamp(g, ironSupport.connectPoints[2].X, ironSupport.connectPoints[2].Y,size);
            this.x = (ironSupport.centerPoint.X + flask.centerPoint.X ) / 2;
            this.y = (ironSupport.centerPoint.Y + flask.centerPoint.Y ) / 2;
            connectPointsDic.Add("Flask", new List<PointF>() { flask.connectPoints[0], flask.connectPoints[1] });
            connectPointsDic.Add("IronSupport", new List<PointF>() { ironSupport.connectPoints[1]});
            if (isLiquid)
            {
                flask.DrawLiquid();
                alcoholLamp.DrawLiquid();
            }
        }
    }//铁架台 反应瓶 酒精灯 石棉网
    public class IronSupport_Flask_Funnel_AlcoholLamp_AsbestosNet : ChemistryGdi //铁架台 反应瓶 漏斗 酒精灯 石棉网
    {
        public IronSupport_Flask_Funnel_AlcoholLamp_AsbestosNet(Graphics g, float x1, float y1, int mode1 = 0, int mode2 = 1,bool isLiquid=false, float size = 10, float angle = 0)
        {
            List<PointF> I = GDIAuxiliary.GetInstance().gdiDic["IronSupport"];
            List<PointF> f = GDIAuxiliary.GetInstance().gdiDic["Flask"];
            float xAux = x1 - I[0].X;
            float yAux = y1 - I[0].Y+f[0].Y/2.2f;
            IronSupport ironSupport = new IronSupport(g, xAux, yAux, 0.3f,size);
            Flask flask = new Flask(g,ironSupport.connectPoints[0].X-f[0].X,ironSupport.connectPoints[0].Y-f[0].Y, mode1,size);
            Funnel funnel = new Funnel(g, flask.connectPoints[0].X, flask.connectPoints[0].Y, mode2,size);
            AsbestosNet asbestosNet = new AsbestosNet(g, ironSupport.connectPoints[1].X, ironSupport.connectPoints[1].Y,size);
            AlcoholLamp alcoholLamp = new AlcoholLamp(g, ironSupport.connectPoints[2].X, ironSupport.connectPoints[2].Y,size);
            this.x = (ironSupport.centerPoint.X + flask.centerPoint.X + funnel.centerPoint.X) / 3;
            this.y = (ironSupport.centerPoint.Y + flask.centerPoint.Y + funnel.centerPoint.Y) / 3;
            connectPointsDic.Add("Flask", new List<PointF>() { flask.connectPoints[0], flask.connectPoints[1] });
            connectPointsDic.Add("IronSupport", new List<PointF>() { ironSupport.connectPoints[1]});
            if (isLiquid)
            {
                flask.DrawLiquid();
                funnel.DrawLiquid();
                alcoholLamp.DrawLiquid();
            }
        }
    }//铁架台 反应瓶 漏斗 酒精灯 石棉网
    public class IronSupport_Flask_Funnel_AlcoholLamp : ChemistryGdi  //铁架台 反应瓶 漏斗 酒精灯
    {
        public IronSupport_Flask_Funnel_AlcoholLamp(Graphics g, float x1, float y1, int mode1 = 0, int mode2 = 1, bool isLiquid = false, float size = 10, float angle = 0)
        {
            List<PointF> I = GDIAuxiliary.GetInstance().gdiDic["IronSupport"];
            List<PointF> f = GDIAuxiliary.GetInstance().gdiDic["Flask"];
            float xAux = x1 - I[0].X;
            float yAux = y1 - I[0].Y + f[0].Y / 2.2f;
            IronSupport ironSupport = new IronSupport(g, xAux, yAux, 0.3f, size);
            Flask flask = new Flask(g, ironSupport.connectPoints[0].X - f[0].X, ironSupport.connectPoints[0].Y - f[0].Y, mode1, size);
            Funnel funnel = new Funnel(g, flask.connectPoints[0].X, flask.connectPoints[0].Y, mode2, size);
            AlcoholLamp alcoholLamp = new AlcoholLamp(g, ironSupport.connectPoints[2].X, ironSupport.connectPoints[2].Y, size);
            this.x = (ironSupport.centerPoint.X + flask.centerPoint.X + funnel.centerPoint.X) / 3;
            this.y = (ironSupport.centerPoint.Y + flask.centerPoint.Y + funnel.centerPoint.Y) / 3;
            connectPointsDic.Add("Flask", new List<PointF>() { flask.connectPoints[0], flask.connectPoints[1] });
            connectPointsDic.Add("IronSupport", new List<PointF>() { ironSupport.connectPoints[1] });
            if (isLiquid)
            {
                flask.DrawLiquid();
                funnel.DrawLiquid();
                alcoholLamp.DrawLiquid();
            }
        }
    }//铁架台 反应瓶 漏斗 酒精灯
    public class IronSupport_Funnel_TestTube : ChemistryGdi 
    {
         public IronSupport_Funnel_TestTube(Graphics g, float x1, float y1, int mode1=1, float size = 10, float angle = 0)
        {
            List<PointF> I = GDIAuxiliary.GetInstance().gdiDic["IronSupport"];
            List<PointF> f = GDIAuxiliary.GetInstance().gdiDic["TestTube"];
            float xAux = x1 - I[0].X;
            float yAux = y1 - I[0].Y+f[0].Y/2.2f;
            IronSupport ironSupport = new IronSupport(g, xAux, yAux, 0.5f,size);
            TestTube testTube = new TestTube(g, ironSupport.connectPoints[0].X-f[0].X, ironSupport.connectPoints[0].Y-f[0].Y, size);
            Funnel funnel = new Funnel(g, testTube.connectPoints[0].X, testTube.connectPoints[0].Y,mode1,size);
            this.x = (ironSupport.centerPoint.X + testTube.centerPoint.X+funnel.centerPoint.X) / 3;
            this.y = (ironSupport.centerPoint.Y + testTube.centerPoint.Y+funnel.centerPoint.Y) / 3;
            connectPointsDic.Add("TestTube", new List<PointF>() { testTube.connectPoints[0] });
        }
    } //铁架台 试管  漏斗
    public class Flask_Funnel : ChemistryGdi  //反应瓶 漏斗
    {
        public Flask_Funnel(Graphics g, float x1, float y1, int mode1 = 3,int mode2=1, float size = 10, float angle = 0)
        {
            Flask flask = new Flask(g,x1 , y1 , mode1,size);
            Funnel funnel = new Funnel(g, flask.connectPoints[0].X, flask.connectPoints[0].Y,mode2,size);
            this.x = (flask.centerPoint.X+funnel.centerPoint.X) / 2;
            this.y = (+ flask.centerPoint.Y+funnel.centerPoint.Y) / 2;
            connectPointsDic.Add("Flask", new List<PointF>() { flask.connectPoints[0], flask.connectPoints[1] });
        }
    } //反应瓶 漏斗
    public class GlassTube_Flask_GlassTube : ChemistryGdi //烧瓶 玻璃管
    {
         public GlassTube_Flask_GlassTube(Graphics g, float x1, float y1, int mode1 = 3, float size = 10, float angle = 0)
        {
            List<PointF> f = GDIAuxiliary.GetInstance().gdiDic["Flask"];
            List<PointF> gt = GDIAuxiliary.GetInstance().gdiDic["GlassTubeLong"];
            GlassTube glassTubeLong = new GlassTube(g, x1, y1,1);
            Flask flask = new Flask(g, glassTubeLong.connectPoints[1].X-f[0].X,glassTubeLong.connectPoints[1].Y-f[0].Y, mode1, size);
            GlassTube glassTubeShort = new GlassTube(g, flask.connectPoints[0].X-gt[0].X, flask.connectPoints[0].Y-gt[0].Y);
            this.x = (flask.centerPoint.X+glassTubeLong.centerPoint.X+glassTubeShort.centerPoint.X) / 3;
            this.y = (flask.centerPoint.Y+glassTubeLong.centerPoint.Y+glassTubeShort.centerPoint.Y) / 3;
            connectPointsDic.Add("GlassTube", new List<PointF>() { glassTubeLong.connectPoints[0],glassTubeShort.connectPoints[1] });
        }
    }//烧瓶 玻璃管
    public class TestTube_GlassTube : ChemistryGdi //试管 玻璃管
    {
         public TestTube_GlassTube(Graphics g, float x1, float y1, float size = 10, float angle = 0)
        {
            List<PointF> f = GDIAuxiliary.GetInstance().gdiDic["TestTube"];
            List<PointF> gt = GDIAuxiliary.GetInstance().gdiDic["GlassTubeShort"];
            GlassTube glassTubeLong = new GlassTube(g, x1, y1,0,size,angle);
            TestTube tt = new TestTube(g, glassTubeLong.connectPoints[1].X - f[0].X, glassTubeLong.connectPoints[1].Y - f[0].Y, size, angle);
            GlassTube glassTubeShort = new GlassTube(g, tt.connectPoints[0].X - gt[0].X, tt.connectPoints[0].Y - gt[0].Y, 0, size, angle);
            this.x = tt.centerPoint.X;
            this.y = tt.centerPoint.Y;
            //graphics = g; 画连接点到中心点线示意图用
            //Rotate(angle);
            connectPointsDic.Add("GlassTube", new List<PointF>() { glassTubeLong.connectPoints[0] });
            //connectPoints.Add(glassTubeShort.connectPoints[1]);
        }
    }//试管 玻璃管
    public class TestTube_Funnel : ChemistryGdi   //试管  漏斗
    {
         public TestTube_Funnel(Graphics g, float x1, float y1,int mode1=1, float size = 10, float angle = 0)
        {
            TestTube tt = new TestTube(g, x1,y1,size, angle);
            Funnel funnel = new Funnel(g, tt.connectPoints[0].X, tt.connectPoints[0].Y, mode1,size,angle);
            this.x = tt.centerPoint.X;
            this.y = tt.centerPoint.Y;
            //graphics = g; 画连接点到中心点线示意图用
            //Rotate(angle);
            connectPointsDic.Add("TestTube", new List<PointF>() { tt.connectPoints[0], tt.connectPoints[1] });
        }
    } //试管  漏斗
    public class GlassTube_Bottle_GlassTube : ChemistryGdi  //广口瓶 玻璃管
    {
         public GlassTube_Bottle_GlassTube(Graphics g, float x1, float y1, float size = 10, float angle = 0)
        {
            List<PointF> f = GDIAuxiliary.GetInstance().gdiDic["Bottle"];
            List<PointF> gt = GDIAuxiliary.GetInstance().gdiDic["GlassTubeShort"];
            GlassTube glassTubeLong = new GlassTube(g, x1, y1,1,size,angle);
            Bottle tt = new Bottle(g, glassTubeLong.connectPoints[1].X - f[0].X, glassTubeLong.connectPoints[1].Y - f[0].Y, size, angle);
            GlassTube glassTubeShort = new GlassTube(g, tt.connectPoints[1].X - gt[0].X, tt.connectPoints[1].Y - gt[0].Y, 0, size, angle);
            this.x = tt.centerPoint.X;
            this.y = tt.centerPoint.Y;
            //graphics = g; 画连接点到中心点线示意图用
            //Rotate(angle);
            connectPointsDic.Add("GlassTube", new List<PointF>() { glassTubeLong.connectPoints[0],glassTubeShort.connectPoints[1] });
        }
    } //广口瓶 玻璃管
    public class Beaker_GlassRod : ChemistryGdi  //烧杯  玻璃棒
    {
         public Beaker_GlassRod(Graphics g, float x1, float y1, float size = 10, float angle = 0)
        {
            List<PointF> f = GDIAuxiliary.GetInstance().gdiDic["Beaker"];
            List<PointF> gt = GDIAuxiliary.GetInstance().gdiDic["GlassRod"];
            Beaker tt = new Beaker(g, x1,y1, size, angle);
            GlassRod gl = new GlassRod(g, tt.connectPoints[0].X-gt[0].X+20, tt.connectPoints[0].Y-gt[0].Y, size, angle+30);
            this.x = tt.centerPoint.X;
            this.y = tt.centerPoint.Y;
            //graphics = g; 画连接点到中心点线示意图用
            //Rotate(angle);
        }
    }//烧杯  玻璃棒


    //物理电学部分
    public class Ammeter : PhysicalElectricityGdi 
    {
        private float width;
        private float height;
        public Ammeter(Graphics g,float x1,float y1, float size, float angle = 0)
        {
            width=2 * size;
            height=2 * size;
            this.x = x1 + width / 2;
            this.y = y1 + height / 2;
            RectangleF rec = new RectangleF(x1 - size, y1 - size,width,height);
            graphics = g;
            Rotate(angle);
            connectPoints.Add(calcNewPoint(new PointF(x, y + height / 2), this.centerPoint, angle));
            connectPoints.Add(calcNewPoint(new PointF(x + width, y + height / 2), this.centerPoint, angle));
            graphics.DrawString("A", new Font("小宋体", 2 * size, FontStyle.Regular, GraphicsUnit.Pixel), Brushes.Black, rec);
            graphics.DrawEllipse(pen, rec);
        }
    }
    public class Voltmeter : PhysicalElectricityGdi 
    {
        private float width;
        private float height;
        public Voltmeter(Graphics g,float x1, float y1,float size, float angle = 0)
        {
            width = 2 * size;
            height = 2 * size;
            this.x = x1 + width / 2;
            this.y = y1 + height / 2;
            RectangleF rec = new RectangleF(x1 - size, y1 - size, width,height);
            graphics = g;
            Rotate(angle);
            connectPoints.Add(calcNewPoint(new PointF(x, y + height / 2), this.centerPoint, angle));
            connectPoints.Add( calcNewPoint(new PointF(x + width, y + height / 2), this.centerPoint, angle));
            graphics.DrawString("V", new Font("小宋体", 2 * size, FontStyle.Regular, GraphicsUnit.Pixel), Brushes.Black, rec);
            graphics.DrawEllipse(pen, rec);
        }
    }
    public class Motor : PhysicalElectricityGdi
    {
        private float width;
        private float height;
        public Motor(Graphics g, float x1, float y1, float size, float angle = 0)
        {
            width = 2 * size;
            height = 2 * size;
            this.x = x1 + width / 2;
            this.y = y1 + height / 2;
            RectangleF rec = new RectangleF(x1 - size, y1 - size, width, height);
            graphics = g;
            Rotate(angle);
            connectPoints.Add(calcNewPoint(new PointF(x, y + height / 2), this.centerPoint, angle));
            connectPoints.Add(calcNewPoint(new PointF(x + width, y + height / 2), this.centerPoint, angle));
            graphics.DrawString("M", new Font("小宋体", 2 * size, FontStyle.Regular, GraphicsUnit.Pixel), Brushes.Black, rec);
            graphics.DrawEllipse(pen, rec);
        }
    }
    public class Resistance : PhysicalElectricityGdi 
    {
        private float width;
        private float height;
        private float lineWidth;
        public Resistance(Graphics g,float x1,float y1,float size, float angle = 0)
        { 
            width = size * 3;
            height = size;
            lineWidth = width / 3;
            this.x = x1;
            this.y = y1;
            graphics = g;
            Rotate(angle);
            connectPoints.Add( calcNewPoint(new PointF(x1 - width / 2 - lineWidth, y1), this.centerPoint, angle));
            connectPoints.Add( calcNewPoint(new PointF(x1 + width / 2 + lineWidth, y1), this.centerPoint, angle));
            graphics.DrawLine(pen, x1 - width / 2 - lineWidth, y1, x1 - width / 2, y1);
            graphics.DrawRectangle(pen, x1 - width / 2, y1 - height / 2, width, height);
            graphics.DrawLine(pen, x1 + width / 2, y1, x1 + width / 2 + lineWidth, y1);

        }
    }
    public class SlidingRheostat : PhysicalElectricityGdi 
    {
        private float width;
        private float height;
        public SlidingRheostat(Graphics g,float x1,float y1,float size, float angle = 0,float scale = 0.3f, bool isLeft = false)
        {
            width = size * 3;
            height = size;
            this.x = x1;
            this.y = y1;
            float lineWidth = width / 3;
            //滑动箭头起点的起始位置
            float pointX = x1 - width / 2 + width * scale;
            float pointY = y1 - (height * 3 / 2) - lineWidth / 2;
            if (scale < 0 || scale > 1)
                throw new Exception("输入scale只能在0到1之间");
            graphics = g;
            Rotate(angle);
            connectPoints.Add( calcNewPoint(new PointF(x1 - lineWidth - width / 2, y1), this.centerPoint, angle));
            connectPoints.Add( calcNewPoint(new PointF(pointX + (isLeft == false ? (1 - scale) * width : -scale * width), pointY), this.centerPoint, angle));
            connectPoints.Add(calcNewPoint(new PointF(x1 + lineWidth + width / 2, y1), this.centerPoint, angle));
            graphics.DrawLine(pen, x1 - lineWidth - width / 2, y1, x1 - width / 2, y1);
            graphics.DrawRectangle(pen, x1 - width / 2, y1 - height / 2, width, height);
            Arrow(graphics, pointX, pointY, pointX, y1 - (height / 2));
            graphics.DrawLine(pen, pointX, pointY, pointX + (isLeft == false ? (1 - scale) * width : -scale * width), pointY);
        }
    }
    public class Bulb : PhysicalElectricityGdi 
    {
        private Line l1;
        private Line l2;
        private float width;
        private float height;
        public Bulb(Graphics g,float x1,float y1,float size, float angle = 0)
        {
            width = size * 2;
            height = size * 2;
            this.x = x1;
            this.y = y1;
            RectangleF rec = new RectangleF(x1 - width / 2, y1 - height / 2, width, height);
            double r = Math.Sqrt(width * width + height * height);
            int d = Convert.ToInt32((width / 2) * (1 - (width / r)));         
            graphics = g;
            Rotate(angle);
            connectPoints.Add( calcNewPoint(new PointF(x, y + height / 2), this.centerPoint, angle));
            connectPoints.Add( calcNewPoint(new PointF(x + width, y + height / 2), this.centerPoint, angle));
            graphics.DrawEllipse(pen, rec);
           l1=new Line(graphics, x1 + d - width / 2, y1 - height / 2 + d, x1 + width / 2 - d, y1 + height / 2 - d);
           l2=new Line(graphics, x1 + width / 2 - d, y1 - height / 2 + d, x1 - width / 2 + d, y1 + height / 2 - d);
        }
    }
    public class Switch : PhysicalElectricityGdi 
    {
        private float width;
        private float height;
        public Switch(Graphics g,float x1,float y1,float size, float angle = 0,bool isClosed=false)
        {
            width = size;
            height = size;
            this.x=x1;
            this.y=y1;
            float lineWidth = size * 2;
            SizeF size1 = new SizeF(size*3/5, size*3/5);
            RectangleF rec1 = new RectangleF(new PointF(x1 - size / 2, y1 - size*3 / 10), size1);
           
            graphics = g;
            Rotate(angle);
            connectPoints.Add( calcNewPoint(new PointF(x1 - lineWidth - size / 2, y1), this.centerPoint, angle));
            connectPoints.Add( calcNewPoint(new PointF(x1 + lineWidth + size * 3, y1), this.centerPoint, angle));
            graphics.DrawLine(pen, x1 - lineWidth - size / 2, y1, x1 - size / 2, y1);
            graphics.DrawEllipse(pen, rec1);
            graphics.DrawLine(pen, x1,(isClosed==false?y1 - size*3 / 10:y1 + size*1/5), x1 + size * 3,(isClosed==false?y1 - size *1.5f:y1-size/5));
            graphics.DrawLine(pen, x1 + size * 3, y1, x1 + lineWidth + size * 3, y1);
        }
    }
    public class Power : PhysicalElectricityGdi 
    {
        private float width;
        private float height;
        public Power(Graphics g,float x1,float y1,float size, float angle = 0)
        {
            width = size;
            height = size;
            this.x = x1;
            this.y = y1;
            float lineWidth = size * 2;
            graphics = g;
            Rotate(angle);
            connectPoints.Add( calcNewPoint(new PointF(x1 - lineWidth - size / 2, y1), this.centerPoint, angle));
            connectPoints.Add( calcNewPoint(new PointF(x1 + size / 2 + lineWidth, y1), this.centerPoint, angle));
            graphics.DrawLine(pen, x1 - lineWidth - size / 2, y1, x1 - size / 2, y1);
            graphics.DrawLine(pen, x1 - size / 2, y1 - size, x1 - size / 2, y1 + size);
            graphics.DrawLine(pen, x1 + size / 2, y1 - size / 2, x1 + size / 2, y1 + size / 2);
            graphics.DrawLine(pen, x1 + size / 2, y1, x1 + size / 2 + lineWidth, y1);
        }
    }
    public class Capacitor : PhysicalElectricityGdi 
    {
        private float width;
        private float height;
         public Capacitor(Graphics g,float x1,float y1,float size, float angle = 0)
        {
            width = size;
            height = size;
            this.x = x1;
            this.y = y1;
            float lineWidth = size * 2;
            graphics = g;
            Rotate(angle);
            connectPoints.Add( calcNewPoint(new PointF(x1 - lineWidth - size / 2, y1), this.centerPoint, angle));
            connectPoints.Add( calcNewPoint(new PointF(x1 + size / 2 + lineWidth, y1), this.centerPoint, angle));
            graphics.DrawLine(pen, x1 - lineWidth - size / 2, y1, x1 - size / 2, y1);
            graphics.DrawLine(pen, x1 - size / 2, y1 - size, x1 - size / 2, y1 + size);
            graphics.DrawLine(pen, x1 + size / 2, y1 - size, x1 + size / 2, y1 + size);
            graphics.DrawLine(pen, x1 + size / 2, y1, x1 + size / 2 + lineWidth, y1);
        }
    }
    public class Bell : PhysicalElectricityGdi 
    {
        private float width;
        private float height;
        public Bell(Graphics g,float x1,float y1,float size, float angle = 0)
        {
            width = size*3;
            height = size*3;
            this.x = x1;
            this.y = y1;
            float lineWidth = size * 3;
            RectangleF rec=new RectangleF(x1-lineWidth/2,y1-5*size/2,size*3,size*3);
            graphics = g;
            Rotate(angle);
            connectPoints.Add( calcNewPoint(new PointF(x1 - lineWidth / 2, y1), this.centerPoint, angle));
            connectPoints.Add( calcNewPoint(new PointF(x1 + lineWidth / 2, y1), this.centerPoint, angle));
            graphics.DrawArc(pen, rec, 0, -180);
            graphics.DrawLine(pen, x1 - lineWidth / 2, y1, x1 - size/2, y1);
            graphics.DrawLine(pen, x1 + size/2, y1, x1 + lineWidth / 2, y1);
            graphics.DrawLine(pen, x1 - size/2, y1, x1 - size/2, y1 - size);
            graphics.DrawLine(pen, x1 + size/2, y1, x1 + size/2, y1 - size);
            graphics.DrawLine(pen, x1 - lineWidth/2, y1-size, x1 + lineWidth/2, y1 - size);
        }
    }
    public class Diode : PhysicalElectricityGdi //二极管
    {
        private float width;
        private float height;
         public Diode(Graphics g,float x1,float y1,float size, float angle = 0)
        {
            width = size;
            height = size;
            this.x = x1;
            this.y = y1;
            float lineWidth = size * 2;
            graphics = g;
            Rotate(angle);
            connectPoints.Add(calcNewPoint(new PointF(x1 - lineWidth - size / 2, y1), this.centerPoint, angle));
            connectPoints.Add( calcNewPoint(new PointF(x1 + size / 2 + lineWidth, y1), this.centerPoint, angle));
            graphics.DrawLine(pen, x1 - lineWidth - size / 2, y1, x1 - size / 2, y1);
            Arrow(g, x1 - size / 2, y1, x1 + size/2, y1,size*7/10,size*7/10);
            graphics.DrawLine(pen, x1 + size / 2, y1 - size/2, x1 + size / 2, y1 + size/2);
            graphics.DrawLine(pen, x1 + size / 2, y1, x1 + size / 2 + lineWidth, y1);
        }
    }
    public class Triode : PhysicalElectricityGdi //三极管
    {
        private float width;
        private float height;
          public Triode(Graphics g,float x1,float y1,float size, float angle = 0,bool isPNP=false)
        {
            width = size;
            height = size;
            this.x = x1;
            this.y = y1;
            float lineWidth = size * 2;
            graphics = g;
            Rotate(angle);
            connectPoints.Add( calcNewPoint(new PointF(x1 - lineWidth, y1), this.centerPoint, angle));
            connectPoints.Add( calcNewPoint(new PointF(x1 + 2 * size, y1 - 2 * size), this.centerPoint, angle));
            connectPoints.Add(calcNewPoint(new PointF(x1 + size * 2, y1 + 4 * size), this.centerPoint, angle));
            graphics.DrawLine(pen, x1 - lineWidth, y1, x1, y1);
            graphics.DrawLine(pen, x1, y1 - 3*size/2, x1, y1 + 3*size/2);
            graphics.DrawLine(pen, x1, y1 - size/2, x1+2*size, y1 - 2*size);
            if (isPNP)
                Arrow(g, x1 + 2 * size, y1 + 2 * size, x1, y1 + size / 2, 5, 5);
            else
                Arrow(g, x1, y1 + size / 2, x1 + 2 * size, y1 + 2 * size, 5, 5);
            graphics.DrawLine(pen, x1 + 2 * size, y1-2*size, x1 + size * 2, y1-4*size);
            graphics.DrawLine(pen, x1 + 2 * size, y1 + 2 * size, x1 + size * 2, y1 + 4 * size);
        }
    }
    public class Ground : PhysicalElectricityGdi //接地极
    {
        private float width;
        private float height;
          public Ground(Graphics g,float x1,float y1,float size, float angle = 0)
        {
            width = size;
            height = size;
            this.x = x1;
            this.y = y1;
            float lineWidth = size ;
            float span = size/2;
            graphics = g;
            Rotate(angle);
            connectPoints.Add( calcNewPoint(new PointF(x1, y1), this.centerPoint, angle));
            graphics.DrawLine(pen, x1, y1, x1, y1+lineWidth);
            graphics.DrawLine(pen, x1 - size*3/2, y1+lineWidth, x1 + size*3/2, y1 +lineWidth);
            graphics.DrawLine(pen, x1 - size, y1 + lineWidth+span, x1 + size, y1 + lineWidth+span);
            graphics.DrawLine(pen, x1 - size/2, y1 + lineWidth + span*2, x1 + size/2, y1 + lineWidth + span*2);
        }
    }
    public class Speaker : PhysicalElectricityGdi //扬声器
    {
        private float width;
        private float height;
          public Speaker(Graphics g,float x1,float y1,float size, float angle = 0)
        {
            width = size;
            height = size;
            this.x = x1;
            this.y = y1;
            graphics = g;
            Rotate(angle);
            connectPoints.Add( calcNewPoint(new PointF(x1, y1), this.centerPoint, angle));
            connectPoints.Add( calcNewPoint(new PointF(x1, y1 + 4 * size), this.centerPoint, angle));
            graphics.DrawLine(pen, x1, y1, x1, y1+size);
            graphics.DrawRectangle(pen, x1 - size / 2, y1 + size,size,2*size);
            graphics.DrawLine(pen, x1, y1+size*3, x1, y1 +4*size);
            graphics.DrawLine(pen, x1 + size*3/2, y1 +size/2, x1 + size*3/2, y1 + size*7/2);
            graphics.DrawLine(pen, x1 + size / 2, y1 + size, x1 + size * 3 / 2, y1 + size / 2);
            graphics.DrawLine(pen, x1 + size / 2, y1 + size * 3, x1 + size * 3 / 2, y1 + size * 7 / 2);
            
        }
    }
    public class NotGate : PhysicalElectricityGdi //倒相放大器 非门
    {
        private float width;
        private float height;
        private float lineWidth;
        public NotGate(Graphics g,float x1,float y1,float size, float angle = 0)
        { 
            width = size * 3;
            height = size;
            lineWidth = width / 3;
            this.x = x1;
            this.y = y1;
            PointF[] points = { new PointF(x1 - width / 2, y1 - height), new PointF(x1 - width / 2, y1 + height), new PointF(x1+width/4, y1) };
            graphics = g;
            Rotate(angle);
            connectPoints.Add( calcNewPoint(new PointF(x1 - width / 2 - lineWidth, y1), this.centerPoint, angle));
            connectPoints.Add( calcNewPoint(new PointF(x1 + width / 2 + lineWidth, y1), this.centerPoint, angle));
            graphics.DrawLine(pen, x1 - width / 2 - lineWidth, y1, x1 - width / 2, y1);
            graphics.DrawPolygon(pen, points);
            graphics.DrawEllipse(pen, x1 + width / 4, y1 - width/8,width/4,width/4);
            graphics.DrawLine(pen, x1 + width / 2, y1, x1 + width / 2 + lineWidth, y1);
        }
    }
    public class AndGate : PhysicalElectricityGdi //与门
    {
        private float width;
        private float height;
        private float lineWidth;
        public AndGate(Graphics g,float x1,float y1,float size, float angle = 0)
        { 
            width = size * 3;
            height = size;
            lineWidth = width / 3;
            this.x = x1;
            this.y = y1;
            graphics = g;
            Rotate(angle);
            connectPoints.Add( calcNewPoint(new PointF(x1 - width / 2 - lineWidth, y1 - height * 3 / 4), this.centerPoint, angle));
            connectPoints.Add( calcNewPoint(new PointF(x1 - width / 2 - lineWidth, y1 + height * 3 / 4), this.centerPoint, angle));
            connectPoints.Add( calcNewPoint(new PointF(x1 + width / 2 + lineWidth, y1), this.centerPoint, angle));
         
            graphics.DrawLine(pen, x1 - width / 2 - lineWidth, y1-height*3/4, x1 - width / 2, y1-height*3/4);
            graphics.DrawLine(pen, x1 - width / 2 - lineWidth, y1 + height*3/4, x1 - width / 2, y1 + height*3/4);
            graphics.DrawLine(pen, x1 - width / 2, y1 - height, x1 - width / 2, y1 + height);
            graphics.DrawLine(pen, x1 - width / 2, y1 - height, x1, y1 - height);
            graphics.DrawLine(pen, x1 - width / 2, y1 + height, x1, y1 + height);
            graphics.DrawArc(pen, x1-width/2, y1 - height, width, height * 2, -90, 180);
            graphics.DrawLine(pen, x1 + width / 2, y1, x1 + width / 2 + lineWidth, y1);
        }
    }
    public class OrGate : PhysicalElectricityGdi //或门
    {
        private float width;
        private float height;
        private float lineWidth;
        public OrGate(Graphics g,float x1,float y1,float size, float angle = 0)
        { 
            width = size * 3;
            height = size;
            lineWidth = width / 3;
            this.x = x1;
            this.y = y1;
            graphics = g;
            Rotate(angle);
            connectPoints.Add(calcNewPoint(new PointF(x1 - width / 2 - lineWidth, y1 - height / 2), this.centerPoint, angle));
            connectPoints.Add( calcNewPoint(new PointF(x1 - width / 2 - lineWidth, y1 + height / 2), this.centerPoint, angle));
            connectPoints.Add( calcNewPoint(new PointF(x1 + width / 2 + lineWidth, y1), this.centerPoint, angle));
            graphics.DrawLine(pen, x1 - width / 2 - lineWidth, y1-height/2, x1 - width / 2, y1-height/2);
            graphics.DrawLine(pen, x1 - width / 2 - lineWidth, y1 + height/2, x1 - width / 2, y1 + height/2);
            graphics.DrawArc(pen, x1 - width / 2-lineWidth, y1 - height, width*11/30, height * 2, -90, 180);
            graphics.DrawLine(pen, x1 - width / 2-lineWidth/2, y1 - height, x1, y1 - height);
            graphics.DrawLine(pen, x1 - width / 2-lineWidth/2, y1 + height, x1, y1 + height);
            graphics.DrawArc(pen, x1-width/2, y1 - height, width, height * 2, -90, 180);
            graphics.DrawLine(pen, x1 + width / 2, y1, x1 + width / 2 + lineWidth, y1);
        }
    }
    public class NAndGate : PhysicalElectricityGdi //与非门
    {
        private float width;
        private float height;
        private float lineWidth;
        public NAndGate(Graphics g,float x1,float y1,float size, float angle = 0)
        { 
            width = size * 3;
            height = size;
            lineWidth = width / 3;
            this.x = x1;
            this.y = y1;
            graphics = g;
            Rotate(angle);
            connectPoints.Add( calcNewPoint(new PointF(x1 - width / 2 - lineWidth, y1 - height * 3 / 4), this.centerPoint, angle));
            connectPoints.Add( calcNewPoint(new PointF(x1 - width / 2 - lineWidth, y1 + height * 3 / 4), this.centerPoint, angle));
            connectPoints.Add(calcNewPoint(new PointF(x1 + width * 3 / 4 + lineWidth, y1), this.centerPoint, angle));
            graphics.DrawLine(pen, x1 - width / 2 - lineWidth, y1-height*3/4, x1 - width / 2, y1-height*3/4);
            graphics.DrawLine(pen, x1 - width / 2 - lineWidth, y1 + height*3/4, x1 - width / 2, y1 + height*3/4);
            graphics.DrawLine(pen, x1 - width / 2, y1 - height, x1 - width / 2, y1 + height);
            graphics.DrawLine(pen, x1 - width / 2, y1 - height, x1, y1 - height);
            graphics.DrawLine(pen, x1 - width / 2, y1 + height, x1, y1 + height);
            graphics.DrawArc(pen, x1-width/2, y1 - height, width, height * 2, -90, 180);
            graphics.DrawEllipse(pen, x1 + width / 2, y1 - width/8,width/4,width/4);
            graphics.DrawLine(pen, x1 + width*3 / 4, y1, x1 + width*3 / 4 + lineWidth, y1);
        }
    }
    public class NOrGate : PhysicalElectricityGdi //或非门
    {
        private float width;
        private float height;
        private float lineWidth;
        public NOrGate(Graphics g, float x1, float y1, float size, float angle = 0)
        {
            width = size * 3;
            height = size;
            lineWidth = width / 3;
            this.x = x1;
            this.y = y1;
            graphics = g;
            Rotate(angle);
            connectPoints.Add( calcNewPoint(new PointF(x1 - width / 2 - lineWidth, y1 - height / 2), this.centerPoint, angle));
            connectPoints.Add( calcNewPoint(new PointF(x1 - width / 2 - lineWidth, y1 + height / 2), this.centerPoint, angle));
            connectPoints.Add( calcNewPoint(new PointF(x1 + width * 3 / 4 + lineWidth, y1), this.centerPoint, angle));
            graphics.DrawLine(pen, x1 - width / 2 - lineWidth, y1 - height / 2, x1 - width / 2, y1 - height / 2);
            graphics.DrawLine(pen, x1 - width / 2 - lineWidth, y1 + height / 2, x1 - width / 2, y1 + height / 2);
            graphics.DrawArc(pen, x1 - width / 2 - lineWidth, y1 - height, width * 11 / 30, height * 2, -90, 180);
            graphics.DrawLine(pen, x1 - width / 2 - lineWidth / 2, y1 - height, x1, y1 - height);
            graphics.DrawLine(pen, x1 - width / 2 - lineWidth / 2, y1 + height, x1, y1 + height);
            graphics.DrawArc(pen, x1 - width / 2, y1 - height, width, height * 2, -90, 180);
            graphics.DrawEllipse(pen, x1 + width / 2, y1 - width / 8, width / 4, width / 4);
            graphics.DrawLine(pen, x1 + width * 3 / 4, y1, x1 + width * 3 / 4 + lineWidth, y1);
        }
    }
    public class XOrGate : PhysicalElectricityGdi//异或门
    {
        private float width;
        private float height;
        private float lineWidth;
        public XOrGate(Graphics g, float x1, float y1, float size, float angle = 0)
        {
            width = size * 3;
            height = size;
            lineWidth = width / 3;
            this.x = x1;
            this.y = y1;
            graphics = g;
            Rotate(angle);
            connectPoints.Add( calcNewPoint(new PointF(x1 - width / 2 - lineWidth, y1 - height / 2), this.centerPoint, angle));
            connectPoints.Add( calcNewPoint(new PointF(x1 - width / 2 - lineWidth, y1 + height / 2), this.centerPoint, angle));
            connectPoints.Add(calcNewPoint(new PointF(x1 + width / 2 + lineWidth, y1), this.centerPoint, angle));
            graphics.DrawLine(pen, x1 - width / 2 - lineWidth, y1 - height / 2, x1 - width / 2, y1 - height / 2);
            graphics.DrawLine(pen, x1 - width / 2 - lineWidth, y1 + height / 2, x1 - width / 2, y1 + height / 2);
            graphics.DrawArc(pen, x1 - width / 2 - lineWidth*3/2, y1 - height, width * 11 / 30, height * 2, -90, 180);
            graphics.DrawArc(pen, x1 - width / 2 - lineWidth, y1 - height, width * 11 / 30, height * 2, -90, 180);
            graphics.DrawLine(pen, x1 - width / 2 - lineWidth / 2, y1 - height, x1, y1 - height);
            graphics.DrawLine(pen, x1 - width / 2 - lineWidth / 2, y1 + height, x1, y1 + height);
            graphics.DrawArc(pen, x1 - width / 2, y1 - height, width, height * 2, -90, 180);
            graphics.DrawLine(pen, x1 + width / 2, y1, x1 + width / 2 + lineWidth, y1);
        }
    }
    public class OperationalAmplifier : PhysicalElectricityGdi //运算放大器
    {
        private float width;
        private float height;
        private float lineWidth;
        public OperationalAmplifier(Graphics g, float x1, float y1, float size, float angle = 0)
        {
            width = size * 3;
            height = size;
            lineWidth = width / 3;
            this.x = x1;
            this.y = y1;
            graphics = g;
            Rotate(angle);
            connectPoints.Add( calcNewPoint(new PointF(x1 - width / 2 - lineWidth, y1 - height * 3 / 4), this.centerPoint, angle));
            connectPoints.Add( calcNewPoint(new PointF(x1 - width / 2 - lineWidth, y1 + height * 3 / 4), this.centerPoint, angle));
            connectPoints.Add( calcNewPoint(new PointF(x1 + width / 2 + lineWidth, y1), this.centerPoint, angle));
            connectPoints.Add( calcNewPoint(new PointF(x1, y1 - height * 3 / 4 - lineWidth), this.centerPoint, angle));
            connectPoints.Add( calcNewPoint(new PointF(x1, y1 + height * 3 / 4 + lineWidth), this.centerPoint, angle));
            graphics.DrawLine(pen, x1 - width / 2 - lineWidth, y1 - height * 3 / 4, x1 - width / 2, y1 - height * 3 / 4);
            graphics.DrawLine(pen, x1 - width / 2 - lineWidth, y1 + height * 3 / 4, x1 - width / 2, y1 + height * 3 / 4);
            graphics.DrawString("+", new Font("小宋体",  size*3/2, FontStyle.Regular, GraphicsUnit.Pixel), Brushes.Black, x1 - width / 2, y1 - height*3/2);
            graphics.DrawString("-", new Font("小宋体",  size*3/2, FontStyle.Regular, GraphicsUnit.Pixel), Brushes.Black, x1 - width / 2, y1 - height/2 );
            PointF[] points = { new PointF(x1 - width / 2, y1 - height*3/2), new PointF(x1 - width / 2, y1 + height*3/2), new PointF(x1 + width / 2, y1) };
            graphics.DrawPolygon(pen, points);
            graphics.DrawLine(pen, x1, y1 - height * 3 / 4, x1, y1 - height * 3 / 4 - lineWidth);
            graphics.DrawLine(pen, x1, y1 + height * 3 / 4, x1, y1 + height * 3 / 4 + lineWidth);
            graphics.DrawLine(pen, x1 + width / 2, y1, x1 + width / 2 + lineWidth, y1);
        }
    }
    public class SignalSource : PhysicalElectricityGdi //信号源
    {
        private float width;
        private float height;
        public SignalSource(Graphics g, float x1, float y1, float size, float angle = 0)
        {
            width=2 * size;
            height=2 * size;
            this.x = x1 + width / 2;
            this.y = y1 + height / 2;
            RectangleF rec = new RectangleF(x1 , y1 ,width,height);
            graphics = g;
            Rotate(angle);
            connectPoints.Add( calcNewPoint(new PointF(x, y + height / 2), this.centerPoint, angle));
            connectPoints.Add( calcNewPoint(new PointF(x + width, y + height / 2), this.centerPoint, angle));
            graphics.DrawArc(pen, x1+width/4 , y1+height/4, width / 4, height / 2, 0, -180);
            graphics.DrawArc(pen, x1 + width*2 / 4, y1 + height / 4, width / 4, height / 2, 0, 180);
            graphics.DrawEllipse(pen, rec);
        }
    }
    public class CrystalOscillator : PhysicalElectricityGdi //石英晶体振荡器
    {
        private float width;
        private float height;
         public CrystalOscillator(Graphics g,float x1,float y1,float size, float angle = 0)
        {
            width = size;
            height = size;
            this.x = x1;
            this.y = y1;
            float lineWidth = size * 2;
            graphics = g;
            Rotate(angle);
            connectPoints.Add(calcNewPoint(new PointF(x1 - lineWidth - (size / 2) - width / 2, y1), this.centerPoint, angle));
            connectPoints.Add(calcNewPoint(new PointF(x1 + size / 2 + lineWidth, y1), this.centerPoint, angle));
            graphics.DrawLine(pen, x1 - lineWidth - (size / 2)-width/2, y1, x1 - size / 2-width/2, y1);
            graphics.DrawLine(pen, x1 - size / 2 -width/2, y1 - size, x1 - size / 2-width/2, y1 + size);
            graphics.DrawRectangle(pen, x1 - size / 2 - width / 4, y1-size*3/2, width*2-width/2, height*3);
            graphics.DrawLine(pen, x1 + size / 2+width/2, y1 - size, x1 + size / 2+width/2, y1 + size);
            graphics.DrawLine(pen, x1 + size / 2+width/2, y1, x1 + size / 2 + lineWidth, y1);
        }
    }
    public class Fuse : PhysicalElectricityGdi//保险丝
    {
        private float width;
        private float height;
        private float lineWidth;
        public Fuse(Graphics g,float x1,float y1,float size, float angle = 0)
        { 
            width = size * 3;
            height = size;
            lineWidth = width / 3;
            this.x = x1;
            this.y = y1;
            graphics = g;
            Rotate(angle);
            connectPoints.Add( calcNewPoint(new PointF(x1 - lineWidth - width / 2, y1), this.centerPoint, angle));
            connectPoints.Add( calcNewPoint(new PointF(x1 + width / 2 + lineWidth, y1), this.centerPoint, angle));
            graphics.DrawLine(pen, x1 - width / 2 - lineWidth, y1, x1 + width / 2 + lineWidth, y1);
            graphics.DrawRectangle(pen, x1 - width / 2, y1 - height / 2, width, height);
        }
    }
    public class AlarmSystem : PhysicalElectricityGdi//报警器
    {
        private float width;
        private float height;
        private float lineWidth;
        public AlarmSystem(Graphics g,float x1,float y1,float size, float angle = 0)
        { 
            width = size * 5;
            height = size*2;
            lineWidth = width / 3;
            this.x = x1;
            this.y = y1;
            RectangleF rec = new RectangleF(x1 - width / 2, y1 - height / 2 + size/3, width, height);
            graphics = g;
            Rotate(angle);
            connectPoints.Add( calcNewPoint(new PointF(x1 - lineWidth - width / 2, y1), this.centerPoint, angle));
            connectPoints.Add( calcNewPoint(new PointF(x1 + width / 2 + lineWidth, y1), this.centerPoint, angle));
            graphics.DrawLine(pen, x1 - width / 2 - lineWidth, y1, x1 - width / 2, y1);
            graphics.DrawRectangle(pen, x1 - width / 2, y1 - height / 2, width, height);
            graphics.DrawLine(pen, x1 + width / 2, y1, x1 + width / 2 + lineWidth, y1);
            graphics.DrawString("报警器", new Font("小宋体",  size*1.3f, FontStyle.Regular, GraphicsUnit.Pixel), Brushes.Black, rec);
        }
    }
    public class Inductor : PhysicalElectricityGdi //电感器
    {
        private float width;
        private float height;
        private float lineWidth;
        public Inductor(Graphics g,float x1,float y1,float size,int mode=0, float angle = 0)
        { 
            width = size * 3;
            height = size;
            lineWidth = width / 4;
            this.x = x1;
            this.y = y1;
            RectangleF rec1 = new RectangleF(x1 - width / 2, y1-lineWidth/2, lineWidth, lineWidth);
            RectangleF rec2 = new RectangleF(x1 - width / 2+lineWidth, y1 - lineWidth / 2, lineWidth, lineWidth);
            RectangleF rec3 = new RectangleF(x1 - width / 2+lineWidth*2, y1 - lineWidth / 2, lineWidth, lineWidth);
            RectangleF rec4 = new RectangleF(x1 - width / 2+lineWidth*3, y1 - lineWidth / 2, lineWidth, lineWidth);
            graphics = g;
            Rotate(angle);
            connectPoints.Add (calcNewPoint(new PointF(x1 - width / 2, y1 + lineWidth), this.centerPoint, angle));
            connectPoints.Add (calcNewPoint(new PointF(x1 + width / 2, y1 + lineWidth), this.centerPoint, angle));
            switch (mode)
            {
                case 0:
                    break;
                case 1:
                    graphics.DrawLine(pen, x1 - width / 2, y1-lineWidth, x1 + width / 2, y1-lineWidth);
                    break;
                case 2:
                    Arrow(g, x1 - width / 4, y1 + lineWidth, x1 + width / 2, y1 - lineWidth*1.5f,0.4f*size,0.4f*size);
                    break;
                default: 
                    throw new ArgumentOutOfRangeException("mode的值只能为0,1,2其中之一");
                    
            }
            graphics.DrawLine(pen, x1 - width / 2, y1, x1 - width / 2, y1+lineWidth);
            graphics.DrawArc(pen, rec1, 0, -180);
            graphics.DrawArc(pen, rec2, 0, -180);
            graphics.DrawArc(pen, rec3, 0, -180);
            graphics.DrawArc(pen, rec4, 0, -180);
            graphics.DrawLine(pen, x1 + width / 2, y1, x1 + width / 2, y1+lineWidth);
        }
    }
    public class ResistanceBox : PhysicalElectricityGdi //电阻箱
    {
        private float width;
        private float height;
        private float lineWidth;
        public ResistanceBox(Graphics g, float x1, float y1, float size, float angle = 0)
        {
            width = size * 3;
            height = size;
            lineWidth = width / 3;
            this.x = x1;
            this.y = y1;
            graphics = g;
            Rotate(angle);
            connectPoints.Add(calcNewPoint(new PointF(x1 - lineWidth - width / 2, y1), this.centerPoint, angle));
            connectPoints.Add( calcNewPoint(new PointF(x1 + width / 2 + lineWidth, y1), this.centerPoint, angle));
            graphics.DrawLine(pen, x1 - width / 2 - lineWidth, y1, x1 - width / 2, y1);
            graphics.DrawRectangle(pen, x1 - width / 2, y1 - height / 2, width, height);
            this.Arrow(g, x1 - width / 2, y1 + height*1.5F, x1 + width / 2, y1 - height*2,0.6f*size,0.6f*size);
            graphics.DrawLine(pen, x1 + width / 2, y1, x1 + width / 2 + lineWidth, y1);

        }
       
    }
    public class ResistanceWire : PhysicalElectricityGdi //电阻丝
    {
        private float width;
        private float height;
        private float linewidth;
        private PointF[] points;
        private float span;
        public ResistanceWire(Graphics g, float x1, float y1, float size, float angle = 0)
        {
            width = size * 3;
            height = size;
            linewidth = width / 3;
            span = size/3;
            this.x = x1;
            this.y = y1;
            points = new PointF[] { new PointF(x1 - width / 2 - linewidth, y1), new PointF(x1 - width / 2, y1), new PointF(x1 - width / 2 + span, y1 - span), new PointF(x1 - width / 2 + span * 2, y1 + span), new PointF(x1 - width / 2 + span * 3, y1 - span), new PointF(x1 - width / 2 + span * 4, y1 + span), new PointF(x1 - width / 2 + span * 5, y1 - span), new PointF(x1 - width / 2 + span * 6, y1 + span), new PointF(x1 - width / 2 + span * 7, y1 - span), new PointF(x1 - width / 2 + span * 8, y1 + span), new PointF(x1 - width / 2 + span * 9, y1 - span), new PointF(x1 - width / 2 + span * 10, y1 + span), new PointF(x1 - width / 2 + span * 11, y1 - span), new PointF(x1 - width / 2 + span * 12, y1 + span), new PointF(x1 - width / 2 + span * 13, y1), new PointF(x1 - width / 2 + span * 13+linewidth, y1) };
            graphics = g;
            Rotate(angle);
            connectPoints.Add( calcNewPoint(new PointF(x1 - linewidth - width / 2, y1), this.centerPoint, angle));
            connectPoints.Add( calcNewPoint(new PointF(x1 + width / 2 + linewidth, y1), this.centerPoint, angle));
            graphics.DrawLines(pen, points);
            
        }
    }
}
