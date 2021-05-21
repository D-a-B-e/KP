using System;
using System.Collections.Generic;
using System.Windows;
using System.Data.SqlClient;


namespace AirportProject
{
    public partial class AddFlight : Window
    {
        SqlConnection connection;
        Window parent;
        private Dictionary<string, string> map;
        private Dictionary<string, string> map2;
        public AddFlight(SqlConnection connection, Window parent)
        {
            InitializeComponent();
            map = new Dictionary<string, string>();
            map2 = new Dictionary<string, string>();
            this.Show();
            this.parent = parent;
            parent.Visibility = Visibility.Hidden;
            this.connection = connection;
            map.Clear();
            
            for (int i = 0; i <= 23; i++) {
                hour_from.Items.Add($"{i}");
                hour_in.Items.Add($"{i}");
            }

            for (int i = 0; i <= 59; i++)
            {
                min_from.Items.Add($"{i}");
                min_in.Items.Add($"{i}");
            }

            try
            {
                string sqlExpression = "SELECT IdAircraft, NameAircraft FROM dbo.Aircrafts";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string local_id_aircraft = reader.GetString(0);
                        string local_name_aircraft = reader.GetString(1);
                        aircraft.Items.Add(local_name_aircraft);
                        map[local_name_aircraft] = local_id_aircraft;
                    }


                    reader.Close();
                }
                else
                {

                    reader.Close();

                }


                sqlExpression = "SELECT IdAirport, NameAirport FROM dbo.Airports";
                command = new SqlCommand(sqlExpression, connection);
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string local_id_airport = reader.GetString(0);
                        string local_name_airport = reader.GetString(1);
                        from_airport.Items.Add(local_name_airport);
                        to_airport.Items.Add(local_name_airport);
                        map2[local_name_airport] = local_id_airport;


                    }


                    reader.Close();
                }
                else
                {

                    reader.Close();

                }






            }
            catch (SqlException exept) {
                MessageBox.Show((exept.Number).ToString() + " " + exept.Message);

            
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            parent.Visibility = Visibility.Visible;
            this.Close();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            string id_fly_t =  id_fly.Text; // 
            string aircraft_t = map[aircraft.Text]; // 
            string from = map2[from_airport.Text]; // 
            string to = map2[to_airport.Text]; //
            DateTime date_o = date_out.DisplayDate; // 
            string time_from = hour_from.Text + ":" + min_from.Text + ":00"; // 
            DateTime date_i = date_in.DisplayDate; // 
            string time_in = hour_in.Text + ":" + min_in.Text + ":00"; //

            //проверка на выбранные комбо боксы и заполненные поля

            try
            {
                string sqlExpression = "INSERT INTO Flights VALUES(@id_fly_value, @id_aircraft_value, @id_airport_out_value, @id_airport_in_value, @date_out_value, @time_out_value, @date_in_value, @time_in_value)";
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                SqlParameter id_fly_par = new SqlParameter("@id_fly_value", id_fly_t);
                command.Parameters.Add(id_fly_par);

                SqlParameter id_aircraft_par = new SqlParameter("@id_aircraft_value", aircraft_t);
                command.Parameters.Add(id_aircraft_par);

                SqlParameter from_par = new SqlParameter("@id_airport_out_value", from);
                command.Parameters.Add(from_par);

                SqlParameter to_par = new SqlParameter("@id_airport_in_value", to);
                command.Parameters.Add(to_par);

                SqlParameter date_out_par = new SqlParameter("@date_out_value", date_o);
                command.Parameters.Add(date_out_par);

                SqlParameter time_out_par = new SqlParameter("@time_out_value", time_from);
                command.Parameters.Add(time_out_par);

                SqlParameter date_in_par = new SqlParameter("@date_in_value", date_i);
                command.Parameters.Add(date_in_par);

                SqlParameter time_in_par = new SqlParameter("@time_in_value", time_in);
                command.Parameters.Add(time_in_par);

                command.ExecuteNonQuery();

                MessageBox.Show("Рейс " + id_fly_t + " добавлен");

            }
            catch (SqlException exept) {

                MessageBox.Show((exept.Number).ToString() + " " + exept.Message);
            }

        }
    }
}
