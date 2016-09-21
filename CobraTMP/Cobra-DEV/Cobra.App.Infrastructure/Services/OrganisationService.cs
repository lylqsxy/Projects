using Cobra.App.Infrastructure.Contracts;
using Cobra.Infrastructure;
using Cobra.Infrastructure.Data;
using Cobra.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobra.App.Infrastructure.Services
{
    public class OrganisationService : IOrganisationService
    {
        #region Private Members 
        private IRepository<Organisation> _organisationRepository;

        #endregion
        public OrganisationService(IRepository<Organisation> organisationRepository)
        {
            _organisationRepository = organisationRepository;
        }

        #region IOrganisationService Members

        public int CreateOrganisation(Organisation organisation)
        {
            _organisationRepository.Add(organisation, true);
            try
            {
                DbWorkManager.Commit();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return organisation.Id;
            //return _organisationRepository.Add(organisation, true) == null ? false : true;

        }

        public Organisation GetById(int id)
        {
            return _organisationRepository.GetById(id);
        }

        public void Update(Organisation organisation)
        {
            _organisationRepository.Update(organisation, true);
            _organisationRepository.Save();
        }

        public void Delete(int id)
        {
            _organisationRepository.Remove(id, true);
        }

        public bool ValidateOrganisation(string organisationName)
        {
           return GetOrganisationByName(organisationName) == null? false: true;
        }

        public Organisation GetOrganisationByName(string name)
        {
            return _organisationRepository.GetByName(name);
        }

        //Author: Aaron Bhardwaj
        public IEnumerable<Organisation> GetAllOrganisation()
        {
            return _organisationRepository.GetAll();
        }

        #endregion
    }
}
