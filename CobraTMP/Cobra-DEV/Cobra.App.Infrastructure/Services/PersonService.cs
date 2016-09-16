using Cobra.App.Infrastructure.Contracts;
using Cobra.Infrastructure;
using Cobra.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobra.App.Infrastructure.Services
{
    public class PersonService : IPersonService
    {
        #region Data Members 
        private IRepository<Person> _personRepository;

        #endregion
        public PersonService(IRepository<Person> personRepository)
        {
            _personRepository = personRepository;
        }

        #region IPersonService Members

        public bool CreateContact(Person person)
        {
            return _personRepository.Add(person, true) == null ? false : true;
        }

        public Person GetById(int id)
        {
            return _personRepository.GetById(id);
        }

        public void Update(Person update)
        {
            _personRepository.Update(update);
        }

        public void Delete(int id)
        {
            _personRepository.Remove(id);
        }

        public bool ValidateContact(Person person)
        {
            return _personRepository.GetById(person.Id) == null ? false : true;
        }

        public IEnumerable<Person> GetAll()
        {
            return _personRepository.GetAll().ToList();
        }

        #endregion
    }
}
