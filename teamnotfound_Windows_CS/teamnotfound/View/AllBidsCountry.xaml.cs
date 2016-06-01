using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using teamnotfound.DataModel;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
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

        private void getCountry()
        {
             IMobileServiceTable<Bid> bidTable = App.MobileService.GetTable<Bid>();
            Hashtable ht = new Hashtable();

        }
    }
}
