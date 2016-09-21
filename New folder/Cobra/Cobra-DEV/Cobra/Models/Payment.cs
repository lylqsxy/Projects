using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cobra.Models
{
    public class CreditCardModel
    {
        public string CardNumber { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CVV2 { get; set; }
        public string CardType { get; set; } //Valid types are: VISA, MASTERCARD, DISCOVER, AMEX
    }

    public class TransactionViewModel
    {
        public List<ItemModel> ItemModel { get; set; }
        public OrderModel OrderModel { get; set; }
    }

    #region Models for ViewModel
    public class ItemModel
    {
        public string ItemName { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
    }

    public class OrderModel
    {
        public string ReferenceId { get; set; }
        public string OrderDescription { get; set; }
        public int LoginId { get; set; }
        public int PaymentMethodId { get; set; }
    }
    #endregion
}