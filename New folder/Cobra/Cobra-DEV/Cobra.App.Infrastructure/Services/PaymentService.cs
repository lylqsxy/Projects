using Cobra.App.Infrastructure.Contracts;
using Cobra.App.Infrastructure.Helplers;
using Cobra.Identity;
using Cobra.Infrastructure;
using Cobra.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobra.App.Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        private IRepository<PaymentMethod> _paymentMethodRepository;
        private ApplicationUserManager _userManager;

        public PaymentService(IRepository<PaymentMethod> paymentMethodRepository, ApplicationUserManager userManager)
        {
            _paymentMethodRepository = paymentMethodRepository;
            _userManager = userManager;
        }

        #region PaymentService Members
        public bool CreatePaymentMethod(PaymentMethod paymentMethod)
        {
            return _paymentMethodRepository.Add(paymentMethod, true) == null ? false : true;
        }
        public PaymentMethod GetPaymentMethodById(int id)
        {
            return _paymentMethodRepository.GetById(id);
        }
        public IEnumerable<PaymentMethod> GetPaymentMethodByLoginId(int id)
        {
            return _paymentMethodRepository.Get(x => x.LoginId == id);
        }
        public IEnumerable<PaymentMethod> GetAllPaymentMethods()
        {
            return _paymentMethodRepository.GetAll();
        }
        public void UpdatePaymentMethod(PaymentMethod paymentMethod)
        {
            _paymentMethodRepository.Update(paymentMethod, true);
            _paymentMethodRepository.Save();
        }
        public void DeletePaymentMethod(int id)
        {
            _paymentMethodRepository.Remove(GetPaymentMethodById(id), true);
            _paymentMethodRepository.Save();
        }
        public async Task<int> GetPayerLoginId(string etoken, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return 0;
            }
            if (user.IsActive && !user.EmailConfirmed && user.EmailConfirmationToken == etoken)
            {
                if (!DateTimeHelper.IsExpired(user.EmailConfirmationTokenExpiryDate))
                {
                    return user.Id;
                }
                return 0;
            }
            return 0;
        }        
        #endregion
    }
}
