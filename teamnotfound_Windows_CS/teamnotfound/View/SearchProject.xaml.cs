using Microsoft.WindowsAzure.MobileServices;
using System;
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

namespace teamnotfound
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SearchProject : Page
    {
        private IMobileServiceTable<Project> projectTable = App.MobileService.GetTable<Project>();
        private MobileServiceCollection<Project, Project> items;
        private IMobileServiceTable<Category> categoryTable = App.MobileService.GetTable<Category>();
        private MobileServiceCollection<Category, Category> category;
        private IMobileServiceTable<Bid> bidTable = App.MobileService.GetTable<Bid>();
        private MobileServiceCollection<Bid, Bid> bids;
        string parameter;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            parameter = e.Parameter as string;
            //Debug.Write("param1: " + parameter);
            getProjects(parameter);
        }
        public SearchProject()
        {
            this.InitializeComponent();
           // Debug.Write("param: " + parameter);
            //getProjects();
        }
        private async void getProjects(String parameter)
        {
            
            string s = parameter.Split('(')[0];
            s = s.TrimEnd();
            category = await categoryTable
               .Where(Category => Category.Name == s)
                   .ToCollectionAsync();

            List<Category> categ = new List<Category>();
            categ = category.ToList();
           // Debug.Write("S: "+s);
            items = await projectTable
                    .Where(Project => Project.Type == categ[0].Id)
                    .ToCollectionAsync();
            gridView.ItemsSource = items;
        }
        
        private async void Project_Tapped(object sender, TappedRoutedEventArgs e)
        {
            List<String> param = new List<String>();
            string text = ((sender as StackPanel).FindName("projectId") as TextBlock).Text;
            param.Add(text);
            bids = await bidTable
                    .Where(Bid => Bid.ProjectId == text)
                    .Where(Bid => Bid.Bidder == "diksha.bajaj@hpe.com")           //Bidder should come from global.cs
                    .ToCollectionAsync();
            List<Bid> bidList = new List<Bid>();
            bidList = bids.ToList();
            if (bidList.Count == 0)    // The user has not bid for this project before
            {
                Debug.Write("Add Bid");
                param.Add("Add");
            }
            else                  // The user wants to update the bid for this project
            {
                Debug.Write("Update Bid");
                param.Add("Update");
            }
            Debug.Write("Text: " + text);
            Frame.Navigate(typeof(Bidding),param);
        }
        
    }
}
