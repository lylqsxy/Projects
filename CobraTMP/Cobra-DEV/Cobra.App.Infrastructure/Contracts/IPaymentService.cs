using Cobra.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobra.App.Infrastructure.Contracts
{
    public interface IPaymentService
    {
        #region CRUD
        bool CreatePaymentMethod(PaymentMethod paymentMethod);
        PaymentMethod GetPaymentMethodById(int id);
        IEnumerable<PaymentMethod> GetPaymentMethodByLoginId(int id);
        IEnumerable<PaymentMethod> GetAllPaymentMethods();
        void UpdatePaymentMethod(PaymentMethod phone);
        void DeletePaymentMethod(int id);
        Task<int> GetPayerLoginId(string etoken, string email);
        #endregion
    }
}
