using System.Windows;
using System.Data.SqlClient;

namespace AirportProject
{
    public partial class MainWindow : Window
    {
        private SqlConnection connection;
        
        public MainWindow()
        {
            InitializeComponent();
            new_client.Click += (e, w) => new SignUp(connection, this);
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
                {
                    DataSource = @"DESKTOP-2BKTQN0\SQLEXPRESS",
                    InitialCatalog = "КР",
                    IntegratedSecurity = true
                };
                connection = new SqlConnection(builder.ConnectionString);

            }
            catch (SqlException e)
            {
                MessageBox.Show("Ошибка подключения" + e.ToString());
            }

        }

        private void log_in_Click(object sender, RoutedEventArgs e)
        {
            if (login_txt.Text.Length > 0 && pass_txt.Password.Length > 0)
            {
                connection.Open();
                SqlParameter login_parameter = new SqlParameter("@login_val", login_txt.Text);
                SqlParameter password_parameter = new SqlParameter("@password_val", pass_txt.Password);
                string sqlExpression = "SELECT Login FROM dbo.USERS WHERE(Login = @login_val AND Password = @password_val)";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add(login_parameter);
                command.Parameters.Add(password_parameter);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Close();
                    sqlExpression = " SELECT dbo.People.FirstName, dbo.People.MiddleName, dbo.Users.IdRole, dbo.Users.Login " +
                    " FROM dbo.Users INNER JOIN "
                   + " dbo.People ON dbo.Users.Login = dbo.People.Login"
                   + " WHERE(dbo.Users.Login = @login_val)";
                    command = new SqlCommand(sqlExpression, connection);
                    login_parameter = new SqlParameter("@login_val", login_txt.Text);
                    command.Parameters.Add(login_parameter);
                    reader = command.ExecuteReader();
                    reader.Read();
                    
                    string first_name = reader.GetString(0);
                    string middle_name = reader.GetString(1);
                    int id_role = reader.GetInt32(2);
                    string login = reader.GetString(3);
                    reader.Close();
                    pass_txt.Password = "";
                    new Menu (connection, login, id_role, first_name, middle_name, this);
                }
                else
                {
                    error.Visibility = Visibility.Visible;
                    connection.Close();
                }
            }
        }
    }
}
