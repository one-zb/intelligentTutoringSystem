
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;



namespace GDI
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }



        private void Form2_Paint_1(object sender, PaintEventArgs e)
        {
            Graphics g = this.CreateGraphics();

            //样例
            //IronSupport ironSupport = new IronSupport(g, 200, 350,0.3f);
            //AsbestosNet asb = new AsbestosNet(g, 250, 291);
            //AlcoholLamp alcohlLamp = new AlcoholLamp(g, 255, 350,8);
            //Flask flask = new Flask(g, 253, 280 );
            Bottle bottle = new Bottle(g, 250, 250, 10);
            //GlassTube glasstube = new GlassTube(g, 350, 200);
            // glasstube.DrawCenterToPoints(g);
            //GlassTube glasstube2 = new GlassTube(g, 372, 220,1);
            //Bottle bottle2 = new Bottle(g, 410, 310, 10);
            //组合图元
            //IronSupport_Flask iff2 = new IronSupport_Flask(g, 300, 300);
            //IronSupport_Funnel_TestTube ii = new IronSupport_Funnel_TestTube(g, 300, 300);
            // IronSupport_Flask fg = new IronSupport_Flask(g, 200, 300);
            //IronSupport_Flask_Funnel_AlcoholLamp_AsbestosNet kkk = new IronSupport_Flask_Funnel_AlcoholLamp_AsbestosNet(g, 200, 300,isLiquid:true);
            //Flask_GlassTube Bott = new Flask_GlassTube(g, 200, 300);

             //List<string> gdiGraph = new List<string>() { "二氧化硫制备实验反应装置",  "广口瓶", "玻璃管", "广口瓶", "玻璃管", "广口瓶", "玻璃管", "广口瓶", "玻璃管", "石棉网", "酒精灯", "分液漏斗", "锥形瓶","铁架台" };
            //"二氧化硫制备实验反应装置""烧杯", "玻璃管", "石棉网", "酒精灯", "铁架台", "锥形瓶", "玻璃管", "广口瓶", "玻璃管", "广口瓶", "玻璃管", "分液漏斗", "反应瓶", "铁架台"
            //"广口瓶","玻璃管", "广口瓶", "玻璃管", "广口瓶", "玻璃管", "广口瓶", "玻璃管", "石棉网", "酒精灯", "分液漏斗", "锥形瓶", "铁架台" 
           //GDIGraphGeneration gdiGeneration = new GDIGraphGeneration();
           //gdiGeneration.Draw(g, gdiGraph);

            //测试
            //GlassRod ik = new GlassRod(g, 300, 250);
            // ik.DrawCenterToPoints(g);
            // MessageBox.Show(ik.ShowCentertoPointsSpan(ik)); 


            //单个图元
            //Line line = new Line(g, 200, 200, 200, 300);
            //Convexlens cs = new Convexlens(g, 100, 100, 10);
            //Concavelens cc = new Concavelens(g, 300, 200, 10,45);
            //Car car = new Car(g, 50, 50, 10);
            //Block bl = new Block(g, 100, 100, 10);
            // Desk ds = new Desk(g, 100, 150, 10);
            //Ruler ru = new Ruler(g, 180, 150, 30);
            //SpringDynamometer s = new SpringDynamometer(g, 100, 100,20 ,2f);
            //Ammeter am = new Ammeter(g, 150, 100, 20);
            //Motor mr = new Motor(g, 100, 100, 10);
            //Resistance re = new Resistance(g, 150, 200, 10);
            //SlidingRheostat st = new SlidingRheostat(g, 200, 200, 10);
            //Bulb bb = new Bulb(g, 100, 200, 10);
            // Switch sh = new Switch(g, 250, 250, 10);
            //Power pr = new Power(g, 100, 200, 10);
            //Capacitor cc = new Capacitor(g, 100, 200, 10);
            //Bell bl = new Bell(g, 100, 100, 10);
            // Diode de = new Diode(g, 230, 250, 10);
            //Triode td = new Triode(g, 200, 250, 10);
            //Ground gd = new Ground(g, 300, 250, 10,90);
            //Speaker sr = new Speaker(g, 200, 250, 10);
            //OrGate ia = new OrGate(g, 250, 200, 10);
            //AndGate ag = new AndGate(g, 200, 250, 10);
            //NAndGate ae = new NAndGate(g, 250, 250, 10);
            //NOrGate uu = new NOrGate(g, 300, 200, 10);
            //XOrGate uui = new XOrGate(g, 300, 250, 10);
            //OperationalAmplifier ol = new OperationalAmplifier(g, 200, 200, 10);
            //SignalSource ss = new SignalSource(g, 200, 250, 10);
            // CrystalOscillator co = new CrystalOscillator(g, 250, 250, 10);
            //Fuse fw = new Fuse(g, 200, 250, 10);
            //AlarmSystem ass = new AlarmSystem(g, 200, 300, 10);
            // Inductor it = new Inductor(g, 200, 150, 10,1);
            // ResistanceBox rb = new ResistanceBox(g, 200, 200, 10);
            // ResistanceWire rw = new ResistanceWire(g, 200, 100, 10);
            // Resistance re = new Resistance(g, 100, 100, 10);
            // IronSupport uu=new IronSupport(g,300,300,10,0.2f);
            //Line line = new Line(g, uu.p1.X, uu.p1.Y, uu.p1.X, uu.p1.Y + 90);
            //Ball bl = new Ball(g, line.p2.X, line.p2.Y, 10);
            // Beaker bk = new Beaker(g, 100, 300, 10);
            //Bottle fk = new Bottle(g, 200, 300, 10);
            // VernierCaliper vc = new VernierCaliper(g, 100, 200, 50,20.3f);//num单位毫米mm
            //TestTube tb = new TestTube(g, 300, 300, 10);
            //ErlenmeyerFlask er = new ErlenmeyerFlask(g, 300, 300, 10);
            // AlcoholLamp al = new AlcoholLamp(g, 300, 300, 10);
            // Funnel fn = new Funnel(g, 300, 300, 10,1);
            // U_Tube ut = new U_Tube(g, 300, 300, 10);
            // Flask fkk=new Flask(g,300,300,10,2);
            // Sink sk = new Sink(g, 300, 300, 10);
            // WatchGlass wg = new WatchGlass(g, 300, 300, 10);
            // VolumetricCylinder vc = new VolumetricCylinder(g, 300, 400,20,50);
            // Triangle te = new Triangle(g, 200, 200);
            // Triangle te = new Triangle(g, 100, 100, 200, 200, 100, 300);
            //Axis ass = new Axis(g, 350, 300,20);//,"t/s","v/(m/s)"
            //ass.Scale(0, 2,4, "t/s");
            //ass.Scale(0, 1,4, "v/(m/s)",true);
            //ass.DrawPoint(4, 2,0);
            // ass.DrawLine(2, 3, 4, 2);
            //GlassRod gr = new GlassRod(g, 300, 300,10);
            // NarrowNeckedBottle nb = new NarrowNeckedBottle(g, 300, 300, 10);
            //CombustionSpoon cs = new CombustionSpoon(g, 300, 300);
            // Dropper dp = new Dropper(g, 300, 200);
            // ThreeNeckedFlask tf = new ThreeNeckedFlask(g, 350, 300,10);
            // Cube ce = new Cube(g, 200, 300,size:20);
            //Cylinder cd = new Cylinder(g, 200, 200);
            //Quadrilateral qa = new Quadrilateral(g, 150, 150);
            //Quadrilateral qa2 = new Quadrilateral(g, 100, 100, 100, 150, 300, 200,50, 200);
            //Colne ce1 = new Colne(g, 100, 100);
            //Line lw = new Line(g, 200, 300, 300, 300);
            //lw.LineWithArrow("X", "=", "Y");
            //Circle ci = new Circle(g, 300, 300, 70);
            //Points pt = new Points(g, 100, 100, 0);
            //Sphere sp = new Sphere(g, 200, 150,79);
            //Curve ce = new Curve(g, 250, 250,1);
            // Pyramid pd = new Pyramid(g, 200, 200,2);
            //Angle ae = new Angle(g, 300, 300,20);
            // Prism pm = new Prism(g, 300, 100,2);
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
