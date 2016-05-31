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
    public sealed partial class MyProjects : Page
    {

        private IMobileServiceTable<Project> projectTable = App.MobileService.GetTable<Project>();
        private MobileServiceCollection<Project, Project> projects;
        private IMobileServiceTable<Category> categoryTable = App.MobileService.GetTable<Category>();
        private MobileServiceCollection<Category, Category> category;
        private IMobileServiceTable<Bid> bidTable = App.MobileService.GetTable<Bid>();
        private MobileServiceCollection<Bid, Bid> bid;
        private MobileServiceCollection<Project, Project> projs;
        public MyProjects()
        {
            this.InitializeComponent();
            //getBids();
        }
       /* private async void getBids()
        {
            // Bidder must come from global.cs
            bid = await bidTable
               .Where(Bid => Bid.Bidder == "diksha.bajaj@hpe.com")
                   .ToCollectionAsync();

            List<Bid> bidList = new List<Bid>();
            bidList = bid.ToList();
            List<String> PIdList = new List<String>();
            Debug.Write("Count: " + bidList.Count);
            for (var i = 0; i < bidList.Count; i++)
            {
                PIdList.Add(bidList[i].ProjectId);
            }
            getProjectDetails(PIdList);
            
        }
        private async void getProjectDetails(List<String> PIdList)
        {
            List<Project> proj = new List<Project>();
            for (var i = 0; i < PIdList.Count; i++)
            {
                Debug.Write("Inside for loop");
                projects = await projectTable
                    .Where(Project => Project.Id == PIdList[i])
                    .ToCollectionAsync();

                
                proj.AddRange(projects);
            }
            Debug.Write("Total projects: " + proj.Count);
            gridView.ItemsSource = proj;
        }
        private void Project_Tapped(object sender, TappedRoutedEventArgs e)
        {
            string text = ((sender as StackPanel).FindName("txtStatus") as TextBlock).Text;
            Debug.Write("Status: " + text);
            string pId = ((sender as StackPanel).FindName("projectId") as TextBlock).Text;
            if (text == "Bidding")
            {
                List<string> param = new List<string>();
                param.Add(pId);
                param.Add("Update");
                Frame.Navigate(typeof(Bidding), param);
            }
            else if (text == "Assigned")
            {
                Frame.Navigate(typeof(UploadSolution),pId);
            }
            else
            {
                //Frame.Navigate(typeof(ProjectCategories));
            }

        }*/

    }
}
