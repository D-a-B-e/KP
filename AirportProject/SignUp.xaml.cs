using System.Windows;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Net.Mail;

namespace AirportProject
{
   
    public partial class SignUp : Window
    {
        private Window parent;
        private SqlConnection connection;
        public SignUp(SqlConnection connection, Window parent)
        {
            InitializeComponent();
            this.Show();
            this.parent = parent;
            this.connection = connection;
            parent.Visibility = Visibility.Hidden;
            connection.Open();
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            parent.Visibility = Visibility.Visible;
            connection.Close();
            this.Close();

        }
        public static string check_login(string login)
        {

           
            string login_local = login.Trim();
            if (login_local.Length == 0)
                return "Заполните поле";
            if (login_local.Length > 50)
                return "Слишком длинное имя";
            Regex regex = new Regex(@"^[a-z0-9]*$");
            if (!regex.IsMatch(login_local))
                return "Буквы и цифры";
            else
                return "";
        }

        public static string check_name(string name)
        {
           
            string name_local = name.Trim();
            if (name_local.Length == 0)
                return "Заполните поле";
            if (name_local.Length > 50)
                return "Слишком длинное имя";
            Regex regex = new Regex(@"^[A-zА-яЁё]+$");
            if (!regex.IsMatch(name_local))
                return "Только буквы";
            return "";
        }

        public static string check_surname(string name)
        {
            
            string name_local = name.Trim();
            if (name_local.Length == 0)
                return "Заполните поле";
            if (name_local.Length > 50)
                return "Слишком длинное имя";
            Regex regex = new Regex(@"^[A-zА-яЁё]*$");
            if (!regex.IsMatch(name_local))
                return "Только буквы";
            return "";
        }
        public static string check_dob(DateTime date)
        {
            DateTime now = DateTime.Today;
            int year_dif = now.Year - date.Year;
            if (year_dif < 1 || year_dif > 110)
                return "Неверная дата";
            return "";
        }

        public static string check_phone(string phone)
        {
            
            string phone_local = phone.Trim();
            if (phone_local.Length == 0)
                return "Заполните поле";
            if (phone_local.Length != 14)
                return "+7(123)1234567";
            Regex regex = new Regex(@"^\+7\(\d{3}\)\d{7}$");
            if (!regex.IsMatch(phone_local))
                return "Ошибка";
            return "";


        }

        public static string check_passport(string passport)
        {

            
            string passport_local = passport.Trim();
            if (passport_local.Length == 0)
                return "Заполните поле";
            if (passport_local.Length != 10)
                return "СерияНомер";
            Regex regex = new Regex(@"^\d{10}$");
            if (!regex.IsMatch(passport_local))
                return "Только цифры";
            return "";
        }

        public static string check_email(string email)
        {

            
            string email_local = email;
            if (email_local.Length == 0)
                return "Заполните поле";
            if (email_local.Length > 30)
                return "Слишком длинный адрес";
            try
            {
                MailAddress m = new MailAddress(email);
                return "";
            }
            catch (FormatException)
            {
                return "Неверный формат!";
            }
        }

        private static string check_password(string new_password, string re_password)
        {
            if (new_password != re_password)
                return "Не совпадение паролей";
            if (new_password.Length == 0)
                return "Заполните поле";
            if (new_password.Length > 50)
                return "Уменьшите пароль";
            
            return "";
        }

        private void Continue(object sender, RoutedEventArgs e)
        {
            string login_text = login.Text;
            string last_name_text = last_name.Text;
            string first_name_text = first_name.Text;
            string middle_name_text = middle_name.Text;
            string phone_text = phone.Text;
            string email_text = email.Text;
            DateTime dob_text = dob.DisplayDate;
            string passport_text = passport.Text;
            string password_text = new_password.Password;
            login_error.Visibility = Visibility.Hidden;
            last_name_error.Visibility = Visibility.Hidden;
            first_name_error.Visibility = Visibility.Hidden;
            middle_name_error.Visibility = Visibility.Hidden;
            dob_error.Visibility = Visibility.Hidden;
            phone_error.Visibility = Visibility.Hidden;
            passport_error.Visibility = Visibility.Hidden;
            email_error.Visibility = Visibility.Hidden;
            password_error.Visibility = Visibility.Hidden;


            bool er = false;
            string error_msg = "";
            if ((error_msg = check_login(login.Text)) != "")
            {
                er = true;
                login_error.Content = error_msg;
                login_error.Visibility = Visibility.Visible;
            }
            if ((error_msg = check_surname(last_name.Text)) != "")
            {
                er = true;
                last_name_error.Content = error_msg;
                last_name_error.Visibility = Visibility.Visible;
            }
            if ((error_msg = check_name(first_name.Text)) != "")
            {
                er = true;
                first_name_error.Content = error_msg;
                first_name_error.Visibility = Visibility.Visible;
            }
            if ((error_msg = check_name(middle_name.Text)) != "")
            {
                er = true;
                middle_name_error.Content = error_msg;
                middle_name_error.Visibility = Visibility.Visible;
            }

            if ((error_msg = check_phone(phone.Text)) != "")
            {
                er = true;
                phone_error.Content = error_msg;
                phone_error.Visibility = Visibility.Visible;
            }

            if ((error_msg = check_dob(dob.DisplayDate)) != "")
            {
                er = true;
                dob_error.Content = error_msg;
                dob_error.Visibility = Visibility.Visible;
            }

            if ((error_msg = check_passport(passport.Text)) != "")
            {
                er = true;
                passport_error.Content = error_msg;
                passport_error.Visibility = Visibility.Visible;
            }

            if ((error_msg = check_email(email.Text)) != "")
            {
                er = true;
                email_error.Content = error_msg;
                email_error.Visibility = Visibility.Visible;
            }

            if ((error_msg = check_password(new_password.Password, confirm_password.Password)) != "")
            {
                er = true;
                password_error.Content = error_msg;
                password_error.Visibility = Visibility.Visible;
            }

            if (!er)
            {
                try
                {
                    string sqlExpression = " INSERT INTO People VALUES (@login_value, @last_name_value, @name_value, @middle_name_value,  @phone_value, @email_value, @dob_value, @passport_value) ";
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    SqlParameter login_param = new SqlParameter("@login_value", login_text);
                    SqlParameter surname_param = new SqlParameter("@last_name_value", last_name_text);
                    SqlParameter name_param = new SqlParameter("@name_value", first_name_text);
                    SqlParameter middle_name_param = new SqlParameter("@middle_name_value", middle_name_text);
                    SqlParameter dob_param = new SqlParameter("@dob_value", dob_text);
                    SqlParameter phone_param = new SqlParameter("@phone_value", phone_text);
                    SqlParameter passport_param = new SqlParameter("@passport_value", passport_text);
                    SqlParameter email_param = new SqlParameter("@email_value", email_text);
                    command.Parameters.Add(login_param);
                    command.Parameters.Add(surname_param);
                    command.Parameters.Add(name_param);
                    command.Parameters.Add(middle_name_param);
                    command.Parameters.Add(dob_param);
                    command.Parameters.Add(phone_param);
                    command.Parameters.Add(passport_param);
                    command.Parameters.Add(email_param);
                    command.ExecuteNonQuery();

                    sqlExpression = " INSERT INTO USERS VALUES (@login_val, @password_value, 2) ";
                    command = new SqlCommand(sqlExpression, connection);
                    SqlParameter login_par = new SqlParameter("@login_val", login_text);
                    command.Parameters.Add(login_par);
                    SqlParameter password_param = new SqlParameter("@password_value", password_text);
                    command.Parameters.Add(password_param);
                    command.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Вы зарегистрированы!"); 
                    parent.Visibility = Visibility.Visible;
                    this.Close();



                }

                catch (SqlException exept)
                {
                    MessageBox.Show((exept.Number).ToString() + " " + exept.Message);
                }

            }
            
        }
    }
}
