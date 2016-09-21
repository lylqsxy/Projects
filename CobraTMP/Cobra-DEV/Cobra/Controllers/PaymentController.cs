using Cobra.App.Infrastructure.Contracts;
using Cobra.Filters;
using Cobra.Identity;
using Cobra.Model;
using Cobra.Models;
using PayPal.Exception;
using PayPal.Manager;
using PayPal.PayPalAPIInterfaceService;
using PayPal.PayPalAPIInterfaceService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Cobra.Controllers
{
    public class PaymentController : Controller
    {
        #region Data Members

        private IPaymentService _paymentService;

        #endregion Data Members

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        #region GetPaymentMethods
        // GET: Payment/PaymentOptions
        public async Task<ActionResult> PaymentOptions(string etoken, string email)
        {
            if (string.IsNullOrWhiteSpace(etoken) || string.IsNullOrWhiteSpace(email))
            {
                return View("PaymentOptionsFaliure");
            }

            var loginId = await _paymentService.GetPayerLoginId(etoken, email);
            if (loginId == 0)
            {
                return View("PaymentOptionsFaliure");
            }
            return View();
        }

        #region PayPal
        // Authorization for payment through PayPal account
        // POST: Payment/SetExpressCheckout
        [HttpPost]
        [ValidateCustomAntiForgeryToken]
        public ActionResult SetExpressCheckout(string etoken, string email)
        {
            string SolutionType = "MARK";                         // Buyer must have a PayPal account to check out. This option does not make any difference with checkout experience.
            string NoShipping = "1";                              // PayPal does not display shipping address fields whatsoever.
            string ReqConfirmShipping = "0";                      // For digital goods, this field is required, and must be 0.
            string AddressOverride = "0";                         // PayPal pages should not display the shipping address.
            string BillingType = "MERCHANTINITIATEDBILLING";      // For Reference transactions: PayPal creates a billing agreement for each transaction associated with buyer.
            string AllowNote = "0";
            string currencyCode = "USD";                          // Get currencyCode from DB
            string billingAgreementDescription = "ALEXSWIFT Emergency Contact Service";   // Get Billing agreement description from DB

            if (string.IsNullOrWhiteSpace(etoken) || string.IsNullOrWhiteSpace(email))
            {
                return Json(new
                {
                    Success = false,
                    MsgText = "Internal error. Please try again."
                });
            }
            etoken = etoken.Replace("+", "%2B");

            // Create request object
            SetExpressCheckoutRequestType request = new SetExpressCheckoutRequestType();

            // Create request details object
            SetExpressCheckoutRequestDetailsType ecDetails = new SetExpressCheckoutRequestDetailsType();

            // Replace .Url.Authority with Request.Url.Host in production environment
            ecDetails.ReturnURL = Request.Url.Scheme + "://" + Request.Url.Authority + "/Payment/GetExpressCheckoutDetails?etoken=" + etoken + "&email=" + email;
            ecDetails.CancelURL = Request.Url.Scheme + "://" + Request.Url.Authority + "/Payment/PaymentOptions?etoken=" + etoken + "&email=" + email;
            ecDetails.NoShipping = NoShipping;
            ecDetails.ReqConfirmShipping = ReqConfirmShipping;
            ecDetails.AddressOverride = AddressOverride;
            ecDetails.SolutionType = (SolutionTypeType)Enum.Parse(typeof(SolutionTypeType), SolutionType);
            ecDetails.AllowNote = AllowNote;

            // Payment details
            PaymentDetailsType paymentDetails = new PaymentDetailsType();

            double orderTotal = 0.0;
            double itemTotal = 0.0;

            // Currency code
            CurrencyCodeType currency = (CurrencyCodeType)Enum.Parse(typeof(CurrencyCodeType), currencyCode);

            paymentDetails.ItemTotal = new BasicAmountType(currency, itemTotal.ToString());
            paymentDetails.OrderTotal = new BasicAmountType(currency, orderTotal.ToString());

            ecDetails.PaymentDetails.Add(paymentDetails);

            // Set billing agreement
            BillingCodeType billingType = (BillingCodeType)Enum.Parse(typeof(BillingCodeType), BillingType);
            BillingAgreementDetailsType billingAgreementDetailsType = new BillingAgreementDetailsType(billingType);
            billingAgreementDetailsType.BillingAgreementDescription = billingAgreementDescription;
            ecDetails.BillingAgreementDetails.Add(billingAgreementDetailsType);

            request.SetExpressCheckoutRequestDetails = ecDetails;

            // Invoke the API
            SetExpressCheckoutReq requestWrapper = new SetExpressCheckoutReq()
            {
                SetExpressCheckoutRequest = request
            };

            // Create the PayPalAPIInterfaceServiceService service object to make the API call
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService(ConfigManager.Instance.GetProperties());

            try
            {
                // # API call 
                // Invoke the SetExpressCheckout method in service wrapper object
                SetExpressCheckoutResponseType response = service.SetExpressCheckout(requestWrapper);

                if (response.Ack.ToString().ToLower() == "success")
                {
                    return Json(new
                    {
                        Success = true,
                        MsgText = "Success",
                        RedirectUrl = "https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_express-checkout&token=" + response.Token
                    });
                }

                return Json(new
                {
                    Success = false,
                    MsgText = "Internal error. Please try again."
                });
            }
            catch (PayPalException)
            {
                return Json(new
                {
                    Success = false,
                    MsgText = "Internal error. Please try again."
                });
            }
        }

        // GET: Payment/GetExpressCheckoutDetails
        public ActionResult GetExpressCheckoutDetails(string etoken, string email, string token)
        {
            if (string.IsNullOrWhiteSpace(etoken) || string.IsNullOrWhiteSpace(email))
            {
                return View("PaymentOptionsFaliure");
            }
            if (string.IsNullOrWhiteSpace(token))
            {
                ViewBag.Error = true;
                ViewBag.MsgText = "Internal error. Please try again.";
                return View("PaymentOptions", new { etoken, email });
            }
            etoken = etoken.Replace("+", "%2B");

            // Create the PayPalAPIInterfaceServiceService service object to make the API call
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService(ConfigManager.Instance.GetProperties());

            // Create request object & Invoke the API
            GetExpressCheckoutDetailsReq requestWrapper = new GetExpressCheckoutDetailsReq()
            {
                GetExpressCheckoutDetailsRequest = new GetExpressCheckoutDetailsRequestType() { Token = token }
            };

            // Dev. Note:
            // The only type of exception encountered during development was ConnectionException, which should be very rare.
            // A try-catch inside catch block will ensure another attempt with a high degree of success.
            try
            {
                // # API call 
                // Invoke the GetExpressCheckoutDetails method in service wrapper object
                GetExpressCheckoutDetailsResponseType response = service.GetExpressCheckoutDetails(requestWrapper);

                if (response.Ack.ToString().ToLower() == "success")
                {
                    //response.Ack.ToString()
                    //response.CorrelationID
                    //response.Timestamp
                    //response.GetExpressCheckoutDetailsResponseDetails.Token
                    return RedirectToAction("CreateBillingAgreement", new { etoken, email, token = response.GetExpressCheckoutDetailsResponseDetails.Token });
                }

                ViewBag.Error = true;
                ViewBag.MsgText = "Internal error. Please try again.";
                return View("PaymentOptions", new { etoken, email });
            }
            catch (PayPalException)
            {
                try
                {
                    GetExpressCheckoutDetailsResponseType response = service.GetExpressCheckoutDetails(requestWrapper);

                    if (response.Ack.ToString().ToLower() == "success")
                    {
                        //response.Ack.ToString()
                        //response.CorrelationID
                        //response.Timestamp
                        //response.GetExpressCheckoutDetailsResponseDetails.Token
                        return RedirectToAction("CreateBillingAgreement", new { etoken, email, token = response.GetExpressCheckoutDetailsResponseDetails.Token });
                    }

                    ViewBag.Error = true;
                    ViewBag.MsgText = "Internal error. Please try again.";
                    return View("PaymentOptions", new { etoken, email });
                }
                catch (PayPalException)
                {
                    ViewBag.Error = true;
                    ViewBag.MsgText = "Internal error. Please try again.";
                    return View("PaymentOptions", new { etoken, email });
                }
            }
        }

        public async Task<ActionResult> CreateBillingAgreement(string etoken, string email, string token)
        {
            if (string.IsNullOrWhiteSpace(etoken) || string.IsNullOrWhiteSpace(email))
            {
                return View("PaymentOptionsFaliure");
            }
            if (string.IsNullOrWhiteSpace(token))
            {
                ViewBag.Error = true;
                ViewBag.MsgText = "Internal error. Please try again.";
                return View("PaymentOptions", new { etoken, email });
            }
            etoken = etoken.Replace("%2B", "+");

            var loginId = await _paymentService.GetPayerLoginId(etoken, email);
            if (loginId == 0)
            {
                return View("PaymentOptionsFaliure");
            }

            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService(ConfigManager.Instance.GetProperties());

            CreateBillingAgreementReq requestWrapper = new CreateBillingAgreementReq()
            {
                CreateBillingAgreementRequest = new CreateBillingAgreementRequestType() { Token = token }
            };

            // Dev. Note:
            // The only type of exception encountered during development was ConnectionException, which should be very rare.
            // A try-catch inside catch block will ensure another attempt with a high degree of success.
            try
            {
                CreateBillingAgreementResponseType response = service.CreateBillingAgreement(requestWrapper);
                if (response.Ack.ToString().ToLower() == "success")
                {
                    var paymentMethod = new PaymentMethod()
                    {
                        LoginId = loginId,
                        Ack = response.Ack.ToString(),
                        CorrelationID = response.CorrelationID,
                        TransactionID = response.BillingAgreementID,
                        PaymentMode = "PayPal",
                        CreatedOn = DateTime.Parse(response.Timestamp)
                    };
                    _paymentService.CreatePaymentMethod(paymentMethod);
                    return RedirectToAction("ConfirmEmail", "Account", new { etoken, email });
                }
                ViewBag.Error = true;
                ViewBag.MsgText = "Internal error. Please try again.";
                return View("PaymentOptions", new { etoken, email });
            }
            catch (PayPalException)
            {
                try
                {
                    CreateBillingAgreementResponseType response = service.CreateBillingAgreement(requestWrapper);
                    if (response.Ack.ToString().ToLower() == "success")
                    {
                        var paymentMethod = new PaymentMethod()
                        {
                            LoginId = loginId,
                            Ack = response.Ack.ToString(),
                            CorrelationID = response.CorrelationID,
                            TransactionID = response.BillingAgreementID,
                            PaymentMode = "PayPal",
                            CreatedOn = DateTime.Parse(response.Timestamp)
                        };
                        _paymentService.CreatePaymentMethod(paymentMethod);
                        return RedirectToAction("ConfirmEmail", "Account", new { etoken, email });
                    }
                    ViewBag.Error = true;
                    ViewBag.MsgText = "Internal error. Please try again.";
                    return View("PaymentOptions", new { etoken, email });
                }
                catch (PayPalException)
                {
                    ViewBag.Error = true;
                    ViewBag.MsgText = "Internal error. Please try again.";
                    return View("PaymentOptions", new { etoken, email });
                }
            }
        }
        #endregion

        #region Card
        // Credit card verification, and obtain payment authorization
        // IMPORTANT: Merchant Account settings on PayPal must set CVV2/CSC/CID (Card security code) field as required, to enable CVV2 verification.
        //            Otherwise only card number would be verified, but not card security code.
        // POST: Payment/DoDirectPayment
        [HttpPost]
        [ValidateCustomAntiForgeryToken]
        public async Task<ActionResult> DoDirectPayment(CreditCardModel cardModel, string etoken, string email)
        {
            if (ModelState.IsValid && !string.IsNullOrWhiteSpace(etoken) && !string.IsNullOrWhiteSpace(email))
            {
                string currencyCode = "USD";            // Get currencyCode from DB
                string paymentAction = "AUTHORIZATION"; // This is a final sale for which payment is requested.


                var loginId = await _paymentService.GetPayerLoginId(etoken, email);
                if (loginId == 0)
                {
                    return View("PaymentOptionsFaliure");
                }

                etoken = etoken.Replace("+", "%2B");
                
                // DoDirectPayment Request
                DoDirectPaymentRequestType request = new DoDirectPaymentRequestType();
                DoDirectPaymentRequestDetailsType requestDetails = new DoDirectPaymentRequestDetailsType();
                requestDetails.PaymentAction = (PaymentActionCodeType)Enum.Parse(typeof(PaymentActionCodeType), paymentAction);

                CreditCardDetailsType cardDetails = new CreditCardDetailsType();

                PayerInfoType payer = new PayerInfoType();
                PersonNameType name = new PersonNameType()
                {
                    FirstName = cardModel.FirstName,
                    LastName = cardModel.LastName
                };
                payer.PayerName = name;

                cardDetails.CardOwner = payer;
                cardDetails.CreditCardNumber = cardModel.CardNumber;
                cardDetails.CreditCardType = (CreditCardTypeType)Enum.Parse(typeof(CreditCardTypeType), cardModel.CardType);
                cardDetails.CVV2 = cardModel.CVV2;
                cardDetails.ExpMonth = cardModel.ExpiryMonth;
                cardDetails.ExpYear = cardModel.ExpiryYear;
                requestDetails.CreditCard = cardDetails;

                CurrencyCodeType currency = (CurrencyCodeType)Enum.Parse(typeof(CurrencyCodeType), currencyCode);
                PaymentDetailsType paymentDetails = new PaymentDetailsType() { OrderTotal = new BasicAmountType(currency, "0.00") };
                //BasicAmountType paymentAmount = new BasicAmountType(currency, "0.00");  // No amount charged while verifying card

                requestDetails.PaymentDetails = paymentDetails;

                request.DoDirectPaymentRequestDetails = requestDetails;

                // DoDirectPayment request wrapper
                DoDirectPaymentReq requestWrapper = new DoDirectPaymentReq()
                {
                    DoDirectPaymentRequest = request
                };

                // API service
                PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService(ConfigManager.Instance.GetProperties());

                // Dev. Note:
                // The only type of exception encountered during development was ConnectionException, which should be very rare.
                // PayPal.Exception.ConnectionException: Invalid HTTP response The remote name could not be resolved: 'api-3t.sandbox.paypal.com'
                // A try-catch inside catch block will ensure another attempt with a high degree of success.
                try
                {
                    // API response
                    DoDirectPaymentResponseType response = service.DoDirectPayment(requestWrapper);

                    // If CVV2 code verification is not implemented in PayPal merchant account,
                    // the CVV2 code will not be verified even though the response CVV2Code value would be "M".
                    // In such case response would contain more than one errors.
                    if (response.Ack.ToString().ToLower() == "successwithwarning" && response.Errors.Count() == 1 && response.CVV2Code == "M")
                    {
                        var paymentMethod = new PaymentMethod()
                        {
                            LoginId = loginId,
                            Ack = response.Ack.ToString(),
                            CorrelationID = response.CorrelationID,
                            TransactionID = response.TransactionID,
                            PaymentMode = "Card",
                            CreatedOn = DateTime.Parse(response.Timestamp)
                        };
                        _paymentService.CreatePaymentMethod(paymentMethod);
                        //response.CVV2Code
                        //response.Errors[0].ShortMessage
                        return Json(new
                        {
                            Success = true,
                            RedirectUrl = "/Account/ConfirmEmail?etoken=" + etoken + "&email=" + email
                        });
                    }

                    //Errors
                    return Json(new
                    {
                        Success = false,
                        MsgText = "Internal error.",
                        Errors = "Internal error. Please try again."
                    });
                }
                catch (PayPalException)
                {
                    try
                    {
                        DoDirectPaymentResponseType response = service.DoDirectPayment(requestWrapper);
                        if (response.Ack.ToString().ToLower() == "successwithwarning" && response.Errors.Count() == 1 && response.CVV2Code == "M")
                        {
                            var paymentMethod = new PaymentMethod()
                            {
                                LoginId = loginId,
                                Ack = response.Ack.ToString(),
                                CorrelationID = response.CorrelationID,
                                TransactionID = response.TransactionID,
                                PaymentMode = "Card",
                                CreatedOn = DateTime.Parse(response.Timestamp)
                            };
                            _paymentService.CreatePaymentMethod(paymentMethod);
                            //response.CVV2Code
                            //response.Errors[0].ShortMessage
                            return Json(new
                            {
                                Success = true,
                                RedirectUrl = "/Account/ConfirmEmail?etoken=" + etoken + "&email=" + email
                            });
                        }
                        return Json(new
                        {
                            Success = false,
                            MsgText = "Internal error.",
                            Errors = "Internal error. Please try again."
                        });
                    }
                    catch (PayPalException)
                    {
                        return Json(new
                        {
                            Success = false,
                            MsgText = "Internal error.",
                            Errors = "Internal error. Please try again."
                        });
                    }
                }
            }
            var errorList = ModelState.Values.SelectMany(m => m.Errors)
                                 .Select(e => e.ErrorMessage)
                                 .ToList();
            return Json(new
            {
                Success = false,
                MsgText = "Error. Invalid input set.",
                Errors = errorList
            });
        }
        #endregion

        #endregion



        // Do transaction
        // POST: Payment/DoReferenceTransaction
        [HttpPost]
        [ValidateCustomAntiForgeryToken]
        //[Authorize(Roles ="Call_Centre_User, administrator")]
        public ActionResult DoReferenceTransaction(TransactionViewModel transactionViewModel)
        {
            string currencyCode = "USD";    // Get currencyCode from DB
            string taxTotal = "0.00";       // Get tax details from DB
            //string itemName = "Item 1";
            //string itemAmount = "1.00";
            //string itemDescription = "127 single-byte alphanumeric characters ONLY";
            //string orderDescription = "127 single-byte alphanumeric characters ONLY";
            string itemCategory = "DIGITAL";    // Digital good (subscription, etc.).
            string paymentAction = "SALE";      // This is a final sale for which payment is requested.

            DoReferenceTransactionRequestDetailsType transactionDetails = new DoReferenceTransactionRequestDetailsType();
            transactionDetails.ReferenceID = transactionViewModel.OrderModel.ReferenceId;
            transactionDetails.PaymentAction = (PaymentActionCodeType)Enum.Parse(typeof(PaymentActionCodeType), paymentAction);

            // Payment details
            PaymentDetailsType paymentDetails = new PaymentDetailsType();
            double orderTotal = 0.0;
            double itemTotal = 0.0;
            CurrencyCodeType currency = (CurrencyCodeType)Enum.Parse(typeof(CurrencyCodeType), currencyCode);

            // Tax amount
            paymentDetails.TaxTotal = new BasicAmountType(currency, taxTotal);
            orderTotal += Convert.ToDouble(taxTotal);
            // Order description
            paymentDetails.OrderDescription = transactionViewModel.OrderModel.OrderDescription;

            // Item details
            foreach (var item in transactionViewModel.ItemModel)
            {
                PaymentDetailsItemType itemDetails = new PaymentDetailsItemType()
                {
                    Name = item.ItemName,
                    Amount = new BasicAmountType(currency, item.Price),
                    Quantity = 1,
                    ItemCategory = (ItemCategoryType)Enum.Parse(typeof(ItemCategoryType), itemCategory),
                    Description = item.Description
                };
                itemTotal += (Convert.ToDouble(itemDetails.Amount.value) * itemDetails.Quantity.Value);
                paymentDetails.PaymentDetailsItem.Add(itemDetails);
            }
            
            orderTotal += itemTotal;
            paymentDetails.ItemTotal = new BasicAmountType(currency, itemTotal.ToString());
            paymentDetails.OrderTotal = new BasicAmountType(currency, orderTotal.ToString());

            transactionDetails.PaymentDetails = paymentDetails;

            DoReferenceTransactionRequestType request = new DoReferenceTransactionRequestType();
            request.DoReferenceTransactionRequestDetails = transactionDetails;

            DoReferenceTransactionReq requestWrapper = new DoReferenceTransactionReq() { DoReferenceTransactionRequest = request };
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService(ConfigManager.Instance.GetProperties());
            DoReferenceTransactionResponseType response = service.DoReferenceTransaction(requestWrapper);
            if (response.Ack.ToString().ToLower() == "success")
            {
                // PaymentMethodId
                // ProfileId
                //response.Ack.ToString()
                //response.CorrelationID
                //response.Timestamp
                //response.DoReferenceTransactionResponseDetails.TransactionID
                //response.DoReferenceTransactionResponseDetails.PaymentInfo.PaymentStatus.ToString()
                //response.DoReferenceTransactionResponseDetails.PaymentInfo.PendingReason.ToString()

                return View();
            }

            var errorList = new List<string>();

            // CVV2 Response Codes, refer https://developer.paypal.com/docs/classic/api/AVSResponseCodes/ for more details
            //response.DoReferenceTransactionResponseDetails.CVV2Code   // only for credit card

            // For details on the meanings of Payment Advice codes, see:
            // https://cms.paypal.com/us/cgi-bin/?&cmd=_render-content&content_ID=merchant/cc_compliance_error_codes
            var pac = response.DoReferenceTransactionResponseDetails.PaymentAdviceCode;
            switch (pac)
            {
                case "01":
                    errorList.Add("Expired Card Account. Please obtain new account information.");
                    break;
                case "02":
                    errorList.Add("Over Credit Limit, or insufficient funds.");
                    break;
                case "03":
                case "21":
                    errorList.Add("Stop current, and all future recurring payment requests. Either the " +
                        "account was closed, fraud was involved, or the card holder has asked their bank " +
                        "to stop this payment for another reason. Obtain another payment method from customer.");
                    break;
            }

            foreach (var error in response.Errors)
            {
                errorList.Add(error.LongMessage);
            }
            return Json(new
            {
                Success = false,
                MsgText = "Transaction failed.",
                Errors = errorList
            });
        }

        [HttpPost]
        [ValidateCustomAntiForgeryToken]
        //[Authorize(Roles ="Call_Centre_User, administrator")]
        public JsonResult GetPaymentMethod(int loginId)
        {
            var model = _paymentService.GetPaymentMethodByLoginId(loginId).Select(x => new {
                PaymentMethodId = x.Id,
                LoginId = x.LoginId,
                PaymentMode = x.PaymentMode,
                ReferenceId = x.TransactionID,
                CreatedOn = x.CreatedOn }).ToList();
            return Json(model);
        }
    }
}