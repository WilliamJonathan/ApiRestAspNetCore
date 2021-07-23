using ApiRest.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiRest.Repository;
using ApiRest.Data.Converter.Implementations;
using ApiRest.Data.VO;

/*
 * regras de negocio serão criadas aqui,
 * Ex: validar se quer cadastrar pessoas nascidas a partir de 1900,
 * aqui seria implementado esta regra
 */

namespace ApiRest.Service.Implementations
{
    public class PersonServiceImplementation : IPersonService
    {
        private readonly IRepository<Person> _repository;

        private readonly PersonConverter _converter;

        public PersonServiceImplementation(IRepository<Person> repository, PersonConverter converter)
        {
            _repository = repository;
            /*
             * Se inicializar aqui não precisa injetar a dependencia
             */
            //_converter = new PersonConverter();
            _converter = converter;

        }

        public async Task<List<PersonVO>> FindAllAsync()
        {
            return _converter.Parse(await _repository.FindAllAsync());
        }

        public async Task<PersonVO> FindByIDAsync(long id)
        {
            return _converter.Parse( await _repository.FindByIDAsync(id));
        }

        public async Task<PersonVO> CreateAsync(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = await _repository.CreateAsync(personEntity);
            return _converter.Parse(personEntity);
        }

        public async Task<PersonVO> UpdateAsync(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = await _repository.UpdateAsync(personEntity);
            return _converter.Parse(personEntity);
        }

        public async Task DeleteAsync(long id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
