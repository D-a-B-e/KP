using System.Windows;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace AirportProject
{
    public class sold_tickets
    {
        public string idBooking { get; set; }
        public string Login { get; set; }
        public string DateBooking { get; set; }
        public string idFly { get; set; }
        public string NameAircraft { get; set; }
        public string DateOut { get; set; }
       
        
    }

    public class buyers
    {
        public string LoginClient { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string MidName { get; set; }
        public string Dob { get; set; }
        public string mail { get; set; }

    }


    public class flights_stat { 
        public string idFly { get; set; }
        public string DateOut { get; set; }
        public string aircraft { get; set; }
        public string from_country { get; set; }
        public string to_country { get; set; }
        
    }



    public partial class Information : Window
    {
        private Window parent;
        private SqlConnection connection;
        public Information(SqlConnection connection, Window parent)
        {
            InitializeComponent();
            this.Show();
            this.parent = parent;
            this.connection = connection;
            parent.Visibility = Visibility.Hidden;
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            parent.Visibility = Visibility.Visible;
            this.Close();

        }

        private void Tickets(object sender, RoutedEventArgs e)
        {
            List<sold_tickets> ticket_list = new List<sold_tickets>();
            string sqlExpression = "SELECT dbo.Booking.IdBooking, dbo.People.Login, dbo.Booking.DateBooking, dbo.Flights.IdFly, dbo.Aircrafts.NameAircraft, dbo.Flights.DateOut "
                + " FROM dbo.Booking INNER JOIN "
               + "  dbo.People ON dbo.Booking.LoginPassenger = dbo.People.Login INNER JOIN"
              +  "  dbo.Flights ON dbo.Booking.IdFly = dbo.Flights.IdFly AND dbo.Booking.DateOut = dbo.Flights.DateOut INNER JOIN"
              + "   dbo.Aircrafts ON dbo.Flights.IdAircraft = dbo.Aircrafts.IdAircraft";

            SqlCommand command = new SqlCommand(sqlExpression, connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {

                //  
                while (reader.Read())
                {

                    sold_tickets rec = new sold_tickets(); 

                    rec.idBooking = reader.GetString(0);
                    rec.Login = reader.GetString(1);
                    rec.DateBooking = reader.GetDateTime(2).ToString("yyyy.dd.MM");
                    rec.idFly = reader.GetString(3);
                    rec.NameAircraft = reader.GetString(4);
                    rec.DateOut = reader.GetDateTime(5).ToString("yyyy.dd.MM");
                    
                    
                    ticket_list.Add(rec);


                }
                reader.Close();

                table.ItemsSource = ticket_list;
                table.Columns[0].Header = "Номер брони";
                table.Columns[1].Header = "Покупатель";
                table.Columns[2].Header = "Дата Брони";
                table.Columns[3].Header = "Рейс";
                table.Columns[4].Header = "Самолёт";
                table.Columns[5].Header = "Дата вылета";
                

            }
            else
            {
               
                reader.Close();

            }





        }

        private void Clients(object sender, RoutedEventArgs e)
        {
            List<buyers> buyers_list = new List<buyers>();
            string sqlExpression =  " SELECT DISTINCT dbo.People.Login, dbo.People.LastName, dbo.People.FirstName, dbo.People.MiddleName, dbo.People.Dob, dbo.People.Email "
            + " FROM     dbo.People INNER JOIN " +
            " dbo.Booking ON dbo.People.Login = dbo.Booking.LoginPassenger ";
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {

                while (reader.Read()) {    
                buyers rec = new buyers();
                    rec.LoginClient = reader.GetString(0);
                    rec.Surname = reader.GetString(1);
                    rec.Name = reader.GetString(2);
                    rec.MidName = reader.GetString(3);
                    rec.Dob = reader.GetDateTime(4).ToString("yyyy.dd.MM");
                    rec.mail = reader.GetString(5);
                    buyers_list.Add(rec);
                }
                reader.Close();
                table.ItemsSource = buyers_list;
                table.Columns[0].Header = "Логин";
                table.Columns[1].Header = "Фамилия";
                table.Columns[2].Header = "Имя";
                table.Columns[3].Header = "Отчество";
                table.Columns[4].Header = "Дата рождения";
                table.Columns[5].Header = "Е-майл";
            }
            else {

                reader.Close();
            }


        }

        private void Flights(object sender, RoutedEventArgs e)
        {
            List<flights_stat> flights_list = new List<flights_stat>();
            string sqlExpression = "SELECT dbo.Flights.IdFly, dbo.Flights.DateOut, dbo.Aircrafts.NameAircraft, Airports_1.Country, Airports_1.City, dbo.Airports.Country AS Expr1, dbo.Airports.City AS Expr2 "
              + "   FROM     dbo.Flights INNER JOIN"
              + "   dbo.Aircrafts ON dbo.Flights.IdAircraft = dbo.Aircrafts.IdAircraft INNER JOIN "
              + "   dbo.Airports ON dbo.Flights.IdAirportOut = dbo.Airports.IdAirport INNER JOIN "
              + "   dbo.Airports AS Airports_1 ON dbo.Flights.IdAirportIn = Airports_1.IdAirport ";

            SqlCommand command = new SqlCommand(sqlExpression, connection);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {

                while (reader.Read()) { 
                    flights_stat rec = new flights_stat();
                    rec.idFly = reader.GetString(0);
                    rec.DateOut = reader.GetDateTime(1).ToString("yyyy.dd.MM");
                    rec.aircraft = reader.GetString(2);
                    rec.from_country = reader.GetString(3) + ", " + reader.GetString(4);
                    rec.to_country = reader.GetString(5) + ", " + reader.GetString(6);
                    flights_list.Add(rec);
                
                }
                reader.Close();
                table.ItemsSource = flights_list;
                table.Columns[0].Header = "Рейс";
                table.Columns[1].Header = "Дата Вылета";
                table.Columns[2].Header = "Самолёт";
                table.Columns[3].Header = "Откуда";
                table.Columns[4].Header = "Куда";
               

            }
            else {

                reader.Close();
            
            }

        }
    }
}
