using System;
using System.Windows;
using System.Data.SqlClient;

namespace AirportProject
{
    
    public partial class Ticket : Window
    {
        string login;
        private SqlConnection connection;
        private Window parent;
        public Ticket(SqlConnection connection, Window parent, string login)
        {
            InitializeComponent();
            this.Show();
            this.parent = parent;
            this.connection = connection;
            this.login = login;
            parent.Visibility = Visibility.Hidden;
            string sqlExpression = "SELECT DISTINCT Country FROM dbo.Airports";
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            SqlDataReader reader = command.ExecuteReader();
            string combo_variable;

            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    combo_variable = reader.GetString(0);
                    from_country.Items.Add(combo_variable);
                    to_country.Items.Add(combo_variable);
                }
                reader.Close();

            }
            else
            {

                reader.Close();

            }

        }

        private void Back(object sender, RoutedEventArgs e)
        {
            parent.Visibility = Visibility.Visible;
            this.Close();

        }



        private void from_country_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            string selected_state = from_country.SelectedItem.ToString();

            string sqlExpression = "SELECT DISTINCT City, Country FROM dbo.Airports WHERE (Country = '" + selected_state + "')";
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            SqlDataReader reader = command.ExecuteReader();
            string combo_variable;

            if (reader.HasRows)
            {
                from_city.Items.Clear();
                while (reader.Read())
                {
                    combo_variable = reader.GetString(0);
                    from_city.Items.Add(combo_variable);

                }
                reader.Close();

            }
            else
            {

                reader.Close();

            }
        }

        private void to_country_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            
            string selected_state = to_country.SelectedItem.ToString();
            string sqlExpression = "SELECT DISTINCT City, Country FROM dbo.Airports WHERE (Country = '" + selected_state + "')";
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            SqlDataReader reader = command.ExecuteReader();
            string combo_variable;

            if (reader.HasRows)
            {
                to_city.Items.Clear(); 
                while (reader.Read())
                {
                    combo_variable = reader.GetString(0);
                    to_city.Items.Add(combo_variable);

                }
                reader.Close();

            }
            else
            {

                reader.Close();

            }


        }

        private void search_Click(object sender, RoutedEventArgs e)
        {
            if (from_city.Text != to_city.Text)
            {
                if (from_country.SelectedItem != null && from_city.SelectedItem != null && to_country.SelectedItem != null && to_city.SelectedItem != null && date_out.DisplayDate != null)
                {
                    string selected_country_out = from_country.SelectedItem.ToString();
                    string selected_country_in = to_country.SelectedItem.ToString();
                    string selected_city_out = from_city.SelectedItem.ToString();
                    string selected_city_in = to_city.SelectedItem.ToString();
                    string selected_date = date_out.DisplayDate.ToString("dd.MM.yyyy");
                    this.Visibility = Visibility.Hidden;
                    new ResultSearch(connection, this, selected_country_out, selected_country_in, selected_city_out, selected_city_in, selected_date, login);

                }
            }
            else {
                MessageBox.Show("Город вылета и город прилёта совпадает!");
            
            }
        }
 
    }
}
