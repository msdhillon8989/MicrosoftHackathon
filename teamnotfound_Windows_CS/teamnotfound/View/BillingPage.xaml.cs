using Stripe;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using teamnotfound.Common;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace teamnotfound.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BillingPage : Page
    {
        public BillingPage()
        {
            this.InitializeComponent();
        }
        private void Proceed_Click(object sender, RoutedEventArgs e)
        {
            Button1_Click();
        }
        private void Button1_Click()
        {
            StripeCustomer current = GetCustomer();
            int chargetotal = 100;//Convert.ToInt32((3.33*Convert.ToInt32(days)*100));
            var mycharge = new StripeChargeCreateOptions();
            mycharge.Amount=chargetotal;
            mycharge.Currency = "USD";
            mycharge.CustomerId = current.Id;
            string key = "sk_test_CQT723mQ9B3Qhzs7pxUtINLv";
            var chargeservice = new StripeChargeService(key);
            StripeCharge currentcharge = chargeservice.Create(mycharge);
          //  currentcharge.
        }
        private StripeCustomer GetCustomer()
        {
            var mycust = new StripeCustomerCreateOptions();
            SourceCard card = new SourceCard();
            card.Number = card1.Text + card2.Text + card3.Text + card4.Text;
            card.Name = nameOnCard.Text;
            card.ExpirationMonth = month.Text;
            card.ExpirationYear = year.Text;
            card.AddressCountry = "USA";
            card.ReceiptEmail = (string)Global.GetRepositoryValue("userName");
            card.AddressCity = "abc";
            mycust.Email = (string)Global.GetRepositoryValue("userName");
            card.Cvc = cvv.Text;
            mycust.PlanId = "123456";
           // mycust.TrialEnd =
            mycust.Description = "Rahul Pandey(rahul@gmail.com)";
            mycust.SourceCard = card;
          //  mycust.SourceCard = "USA";

          //  mycust.CardExpirationMonth = "10";
          //  mycust.CardExpirationYear = "2016";
            // mycust.PlanId = "100";
          
            //mycust.TrialEnd = getrialend();
            var customerservice = new StripeCustomerService("sk_test_CQT723mQ9B3Qhzs7pxUtINLv");
            return customerservice.Create(mycust);
        }
    }
}
