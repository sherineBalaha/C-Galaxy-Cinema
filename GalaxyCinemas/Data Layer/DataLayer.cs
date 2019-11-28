using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data;

//added statement to reference Business_Objects Library
using Common.Business_Objects;

namespace GalaxyCinemas
{
    public class DataLayer
    {
        private static string connStr = null;

        public static string ConnectionString
        {
            get
            {
                if (connStr == null)
                {
                    connStr = ConfigurationManager.ConnectionStrings["GalaxyCinemasConnectionString"].ConnectionString;
                }
                return connStr;
            }
        }

        /// <summary>
        /// Get list of all Movies from the database.
        /// </summary>
        public static List<Movie> GetAllMovies()
        {
           
            List<Movie> movies = new List<Movie>();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string sql = @"select MovieID, Title
                             from [Movie]";
                             
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                                  
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Movie movie = new Movie();
                            movie.MovieID = reader.GetInt32(1);
                            movie.Title = reader.GetString(2);
                            movies.Add(movie);
                        }
                    }
                }
            }

            return movies;


        }


        /// <summary>
        /// Get all bookings from the database in the future.
        /// </summary>
        public static List<Booking> GetBookingsInDateRange(DateTime fromDate, DateTime toDate)
        {
            List<Booking> bookings = new List<Booking>();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string sql = @"select BookingNumber, [Booking].SessionID, Quantity, Special, OriginalPrice, Discount, FinalPrice, [Session].SessionDate
                            from [Booking] inner join [Session] on [Booking].SessionID = [Session].SessionID
                            where [Session].SessionDate >= @startdate and [Session].SessionDate < @enddate";
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("startdate", fromDate.Date);
                    command.Parameters.AddWithValue("enddate", toDate.Date.AddDays(1));
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Booking booking = new Booking();
                            booking.BookingNumber = reader.GetInt32(0);
                            booking.SessionID = reader.GetInt32(1);
                            booking.Quantity = reader.GetByte(2);
                            booking.Special = reader.GetString(3);
                            booking.OriginalPrice = reader.GetDecimal(4);
                            booking.Discount=reader.GetDecimal(5);
                            booking.FinalPrice = reader.GetDecimal(6);
                            booking.SessionDate = reader.GetDateTime(7);
                            bookings.Add(booking);
                        }
                    }
                }
            }

            return bookings;
        }

        /// <summary>
        /// Add a booking to the database.
        /// </summary>
        public static void AddBooking(Booking booking)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string sql = @"insert into [Booking] (SessionID, Quantity, Special, OriginalPrice, Discount, FinalPrice)
                            values (@SessionID, @Quantity, @Special, @OriginalPrice, @Discount, @FinalPrice);";
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("SessionID", booking.SessionID);
                    command.Parameters.AddWithValue("Quantity", booking.Quantity);
                    // ADO.NET doesn't accept null as a value, you need to use DBNull.
                    SqlParameter specialParam = new SqlParameter("Special", SqlDbType.VarChar);
                    if (booking.Special == null)
                        specialParam.Value = System.DBNull.Value;
                    else specialParam.Value = booking.Special;
                    command.Parameters.Add(specialParam);
                    command.Parameters.AddWithValue("OriginalPrice", booking.OriginalPrice);
                    command.Parameters.AddWithValue("Discount", booking.Discount);
                    command.Parameters.AddWithValue("FinalPrice", booking.FinalPrice);
                    command.ExecuteNonQuery();
                }

                sql = @"select max(BookingNumber) from [Booking] where SessionID = @SessionID and Quantity=@Quantity;";
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("SessionID", booking.SessionID);
                    command.Parameters.AddWithValue("Quantity", booking.Quantity);
                    booking.BookingNumber = (int)command.ExecuteScalar();
                }
            }
        }



        /// <summary>
        /// Get all Sessions for the given Movie on the given date from the database.
        /// </summary>
        public static List<Session> GetAllSessionsForMovie(int movieID, DateTime date)
        {
            List<Session> sessions = new List<Session>();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string sql = @"select SessionID, MovieID, SessionDate, CinemaNumber
                            from [Session]
                            where MovieID = @movieID and SessionDate >= @startDate and SessionDate < @endDate";
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("movieID", movieID);
                    command.Parameters.AddWithValue("startDate", date.Date);
                    command.Parameters.AddWithValue("endDate", date.Date.AddDays(1));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Session session = new Session();
                            session.SessionID = reader.GetInt32(0);
                            session.MovieID = reader.GetInt32(1);
                            session.SessionDate = reader.GetDateTime(2);
                            session.CinemaNumber = reader.GetByte(3);
                            sessions.Add(session);
                        }
                    }
                }
            }

            return sessions;
        }

        /// <summary>
        /// Get all Sessions in the future.
        /// </summary>
        public static List<Session> GetAllSessionsInTheFuture()
        {
            

            List<Session> sessions = new List<Session>();
            DateTime todaysDate = DateTime.Now;

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string sql = @"select SessionID, MovieID, SessionDate, CinemaNumber
                             from [Session]
                             where SessionDate =  @todaysDate";

                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("todaysDate", todaysDate);
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Session session      = new Session();
                            session.SessionID    = reader.GetInt32(0);
                            session.MovieID      = reader.GetInt32(1);
                            session.SessionDate  = reader.GetDateTime(2);
                            session.CinemaNumber = reader.GetByte(3);
                            sessions.Add(session);
                        }
                    }
                }
            }

            return sessions;
        }


        /// <summary>
        /// Get list of all Movies from the database.
        /// </summary>
        public static Session GetSessionByID(int sessionID)
        {

                   
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                Session session = new Session();
                string sql = @"select SessionID, MovieID, SessionDate, CinemaNumber
                             from [Session]
                             where SessionID =  @sessionID";

                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("sessionID", sessionID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader == null)
                            return null;
                        else
                        {
                             while (reader.Read())
                            {
                            
                                session.SessionID = reader.GetInt32(0);
                                session.MovieID = reader.GetInt32(1);
                                session.SessionDate = reader.GetDateTime(2);
                                session.CinemaNumber = reader.GetByte(3);
                            
                            }

                            return session;  
                        }
                    }
                    
               }
                
            }

       
        }

        /// <summary>
        /// Insert a movie into the database.
        /// </summary>
        public static void AddMovie(Movie movie)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string sql = @"insert into [Movie] (MovieID, Title) 
                             values (@movieID, @title)";

                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    
                    command.Parameters.AddWithValue("movieID", movie.MovieID);
                    command.Parameters.AddWithValue("title", movie.Title);

                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Add a session to the database.
        /// </summary>
        public static void AddSession(Session session)
        {


            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string sql = @"insert into [Session] (SessionID, MovieID, SessionDate, CinemaNumber) 
                             values (@sessionID, @movieID, @sessionDate, @cinemaNumber)";

                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("sessionID", session.SessionID);
                    command.Parameters.AddWithValue("movieID", session.MovieID);
                    command.Parameters.AddWithValue("sessionDate", session.SessionDate);
                    command.Parameters.AddWithValue("cinemaNumber", session.CinemaNumber);


                    command.ExecuteNonQuery();
                }
            }








        }

        /// <summary>
        /// Update a movie in the database.
        /// </summary>
        public static void UpdateMovie(Movie movie)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string sql = @"update [Movie]
                               set Title=@title
                               where MovieID=@movieID";
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("title", movie.Title);
                    command.Parameters.AddWithValue("movieID", movie.MovieID);

                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Update a session in the database.
        /// </summary>
        public static void UpdateSession(Session session)
        {
 
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string sql = @"update [Session] 
                               set (MovieID, SessionDate, CinemaNumber) 
                               where SessionID = @sessionID";

                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("sessionID", session.SessionID);
                    command.ExecuteNonQuery();

                }
            }










        }
    }
}
