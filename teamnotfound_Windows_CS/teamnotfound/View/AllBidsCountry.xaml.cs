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
        List<CountryBasedBidCount> countryBidCount = new List<CountryBasedBidCount>();
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
        Hashtable stht = new Hashtable();
        private async void getCountry()
        {
            IMobileServiceTable<Bid> bidTable = App.MobileService.GetTable<Bid>();
            Hashtable ht = new Hashtable();
           
            MobileServiceCollection<Bid, Bid> bids = await bidTable.Where(Bid => Bid.EventId == event1.ToString()).ToCollectionAsync();
            
            foreach (var _bid in bids)
            {
                var country = _bid.Countr;
                var count = ht[country];
                if (!stht.ContainsKey(country))
                {
                    stht[country] = "Open";
                }

                if(_bid.Status!=null && _bid.Status.ToString() == "Accepted")
                {
                    stht[country] = "Assigned";
                }

                if(!ht.ContainsKey(country))
                {
                    ht.Add(country, 1);
                }
                else
                {
                    
                    var x = Int32.Parse(count.ToString()) + 1;
                    ht[country] = x;
                }
            }

          

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

        private void StackPanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var selected = countryBidCountlist.SelectedIndex;
            CountryBasedBidCount eventcountry = countryBidCount.ElementAt(selected);

            Frame.Navigate(typeof(AllBids), event1+"|"+eventcountry.country+"|"+stht[eventcountry.country].ToString());
        }
    }
}
