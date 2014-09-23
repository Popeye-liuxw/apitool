using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApiTool
{
    class Program
    {
        static void Main(string[] args)
        {
            //本工程用来测试各种网络接口的请求及相应信息
            //支持cookies请求
           
            Tool tl = new Tool();
            tl.Run();
        }
    }
}
