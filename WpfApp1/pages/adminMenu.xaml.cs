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
using WPF1;

namespace WpfApp1.pages
{
    /// <summary>
    /// Логика взаимодействия для adminMenu.xaml
    /// </summary>
    public partial class adminMenu : Page
    {
        public adminMenu()
        {
            InitializeComponent();
            dgUsers.ItemsSource = BaseConnect.BaseModel.auth.ToList();
        }

        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            BaseConnect.BaseModel.SaveChanges();
            MessageBox.Show("Изменения сохранены");
        }

        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            auth SelectedUser = (auth)dgUsers.SelectedItem;//сохраняем выбранную строку в отдельный объект
            BaseConnect.BaseModel.auth.Remove(SelectedUser);// удаляем выбранную строку
            BaseConnect.BaseModel.SaveChanges();//синхронизируем изменения с сервером 
            MessageBox.Show("Пользователь успешно удален");
            dgUsers.ItemsSource = BaseConnect.BaseModel.auth.ToList();//Обновить строки а датагрид
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.MainFrame.GoBack();
        }

        private void btnEditUser_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.MainFrame.Navigate(new EditUser((auth)dgUsers.SelectedItem));
        }
    }
}
