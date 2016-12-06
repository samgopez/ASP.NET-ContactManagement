using MySql.Data.MySqlClient;
using Project1.Models;
using Project1.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Project1.Services
{
    public class ContactService : IContactService
    {
        public long AddContactToDb(Contact contact)
        {
            long generatedId = 0;
            using (MySqlConnection mysqlConnection = Connection.GetConnection())
            using (MySqlCommand mysqlCommand = new MySqlCommand())
            {
                mysqlCommand.CommandType = CommandType.Text;
                mysqlCommand.CommandText = "INSERT "
                                         + "INTO "
                                         + "contact"
                                         + "(firstname, lastname, addressline, city, province, country, email, contact_number) "
                                         + "VALUES"
                                         + "(@FirstName, @LastName, @AddressLine, @City, @Province, @Country, @Email, @ContactNumber)";

                mysqlCommand.Connection = mysqlConnection;

                mysqlCommand.Parameters.AddWithValue("@FirstName", contact.FirstName.ToUpper());
                mysqlCommand.Parameters.AddWithValue("@LastName", contact.LastName.ToUpper());
                mysqlCommand.Parameters.AddWithValue("@AddressLine", contact.AddressLine.ToUpper());
                mysqlCommand.Parameters.AddWithValue("@City", contact.City.ToUpper());
                mysqlCommand.Parameters.AddWithValue("@Province", contact.Province.ToUpper());
                mysqlCommand.Parameters.AddWithValue("@Country", contact.Country.ToUpper());
                mysqlCommand.Parameters.AddWithValue("@Email", contact.Email);
                mysqlCommand.Parameters.AddWithValue("@ContactNumber", contact.ContactNumber);

                mysqlConnection.Open();

                int rowInserted = mysqlCommand.ExecuteNonQuery();

                if (rowInserted > 0)
                {
                    generatedId = mysqlCommand.LastInsertedId;
                }
            }

            return generatedId;
        }

        public bool DeleteContactDb(int id)
        {
            bool status = false;

            using (MySqlConnection mysqlConnection = Connection.GetConnection())
            using (MySqlCommand mysqlCommand = new MySqlCommand())
            {
                mysqlCommand.CommandType = CommandType.Text;
                mysqlCommand.CommandText = "UPDATE "
                                         +     "contact "
                                         + "SET "
                                         +     "is_delete='0' "
                                         + "WHERE "
                                         +     "contact_id=@ContactId "
                                         + "AND "
                                         +     "is_delete='1'";
                mysqlCommand.Connection = mysqlConnection;

                mysqlConnection.Open();

                mysqlCommand.Parameters.AddWithValue("@ContactId", id);

                int rowAffected = mysqlCommand.ExecuteNonQuery();

                if (rowAffected > 0)
                {
                    status = true;
                }
            }

            return status;
        }

        public Contact GetContactById(int id)
        {
            Contact contact = new Contact();
            using (MySqlConnection mysqlConnection = Connection.GetConnection())
            using (MySqlCommand mysqlCommand = new MySqlCommand())
            {
                MySqlDataReader mysqlReader = null;
                mysqlCommand.CommandType = CommandType.Text;
                mysqlCommand.CommandText = "SELECT "
                                         +     "* "
                                         + "FROM "
                                         +     "contact "
                                         + "WHERE "
                                         +     "contact_id=@ContactId "
                                         + "AND "
                                         +     "is_delete='1'";

                mysqlCommand.Connection = mysqlConnection;

                mysqlCommand.Parameters.AddWithValue("@ContactId", id);

                mysqlConnection.Open();

                mysqlReader = mysqlCommand.ExecuteReader();

                while (mysqlReader.Read())
                {
                    contact.ContactId = Convert.ToInt32(mysqlReader.GetValue(mysqlReader.GetOrdinal("contact_id")));
                    contact.FirstName = mysqlReader.GetString(mysqlReader.GetOrdinal("firstname"));
                    contact.LastName = mysqlReader.GetString(mysqlReader.GetOrdinal("lastname"));
                    contact.AddressLine = mysqlReader.GetString(mysqlReader.GetOrdinal("addressline"));
                    contact.City = mysqlReader.GetString(mysqlReader.GetOrdinal("city"));
                    contact.Province = mysqlReader.GetString(mysqlReader.GetOrdinal("province"));
                    contact.Country = mysqlReader.GetString(mysqlReader.GetOrdinal("country"));
                    contact.Email = mysqlReader.GetString(mysqlReader.GetOrdinal("email"));
                    contact.ContactNumber = mysqlReader.GetString(mysqlReader.GetOrdinal("contact_number"));
                }
            }

            return contact;

        }

        [HttpGet]
        public IEnumerable<Contact> GetContactList()
        {
            List<Contact> contactList = new List<Contact>();
            using (MySqlConnection mysqlConnection = Connection.GetConnection())
            using (MySqlCommand mysqlCommand = new MySqlCommand())
            {
                MySqlDataReader mysqlReader = null;
                mysqlCommand.CommandType = CommandType.Text;
                mysqlCommand.CommandText = "SELECT " 
                                         +     "* "
                                         + "FROM "
                                         +     "contact "
                                         + "WHERE "
                                         +     "is_delete='1'";

                mysqlCommand.Connection = mysqlConnection;

                mysqlConnection.Open();

                mysqlReader = mysqlCommand.ExecuteReader();

                while (mysqlReader.Read())
                {
                    Contact contact = new Contact();
                    contact.ContactId = Convert.ToInt32(mysqlReader.GetValue(mysqlReader.GetOrdinal("contact_id")));
                    contact.FirstName = mysqlReader.GetString(mysqlReader.GetOrdinal("firstname"));
                    contact.LastName = mysqlReader.GetString(mysqlReader.GetOrdinal("lastname"));
                    contact.AddressLine = mysqlReader.GetString(mysqlReader.GetOrdinal("addressline"));
                    contact.City = mysqlReader.GetString(mysqlReader.GetOrdinal("city"));
                    contact.Province = mysqlReader.GetString(mysqlReader.GetOrdinal("province"));
                    contact.Country = mysqlReader.GetString(mysqlReader.GetOrdinal("country"));
                    contact.Email = mysqlReader.GetString(mysqlReader.GetOrdinal("email"));
                    contact.ContactNumber = mysqlReader.GetString(mysqlReader.GetOrdinal("contact_number"));

                    contactList.Add(contact);
                }
            }

            return contactList.ToArray();
        }

        public bool UpdateContactDb(Contact contact)
        {
            bool status = false;

            using (MySqlConnection mysqlConnection = Connection.GetConnection())
            using (MySqlCommand mysqlCommand = new MySqlCommand())
            {
                mysqlCommand.CommandType = CommandType.Text;
                mysqlCommand.CommandText = "UPDATE "
                                         +     "contact "
                                         + "SET "
                                         +     "firstname = @FirstName, "
                                         +     "lastname = @LastName, "
                                         +     "addressLine = @AddressLine, "
                                         +     "city = @City, "
                                         +     "province = @Province, "
                                         +     "country = @Country, "
                                         +     "email = @Email, "
                                         +     "contact_number = @ContactNumber "
                                         + "WHERE "
                                         +     "contact_id=@ContactId "
                                         + "AND "
                                         +     "is_delete='1'";

                mysqlCommand.Connection = mysqlConnection;

                mysqlCommand.Parameters.AddWithValue("@Firstname", contact.FirstName.ToUpper());
                mysqlCommand.Parameters.AddWithValue("@LastName", contact.LastName.ToUpper());
                mysqlCommand.Parameters.AddWithValue("@AddressLine", contact.AddressLine.ToUpper());
                mysqlCommand.Parameters.AddWithValue("@City", contact.City.ToUpper());
                mysqlCommand.Parameters.AddWithValue("@Province", contact.Province.ToUpper());
                mysqlCommand.Parameters.AddWithValue("@Country", contact.Country.ToUpper());
                mysqlCommand.Parameters.AddWithValue("@Email", contact.Email);
                mysqlCommand.Parameters.AddWithValue("@ContactNumber", contact.ContactNumber);
                mysqlCommand.Parameters.AddWithValue("@ContactId", contact.ContactId);

                mysqlConnection.Open();

                int rowAffected = mysqlCommand.ExecuteNonQuery();

                if (rowAffected > 0)
                {
                    status = true;
                }
            }

            return status;
        }
    }
}