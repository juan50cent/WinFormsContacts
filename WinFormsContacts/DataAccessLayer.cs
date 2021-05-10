using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsContacts
{
    public class DataAccessLayer//Capa de acceso a datos
    {
        // Se utiliza la libreria >>> System.Data.SqlClient;
        private SqlConnection conn = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=WinFormsContacts;Data Source=JUANESTEBANPC"); 
     
        public void InsertContact(Contact contact)
        {
            try
            {
                conn.Open();
                // El @ me permite escribir la consulta en mas de una lìnea
                string query = @"
                                INSERT INTO Contacts ([FirstName], [LastName], [Phone], [Address])    
                                VALUES (@FirstName, @LastName, @Phone, @Address) ";

                //Forma larga para insertar los valores en la base de datos
                SqlParameter firstName = new SqlParameter();
                firstName.ParameterName = "@FirstName";
                firstName.Value = contact.FirstName;
                firstName.DbType = System.Data.DbType.String;

                //Forma corta para insertar valores en la base de datos
                SqlParameter lastName = new SqlParameter("@LastName", contact.LastName);
                SqlParameter phone = new SqlParameter("@Phone", contact.Phone);
                SqlParameter address = new SqlParameter("@Address", contact.Address);

                //Se pasa como parametros la consulta SQL y la conexion
                SqlCommand command = new SqlCommand(query, conn);

                //Conexion de parametros
                command.Parameters.Add(firstName);
                command.Parameters.Add(lastName);
                command.Parameters.Add(phone);
                command.Parameters.Add(address);

                //Ejecutamos la consulta
                command.ExecuteNonQuery();


            }
            catch (Exception)
            {

                throw;
            }
            finally //El finally se va a ejecutar siempre asi hayan o no errores
            {
                conn.Close();
            }
        }

        public void UpdateContact(Contact contact)
        {
            try
            {
                conn.Open();
                string query = @" UPDATE Contacts
                                  SET FirstName = @FirstName,
                                      LastName = @LastName,
                                      Phone = @Phone,
                                      Address = @Address 
                                  WHERE Id = @Id ";

                //Forma corta para insertar valores en la base de datos
                SqlParameter id = new SqlParameter("@Id", contact.Id);
                SqlParameter firsName = new SqlParameter("@FirstName", contact.FirstName);
                SqlParameter lastName = new SqlParameter("@LastName", contact.LastName);
                SqlParameter phone = new SqlParameter("@Phone", contact.Phone);
                SqlParameter address = new SqlParameter("@Address", contact.Address);

                SqlCommand command = new SqlCommand(query, conn);

                command.Parameters.Add(id);
                command.Parameters.Add(firsName);
                command.Parameters.Add(lastName);
                command.Parameters.Add(phone);
                command.Parameters.Add(address);

                command.ExecuteNonQuery();

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public void DeleteContact(int id)
        {
            try
            {
                conn.Open();
                string query = @"DELETE FROM Contacts WHERE Id = @Id";

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.Add(new SqlParameter("@Id", id));

                command.ExecuteNonQuery();

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        //Cuando yo agrego un valor nulo a un parametro , esto significa que es opcional.
        public List<Contact> GetContacts(string search = null)
        {
            //Creamos una lista de contactos vacia
            List<Contact> contacts = new List<Contact>();
            try
            {
                conn.Open();
                string query = @" SELECT Id, FirstName, Lastname, Phone, Address
                                FROM Contacts ";

                //Se pasa como parametros la consulta SQL y la conexion
                SqlCommand command = new SqlCommand();

                //Cuando search no sea nulo ni este vacio
                if (!string.IsNullOrEmpty(search))
                {
                    query += @"WHERE FirstName LIKE @search OR LastName LIKE @search OR Phone LIKE @search OR
                               Address LIKE @search ";
                    command.Parameters.Add(new SqlParameter("@search", $"%{search}%"));
                }
                //Si no entra al If , viene aca y recibe sus parametros
                command.CommandText = query;
                command.Connection = conn;

                //Me devuelve todas las filas de la tabla Contacts
                SqlDataReader reader = command.ExecuteReader();

                // Devuelve:  true si hay más filas; de lo contrario, false.
                while (reader.Read())
                {
                    //Se almacena en una lista los datos extraidos de la base de datos
                    contacts.Add(new Contact
                    { 
                        Id = int.Parse(reader["Id"].ToString()),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Address = reader["Address"].ToString(),
                    }); 
                }


            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                //Sin importar si hay o no hay error , pasa por finally y se cierra la conexion.
                conn.Close();
            }
            //Se va a retornar tambien pase lo que pase la Lista de contactos
            return contacts;

        }


    }
}
