using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise03 {
    class Program {
        static void Main(string[] args) {
            int select = 0;
            var sales = new SalesCounter("sales.csv");

            while(true) {
                Console.WriteLine("売上高の表示選択");
                Console.WriteLine("1.店舗別");
                Console.WriteLine("2.商品カテゴリー別");
                Console.Write(">");

                select = int.Parse(Console.ReadLine());

                switch (select) {
                    case 1:
                        OutPut(sales.GetPerStoreSales());
                        break;

                    case 2:
                        OutPut(sales.GetPerCategorySales());
                        break;

                    case 999:
                        return;
                }
            }
        }

        private static void OutPut(IDictionary<string, int> amountPerStore) {
            foreach (var obj in amountPerStore) {
                Console.WriteLine("{0} {1}", obj.Key, obj.Value);
            }
        }
    }
}
