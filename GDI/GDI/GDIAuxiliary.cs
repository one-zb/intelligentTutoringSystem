using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace GDI
{
    class GDIAuxiliary
    { 
        public Dictionary<string, List<PointF>> gdiDic = new Dictionary<string, List<PointF>>();//记录各个容器 中心点与各个连接点之间的 距离
        private static GDIAuxiliary instance = null;
        private GDIAuxiliary() 
        {
            //记录各个中心点到联接点的距离
            gdiDic.Add("IronSupport", new List<PointF> { new PointF(46.63f, -100.8f), new PointF(46.63f, -35.52f), new PointF(48f, 0) });
            gdiDic.Add("Flask", new List<PointF> { new PointF(0.2f, -62.05f) });
            gdiDic.Add("TestTube", new List<PointF> { new PointF(0, -60) });
            gdiDic.Add("AlcoholLamp", new List<PointF> { new PointF(0, 0) });
            gdiDic.Add("Funnel", new List<PointF> { new PointF(0, 0) });
            gdiDic.Add("AsbestosNet", new List<PointF> { new PointF(0, 0) });
            gdiDic.Add("GlassTubeShort", new List<PointF> { new PointF(-40, 25.25f),new PointF(40,27.78f) });
            gdiDic.Add("GlassTubeLong", new List<PointF> { new PointF(-40, 25.25f), new PointF(25.25f, 69.44f) });
            gdiDic.Add("GlassTube", new List<PointF> { new PointF(-40, 25.25f), new PointF(25.25f, 69.44f) });
            gdiDic.Add("Bottle", new List<PointF> { new PointF(-2.86f, -50) });
            gdiDic.Add("NarrowNeckedBottle", new List<PointF> { new PointF(-3.08f, -50)});
            gdiDic.Add("Beaker", new List<PointF> { new PointF(0, -50) });
            gdiDic.Add("GlassRod", new List<PointF> { new PointF(0, 40) });
            gdiDic.Add("U_Tube", new List<PointF> { new PointF(-25, -78), new PointF(0, 0) }); // U型管
            gdiDic.Add("Sink", new List<PointF> { new PointF(0, 30) }); // 水槽
            gdiDic.Add("IronSupport_Flask_AlcoholLamp", new List<PointF> { new PointF(102.29f, 233.39f), new PointF(104.29f, 233.39f) });


        }
        public static GDIAuxiliary GetInstance() 
        {
            if (instance == null)
                instance = new GDIAuxiliary();
            return instance;
        }

    }
}
