using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using teamnotfound.DataModel;
using teamnotfound.Common;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace teamnotfound.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProjectStatus : Page
    {
        
        public ProjectStatus()
        {
            this.InitializeComponent();
            int index = (int) Global.GetRepositoryValue("selectedProject");
            List<ProjectCount> projects =(List<ProjectCount>) Global.GetRepositoryValue("MyPosts");
            ProjectCount currentProject = projects.ElementAt(index);

            if(currentProject.Project.Status=="Bidding")
            {
               // UpdateButton.Visibility = "Visible";
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DownloadButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
