using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using teamnotfound.Common;
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
    public sealed partial class MyPost : Page
    {
        public MyPost()
        {
            this.InitializeComponent();
            getProjects();
        }

        private IMobileServiceTable<Event> eventTable = App.MobileService.GetTable<Event>();
        private IMobileServiceTable<Bid> bidTable = App.MobileService.GetTable<Bid>();
        private MobileServiceCollection<Event, Event> items;
        private MobileServiceCollection<Bid, Bid> bids;
        private List<EventBidCount> eventCount = new List<EventBidCount>();

        //   private MobileServiceCollection<, Project> items;




        private async void getProjects()
        {


            items = await eventTable .Where(Event => Event.Status=="Bidding").ToCollectionAsync();

             
           
            foreach (var _event in items)
            {
                
               
                Debug.Write(_event.Id);
                bids = await bidTable.Where(Bid => Bid.EventId == _event.Id.ToString()).ToCollectionAsync();
                
                Debug.Write("    count "+bids.Count+"    ");
                var count = new EventBidCount { Event = _event, BidCount = (int)bids.Count};
                eventCount.Add(count);

            }
            allevents.ItemsSource = eventCount;
            //gridView.ItemsSource = items;
            //  Global.SetRepositoryValue("MyPost", projectCount);


        }

        private void StackPanel_Tapped_1(object sender, TappedRoutedEventArgs e)
        {
            var selected = allevents.SelectedIndex;
            EventBidCount event1 = eventCount.ElementAt(selected);

            Frame.Navigate(typeof(AllBidsCountry),event1.Event.Id);
        }
    }
}
