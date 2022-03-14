using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IncomingMailWebApp.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace IncomingMailWebApp.Services
{
    public class LoadDb
    {
        readonly string connectionDb = @"Server=.\SQLEXPRESS;Database=IncomingMail;Trusted_Connection=True;Trust Server Certificate=True";

        public async Task<List<Tag>> LoadTag()
        {
            List<Tag> tags = new List<Tag>();

            string sqlLoadAllTag = "GetTags";

            using (SqlConnection connection = new SqlConnection(connectionDb))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlLoadAllTag, connection);

                command.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader != null)
                {
                    while (await reader.ReadAsync())
                    {
                        Tag tag = new Tag();

                        tag.Id = reader.GetInt32(0);
                        tag.TagName = reader.GetString(1);

                        tags.Add(tag);
                    }
                }
                return tags;
            }
        }

        public async Task<Tag> SearchTag(int id)
        {
            string sqlSearchTag = $"SELECT * FROM Tags WHERE Id = @id";

            Tag tag = new Tag();

            using (SqlConnection connection = new SqlConnection(connectionDb))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlSearchTag, connection);

                command.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader != null)
                {
                    while (await reader.ReadAsync())
                    {
                        tag.Id = reader.GetInt32(0);
                        tag.TagName = reader.GetString(1);
                    }
                }
                return tag;
            }
        }

        public async Task<List<Addressee>> LoadAddressee()
        {
            List<Addressee> addressees = new List<Addressee>();

            string sqlLoadAllAddressee = "GetAddressees";

            using (SqlConnection connection = new SqlConnection(connectionDb))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlLoadAllAddressee, connection);

                command.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader != null)
                {
                    while (await reader.ReadAsync())
                    {
                        Addressee addressee = new Addressee();

                        addressee.Id = reader.GetInt32(0);
                        addressee.AddresseeName = reader.GetString(1);

                        addressees.Add(addressee);
                    }
                }
                return addressees;
            }
        }

        public async Task<Addressee> SearchAddressee(int id)
        {
            string sqlSearchAddressee = $"SELECT * FROM Addressees WHERE Id = @id";

            Addressee addressee = new Addressee();

            using (SqlConnection connection = new SqlConnection(connectionDb))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlSearchAddressee, connection);

                command.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader != null)
                {
                    while (await reader.ReadAsync())
                    {
                        addressee.Id = reader.GetInt32(0);
                        addressee.AddresseeName = reader.GetString(1);
                    }
                }
                return addressee;
            }
        }

        public async Task<List<Sender>> LoadSender()
        {
            List<Sender> senders = new List<Sender>();

            string sqlLoadAllSender = "GetSenders";

            using (SqlConnection connection = new SqlConnection(connectionDb))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlLoadAllSender, connection);

                command.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader != null)
                {
                    while (await reader.ReadAsync())
                    {
                        Sender sender = new Sender();

                        sender.Id = reader.GetInt32(0);
                        sender.SenderName = reader.GetString(1);

                        senders.Add(sender);
                    }
                }
                return senders;
            }
        }

        public async Task<Sender> SearchSender(int id)
        {
            string sqlSearchSender = $"SELECT * FROM Senders WHERE Id = @id";

            Sender sender = new Sender();

            using (SqlConnection connection = new SqlConnection(connectionDb))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlSearchSender, connection);

                command.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader != null)
                {
                    while (await reader.ReadAsync())
                    {
                        sender.Id = reader.GetInt32(0);
                        sender.SenderName = reader.GetString(1);
                    }
                }
                return sender;
            }
        }

        public async Task<List<Mail>> LoadLetter()
        {
            List<Mail> letters = new List<Mail>();

            string sqlLoadAllLetter = "GetLetters";

            using (SqlConnection connection = new SqlConnection(connectionDb))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlLoadAllLetter, connection);

                command.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader != null)
                {
                    while (await reader.ReadAsync())
                    {
                        Mail mail = new Mail();

                        mail.Id = reader.GetInt32(0);
                        mail.LetterTitle = reader.GetString(1);
                        mail.DateOfRegistration = reader.GetString(2);
                        mail.ContentLetter = reader.GetString(3);
                        mail.AddresseeId = reader.GetInt32(4);
                        mail.SenderId = reader.GetInt32(5);

                        letters.Add(mail);
                    }
                }
                return letters;
            }
        }

        public async Task<Mail> SearchLetter(int id)
        {
            string sqlSearchLetter = $"SELECT * FROM Mails WHERE Id = @id";

            Mail mail = new Mail();

            using (SqlConnection connection = new SqlConnection(connectionDb))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlSearchLetter, connection);

                command.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader != null)
                {
                    while (await reader.ReadAsync())
                    {
                        mail.Id = reader.GetInt32(0);
                        mail.LetterTitle = reader.GetString(1);
                        mail.DateOfRegistration = reader.GetString(2);
                        mail.ContentLetter = reader.GetString(3);
                        mail.AddresseeId = reader.GetInt32(4);
                        mail.SenderId = reader.GetInt32(5);
                    }
                }
                return mail;
            }
        }
    }
}
