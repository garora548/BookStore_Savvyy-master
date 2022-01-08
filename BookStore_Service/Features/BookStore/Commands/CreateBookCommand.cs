using BookStore_Domain.Interfaces;
using BookStore_Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore_Service.Features.BookStore.Commands
{
    public class CreateBookCommand : IRequest<Response<int>>
    {
        public string Title { get; set; }
        public string Description { get; set; }        
        public string Author { get; set; }
        public string CoverImage { get; set; }        
        public decimal Price { get; set; }

        public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Response<int>>
        {
            private readonly IBookService _bookService;

            public CreateBookCommandHandler(IBookService bookService)
            {
                _bookService = bookService;
            }

            public async Task<Response<int>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
            {
                Book book = new Book();
                book.Title = request.Title;
                book.Description = request.Description;
                book.Author = request.Author;
                book.CoverImage = request.CoverImage;
                book.Price = request.Price;

                var resp = await _bookService.SaveBook(book);

                return resp;
            }
        }

    }
}
