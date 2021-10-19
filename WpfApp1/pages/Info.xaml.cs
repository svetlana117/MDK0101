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
using System.IO;
using System.Diagnostics;


namespace WpfApp1.pages
{
    /// <summary>
    /// Логика взаимодействия для Info.xaml
    /// </summary>
    public partial class Info : Page
    {
        public Info(auth CurrentUsers)
        {
            InitializeComponent();
            try
            {
                tbName.Text = CurrentUsers.users.name;
                tbDR.Text = CurrentUsers.users.dr.ToString("yyyy MM dd");
                tbGender.Text = CurrentUsers.users.genders.gender;
                //cписок из качеств личности авторизованного пользователя
                List<users_to_traits> LUTT = BaseConnect.BaseModel.users_to_traits.Where(x => x.id_user == CurrentUsers.id).ToList();
                foreach (users_to_traits UT in LUTT)
                {
                    tbTraits.Text += UT.traits.trait + "; ";
                }
            }
            catch
            {
                MessageBox.Show("Чел ты не существуешь");
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.MainFrame.GoBack();
        }

        //public int s = 0;
        //private void Button_Click(object sender, RoutedEventArgs e)
        //{

        //    s = 0;
        //    Output(1);

        //}

        //private void Output(int ii)
        //{
        //    try
        //    {
        //        FIO[] p = new FIO[1];
        //        using (StreamReader sr = new StreamReader(File.Open("Input.txt", FileMode.OpenOrCreate)))
        //        {
        //            for (int i = 0; sr.EndOfStream != true; i++)
        //            {
        //                Array.Resize(ref p, p.Length + 1);
        //                p[i].Name = sr.ReadLine();
        //                Debug.WriteLine(p.Length + " " + i);
        //                Debug.WriteLine(p[i].Name);
        //                p[i].BirthDt = sr.ReadLine();

        //                p[i].Gen = sr.ReadLine();

        //                p[i].Info = sr.ReadLine();

        //            }
        //        }
        //        if (ii == 1)
        //        {

        //            Name.Content = p[0].Name;
        //            Birth.Content = p[0].BirthDt;
        //            Gender.Content = p[0].Gen;
        //            Info.Content = p[0].Info;
        //        }
        //        else
        //        {

        //            Debug.WriteLine(s);
        //            Debug.WriteLine(p.Length);
        //            Name.Content = p[s].Name;
        //            Birth.Content = p[s].BirthDt;
        //            Gender.Content = p[s].Gen;
        //            Info.Content = p[s].Info;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show("Список пуст\n" + e.Message);

        //    }
        //}

        //struct FIO
        //{
        //    public string Name;
        //    public string BirthDt;
        //    public string Gen;
        //    public string Info;

        //    public FIO(string Name, string BirthDt, string Gen, string Info)
        //    {
        //        this.Name = Name;
        //        this.BirthDt = BirthDt;
        //        this.Gen = Gen;
        //        this.Info = Info;
        //    }
        //}



        //private void Naame_TextChanged(object sender, TextChangedEventArgs e)
        //{

        //}

        //private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{

        //}

        //private void Button_Click_2(object sender, RoutedEventArgs e)
        //{

        //    s++;
        //    Output(2);

        //}

        //private void Button_Click_3(object sender, RoutedEventArgs e)
        //{
        //    int i = 0;
        //    FIO[] m = new FIO[1];
        //    FIO fio;
        //    string n, b, g, ott = "";
        //    if (One.IsChecked == true)
        //    {

        //        ott = ott + One.Content + " ";

        //    }
        //    if (Two.IsChecked == true)
        //    {

        //        ott = ott + Two.Content + " ";
        //    }
        //    if (Three.IsChecked == true)
        //    {
        //        ott = ott + Three.Content + " ";
        //    }
        //    ott = ott + "\n";
        //    n = Naame.Text + "\n";
        //    b = BirthDay.Text + "\n";
        //    g = Gen.SelectedItem.ToString() + "\n";
        //    fio = new FIO(n, b, g, ott);
        //    //MessageBox.Show(ott);
        //    m[i] = fio;
        //    i++;
        //    Array.Resize(ref m, m.Length + 1);

        //    using (StreamWriter sw = new StreamWriter(File.Open("Input.txt", FileMode.Append)))
        //    {
        //        sw.Write(m[i - 1].Name);
        //        sw.Write(m[i - 1].BirthDt);
        //        sw.Write(m[i - 1].Gen);
        //        sw.Write(m[i - 1].Info);
        //    }

        //    One.IsChecked = false;
        //    Two.IsChecked = false;
        //    Three.IsChecked = false;
        //    Naame.Clear();
        //    BirthDay.Text = "";
        //    Gen.SelectedItem = false;
        //}



    }
}
