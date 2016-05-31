using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using teamnotfound.Common;
using teamnotfound.DataModel;

using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace teamnotfound.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Profile : Page
    {
        private IMobileServiceTable<User> usertable = App.MobileService.GetTable<User>();

        public Profile()
        {
            this.InitializeComponent();
            SetUpProfile();
        }
        private static CloudBlobContainer GetContainer()
        {
          
            var account = new CloudStorageAccount(new StorageCredentials("teamnotfound",
                "2NzBRdZDr43TeKpLQG3C4z2LO968jsU42mGmt1K6NxDyzO+GPmZKpj1kjCBYpB+rwHVojZXgwiC0mUUArDTv7Q=="),
                    true);
            var blobClient = account.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("profile");
            return container;
        }
        private async Task GetProfileImage(string key)
        {
            Stream stream = null ;
            Stream imgstream = null;
            StorageFile file = null;
            try
            {
                var container = GetContainer();
                StorageFolder folder = ApplicationData.Current.LocalFolder;
                var Imageblob = container.GetBlobReference(key + ".png");
                imgstream = new MemoryStream();
                try
                {
                    await Imageblob.DownloadToStreamAsync(imgstream);
                }
                catch (Exception ex)
                {
                    throw ex;
                }              
                try
                {
                    file = await folder.GetFileAsync(key + ".png");
                }
                catch (FileNotFoundException ex)
                {
                    file = await folder.CreateFileAsync(key + ".png");
                }
                stream = await file.OpenStreamForWriteAsync();
                imgstream.CopyTo(stream);
                var bitmap = new BitmapImage(new Uri(file.Path));
                ProfileImage.Source = bitmap;
            }catch(Exception ex)
            {
                return;
            }
            finally
            {
                if(imgstream != null)
                     imgstream.Dispose();
                if(stream != null)
                    stream.Dispose();
               
            }


        }



        private async void SetUpProfile()
        {

            string userName = (string)Global.GetRepositoryValue("userName");
            var userRes = await usertable.Where(usr => usr.Email == userName).ToEnumerableAsync();
            User user = userRes.SingleOrDefault();
            if (user != null)
            {
                await GetProfileImage(user.Email);
                FirstNameTextBlock.Text = user.Fname;
                LastNameTextBlock.Text = user.Lname;
                EmailTextBlock.Text = user.Email;
                MobileTextBlock.Text = user.Mobile;
                SummaryTextBox.Text = user.Summary;
                string serialized = JsonConvert.SerializeObject(user);
                Global.SetRepositoryValue("userProfile",serialized);

                // SummaryTextBox.T
            }

        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(EditProfile));
        }
    }
}
