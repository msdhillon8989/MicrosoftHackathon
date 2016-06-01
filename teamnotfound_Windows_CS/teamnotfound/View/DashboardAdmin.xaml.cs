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
    public sealed partial class DashBoardAdmin : Page
    {
        public DashBoardAdmin()
        {
            this.InitializeComponent();
        }
        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }
        private void PostEvent_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = false;
            myFrame.Navigate(typeof(PostProject));
        }
        private void MyPosts_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = false;
            myFrame.Navigate(typeof(MyPost));
        }
        private void History_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }
        private void Logout_Tapped(object sender, RoutedEventArgs e)
        {
            //MySplitView.IsPaneOpen = false;
            //Load the custom page
            Frame.Navigate(typeof(Login));

            Global.SetRepositoryValue("userName", "");
        }
    }
}
