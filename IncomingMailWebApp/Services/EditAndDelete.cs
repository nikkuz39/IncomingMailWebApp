using Microsoft.Data.SqlClient;
using System;
using System.Threading.Tasks;
using IncomingMailWebApp.Models;
using System.Data;

namespace IncomingMailWebApp.Services
{
    public class EditAndDelete
    {
        readonly string connectionDb = @"Server=.\SQLEXPRESS;Database=IncomingMail;Trusted_Connection=True;Trust Server Certificate=True";
        
        public async Task EditTag(Tag tag)
        {
            int id = tag.Id;
            string name = tag.TagName;

            string sqlEditTag = $"UPDATE Tags SET TagName = @name WHERE Id = @id";

            using (SqlConnection connection = new SqlConnection(connectionDb))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlEditTag, connection);

                SqlParameter nameParameter = new SqlParameter("@name", name);
                command.Parameters.Add(nameParameter);

                command.Parameters.AddWithValue("@id", id);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteTag(int id)
        {
            string sqlDeleteTag = $"DELETE FROM Tags WHERE Id = @id";

            using (SqlConnection connection = new SqlConnection(connectionDb))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlDeleteTag, connection);

                command.Parameters.AddWithValue("@id", id);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task EditAddressee(Addressee addressee)
        {
            int id = addressee.Id;
            string name = addressee.AddresseeName;

            string sqlEditAddressee = $"UPDATE Addressees SET AddresseeName = @name WHERE Id = @id";

            using (SqlConnection connection = new SqlConnection(connectionDb))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlEditAddressee, connection);

                SqlParameter nameParameter = new SqlParameter("@name", name);
                command.Parameters.Add(nameParameter);

                command.Parameters.AddWithValue("@id", id);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteAddressee(int id)
        {
            string sqlDeleteAddressee = $"DELETE FROM Addressees WHERE Id = @id";

            using (SqlConnection connection = new SqlConnection(connectionDb))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlDeleteAddressee, connection);

                command.Parameters.AddWithValue("@id", id);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task EditSender(Sender sender)
        {
            int id = sender.Id;
            string name = sender.SenderName;

            string sqlEditSender = $"UPDATE Senders SET SenderName = @name WHERE Id = @id";

            using (SqlConnection connection = new SqlConnection(connectionDb))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlEditSender, connection);

                SqlParameter nameParameter = new SqlParameter("@name", name);
                command.Parameters.Add(nameParameter);

                command.Parameters.AddWithValue("@id", id);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteSender(int id)
        {
            string sqlDeleteSender = $"DELETE FROM Senders WHERE Id = @id";

            using (SqlConnection connection = new SqlConnection(connectionDb))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlDeleteSender, connection);

                command.Parameters.AddWithValue("@id", id);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteLetter(int id)
        {
            string sqlDeleteSender = $"DELETE FROM Mails WHERE Id = @id";

            using (SqlConnection connection = new SqlConnection(connectionDb))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlDeleteSender, connection);

                command.Parameters.AddWithValue("@id", id);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task EditLetter(Mail mail)
        {
            int letterId = mail.Id;
            string title = mail.LetterTitle;
            string date = mail.DateOfRegistration.ToString();
            string content = mail.ContentLetter;
            int senderId = mail.SenderId;
            int addresseeId = mail.AddresseeId;

            string sqlEditLetter = $"UPDATE Mails SET LetterTitle = @title, DateOfRegistration = @date, ContentLetter = @content, SenderId = @senderId, AddresseeId = @addresseeId WHERE Id = @letterId";

            using (SqlConnection connection = new SqlConnection(connectionDb))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlEditLetter, connection);

                SqlParameter titleParameter = new SqlParameter("@title", title);
                command.Parameters.Add(titleParameter);

                SqlParameter dateParameter = new SqlParameter("@date", date);
                command.Parameters.Add(dateParameter);

                SqlParameter contentParameter = new SqlParameter("@content", content);
                command.Parameters.Add(contentParameter);

                command.Parameters.AddWithValue("@senderId", senderId);

                command.Parameters.AddWithValue("@addresseeId", addresseeId);

                command.Parameters.AddWithValue("@letterId", letterId);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task EditMailTag(int idLetter, int idTag)
        {
            string sqlEditMailTag = $"UPDATE MailTag SET TagsId = @idTag WHERE MailsId = @idLetter";

            using (SqlConnection connection = new SqlConnection(connectionDb))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlEditMailTag, connection);

                command.Parameters.AddWithValue("@idLetter", idLetter);

                command.Parameters.AddWithValue("@idTag", idTag);
                
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
