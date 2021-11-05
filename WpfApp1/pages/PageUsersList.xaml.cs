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
        List<users> listUsers;
        public PageUsersList()
        {
            InitializeComponent();
            users= BaseConnect.BaseModel.users.ToList();
            lbUsersList.ItemsSource = users;
            List<genders> genders = BaseConnect.BaseModel.genders.ToList();
            cbGenderS.ItemsSource = genders;
            cbGenderS.SelectedValuePath = "id";
            cbGenderS.DisplayMemberPath = "gender";
            btnNewUser.Content = "Создать\nнового\nпользователя";
            listUsers = users;
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

        private void Filter(object sender, RoutedEventArgs e)
        {
            List<users> listUsers = users;
            try
            {
                int start = Convert.ToInt32(tbOT.Text) - 1;
                int finish = Convert.ToInt32(tbDO.Text);
                listUsers = users.Skip(start).Take(finish - start).ToList();
            }
            catch { }
            //фильтр по части имени
            if(tbPartName.Text!="")
            {
                listUsers = users.Where(x => x.name.Contains(tbPartName.Text)).ToList();
            }
            //if (tbName.Text != "")
            //{
            //    listUsers = users.Where(x => x.name == tbName.Text).ToList();
            //}
            if (tbBD.SelectedDate!=null)
            {
                listUsers = listUsers.Where(x => x.dr == (DateTime)tbBD.SelectedDate).ToList();
            }
            if (cbGenderS.SelectedValue!=null)
            {
                listUsers = listUsers.Where(x => x.gender == Convert.ToInt32(cbGenderS.SelectedValue)).ToList();
            }
            lbUsersList.ItemsSource = listUsers;
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            lbUsersList.ItemsSource = users;
            tbPartName.Text = "";
            cbGenderS.SelectedValue = null;
            tbBD.SelectedDate = null;
            tbOT.Text = "";
            tbDO.Text = "";
        }
        //private void ComboBox_Selected(object sender, RoutedEventArgs e)
        //{
        //    if (cbSort.SelectedIndex==0)
        //    {
        //        gbOT.Visibility = Visibility.Visible;
        //        gbDO.Visibility = Visibility.Visible;
        //        gbName.Visibility = Visibility.Collapsed;
        //        gbGen.Visibility = Visibility.Collapsed;
        //        gbBD.Visibility = Visibility.Collapsed;
        //    }
        //    if (cbSort.SelectedIndex == 1)
        //    {
        //        gbOT.Visibility = Visibility.Collapsed;
        //        gbDO.Visibility = Visibility.Collapsed;
        //        gbName.Visibility = Visibility.Visible;
        //        gbGen.Visibility = Visibility.Collapsed;
        //        gbBD.Visibility = Visibility.Collapsed;
        //    }
        //    if (cbSort.SelectedIndex == 2)
        //    {
        //        gbOT.Visibility = Visibility.Collapsed;
        //        gbDO.Visibility = Visibility.Collapsed;
        //        gbName.Visibility = Visibility.Collapsed;
        //        gbGen.Visibility = Visibility.Visible;
        //        gbBD.Visibility = Visibility.Collapsed;
        //    }
        //    if (cbSort.SelectedIndex == 3)
        //    {
        //        gbOT.Visibility = Visibility.Collapsed;
        //        gbDO.Visibility = Visibility.Collapsed;
        //        gbName.Visibility = Visibility.Collapsed;
        //        gbGen.Visibility = Visibility.Collapsed;
        //        gbBD.Visibility = Visibility.Visible;
        //    }

        //}

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
        int CurrentPages = 1;
        private void GoPage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                List<users> lb = users.ToList();
                TextBlock tb = (TextBlock)sender;
                int CountZapInList = users.Count;
                int CountZapOnPage = Convert.ToInt32(txtPageCount.Text);
                int CountPage;
                if (CountZapOnPage % 2 == 0)
                {
                    CountPage = (CountZapInList / CountZapOnPage);
                }
                else
                {
                    CountPage = (CountZapInList / CountZapOnPage) + 1;
                }
                switch (tb.Uid)
                {
                    case "prev":
                        CurrentPages--;
                        break;
                    case "1":
                        if (CurrentPages < 3) CurrentPages = 1;
                        else if (CurrentPages > CountPage) CurrentPages = CountPage - 4;
                        else CurrentPages -= 2;
                        break;
                    case "2":
                        if (CurrentPages < 3) CurrentPages = 2;
                        else if (CurrentPages > CountPage) CurrentPages = CountPage - 3;
                        else CurrentPages -= 1;
                        break;
                    case "3":
                        if (CurrentPages < 3) CurrentPages = 3;
                        else if (CurrentPages > CountPage) CurrentPages = CountPage - 2;
                        break;
                    case "4":
                        if (CurrentPages < 3) CurrentPages = 4;
                        else if (CurrentPages > CountPage) CurrentPages = CountPage - 1;
                        else CurrentPages++;
                        break;
                    case "5":
                        if (CurrentPages < 3) CurrentPages = 5;
                        else if (CurrentPages > CountPage) CurrentPages = CountPage;
                        else CurrentPages += 2;
                        break;
                    case "next":
                        CurrentPages++;
                        break;
                    default:
                        CurrentPages = 1;
                        break;
                }

                if (CurrentPages < 1) CurrentPages = 1;
                if (CurrentPages > CountPage) CurrentPages = CountPage;
                //отрисовка
                if (CountPage < 5)
                {
                    txt5.Visibility = Visibility.Collapsed;
                }
                else if (CountPage < 4)
                {
                    txt4.Visibility = Visibility.Collapsed;
                }
                else if (CountPage < 3)
                {
                    txt3.Visibility = Visibility.Collapsed;
                }
                else if (CountPage < 2)
                {
                    txt2.Visibility = Visibility.Collapsed;
                }
                else
                {
                    txt2.Visibility = Visibility.Visible;
                    txt3.Visibility = Visibility.Visible;
                    txt4.Visibility = Visibility.Visible;
                    txt5.Visibility = Visibility.Visible;
                }
                if (CurrentPages < 3)
                {
                    txt1.Text = " 1 ";
                    txt2.Text = " 2 ";
                    txt3.Text = " 3 ";
                    txt4.Text = " 4 ";
                    txt5.Text = " 5 ";
                }
                else if (CurrentPages > CountPage - 2)
                {
                    txt1.Text = " " + (CountPage - 4).ToString() + " ";
                    txt2.Text = " " + (CountPage - 3).ToString() + " ";
                    txt3.Text = " " + (CountPage - 2).ToString() + " ";
                    txt4.Text = " " + (CountPage - 1).ToString() + " ";
                    txt5.Text = " " + (CountPage).ToString() + " ";
                }
                else
                {
                    txt1.Text = " " + (CurrentPages - 2).ToString() + " ";
                    txt2.Text = " " + (CurrentPages - 1).ToString() + " ";
                    txt3.Text = " " + (CurrentPages).ToString() + " ";
                    txt4.Text = " " + (CurrentPages + 1).ToString() + " ";
                    txt5.Text = " " + (CurrentPages + 2).ToString() + " ";

                }
                txtCurentPage.Text = "Стр: " + (CurrentPages).ToString();

                lb = users.Skip(CurrentPages * CountZapOnPage - CountZapOnPage).Take(CountZapOnPage).ToList();
                lbUsersList.ItemsSource = lb;
            }
            catch { }
        }

        private void txtPageCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                listUsers = users.Skip(0).Take(Convert.ToInt32(txtPageCount.Text)).ToList();
                lbUsersList.ItemsSource = listUsers;
            }
            catch { }
        }

        private void btnDLL_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.MainFrame.Navigate(new Pdll());
        }
    }
}
