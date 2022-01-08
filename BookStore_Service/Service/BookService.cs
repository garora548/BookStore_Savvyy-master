using BookStore_Domain.Interfaces;
using BookStore_Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStore_Service.Service
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public Response<int> DeleteBook(int Id)
        {
            Response<int> response = new Response<int>();
            try
            {
                if (Id <= 0)
                {
                    response.Message = "Please provide us the correct Id to delete";
                    response.Status = false;
                }
                var bookResp = _bookRepository.GetById(Id);
                if (bookResp.Status && bookResp.Data != null)
                {
                    response = _bookRepository.Delete(bookResp.Data);
                }
                else
                {
                    response.Message = "Not able to find the Book to delete";
                    response.Status = false;
                }
            }
            catch (Exception ex)
            {
                response.Message = "Issue while deleting a book.";
                response.Exception = ex;
                response.Status = false;
            }
            return response;
        }

        public Response<List<Book>> GetAllBooks()
        {
            Response<List<Book>> response = new Response<List<Book>>();
            try
            {
                response = _bookRepository.GetAll();
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Message = "Internal Service error";
                response.Exception = ex;
                response.Status = false;
            }
            return response;
        }

        public Response<Book> GetBookById(int Id)
        {
            Response<Book> response = new Response<Book>();
            try
            {
                response = _bookRepository.GetById(Id);
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Message = "Internal Service Error while getting a book";
                response.Exception = ex;
                response.Status = false;
            }
            return response;
        }

        public async Task<Response<int>> SaveBook(Book book)
        {
            Response<int> response = new Response<int>();
            try
            {
                response = _bookRepository.Save(book);
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Message = "Internal Service Error while saving a book";
                response.Exception = ex;
                response.Status = false;
            }
            return response;
        }

        public Response<int> UpdateBook(Book book)
        {
            Response<int> response = new Response<int>();
            try
            {
                if (book.Id <= 0)
                {
                    response.Message = "Please provide us the correct Id to update";
                    response.Status = false;
                }
                var bookResp = _bookRepository.GetById(book.Id);
                if (bookResp.Status && bookResp.Data != null)
                {
                    response = _bookRepository.Update(book);
                }
                else
                {
                    response.Message = "Not able to find the Book to update";
                    response.Status = false;
                }
            }
            catch (Exception ex)
            {
                response.Message = "Internal Service Error while updating.";
                response.Exception = ex;
                response.Status = false;
            }
            return response;
        }
    }
}
