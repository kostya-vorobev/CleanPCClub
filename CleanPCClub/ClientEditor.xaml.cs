using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CleanPCClub
{
    /// <summary>
    /// Логика взаимодействия для ClientEditor.xaml
    /// </summary>
    public partial class ClientEditor : Window
    {
        private Client clientEdit;
        public ClientEditor(DataRowView dataRow)
        {
            InitializeComponent();
            clientEdit = new Client(dataRow);
            Login_TB.Text = clientEdit.Login;
            Pass_TB.Text = clientEdit.Password;
            Name_TB.Text = clientEdit.Name;
            LastName_TB.Text = clientEdit.LastName;
            Phone_TB.Text = clientEdit.Phone;
            DateBrith_DP.SelectedDate = clientEdit.DateBrith;
            Email_TB.Text = clientEdit.Email;
            headerLabel.Content = "Изменение данных клиента";
            Grand_B.Content = "Обновить";
        }
        public ClientEditor()
        {
            InitializeComponent();
            clientEdit = new Client();
        }



        private void Insert()
        {
            try
            {
                if (Client.SearchLogin(Login_TB.Text))
                {
                    CheckData();
                    Client client = new Client(Login_TB.Text, Pass_TB.Text, Name_TB.Text, LastName_TB.Text,
                        DateBrith_DP.SelectedDate.Value, Phone_TB.Text, Email_TB.Text);
                    if (client.Insert())
                    {
                        MessageBox.Show("Успешно");
                    }
                    else
                    {
                        throw new Exception("Не удалось добавить запись в базу данных");
                    }
                }
                else
                {
                    MessageBox.Show("Данынй логин занят, попробуйте другой");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Grand_Click(object sender, RoutedEventArgs e)
        {
            if (clientEdit != null)
            {
                Update();
            }
            else
            {
                Insert();
            }
        }
        private void CheckData()
        {
            if (Login_TB.Foreground == Brushes.Red)
                throw new Exception("Логин введен некорерктно");
            if (Pass_TB.Foreground == Brushes.Red)
                throw new Exception("Пароль введен некорерктно");
            if (Name_TB.Foreground == Brushes.Red)
                throw new Exception("Имя введено некорерктно");
            if (LastName_TB.Foreground == Brushes.Red)
                throw new Exception("Фамилия введена некорерктно");
            if (DateBrith_DP.Foreground == Brushes.Red)
                throw new Exception("Дата рождения введена некорерктно");
            if (Phone_TB.Foreground == Brushes.Red)
                throw new Exception("Телефон введен некорерктно");
            if (Email_TB.Foreground == Brushes.Red)
                throw new Exception("Email введен некорерктно");
        }
        private void Update()
        {
            try
            {
                if (clientEdit.Login != Login_TB.Text)
                    if (Client.SearchLogin(Login_TB.Text))
                        throw new Exception("Данынй логин занят, попробуйте другой");
                CheckData();
                Client client = new Client(clientEdit.Id, Login_TB.Text, Pass_TB.Text, Name_TB.Text, LastName_TB.Text,
                    DateBrith_DP.SelectedDate.Value, Phone_TB.Text, Email_TB.Text);
                if (client.Update())
                {
                    MessageBox.Show("Успешно");
                }
                else
                {
                    throw new Exception("Не удалось добавить запись в базу данных");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Login_TB.Text.Length > 50 || Login_TB.Text.Length < 2)
            {
                Login_TB.Foreground = Brushes.Red;
            }
            else
            {
                Login_TB.Foreground = Brushes.Black;
            }
        }

        private void Phone_TB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Phone_TB.Text.Length == 11)
            {
                Phone_TB.Foreground = Brushes.Black;
            }
            else
            {
                Phone_TB.Foreground = Brushes.Red;
            }
        }

        private void Email_TB_TextChanged(object sender, TextChangedEventArgs e)
        {
            Regex rex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            if (rex.IsMatch(Email_TB.Text))
            {
                Email_TB.Foreground = Brushes.Black;
            }
            else
            {
                Email_TB.Foreground = Brushes.Red;
            }
        }

        private void DateBrith_DP_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DateBrith_DP.SelectedDate.Value.Year <= 1900 || DateBrith_DP.SelectedDate.Value >= DateTime.Now)
                {
                    DateBrith_DP.Foreground = Brushes.Red;
                }
                else
                {
                    DateBrith_DP.Foreground = Brushes.Black;
                }
            }catch
            {

            }
        }

        private void Pass_TB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Pass_TB.Text.Length > 50 || Pass_TB.Text.Length < 8)
            {
                Pass_TB.Foreground = Brushes.Red;
            }
            else
            {
                Pass_TB.Foreground = Brushes.Black;
            }
        }

        private void LastName_TB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (LastName_TB.Text.Length > 50 || LastName_TB.Text.Length < 3)
            {
                LastName_TB.Foreground = Brushes.Red;
            }
            else
            {
                LastName_TB.Foreground = Brushes.Black;
            }
        }

        private void Name_TB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Name_TB.Text.Length > 50 || Name_TB.Text.Length < 2)
            {
                Name_TB.Foreground = Brushes.Red;
            }
            else
            {
                Name_TB.Foreground = Brushes.Black;
            }
        }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }

    }
}
