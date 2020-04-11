using System;

using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Text;

using Microsoft.CSharp;                                             //需要自己添加

using System.CodeDom.Compiler;                             //需要自己添加

using System.Reflection;                                          //反射命名空间

using UnityEngine;                                                   //需要自己添加，再强调一下，得添加UnityEngine.dll引用


namespace 在线编程服务器端

{

    internal class OnLineProgramming

    {
        static void Main(string[] args)
        {
            OnLineProgramming op = new OnLineProgramming();
            string wrongmessage = null;
            if (args.Length == 2)
            {
                string text = System.IO.File.ReadAllText(args[0]);
                wrongmessage = op.TestCompiler(text, args[1]);
            }
            else if (args.Length == 1)
            {
                string text = System.IO.File.ReadAllText(args[0]);
                wrongmessage = op.TestCompiler(text);
            }
            else if (args.Length == 0)
            {
                string code = Console.ReadLine();
                if (code == "")
                {
                    string text = System.IO.File.ReadAllText("./Script1.cs");
                    wrongmessage = op.TestCompiler(text,"Script1");
                }
                else
                {
                    string name = Console.ReadLine();
                    string text = System.IO.File.ReadAllText(code);
                    wrongmessage = op.TestCompiler(text, name);
                }

            }
            Console.WriteLine(wrongmessage);
            File.WriteAllText("./wrong.txt", wrongmessage);

        }

        CSharpCodeProvider provider;

        CompilerParameters parameters;

        public OnLineProgramming()
        {

            //定义c#代码容器

            provider = new CSharpCodeProvider();

            parameters = new CompilerParameters();

        }

        public string TestCompiler(string code, string name)        //参数code是unity传来的脚本代码，返回值是编译后的结果

        {

            StringBuilder sb = new StringBuilder();

            try

            {

                //动态添加dll库

                //parameters.ReferencedAssemblies.Add("UnityEngine.dll");
                parameters.ReferencedAssemblies.Add("System.dll");
                parameters.ReferencedAssemblies.Add("System.Data.dll");
                parameters.ReferencedAssemblies.Add("System.Xml.dll");
                parameters.ReferencedAssemblies.Add("Assembly-CSharp-firstpass.dll");
                parameters.ReferencedAssemblies.Add("Assembly-CSharp.dll");
                parameters.ReferencedAssemblies.Add("UnityEngine.CoreModule.dll");



                //True - 生成在内存中, false - 生成在外部文件中

                parameters.GenerateInMemory = false;

                //True - 生成 exe, false - 生成 dll

                parameters.GenerateExecutable = false;

                parameters.OutputAssembly = name + ".dll";         //编译后的dll库输出的名称，会在bin/Debug下生成Test.dll库





                //取得编译结果

                CompilerResults results = provider.CompileAssemblyFromSource(parameters, code);

                if (results.Errors.HasErrors)

                {

                    foreach (CompilerError error in results.Errors)

                    {

                        sb.AppendLine(String.Format("Error({0}): {1}", error.Line, error.ErrorText));



                    }

                    return sb.ToString();                                     //编译不通过，返回错误信息

                }

                else

                {

                    return "Ok";                                             //编译通过，返回Ok

                }



            }

            catch (Exception e)

            {

                Console.WriteLine("抛异常了");
                Console.WriteLine(e.Message);
                return sb.ToString();

            }

        }
        public string TestCompiler(string code)
        {
            return TestCompiler(code, "Test");
        }
    }

}
