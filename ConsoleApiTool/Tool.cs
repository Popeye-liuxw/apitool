// ===============================================================================
// Project Name        :    ConsoleApiTool
// Project Description :    
// ===============================================================================
// Class Name          :    Tool
// Class Version       :    v1.0.0.0
// Author              :    Liuxw
// Email               :    lxw9586[at]live.com
// Create Time         :    9/23/2014 12:10:39 PM
// Update Time         :    9/23/2014 12:10:39 PM
// ===============================================================================
// Copyright © WIN2012-FBI 2014 . All rights reserved.
// ===============================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
 * FileName :  Tool
 * GUID     :  2cb51c04-07bd-4d21-92b6-00e15c447fd1
 * CLR      :  4.0.30319.34003
 */

namespace ConsoleApiTool
{
   public  class Tool
    {

       public void Run()
       {
           try
           {
               HttpFun.Cfg cfg = new HttpFun.Cfg();

               cfg.Accept = "application/json";
               cfg.AcceptEncoding = "utf-8";
               cfg.Cookie = "";
               Console.WriteLine("是否进入登录模式，y 登录，n 不需要登录");
               string islogin = Console.ReadLine();
               string strResult = string.Empty;
               HttpFun hf = new HttpFun();
               if (islogin.Contains('y'))
               {
                   Console.WriteLine("请输入接口地址：");
                   cfg.Url = Console.ReadLine();
                   Console.WriteLine("输入用户名和密码");
                   string username = Console.ReadLine();
                   string userpwd = Console.ReadLine();
                   cfg.data = string.Format("name={0}&pwd={1}", username, userpwd);

                   strResult = hf.Post(cfg);

                   Console.WriteLine("信息头数据:" + cfg.Header);

                   Console.WriteLine("用户登录消息：" + strResult);
                   Console.WriteLine("请填写cookie信息:");
                   string cookie = Console.ReadLine();
                   cfg.Cookie = cookie;
               }
               while (true)
               {
                   Console.WriteLine("请输入接口地址：");
                   cfg.Url = Console.ReadLine();
                   Console.WriteLine("请选择请求方式，1.get 2.post 3.put 4 delte");
                   string meth = Console.ReadLine();
                   if (meth.Contains('1'))
                   {
                       strResult = hf.Get(cfg);
                   }
                   else if (meth.Contains('2'))
                   {
                       Console.WriteLine("请输入数据项：如格式name={0}&pwd={1}");
                       cfg.data = Console.ReadLine();
                       strResult = hf.Post(cfg);
                   }
                   else if (meth.Contains('3'))
                   {
                       Console.WriteLine("请输入数据项：如格式name={0}&pwd={1}");
                       cfg.data = Console.ReadLine();
                       strResult = hf.Put(cfg);

                   }
                   else
                   {
                       strResult = hf.Delete(cfg);
                   }

                   Console.WriteLine("接口回应：" + strResult);

                   Console.WriteLine("是否继续：任意字符继续，exit退出");
                   string back = Console.ReadLine();
                   if (back.Contains("exit"))
                   {
                       break;
                   }
               }
               Console.WriteLine("程序运行完毕。");
           }
           catch (Exception ex)
           {
               Console.WriteLine("出现异常：", ex);
           }
       }
    }
}
