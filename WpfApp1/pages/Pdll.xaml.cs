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
using DLL;

namespace WpfApp1.pages
{
    /// <summary>
    /// Логика взаимодействия для Pdll.xaml
    /// </summary>
    public partial class Pdll : Page
    {
        List<users> Users;
        public Pdll()
        {
            InitializeComponent();
            Users = BaseConnect.BaseModel.users.ToList();
        }

        private void btnAge_Click(object sender, RoutedEventArgs e)
        {
            List<DateTime> dates = new List<DateTime> { };
            foreach (users user in Users)
            {
                dates.Add(user.dr);
            }
            txtAge.Text = "Средний возраст выбранных пользователей:" + dll.Age(dates);
        }

        private void btnSerch_Click(object sender, RoutedEventArgs e)
        {
            List<string> nameserch = new List<string>();

            foreach (users u in Users)
            {
                nameserch.Add(u.name);
            }

            nameserch = dll.SerchName(nameserch, txtStr.Text);

            foreach (string s in nameserch)
            {
                listUser.Items.Add(s);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.MainFrame.GoBack();
        }
    }
}
