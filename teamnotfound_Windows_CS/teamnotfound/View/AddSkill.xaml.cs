using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
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
    
    public sealed partial class AddSkill : Page
    {
        private IMobileServiceTable<Skills> skillTable = App.MobileService.GetTable<Skills>();
        private IMobileServiceTable<UserSkills> userSkillTable = App.MobileService.GetTable<UserSkills>();

        public AddSkill()
        {
            this.InitializeComponent();
            LoadSkills();
        }
        private class CustomSkill
        {
            public string EmailId { get; set; }
            public bool Selected { get; set; }
            public int Sid { get; set; }
        }
        private async void LoadSkills()
        {
            var skillList = await  skillTable.ToListAsync();
            skillListView.ItemsSource = skillList;
            LoadUserSkills();
        }

        private async void LoadUserSkills()
        {
            string userName = (string)Global.GetRepositoryValue("userName");
            List<UserSkills> resList = await userSkillTable.Where(uS => uS.EmailId == userName).ToListAsync();
            List<Skills> sList = (List<Skills>)skillListView.ItemsSource;
            foreach (UserSkills u in resList)
            {
                foreach(Skills s in sList)
                {
                    if(Convert.ToInt32(s.Sid) == u.Sid)
                    {
                        ListViewItem lvi = skillListView.ContainerFromItem(s) as ListViewItem;
                        lvi.IsSelected = true;
                    }

                }
            }
        }
        private async void StoreSkill(int sid)
        {
           
            string userName = (string)Global.GetRepositoryValue("userName");
           var resList = await userSkillTable.Where(uS => uS.EmailId == userName && uS.Sid == sid).ToListAsync();
            if (resList.Count != 0)
            {
                await userSkillTable.DeleteAsync(resList[0]);
            }
            else
            {
                UserSkills userSkills = new UserSkills();
                userSkills.Sid = sid;
                userSkills.EmailId = userName;
                await userSkillTable.InsertAsync(userSkills);
            } 
                       
        }

        private void skillListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Skills clicked = (Skills)e.ClickedItem;
            int sid = Convert.ToInt32(clicked.Sid);
            StoreSkill(sid);
        }

       
    }
}
