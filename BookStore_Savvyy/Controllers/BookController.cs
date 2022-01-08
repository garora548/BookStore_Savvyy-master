using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore_Domain.Interfaces;
using BookStore_Domain.Models;
using BookStore_Infrastructure.ApplicationDbContext;
using BookStore_Service.Features.BookStore.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookStore_Savvyy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {        
        
        private readonly IBookService _bookService;
        private IMediator _mediator;
        public BookController(IBookService bookService, IMediator mediator)
        {            
            _bookService = bookService;
            _mediator = mediator;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            Response<List<Book>> response = new Response<List<Book>>();
            try
            {
                response = _bookService.GetAllBooks();               
            }
            catch (Exception ex)
            {
                response.Message = "Issue while getting books " + ex.Message;
                response.Status = false;
                response.Data = null;
            }
            return Ok(response);
        }

        [HttpPost]
        public ActionResult Save([FromBody]CreateBookCommand book)
        {
            Response<int> response = new Response<int>();
            try
            {
                response = _mediator.Send(book).GetAwaiter().GetResult();                           
            }
            catch (Exception ex)
            {
                response.Message = "Issue while Saving books " + ex.Message;
                response.Status = false;                
            }

            return Ok(response);
        }

        [HttpGet("GetById")]
        public ActionResult GetById(int Id)
        {
            Response<Book> response = new Response<Book>();
            try
            {
                response = _bookService.GetBookById(Id);
            }
            catch (Exception ex)
            {
                response.Message = "Issue while getting book " + ex.Message;
                response.Status = false;
                response.Data = null;
            }
            return Ok(response);
        }

        [HttpPut]
        public ActionResult Update([FromBody] Book book)
        {
            Response<int> response = new Response<int>();
            try
            {
                response = _bookService.UpdateBook(book);
            }
            catch (Exception ex)
            {
                response.Message = "Issue while getting book " + ex.Message;
                response.Status = false;                
            }
            return Ok(response);
        }

        [HttpDelete]
        public ActionResult Delete(int Id)
        {
            Response<int> response = new Response<int>();
            try
            {
                response = _bookService.DeleteBook(Id);              
            }
            catch (Exception ex)
            {
                response.Message = "Issue while getting book " + ex.Message;
                response.Status = false;                
            }
            return Ok(response);
        }

    }
}
