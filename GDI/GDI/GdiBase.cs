using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Drawing;
using System.Collections;
namespace GDI
{
    //图形的属性
    public class GdiBase
    {
        protected Graphics graphics;
        protected Pen pen = new Pen(Brushes.Black);
        protected float x;//物体中心x坐标
        protected float y;//物体中心y坐标
        public PointF centerPoint;//中心点
        public List<PointF> connectPoints;//记录单一图元 联接点 存储元件可连接的点,从左到右，顺序为1/2/3连接点
        public Dictionary<string, List<PointF>> connectPointsDic;//记录 组合图形的联接点
        public GdiBase() 
        {
            //connectPoints = new List<PointF>();
            connectPointsDic = new Dictionary<string, List<PointF>>();
        }
        //根据中心点旋转的角度
        protected void Rotate(float angle)
        {
            centerPoint = new PointF(x, y);
            var m = new System.Drawing.Drawing2D.Matrix();
            m.RotateAt(angle, centerPoint);
            graphics.Transform = m;
        }
        protected void Rotate(float x,float y,float angle)
        {
            PointF p = new PointF(x, y);
            var m = new System.Drawing.Drawing2D.Matrix();
            m.RotateAt(angle, p);
            graphics.Transform = m;
        }
        //计算旋转后的连接点的坐标
        protected  PointF calcNewPoint(PointF p, PointF pCenter, float angle)
        {
            // calc arc 
            float l = (float)((angle * Math.PI) / 180);

            //sin/cos value
            float cosv = (float)Math.Cos(l);
            float sinv = (float)Math.Sin(l);

            // calc new point
            float newX = (float)((p.X - pCenter.X) * cosv - (p.Y - pCenter.Y) * sinv + pCenter.X);
            float newY = (float)((p.X - pCenter.X) * sinv + (p.Y - pCenter.Y) * cosv + pCenter.Y);
            return new PointF(newX, newY);
        }
        //生成刻度（水平方向）
        protected void ScaleMark(Graphics gra, float x1, float y1, float width, float height, int scale, bool isDown = false, float size = 9)
        {
            float xOffset = x1;
            float yOffset = y1;
            float offset = 0;
            StringFormat strfmt = new StringFormat();
            strfmt.Alignment = StringAlignment.Center;
            for (int i = 0; i <= scale; i++)
            {
                offset += width / scale;
                if (i % 10 == 0)
                {
                    gra.DrawLine(pen,
                       new PointF(xOffset + offset, yOffset),
                       new PointF(xOffset + offset, yOffset + (isDown == false ? (10+height/5) : (-height/5-10))));

                    graphics.DrawString((i / 10).ToString(), new Font("宋体", size), Brushes.Black,
                        new PointF(xOffset + offset, yOffset + (isDown == false ? (+10+height/5) : (-20-height/5))),
                         strfmt);
                }
                else if (i % 5 == 0)
                {
                    graphics.DrawLine(pen,
                        new PointF(xOffset + offset, yOffset),
                        new PointF(xOffset + offset, yOffset + (isDown == false ? (7+height/8) : (-7-height/8))));
                }
                else
                {
                    graphics.DrawLine(pen,
                     new PointF(xOffset + offset, yOffset),
                      new PointF(xOffset + offset, yOffset + (isDown == false ? (+5+height/10) : (-5-height/10))));
                }
            }
        }
        //生成刻度（垂直方向）
        protected void ScaleMarkV(Graphics gra, float x1, float y1, float width, float height, int scale, bool isLeft = false, float size = 9)
        {
            float xOffset = x1;
            float yOffset = y1;
            float offset = 0;
            double k = isLeft ? (0.1) : (1 * 10);
            StringFormat strfmt = new StringFormat();
            strfmt.Alignment = StringAlignment.Center;
            for (int i = 0; i <= scale; i++)
            {
                offset += height / scale;
                if (i % 10 == 0)
                {
                    gra.DrawLine(pen,
                       new PointF(xOffset, yOffset + offset),
                       new PointF(xOffset + (isLeft == false ? (10) : (-10)), yOffset + offset));

                    graphics.DrawString((i * k).ToString(), new Font("宋体", size / 2), Brushes.Black,
                        new PointF(xOffset + (isLeft == false ? (15) : (-20)) + height / scale, yOffset + offset - height / scale),
                         strfmt);
                }
                else if (i % 5 == 0)
                {
                    graphics.DrawLine(pen,
                       new PointF(xOffset, yOffset + offset),
                       new PointF(xOffset + (isLeft == false ? (7) : (-7)), yOffset + offset));
                }
                else
                {
                    graphics.DrawLine(pen,
                     new PointF(xOffset, yOffset + offset),
                     new PointF(xOffset + (isLeft == false ? 5 : -5), yOffset + offset));
                }
            }
        }
        //生成文字字符
        protected void Str(Graphics g, string str, float x1, float y1, float size = 20)
        {
            RectangleF rec = new RectangleF(x1, y1, size * 4, size * 2);
            graphics = g;
            graphics.DrawString(str, new Font("宋体", size, FontStyle.Regular, GraphicsUnit.Pixel), Brushes.Black, rec);
        }
        //带箭头的直线
        protected void Arrow(Graphics g, float x1, float y1, float x2, float y2, float arrowWidth = 6, float arrowHeight = 6)
        {
            System.Drawing.Drawing2D.AdjustableArrowCap lineCap = new System.Drawing.Drawing2D.AdjustableArrowCap(arrowWidth, arrowHeight, true);
            Pen p = new Pen(Brushes.Black);
            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;//恢复实线
            p.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;//定义线尾的样式为箭头
            p.CustomEndCap = lineCap;

            graphics = g;
            graphics.DrawLine(p, x1, y1, x2, y2);
        }
    }
    
    //基础图形
    public class BasicGraphics : GdiBase 
    {

    }

    //化学类
    public class ChemistryGdi : GdiBase   
    {
       public ChemistryGdi() 
       {
           centerPoint = new PointF(x,y); //中心点
           connectPoints = new List<PointF>();//单个图形连接点
           connectPointsDic = new Dictionary<string, List<PointF>>();//组合图形连接点
       }
       public void DrawCenterToPoints(Graphics g) //展示各个连接点到中心点的连线
       {
           bool isRed = true;
           bool isBlue = true;
           int nums = connectPoints.Count;
           for (int i = 0; i < nums; i++)
           {
               if (isRed)
               {
                   g.DrawLine(new Pen(Brushes.Red),centerPoint,connectPoints[i]);
                   isRed = false;
               }
               else if (isBlue) 
               {
                   g.DrawLine(new Pen(Brushes.Blue), centerPoint, connectPoints[i]);
                   isBlue = false;
               }
               else
               {
                   g.DrawLine(new Pen(Brushes.Yellow), centerPoint, connectPoints[i]);
               }
           }
       }             
       public string ShowCentertoPointsSpan(ChemistryGdi gdiSpan) //展示各个连接点到中心点的距离
       {
           string textX = null;
           string textY = null;
           for (int i = 0; i < gdiSpan.connectPoints.Count; i++)
           {
               textX += (gdiSpan.connectPoints[i].X - gdiSpan.centerPoint.X).ToString() + "||";
               textY += (gdiSpan.connectPoints[i].Y - gdiSpan.centerPoint.Y).ToString() + "||";
           }
           return "[" + textX + "]" + "[" + textY + "]";
       }
       public void ShowLiquid(float firstX,float firstY,float secondX,float secondY) //容器添加液体的效果
       {
           float span;
           int num;
           span =(float)Math.Sqrt(Math.Pow(secondX - firstX, 2) + Math.Pow(secondY - firstY, 2))*0.2f;
           num = (int)(Math.Sqrt(Math.Pow(secondX - firstX, 2) + Math.Pow(secondY - firstY, 2)) / span)/2;
           for (int i = 0; i <=num ; i++)
           {
               graphics.DrawLine(pen, firstX, firstY, firstX+span, secondY);
               firstX = firstX + span+span;
           }
       }
    }

    //物理电学 电路图
    public class PhysicalElectricityGdi : GdiBase 
    {
       public PhysicalElectricityGdi() 
       {
           connectPoints = new List<PointF>();
       }

    }

    //物理力学
    public class PhysicalMechanics :GdiBase 
    {
        
    }
}
