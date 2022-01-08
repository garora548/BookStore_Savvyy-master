using BookStore_Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStore_Domain.Interfaces
{
    public interface IBookService
    {
        Response<List<Book>> GetAllBooks();
        Response<Book> GetBookById(int Id);
        Task<Response<int>> SaveBook(Book book);
        Response<int> UpdateBook(Book book);
        Response<int> DeleteBook(int Id);
    }
}
