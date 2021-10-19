using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1;


namespace WpfApp1.pages
{
    /// <summary>
    /// Логика взаимодействия для EditUser.xaml
    /// </summary>
    public partial class EditUser : Page
    {
        static users Old;
        static auth SelectedUser;
        public EditUser(auth SelectedUsers)
        {
            InitializeComponent();
            string[] traits2 = new string[3];
            List<traits> traits1 = BaseConnect.BaseModel.traits.ToList();
            int i = 0;
            foreach (traits tr in traits1)
            {
                traits2[i] = tr.trait;
                i++;
            }
            cb1.Content = traits2[0];
            cb2.Content = traits2[1];
            cb3.Content = traits2[2];
            listGenders.ItemsSource = BaseConnect.BaseModel.genders.ToList();
            listGenders.SelectedValuePath = "id";
            listGenders.DisplayMemberPath = "gender";
            SelectedUser = SelectedUsers;
            try
            {
                txtLog.Text = SelectedUsers.login;
                txtPass.Password = SelectedUsers.password;
            } 
            catch { }
            Old = BaseConnect.BaseModel.users.FirstOrDefault(x => x.id == SelectedUsers.id);
            if (Old != null)
            {
                try
                {
                    txtName.Text = Old.name;
                }
                catch { }
                try
                {
                    dpDr.Text = Old.dr.ToString("yyyy MM dd");
                }
                catch { }
                try
                {
                    List<users_to_traits> LUTT;
                    LUTT = null;

                    LUTT = BaseConnect.BaseModel.users_to_traits.Where(x => x.id_user == SelectedUsers.id).ToList();
                    if (LUTT != null)
                    {
                        foreach (users_to_traits UTT in LUTT)
                        {
                            if (UTT != null)
                            {
                                if (UTT.traits.trait == cb1.Content.ToString())
                                {
                                    cb1.IsChecked = true;
                                }
                                if (UTT.traits.trait == cb2.Content.ToString())
                                {
                                    cb2.IsChecked = true;
                                }
                                if (UTT.traits.trait == cb3.Content.ToString())
                                {
                                    cb3.IsChecked = true;
                                }
                            }
                        }
                    }
                }
                catch { }
            }
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (Old != null)
            {
                Old.name = txtName.Text;
                Old.dr = (DateTime)dpDr.SelectedDate;
            }
            else
            {
                users user = new users { name = txtName.Text, id = SelectedUser.id, gender = (int)listGenders.SelectedValue, dr = (DateTime)dpDr.SelectedDate };
                BaseConnect.BaseModel.users.Add(user);
            }
            SelectedUser.login = txtLog.Text;
            SelectedUser.password = txtPass.Password;
            users_to_traits UTT1 = BaseConnect.BaseModel.users_to_traits.FirstOrDefault(x => x.id_user == SelectedUser.id && x.id_trait == 1);
            users_to_traits UTT2 = BaseConnect.BaseModel.users_to_traits.FirstOrDefault(x => x.id_user == SelectedUser.id && x.id_trait == 2);
            users_to_traits UTT3 = BaseConnect.BaseModel.users_to_traits.FirstOrDefault(x => x.id_user == SelectedUser.id && x.id_trait == 3);
            if (cb1.IsChecked == true && UTT1 == null)
            {
                users_to_traits UTT = new users_to_traits();
                UTT.id_user = SelectedUser.id;
                UTT.id_trait = 1;
                BaseConnect.BaseModel.users_to_traits.Add(UTT);
            }
            else if (UTT1 != null && cb1.IsChecked != true) { BaseConnect.BaseModel.users_to_traits.Remove(UTT1); }
            if (cb2.IsChecked == true && UTT2 == null)
            {
                users_to_traits UTT = new users_to_traits();
                UTT.id_user = SelectedUser.id;
                UTT.id_trait = 2;
                BaseConnect.BaseModel.users_to_traits.Add(UTT);
            }
            else if (UTT2 != null && cb2.IsChecked != true) { BaseConnect.BaseModel.users_to_traits.Remove(UTT2); }
            if (cb3.IsChecked == true && UTT3 == null)
            {
                users_to_traits UTT = new users_to_traits();
                UTT.id_user = SelectedUser.id;
                UTT.id_trait = 3;
                BaseConnect.BaseModel.users_to_traits.Add(UTT);
            }
            else if (UTT3 != null && cb3.IsChecked != true) { BaseConnect.BaseModel.users_to_traits.Remove(UTT3); }
            BaseConnect.BaseModel.SaveChanges();
            BaseConnect.BaseModel.Database.Connection.Close();
            BaseConnect.BaseModel.Database.Connection.Open();
            MessageBox.Show("Данные сохранены");
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.MainFrame.GoBack();
        }
    }
}
