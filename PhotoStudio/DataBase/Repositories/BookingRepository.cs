using System;
using System.Collections.Generic;
using Npgsql;
using PhotoStudio.Models.DataBase;
using PhotoStudio.Services.Interfaces;

namespace PhotoStudio.DataBase.Repositories;

public class BookingRepository : IBookingInterface
{
    private readonly string _connectionString;
    private NpgsqlConnection connection;

    public BookingRepository(string connectionString)
    {
        _connectionString = connectionString;
        connection = new NpgsqlConnection(_connectionString);
    }

    public Booking GetBooking(int id)
    {
        connection.Open();
        Booking booking = new();
        string query =
            "select * from booking join worker w on booking.id_worker = w.id_worker join request r on booking.id_request = r.id_request where id_order = ($1)";
        NpgsqlCommand command = new(query, connection)
        {
            Parameters = { new NpgsqlParameter() { Value = id } }
        };
        using (command)
        {
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    booking.Id = Convert.ToInt32(reader["id_order"]);
                    booking.Worker.PersonalInfo.LastName = reader["last_name"].ToString();
                    booking.Worker.PersonalInfo.FirstName = reader["first_name"].ToString();
                    booking.Worker.PersonalInfo.MiddleName = reader["middle_name"].ToString();
                    booking.Worker.PersonalInfo.Email = reader["email"].ToString();
                    booking.Worker.PersonalInfo.MobilePhone = reader["mobile_phone"].ToString();
                    booking.Worker.Role.RoleName = reader["role_name"].ToString();
                    booking.Request.Client.PersonalInfo.LastName = reader["last_name"].ToString();
                    booking.Request.Client.PersonalInfo.FirstName = reader["first_name"].ToString();
                    booking.Request.Client.PersonalInfo.MiddleName = reader["middle_name"].ToString();
                    booking.Request.Client.PersonalInfo.Email = reader["email"].ToString();
                    booking.Request.Client.PersonalInfo.MobilePhone = reader["mobile_phone"].ToString();
                    booking.Request.RequestTimestamp = Convert.ToDateTime(reader["order_timestamp"]);
                    booking.OrderTimestamp = Convert.ToDateTime(reader["order_timestamp"]);
                    booking.TotalPrice = Convert.ToDecimal(reader["order_total_price"]);

                }
            }
        }

        connection.Close();
        return booking;
    }

    public Booking AddBooking(Booking booking)
    {
        connection.Open();
        string query =
            "insert into booking (id_worker, id_request, order_total_price, order_timestamp) values ($1, $2, $3, $4) ";
        NpgsqlCommand command = new(query, connection)
        {
            Parameters =
            {
                new NpgsqlParameter() { Value = booking.Worker.Id },
                new NpgsqlParameter() { Value = booking.Request.Id },
                new NpgsqlParameter() { Value = booking.TotalPrice },
                new NpgsqlParameter() { Value = booking.OrderTimestamp }
            }
        };
        try
        {
            command.ExecuteNonQuery();
            return booking;
        }
        catch (Exception e)
        {
            return null;
        }
        finally
        {
            connection.Close();
        }
    }

    public bool EditBooking(Booking booking)
    {
        connection.Open();
        string query =
            "update booking set id_worker = ($1), id_request=($2), order_total_price = ($3), order_timestamp=($4) where id_order= ($5)";
        NpgsqlCommand command = new(query, connection)
        {
            Parameters =
            {
                new NpgsqlParameter() { Value = booking.Worker.Id },
                new NpgsqlParameter() { Value = booking.Request.Id },
                new NpgsqlParameter() { Value = booking.TotalPrice },
                new NpgsqlParameter() { Value = booking.OrderTimestamp },
                new NpgsqlParameter() { Value = booking.Id }
            }
        };
        try
        {
            command.ExecuteNonQuery();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
        finally
        {
            connection.Close();
        }
    }

    public bool DeleteBooking(int id)
    {
        connection.Open();
        string query = "delete from booking where  id_order = ($1)";
        NpgsqlCommand command = new(query, connection)
        {
            Parameters = { new NpgsqlParameter() { Value = id } }
        };
        try
        {
            command.ExecuteNonQuery();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
        finally
        {
            connection.Close();
        }
    }

    public List<Booking> GetAllBookings(Booking booking)
    {
        connection.Open();
        List<Booking> bookings = new();
        string query =
            "select * from booking join worker w on booking.id_worker = w.id_worker join request r on booking.id_request = r.id_request";
        NpgsqlCommand command = new(query, connection);
        using (command)
        {
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    booking.Id = Convert.ToInt32(reader["id_order"]);
                    booking.Worker.PersonalInfo.LastName = reader["last_name"].ToString();
                    booking.Worker.PersonalInfo.FirstName = reader["first_name"].ToString();
                    booking.Worker.PersonalInfo.MiddleName = reader["middle_name"].ToString();
                    booking.Worker.PersonalInfo.Email = reader["email"].ToString();
                    booking.Worker.PersonalInfo.MobilePhone = reader["mobile_phone"].ToString();
                    booking.Worker.Role.RoleName = reader["role_name"].ToString();
                    booking.Request.Client.PersonalInfo.LastName = reader["last_name"].ToString();
                    booking.Request.Client.PersonalInfo.FirstName = reader["first_name"].ToString();
                    booking.Request.Client.PersonalInfo.MiddleName = reader["middle_name"].ToString();
                    booking.Request.Client.PersonalInfo.Email = reader["email"].ToString();
                    booking.Request.Client.PersonalInfo.MobilePhone = reader["mobile_phone"].ToString();
                    booking.Request.RequestTimestamp = Convert.ToDateTime(reader["order_timestamp"]);
                    booking.OrderTimestamp = Convert.ToDateTime(reader["order_timestamp"]);
                    booking.TotalPrice = Convert.ToDecimal(reader["order_total_price"]);
                    bookings.Add(booking);

                }
            }
        }
        connection.Close();
        return bookings;
    }
}