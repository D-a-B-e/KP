using System;
using System.Collections.Generic;
using System.Windows;
using System.Data.SqlClient;
using System.IO;

namespace AirportProject
{

    public class all_tickets
    {
        public string idFly { get; set; }
        public string CountryFrom { get; set; }
        public string CityFrom { get; set; }
        public string DateOut { get; set; }
        public TimeSpan TimeOut { get; set; }
        public string CountryIn { get; set; }
        public string CityIn { get; set; }
        public TimeSpan TimeIn { get; set; }
        public string NameAircraft { get; set; }

    }


    public partial class ResultSearch : Window
    {
        private SqlConnection connection;
        private Window parent;
        string login;

       public ResultSearch(SqlConnection connection, Window parent, string selected_country_out, string selected_country_in, string selected_city_out, string selected_city_in, string selected_date, string login)
        {
            InitializeComponent();
            this.Show();
            this.parent = parent;
            this.connection = connection;
            this.login = login;
            List<all_tickets> tickets_list = new List<all_tickets>();
          
            string sqlExpression = "SELECT DISTINCT dbo.Flights.IdFly, dbo.Flights.DateOut, dbo.Aircrafts.NameAircraft, dbo.Airports.Country, dbo.Airports.City, dbo.Flights.TimeOut, Airports_1.Country AS Expr1, Airports_1.City AS Expr2, dbo.Flights.TimeIn "
             +   " FROM            dbo.zapros INNER JOIN "
             +            " dbo.Flights ON dbo.zapros.IdFly = dbo.Flights.IdFly INNER JOIN "
             +           " dbo.Aircrafts ON dbo.Flights.IdAircraft = dbo.Aircrafts.IdAircraft INNER JOIN "
             +           " dbo.Booking ON dbo.Flights.IdFly = dbo.Booking.IdFly AND dbo.Flights.DateOut = dbo.Booking.DateOut INNER JOIN "
             +           " dbo.Airports ON dbo.Flights.IdAirportOut = dbo.Airports.IdAirport INNER JOIN "
             +           " dbo.Airports AS Airports_1 ON dbo.Flights.IdAirportIn = Airports_1.IdAirport "
             +   " WHERE(dbo.Aircrafts.PlacesCount - dbo.zapros.Expr1 > 0) AND(dbo.Airports.Country = N'" + selected_country_out + "') AND(dbo.Airports.City = N'" + selected_city_out + "') AND(Airports_1.Country = N'" + selected_country_in + "') AND(Airports_1.City = N'" + selected_city_in + "'  AND (dbo.Flights.DateOut = '" + selected_date + "')) ";

            SqlCommand command = new SqlCommand(sqlExpression, connection);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    all_tickets row =  new all_tickets();
                    row.idFly = reader.GetString(0);
                    row.DateOut = reader.GetDateTime(1).ToString("yyyy.MM.dd");
                    row.NameAircraft = reader.GetString(2);
                    row.CountryFrom = reader.GetString(3);
                    row.CityFrom = reader.GetString(4);
                    row.TimeOut = reader.GetTimeSpan(5);
                    row.CountryIn = reader.GetString(6);
                    row.CityIn = reader.GetString(7);
                    row.TimeIn = reader.GetTimeSpan(8);
                    
                    tickets_list.Add(row);

                }
                reader.Close();
                grid.ItemsSource = tickets_list;

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

        private void Buy_ticket_flight(object sender, RoutedEventArgs e)
        {

            all_tickets ticket = (all_tickets)grid.SelectedItem;
            if (ticket != null) {
                string id_fly = ticket.idFly;
                string date_out = ticket.DateOut;
                DateTime local_time = DateTime.Now;
                string code_booking = local_time.ToString("yyyyMMddhhmm") + id_fly;
                string date_booking = local_time.ToString("yyyy.MM.dd");
                string sqlExpression = "INSERT INTO dbo.Booking VALUES(@booking_value, @date_booking_value, @login_value, @id_fly_value, @date_out_value)";
                SqlCommand command = new SqlCommand(sqlExpression, connection); ;
                SqlParameter booking_par = new SqlParameter("@booking_value", code_booking);
                SqlParameter date_booking_par = new SqlParameter("@date_booking_value", date_booking);
                SqlParameter login_par = new SqlParameter("@login_value", login);
                SqlParameter id_fly_par = new SqlParameter("@id_fly_value", id_fly);
                SqlParameter date_out_par = new SqlParameter("@date_out_value", date_out);
                command.Parameters.Add(booking_par);
                command.Parameters.Add(date_booking_par);
                command.Parameters.Add(login_par);
                command.Parameters.Add(id_fly_par);
                command.Parameters.Add(date_out_par);
                command.ExecuteNonQuery();
                string file_name = code_booking + ".txt";
                StreamWriter file = new StreamWriter(code_booking);
                file.WriteLine("Номер бронирования: " + code_booking);
                file.WriteLine("Дата бронирования: " + date_booking);
                file.WriteLine("Номер рейса: " + id_fly );
                file.WriteLine("Дата вылета: " + date_out);
                file.Close();
                MessageBox.Show("Билет куплен! Спасибо за покупку!");

            }
        }
    }
}
