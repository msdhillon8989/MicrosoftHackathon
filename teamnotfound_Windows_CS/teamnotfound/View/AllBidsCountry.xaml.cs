using System;
using Windows.UI.Xaml.Controls;
using teamnotfound.DataModel;
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;
using Microsoft.WindowsAzure.MobileServices;
using System.Collections;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace teamnotfound.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AllBidsCountry : Page
    {
        string event1;
        public AllBidsCountry()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            event1 = e.Parameter as String;
            Debug.Write("param1: " + event1);
            //getProjects(parameter);
            //createCountry();
            getCountry();

        }

        private async void getCountry()
        {
            IMobileServiceTable<Bid> bidTable = App.MobileService.GetTable<Bid>();
            Hashtable ht = new Hashtable();
            Hashtable stht = new Hashtable();
            MobileServiceCollection<Bid, Bid> bids = await bidTable.Where(Bid => Bid.EventId == event1.ToString()).ToCollectionAsync();
            
            foreach (var _bid in bids)
            {
                var country = _bid.Countr;
                var count = ht[country];
                if (stht[country] == null)
                {
                    stht[country] = "Open";
                }

                if(_bid.Status!=null && _bid.Status.ToString() == "Accepted")
                {
                    stht[country] = "Assigned";
                }

                if(count==null)
                {
                    ht.Add(country, 1);
                }
                else
                {
                    var x = Int32.Parse(count.ToString()) + 1;
                    ht.Add(country, x);
                }
            }

            List<CountryBasedBidCount> countryBidCount = new List<CountryBasedBidCount>();

            ICollection key = ht.Keys;

            foreach (string k in key)
            {
                Debug.WriteLine(k + ": " + ht[k]);
                CountryBasedBidCount cc = new CountryBasedBidCount();
                cc.country = k;
                cc.count = ht[k].ToString();
                cc.Status = stht[k].ToString();


                countryBidCount.Add(cc);
            }

            countryBidCountlist.ItemsSource = countryBidCount;


        }
}
}
