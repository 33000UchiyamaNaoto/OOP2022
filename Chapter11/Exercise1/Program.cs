using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;

namespace Exercise1
{
    class Program
    {
        static void Main(string[] args)
        {
            var file = "sample.xml";
            Exercise1_1(file);
            Console.WriteLine();
            Exercise1_2(file);
            Console.WriteLine();
            Exercise1_3(file);
            Console.WriteLine();

            var newfile = "sports.xml";
            Exercise1_4(file, newfile);

        }

        private static void Exercise1_1(string file)
        {
            //匿名クラス　P284


        }

        private static void Exercise1_2(string file)
        {
            //匿名クラス（最初にプレーされた年の若い順と合わせて競技名を表示）

        }

        private static void Exercise1_3(string file)
        {
            
        }

        private static void Exercise1_4(string file, string newfile)
        {
            //要素の追加  P290
        }
    }
}
