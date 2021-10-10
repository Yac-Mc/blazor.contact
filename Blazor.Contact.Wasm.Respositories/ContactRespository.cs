using Blazor.Contacts.Wasm.Shared;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Contacts.Wasm.Respositories
{
    public class ContactRespository : IContactRespository
    {
        private readonly IDbConnection _dbConnection;

        public ContactRespository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task DeleteContact(int id)
        {
            var sql = "DELETE FROM [dbo].[Contacts] WHERE Id = @Id";
            var result = await _dbConnection.QueryFirstOrDefaultAsync<Contact>(
                sql, new { Id = id });
        }

        public async Task<IEnumerable<Contact>> GetAllContacts()
        {
            var sql = @"SELECT Id, 
                               FirstName, 
                               LastName, 
                               Phone, 
                               [Address]
                        FROM [dbo].[Contacts]";

            return await _dbConnection.QueryAsync<Contact>(sql);
        }

        public async Task<Contact> GetContactById(int id)
        {
            var sql = @"SELECT Id, 
                               FirstName, 
                               LastName, 
                               Phone, 
                               [Address]
                        FROM [dbo].[Contacts]
                        WHERE Id = @Id";

            return await _dbConnection.QueryFirstOrDefaultAsync<Contact>(
                sql, new { Id = id });
        }

        public async Task<bool> InsertContact(Contact contact)
        {
            try
            {
                var sql = @"INSERT INTO [dbo].[Contacts](FirstName, LastName, Phone, [Address])
                            VALUES(@FirstName, @LastName, @Phone, @Address)";

                var result = await _dbConnection.ExecuteAsync(
                    sql,
                    new 
                    {
                        contact.FirstName,
                        contact.LastName,
                        contact.Phone,
                        contact.Address
                    }
                );

                return result > 0;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> UpdateContact(Contact contact)
        {
            try
            {
                var sql = @"UPDATE [dbo].[Contacts]
                            SET FirstName = @FirstName,
                                LastName = @LastName,
                                Phone = @Phone,
                                [Address] = @Address
                            WHERE Id = @Id";

                var result = await _dbConnection.ExecuteAsync(
                    sql,
                    new
                    {
                        contact.Id,
                        contact.FirstName,
                        contact.LastName,
                        contact.Phone,
                        contact.Address
                    }
                );

                return result > 0;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
