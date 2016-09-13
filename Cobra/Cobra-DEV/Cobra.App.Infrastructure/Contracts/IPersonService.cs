using Cobra.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobra.App.Infrastructure.Contracts
{
    public interface IPersonService
    {
        #region CRUD 
        bool CreateContact(Person person);
        Person GetById(int id);
        void Update(Person update);
        void Delete(int id);
        #endregion

        #region Other Method
        bool ValidateContact(Person contact);
        IEnumerable<Person> GetAll();
        #endregion
    }
}
