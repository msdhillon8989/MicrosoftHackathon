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

        private IMobileServiceTable<Project> projectTable = App.MobileService.GetTable<Project>();
        private IMobileServiceTable<Bid> bidTable = App.MobileService.GetTable<Bid>();
        private MobileServiceCollection<Project, Project> items;
        private MobileServiceCollection<Bid, Bid> bids;
        private List<ProjectCount> projectCount = new List<ProjectCount>();

        //   private MobileServiceCollection<, Project> items;




        private async void getProjects()
        {


            items = await projectTable .Where(Project => Project.Owner == Global.GetRepositoryValue("userName").ToString()).ToCollectionAsync();
             
           
                foreach (var project in items)
            {
                
               
                Debug.Write(project.Id);
                bids = await bidTable.Where(Bid => Bid.ProjectId == project.Id.ToString()).ToCollectionAsync();

                Debug.Write("    count "+bids.Count+"    ");
                var count = new ProjectCount { Project = project, BidCount = (int)bids.Count, Bids = bids.ToList() };
                projectCount.Add(count);
            }
            //gridView.ItemsSource = items;
          //  Global.SetRepositoryValue("MyPost", projectCount);


        }

     
      
    }
}
