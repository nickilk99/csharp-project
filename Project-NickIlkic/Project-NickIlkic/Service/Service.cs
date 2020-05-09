using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_NickIlkic.Service
{
    class Service
    {
        LibraryEntities5 libDB = new LibraryEntities5();
        

        public int AddBook(Book book)
        {
            using (var bookContext = new LibraryEntities5())
            {
                bookContext.Books.Add(book);
                bookContext.SaveChanges();
            }
            var query = from b in libDB.Books
                        where b.ISDN == "41242"
                        select b.Id;

            return query.FirstOrDefault();

        }


        public bool EditBook(int bookId, string newName)
        {
            using (var bookContext = new LibraryEntities5())
            {

                var query = from b in bookContext.Books
                            where b.Id == bookId
                            select b;

                Book selectedOne = query.First();
                selectedOne.Title = newName;


                bookContext.SaveChanges();

            }
            return true;
        }

        public bool Lendbook(int bookId, int userId)
        {
            using (var bookContext = new LibraryEntities5())
            {

                var query = from u in bookContext.Users
                            where u.Id == userId
                            select u;

                User selectedOne = query.First();
                selectedOne.BorrowedBooks = bookId;


                bookContext.SaveChanges();

            }
            return true;
        }

    }
}
