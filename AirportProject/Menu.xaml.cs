using System.Windows;
using System.Data.SqlClient;

namespace AirportProject
{
    public partial class Menu : Window
    {
        Window window;
        SqlConnection connection;
        public Menu(SqlConnection connection, string login, int id_role, string first_name, string middle_name, MainWindow window)
        {
            
            InitializeComponent();
            this.window = window;
            this.connection = connection;
            info_but.Click += (e, w) => new Information(connection, this);
            buy_ticket_but.Click += (e, w) => new Ticket(connection, this, login);
            add_fly.Click += (e, w) => new AddFlight(connection, this);
     
            if (id_role == 1)
            {
                info_but.Visibility = Visibility.Visible;
                add_fly.Visibility = Visibility.Visible;
            }
            else if (id_role == 2)
            {

             
                 buy_ticket_but.Visibility = Visibility.Visible;
                

            }
           
            window.Visibility = Visibility.Hidden;
            this.Show();

        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            window.Visibility = Visibility.Visible;
            connection.Close();
            this.Close();
        }
    }
}
