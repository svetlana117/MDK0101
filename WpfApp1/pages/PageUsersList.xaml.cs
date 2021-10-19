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

namespace WpfApp1.pages
{
    /// <summary>
    /// Логика взаимодействия для PageUsersList.xaml
    /// </summary>
    public partial class PageUsersList : Page
    {
        List<users> users;
        public PageUsersList()
        {
            InitializeComponent();
            users= BaseConnect.BaseModel.users.ToList();
            lbUsersList.ItemsSource = users;
            btnNewUser.Content = "Создать\nнового\nпользователя";
        }
        private void lbTraits_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock lb = (TextBlock)sender;
            int index = Convert.ToInt32(lb.Uid);
            List <users_to_traits> l = BaseConnect.BaseModel.users_to_traits.Where(x => x.id_user == index).ToList();
            lb.Text = "";
            foreach (var a in l)
            {
                lb.Text += a.traits.trait + "; ";
            }
        }

        private void btnGO_Click(object sender, RoutedEventArgs e)
        {
            List<users> listUsers = users;
            if (cbSort.SelectedIndex == 0)
            {
                int start = Convert.ToInt32(tbOT.Text) - 1;
                int finish = Convert.ToInt32(tbDO.Text);
                listUsers = users.Skip(start).Take(finish - start).ToList();
            }
            if (cbSort.SelectedIndex == 1)
            {
                listUsers = users.Where(x => x.name == tbName.Text).ToList();
            }
            if (cbSort.SelectedIndex == 2)
            {
                listUsers = users.Where(x => x.genders.gender == tbGen.Text).ToList();
            }
            if (cbSort.SelectedIndex == 3)
            {
                listUsers = users.Where(x => x.dr == (DateTime)tbBD.SelectedDate).ToList();
            }
            lbUsersList.ItemsSource = listUsers;
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            lbUsersList.ItemsSource = users;
        }

        

        private void ComboBox_Selected(object sender, RoutedEventArgs e)
        {
            if (cbSort.SelectedIndex==0)
            {
                gbOT.Visibility = Visibility.Visible;
                gbDO.Visibility = Visibility.Visible;
                gbName.Visibility = Visibility.Collapsed;
                gbGen.Visibility = Visibility.Collapsed;
                gbBD.Visibility = Visibility.Collapsed;
            }
            if (cbSort.SelectedIndex == 1)
            {
                gbOT.Visibility = Visibility.Collapsed;
                gbDO.Visibility = Visibility.Collapsed;
                gbName.Visibility = Visibility.Visible;
                gbGen.Visibility = Visibility.Collapsed;
                gbBD.Visibility = Visibility.Collapsed;
            }
            if (cbSort.SelectedIndex == 2)
            {
                gbOT.Visibility = Visibility.Collapsed;
                gbDO.Visibility = Visibility.Collapsed;
                gbName.Visibility = Visibility.Collapsed;
                gbGen.Visibility = Visibility.Visible;
                gbBD.Visibility = Visibility.Collapsed;
            }
            if (cbSort.SelectedIndex == 3)
            {
                gbOT.Visibility = Visibility.Collapsed;
                gbDO.Visibility = Visibility.Collapsed;
                gbName.Visibility = Visibility.Collapsed;
                gbGen.Visibility = Visibility.Collapsed;
                gbBD.Visibility = Visibility.Visible;
            }

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int id = Convert.ToInt32(btn.Uid);
            auth CurrentUser = BaseConnect.BaseModel.auth.FirstOrDefault(x => x.id == id);
            BaseConnect.BaseModel.auth.Remove(CurrentUser);
            BaseConnect.BaseModel.SaveChanges();
            MessageBox.Show("Пользователь был удален");
            users = BaseConnect.BaseModel.users.ToList();
            lbUsersList.ItemsSource = users;
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int id = Convert.ToInt32(btn.Uid);
            auth CurrentUser = BaseConnect.BaseModel.auth.FirstOrDefault(x => x.id == id);
            LoadPages.MainFrame.Navigate(new EditUser(CurrentUser));
        }

        private void btnNewUser_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.MainFrame.Navigate(new reg());
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.MainFrame.GoBack();
        }
    }
}
