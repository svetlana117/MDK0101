using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
        PageChange pc = new PageChange();

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
            DataContext = pc;
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
            pc.Countlist = listUsers.Count;
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
        
        private void GoPage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;//определяем, какой текстовый блок был нажат           
            //изменение номера страници при нажатии на кнопку
            switch (tb.Uid)
            {
                case "prev":
                    pc.CurrentPage--;
                    break;
                case "next":
                    pc.CurrentPage++;
                    break;
                default:
                    pc.CurrentPage = Convert.ToInt32(tb.Text);
                    break;
            }

            //определение списка
            lbUsersList.ItemsSource = listUsers.Skip(pc.CurrentPage * pc.CountPage - pc.CountPage).Take(pc.CountPage).ToList();
            txtCurentPage.Text = "Текущая страница: " + (pc.CurrentPage).ToString();

        }

        private void txtPageCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                pc.CountPage = Convert.ToInt32(txtPageCount.Text);
            }
            catch
            {
                pc.CountPage = listUsers.Count;
            }
            pc.Countlist = users.Count;
            lbUsersList.ItemsSource = listUsers.Skip(0).Take(pc.CountPage).ToList();
        }

        private void btnDLL_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.MainFrame.Navigate(new Pdll());
        }

        private void UserImage_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Image IMG = sender as System.Windows.Controls.Image;
            int ind = Convert.ToInt32(IMG.Uid);
            users U = BaseConnect.BaseModel.users.FirstOrDefault(x => x.id == ind);//запись о текущем пользователе
            usersimage UI = BaseConnect.BaseModel.usersimage.FirstOrDefault(x => x.id_user == ind && x.avatar == true);//получаем запись о картинке для текущего пользователя
            BitmapImage BI = new BitmapImage();
            if (UI != null)//если для текущего пользователя существует запись о его катринке
            {

                if (UI.path != null)//если присутствует путь к картинке
                {
                    BI = new BitmapImage(new Uri(UI.path, UriKind.Relative));
                }
                else//если присутствуют двоичные данные
                {
                    BI.BeginInit();//начать инициализацию BitmapImage (для помещения данных из какого-либо потока)
                    BI.StreamSource = new MemoryStream(UI.image);//помещаем в источник данных двоичные данные из потока
                    BI.EndInit();//закончить инициализацию
                }

            }

            else//если в базе не содержится картинки, то ставим заглушку
            {
                switch (U.gender)//в зависимости от пола пользователя устанавливаем ту или иную картинку
                {
                    case 1:
                        BI = new BitmapImage(new Uri(@"/название/male.jpg", UriKind.Relative));
                        break;
                    case 2:
                        BI = new BitmapImage(new Uri(@"/название/female.jpg", UriKind.Relative));
                        break;
                    default:
                        BI = new BitmapImage(new Uri(@"/название/other.jpg", UriKind.Relative));
                        break;
                }
            }
            IMG.Source = BI;//помещаем картинку в image
        }
        private void BtmAddImage_Click(object sender, RoutedEventArgs e)
        {
            Button BTN = (Button)sender;
            int ind = Convert.ToInt32(BTN.Uid);
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = ".jpg"; // задаем расширение по умолчанию
            openFileDialog.Filter = "Изображения |*.jpg;*.png"; // задаем фильтр на форматы файлов
            var result = openFileDialog.ShowDialog();
            if (result == true)//если файл выбран
            {
                usersimage avatar = new usersimage();//создаем новый объект usersimage
                List<usersimage> U = BaseConnect.BaseModel.usersimage.Where(x => x.id_user == ind).ToList();//получаем запись о картинке для текущего пользователя
                if (U != null)
                {
                    foreach (usersimage um in U)
                    {
                        if (um.avatar == true)
                        {
                            avatar = um;
                        }
                    }
                }
                System.Drawing.Image UserImage;
                ImageConverter IC;
                byte[] ByteArr;
                usersimage UI;
                if (avatar != null)
                {

                    if (MessageBox.Show("Сменить аватар пользователя?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        UserImage = System.Drawing.Image.FromFile(openFileDialog.FileName);//создаем изображение
                        IC = new ImageConverter();//конвертер изображения в массив байт
                        ByteArr = (byte[])IC.ConvertTo(UserImage, typeof(byte[]));//непосредственно конвертация
                        UI = new usersimage() { id_user = ind, image = ByteArr, avatar = true };//создаем новый объект usersimage
                        avatar.avatar = false;
                    }
                    else
                    {
                        UserImage = System.Drawing.Image.FromFile(openFileDialog.FileName);//создаем изображение
                        IC = new ImageConverter();//конвертер изображения в массив байт
                        ByteArr = (byte[])IC.ConvertTo(UserImage, typeof(byte[]));//непосредственно конвертация
                        UI = new usersimage() { id_user = ind, image = ByteArr, avatar = false };//создаем новый объект usersimage

                    }

                }
                else
                {
                    UserImage = System.Drawing.Image.FromFile(openFileDialog.FileName);//создаем изображение
                    IC = new ImageConverter();//конвертер изображения в массив байт
                    ByteArr = (byte[])IC.ConvertTo(UserImage, typeof(byte[]));//непосредственно конвертация
                    UI = new usersimage() { id_user = ind, image = ByteArr, avatar = false };//создаем новый объект usersimage

                }
                BaseConnect.BaseModel.usersimage.Add(UI);//добавляем его в модель
                BaseConnect.BaseModel.SaveChanges();//синхронизируем с базой
                MessageBox.Show("картинка пользователя добавлена в базу");
            }
            else
            {
                MessageBox.Show("операция выбора изображения отменена");
            }
            users = BaseConnect.BaseModel.users.ToList();
            lbUsersList.ItemsSource = users;
        }
        private void btnGoToGallery_Click(object sender, RoutedEventArgs e)
        {
            Button BTN = (Button)sender;
            int ind = Convert.ToInt32(BTN.Uid);
            users currenUser = BaseConnect.BaseModel.users.FirstOrDefault(x => x.id == ind);
            LoadPages.MainFrame.Navigate(new Gallery(currenUser));
        }

    }
}
