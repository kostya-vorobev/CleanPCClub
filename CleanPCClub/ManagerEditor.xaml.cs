using CleanPCClub.Classes;
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
    /// Логика взаимодействия для ManagerEditor.xaml
    /// </summary>
    public partial class ManagerEditor : Window
    {
        private Managers managerEdit;
        public ManagerEditor(DataRowView dataRow)
        {
            InitializeComponent();
            managerEdit = new Managers(dataRow);
            Name_TB.Text = managerEdit.Name;
            LastName_TB.Text = managerEdit.LastName;
            Phone_TB.Text = managerEdit.Phone;
            DateBrith_DP.SelectedDate = managerEdit.DateBrith;
            Email_TB.Text = managerEdit.Email;
            Login_TB.Text = managerEdit.Login;
            Pass_TB.Text = managerEdit.Pass;
            salary_TB.Text = managerEdit.Salary.ToString();
            headerLabel.Content = "Изменение данных менеджера";
            Grand_B.Content = "Обновить";
        }
        public ManagerEditor()
        {
            InitializeComponent();
            managerEdit = new Managers();
            managerEdit = null;
        }



        private void Insert()
        {
            try
            {
                if (Managers.SearchLogin(Login_TB.Text))
                {
                    CheckData();
                    Managers manager = new Managers(Login_TB.Text, Pass_TB.Text, Name_TB.Text, LastName_TB.Text,
                        DateBrith_DP.SelectedDate.Value, Phone_TB.Text, Email_TB.Text, Convert.ToInt32(salary_TB.Text));
                    if (manager.Insert())
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
            if (managerEdit != null)
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
            if (salary_TB.Foreground == Brushes.Red)
                throw new Exception("Заработная плата введена некорерктно");
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
                if (managerEdit.Login != Login_TB.Text)
                    if (!Managers.SearchLogin(Login_TB.Text))
                        throw new Exception("Данынй логин занят, попробуйте другой");
                    else managerEdit.Login = Login_TB.Text;
                CheckData();
                Managers client = new Managers(managerEdit.Id, Login_TB.Text, Pass_TB.Text, Name_TB.Text, LastName_TB.Text,
                    DateBrith_DP.SelectedDate.Value, Phone_TB.Text, Email_TB.Text, Convert.ToInt32(salary_TB.Text), managerEdit.IdUser);
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
                Login_TB.Foreground = Brushes.WhiteSmoke;
            }
        }

        private void Phone_TB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Phone_TB.Text.Length == 11)
            {
                Phone_TB.Foreground = Brushes.WhiteSmoke;
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
                Email_TB.Foreground = Brushes.WhiteSmoke;
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
            }
            catch
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
                Pass_TB.Foreground = Brushes.WhiteSmoke;
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
                LastName_TB.Foreground = Brushes.WhiteSmoke;
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
                Name_TB.Foreground = Brushes.WhiteSmoke;
            }
        }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }

        private void salary_TB_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            Regex rex = new Regex(@"^[0-9]*(?:\.[0-9]*)?$");
            if (rex.IsMatch(salary_TB.Text))
            {
                salary_TB.Foreground = Brushes.WhiteSmoke;
            }
            else
            {
                salary_TB.Foreground = Brushes.Red;
            }
        }
    }
}
