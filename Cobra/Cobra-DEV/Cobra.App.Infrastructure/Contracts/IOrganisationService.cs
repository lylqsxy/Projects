using Cobra.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobra.App.Infrastructure.Contracts
{
    public interface IOrganisationService
    {
        #region CRUD 
        bool CreateOrganisation(Organisation organisation);
        Organisation GetById(int id);
        void Update(Organisation organisation);
        void Delete(int id);

        #endregion

        bool ValidateOrganisation(string organisationName);        
        Organisation GetOrganisationByName(string name);
        //Author: Aaron Bhardwaj
        IEnumerable<Organisation> GetAllOrganisation();
    }
}
