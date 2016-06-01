using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using teamnotfound.DataModel;
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
    public sealed partial class AllBids : Page
    {
        public AllBids()
        {
            this.InitializeComponent();
          

        }

        string[] bid;
        List<CountryBasedBidCount> countryBidCount = new List<CountryBasedBidCount>();
       

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string event1 = e.Parameter as String;
            bid = event1.Split('|');
            Debug.Write("param1: " + bid[0]+"   "+bid[1]+" "+bid[2]);
            //getProjects(parameter);
            //createCountry();
            getCountry();

        }
        List<ShowUserBid> allbids = new List<ShowUserBid>();
        IMobileServiceTable<Bid> bidTable = App.MobileService.GetTable<Bid>();
        private async void getCountry()
        {

            allbids.Clear();
            MobileServiceCollection<Bid, Bid> bids = await bidTable.Where(Bid => Bid.EventId == bid[0].ToString()  && Bid.Countr == bid[1].ToString() ).ToCollectionAsync();


            foreach (var _bid in bids)
            {

                ShowUserBid b = new ShowUserBid();

                b.username = _bid.Bidder;
                b.bid = _bid.BiddAmt;
                b.eventid = _bid.EventId;
                b.bidId = _bid.Id;
                if(bid[2]=="Open")
                {
                    b.showButton = "Visible";
                    b.showAssigned = "Collapsed";
                }
                else
                {
                    b.showButton = "Collapsed";
                    b.showAssigned = "Collapsed";
                    if (_bid.Status!=null && _bid.Status=="Accepted")
                    {
                        b.showAssigned = "Visible";
                    }
                }
                allbids.Add(b);
                Debug.WriteLine("Bidder  "+b.username+ "  Bid"+b.bid);
            }




            allBidsList.ItemsSource = allbids;


        }

        private async void StackPanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var selected = allBidsList.SelectedIndex;
            Debug.WriteLine(selected + "  ");
            ShowUserBid selectedBid = allbids.ElementAt(selected);


            for (int i=0; i < allbids.Count; i++)
            {
                ShowUserBid _bid = allbids.ElementAt(i);
                string status = "Rejected";
                if(_bid.username == selectedBid.username)
                {
                    status = "Accepted";
                }

                Bid bidding = new Bid { Id = _bid.bidId, BiddAmt = _bid.bid, Bidder = _bid.username, EventId = bid[0], Countr = bid[1], Status = status};
                await bidTable.UpdateAsync(bidding);
                getCountry();
            }

        }
    }
}
