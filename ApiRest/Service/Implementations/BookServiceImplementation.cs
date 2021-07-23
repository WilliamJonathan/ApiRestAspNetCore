using ApiRest.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiRest.Repository;
using ApiRest.Data.Converter.Implementations;
using ApiRest.Data.VO;

namespace ApiRest.Service.Implementations
{
    public class BookServiceImplementation : IBookService
    {
        private readonly IRepository<Book> _repository;

        private readonly BookConverter _converter;

        public BookServiceImplementation(IRepository<Book> repository)
        {
            _repository = repository;
            _converter = new BookConverter();
        }

        public async Task<List<BookVO>> FindAllAsync()
        {
            return _converter.Parse(await _repository.FindAllAsync());
        }

        public async Task<BookVO> FindByIDAsync(long id)
        {
            return _converter.Parse(await _repository.FindByIDAsync(id));

        }

        public async Task<BookVO> CreateAsync(BookVO books)
        {
            var bookEntity = _converter.Parse(books);
            bookEntity = await _repository.CreateAsync(bookEntity);
            return _converter.Parse(bookEntity);
        }

        public async Task<BookVO> UpdateAsync(BookVO books)
        {
            var bookEntity = _converter.Parse(books);
            bookEntity = await _repository.UpdateAsync(bookEntity);
            return _converter.Parse(bookEntity);
        }

        public async Task DeleteAsync(long id)
        {
           await _repository.DeleteAsync(id);
        }
    }
}
