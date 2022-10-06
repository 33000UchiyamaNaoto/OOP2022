using SampleEntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleEntityFramework {
    class Program {
        static void Main(string[] args) {
            //InsertBooks();
            //AddAuthors();
            //AddBooks();


            using (var db = new BooksDbContext())
            {
                var authors = db.Authors
                    .OrderByDescending(b => b.Birthday)
                    .ToList();

                foreach (var author in authors)
                {
                    Console.WriteLine("{0} {1:yyyy/MM}", author.Name, author.Birthday);
                    foreach (var book in author.Books)
                    {
                        Console.WriteLine("  {0} {1}",
                                book.Title, book.PublishedYear,
                                book.Author.Name, book.Author.Birthday);
                    }
                    Console.WriteLine();    //改行
                }
            }
            Console.ReadLine(); //F5だけでも画面を閉じないようにする
        }


        // List 13-5
        static void InsertBooks() {
            using (var db = new BooksDbContext())
            {
                var book1 = new Book
                {
                    Title = "坊ちゃん",
                    PublishedYear = 2003,
                    Author = new Author
                    {
                        Birthday = new DateTime(1867, 2, 9),
                        Gender = "M",
                        Name = "夏目漱石",
                    }
                };

                db.Books.Add(book1);

                var book2 = new Book
                {
                    Title = "人間失格",
                    PublishedYear = 1990,
                    Author = new Author
                    {
                        Birthday = new DateTime(1909, 6, 19),
                        Gender = "M",
                        Name = "太宰治",
                    }
                };

                db.Books.Add(book2);
                db.SaveChanges();   //データベースの更新
            }
        }

        // List 13-9
        private static void AddAuthors() {
            using (var db = new BooksDbContext())
            {
                var author1 = new Author
                {
                    Birthday = new DateTime(1878, 12, 7),
                    Gender = "F",
                    Name = "与謝野晶子"
                };
                db.Authors.Add(author1);
                var author2 = new Author
                {
                    Birthday = new DateTime(1896, 8, 27),
                    Gender = "M",
                    Name = "宮沢賢治"
                };
                db.Authors.Add(author2);
                db.SaveChanges();
            }
        }

        // List 13-10
        private static void AddBooks() {
            using (var db = new BooksDbContext())
            {
                var author1 = db.Authors.Single(a => a.Name == "与謝野晶子");
                var book1 = new Book
                {
                    Title = "みだれ髪",
                    PublishedYear = 2000,
                    Author = author1,
                };
                db.Books.Add(book1);
                var author2 = db.Authors.Single(a => a.Name == "宮沢賢治");
                var book2 = new Book
                {
                    Title = "銀河鉄道の夜",
                    PublishedYear = 1989,
                    Author = author2,
                };
                db.Books.Add(book2);
                db.SaveChanges();
            }
        }

        static IEnumerable<Book> GetAllBooks() {

            
            using (var db = new BooksDbContext())
            {

                return db.Books
                    .Include(nameof(Author))//.Include("Author")でもOK
                    .ToList();
            }
        }
        
        static IEnumerable<Book> GetBooks() {
            using (var db = new BooksDbContext())
            {
                return db.Books
                    .Include(nameof(Author))
                    .Where(b=>b.Title.Length == db.Books.Max(x=>x.Title.Length))
                    .ToList();
            }
        }
        //13.1.4
        static IEnumerable<Book> GetBooks_4() {
            using (var db = new BooksDbContext())
            {
                return db.Books
                    .Include(nameof(Author))
                    .OrderBy(b => b.PublishedYear)
                    .Take(3)
                    .ToList();
            }
        }

    }
}
