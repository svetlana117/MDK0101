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
    /// Логика взаимодействия для reg.xaml
    /// </summary>
    public partial class reg : Page
    {
        public reg()
        {
            InitializeComponent();
            listGenders.ItemsSource = BaseConnect.BaseModel.genders.ToList();
            listGenders.SelectedValuePath = "id";
            listGenders.DisplayMemberPath = "gender";
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
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.MainFrame.GoBack();
        }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            //записываем данные в таблицу auth
            auth logPass = new auth() { login= txtLog.Text, password = txtPass.Password, role=2 };//создать новую запись
            BaseConnect.BaseModel.auth.Add(logPass);//добавить в модель нового пользователя
            BaseConnect.BaseModel.SaveChanges();//отправить на сервак
            //создаем данные в таблице юзеров, соответствующую данной
            users User = new users() { name = txtName.Text, id = logPass.id, gender = (int)listGenders.SelectedValue, dr = (DateTime)date.SelectedDate };
            BaseConnect.BaseModel.users.Add(User);

            if (cb1.IsChecked == true)
            {
                users_to_traits UTT = new users_to_traits();
                UTT.id_user = User.id;
                UTT.id_trait = 1;
                BaseConnect.BaseModel.users_to_traits.Add(UTT);
            }
            if (cb2.IsChecked == true)
            {
                users_to_traits UTT = new users_to_traits();
                UTT.id_user = User.id;
                UTT.id_trait = 2;
                BaseConnect.BaseModel.users_to_traits.Add(UTT);
            }
            if (cb3.IsChecked == true)
            {
                users_to_traits UTT = new users_to_traits();
                UTT.id_user = User.id;
                UTT.id_trait = 3;
                BaseConnect.BaseModel.users_to_traits.Add(UTT);
            }

            //foreach (traits t in lbTarits.SelectedItems)
            //{
            //    users_to_traits UTT = new users_to_traits();
            //    UTT.id_user = User.id;
            //    UTT.id_trait = t.id;
            //    BaseConnect.BaseModel.users_to_traits.Add(UTT);
            //}
            //BaseConnect.BaseModel.SaveChanges();
            MessageBox.Show("Данные записаны успешно");
        }
    }
}
