using Microsoft.Data.SqlClient;
using IncomingMailWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Data;

namespace IncomingMailWebApp.Services
{
    public class AddLetterAndAttributes
    {
        readonly string connectionDb = @"Server=.\SQLEXPRESS;Database=IncomingMail;Trusted_Connection=True;Trust Server Certificate=True";

        public async Task AddAddresseeInDB(Addressee addressee)
        {
            string name = addressee.AddresseeName;

            string sqlAddAddressee = "INSERT INTO Addressees (AddresseeName) VALUES (@name);SET @id=SCOPE_IDENTITY()";

            using (SqlConnection connection = new SqlConnection(connectionDb))
            {         
                SqlCommand command = new SqlCommand(sqlAddAddressee, connection);

                SqlParameter nameParameter = new SqlParameter("@name", name);
                command.Parameters.Add(nameParameter);

                SqlParameter idParam = new SqlParameter();
                idParam.ParameterName = "@id";
                idParam.Size = 4;
                idParam.Direction = ParameterDirection.Output;
                command.Parameters.Add(idParam);

                await connection.OpenAsync();

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task AddSenderInDB(Sender sender)
        {
            string name = sender.SenderName;

            string sqlAddSender = "INSERT INTO Senders (SenderName) VALUES (@name); SET @id=SCOPE_IDENTITY()";

            using (SqlConnection connection = new SqlConnection(connectionDb))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlAddSender, connection);

                SqlParameter nameParameter = new SqlParameter("@name", name);
                command.Parameters.Add(nameParameter);

                SqlParameter idParameter = new SqlParameter
                {
                    ParameterName = "id",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Direction = System.Data.ParameterDirection.Output
                };

                command.Parameters.Add(idParameter);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<int> AddLetterInDB(ViewModelLetter viewModelLetter)
        {
            string title = viewModelLetter.Mail.LetterTitle;
            string date = viewModelLetter.Mail.DateOfRegistration.ToString();
            string content = viewModelLetter.Mail.ContentLetter;
            int senderId = viewModelLetter.Sender.Id;
            int addresseeId = viewModelLetter.Addressee.Id;

            string sqlAddMail = "INSERT INTO Mails (LetterTitle, DateOfRegistration, ContentLetter, SenderId, AddresseeId) VALUES (@title, @date, @content, @senderId, @addresseeId); SET @id=SCOPE_IDENTITY()";

            int idLetter = 0;

            using (SqlConnection connection = new SqlConnection(connectionDb))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlAddMail, connection);

                SqlParameter titleParameter = new SqlParameter("@title", title);
                command.Parameters.Add(titleParameter);

                SqlParameter dateParameter = new SqlParameter("@date", date);
                command.Parameters.Add(dateParameter);

                SqlParameter contentParameter = new SqlParameter("@content", content);
                command.Parameters.Add(contentParameter);

                command.Parameters.AddWithValue("@senderId", senderId);

                command.Parameters.AddWithValue("@addresseeId", addresseeId);

                SqlParameter idParameter = new SqlParameter
                {
                    ParameterName = "id",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Direction = System.Data.ParameterDirection.Output
                };

                command.Parameters.Add(idParameter);

                await command.ExecuteNonQueryAsync();

                idLetter = Convert.ToInt32(idParameter.Value);
            }

            return idLetter;
        }

        public async Task AddTagDB(Tag tag)
        {
            string name = tag.TagName;

            string sqlAddSender = "INSERT INTO Tags (TagName) VALUES (@name);";

            using (SqlConnection connection = new SqlConnection(connectionDb))
            {
                SqlCommand command = new SqlCommand(sqlAddSender, connection);

                SqlParameter nameParameter = new SqlParameter("@name", name);
                command.Parameters.Add(nameParameter);


                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task AddMailTagDB(int idLetter, int idTag)
        {
            string sqlAddSender = "INSERT INTO MailTag (MailsId, TagsId) VALUES (@idLetter, @idTag)";

            using (SqlConnection connection = new SqlConnection(connectionDb))
            {
                SqlCommand command = new SqlCommand(sqlAddSender, connection);

                command.Parameters.AddWithValue("@idLetter", idLetter);

                command.Parameters.AddWithValue("@idTag", idTag);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
