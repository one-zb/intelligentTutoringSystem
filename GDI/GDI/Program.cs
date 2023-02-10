using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace GDI
{
    static class Program
    {
        
        [STAThread]
        static void Main()
        {
           Application.EnableVisualStyles();
           Application.SetCompatibleTextRenderingDefault(false);
           Application.Run(new Test());
           //Application.Run(new Form2());
        }


        // <summary>
        /// 应用程序的主入口点。
        /// </summary>
        //static void Main(String[] args)
        //{


          



        //    //创建InitilizeCmd类的对象，并初始化。
        //    InitializeCmd icmd = new InitializeCmd();


        //    //创建MethodInfo对象
        //    MethodInfo M;

        //    do
        //    {
        //        Console.WriteLine("请输入命令...");
        //        String inputCmd = Console.ReadLine().ToLower();
        //        Console.WriteLine(inputCmd);

        //        try
        //        {
        //            M = icmd.Cmd[inputCmd];
        //            M.Invoke(null, null);
        //        }
        //        catch (Exception e)
        //        {
        //            Console.WriteLine("命令输入有误，请重新输入\n" + e.Message);
        //        }
        //    } while (true);
        //}
    }
}
