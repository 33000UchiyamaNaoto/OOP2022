﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter15 {
    class Program {
        static void Main(string[] args) {

            var years = new List<int>();
            int year;

            Console.WriteLine("出力したい西暦を入力（終了：-1）");
            year = int.Parse(Console.ReadLine());
            while (year != -1)
            {
                years.Add(year);
                year = int.Parse(Console.ReadLine());
            }

            int sort;
            
            Console.WriteLine();
            Console.Write("昇順：1 降順：2 ：");
            sort = int.Parse(Console.ReadLine());

            IEnumerable<Book> books;
            if ( sort == 1)
            {   //昇順
                books = Library.Books
                    .Where(b => years.Contains(b.PublishedYear))
                    .OrderBy(b => b.PublishedYear);
            }
            else
            {   //降順
                books = Library.Books
                    .Where(b => years.Contains(b.PublishedYear))
                    .OrderByDescending(b => b.PublishedYear);
            }

            //var years = new int[] { 2013, 2016 };
            foreach (var book in books)
            {
                Console.WriteLine(book);
            }

            Console.WriteLine();
            //var groups = Library.Books
            //    .Where(b => years.Contains(b.PublishedYear))
            //    .GroupBy(b => b.PublishedYear)
            //    .OrderBy(g => g.Key);

            var selected = Library.Books
                .Where(b => years.Contains(b.PublishedYear))
                .Join(Library.Categories,       //結合する２番目のシーケンス
                        book => book.CategoryId,  //対象シーケンスの結合キー
                        category => category.Id,//２番目のシーケンスの結合キー
                        (book, category) => new {
                            Titile = book.Title,
                            Category = category.Name,
                            PublishedYear = book.PublishedYear,
                            price = book.Price,
                        }
                ).ToList();

            foreach (var book in selected
                                    .OrderByDescending(x=>x.PublishedYear)
                                    .ThenBy(x=>x.Category))
            {
                Console.WriteLine($"{book.PublishedYear}年, {book.Titile}, {book.Category}, {book.price}円");
            }
            Console.WriteLine($"金額の合計{selected.Sum(b=>b.price)}円");
        }
    }
}
