using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace serverSide {
    internal class DbHandler {
        private const string dbConnectionString = "Server=(localdb)\\MSSQLLocalDB;Database=impPrepDb;";

        private static DbConnection _dbConnection = new SqlConnection(dbConnectionString);

        public DbHandler() {
            //if (_dbConnection != null) {
            //    return;
            //}

            //OpenConnection();
        }

        ~DbHandler()  // finalizer (aka destructor)
        {
            //CloseConnection();
        }

        public static void OpenConnection() {
            try {
                _dbConnection.Open();
            }
            catch (Exception e) {
                Console.WriteLine($"Error opening database connection: {e.Message}");
            }
        }

        public static void CloseConnection() {
            try {
                if (_dbConnection != null) {
                    CloseConnection();
                    _dbConnection.Dispose();
                    _dbConnection = null;
                }
            }
            catch (Exception e) {
                Console.WriteLine($"Error closing database connection: {e.Message}");
            }
        }

        // TODO methods:
        // execute insert query if input is valid (user can't check in if he's already checked in)
        // get all dates entry counts, and time spent on those dates for a user by id
        // (and maybe show all that in a winform in a separate class?)

        public int AddEntry(string message) {
            int result;

            // Split the message into its components
            string[] parts = message.Split(' ');
            string action = parts[0];
            int id = int.Parse(parts[1]);
            
            // Create Insert query which will insert the userId, the action and the current date into the database
            string query = $"INSERT INTO Entries1 (EmployeeId, EventType, Timestamp) VALUES ({id}, '{action}', GETDATE())";

            // Check if the user id exists in the database
            bool userValid = CheckUserExistsById(id);
            if (!userValid) {
                Console.WriteLine($"User with ID {id} does not exist.");
                return -1;
            }

            // Create a command to execute the query
            using (DbCommand command = _dbConnection.CreateCommand()) {
                command.CommandText = query;
                // Execute the command
                try {
                    result = command.ExecuteNonQuery();
                }
                catch (Exception e) {
                    Console.WriteLine($"Error executing query: {e.Message}");
                    return -1;
                }
            }

            return result;
        }

        private bool CheckUserExistsById(int id) {
            // Create a command to check if the user exists
            string query = $"SELECT COUNT(*) FROM Employees1 WHERE Id = {id}";
            using (DbCommand command = _dbConnection.CreateCommand()) {
                command.CommandText = query;
                try {
                    int count = (int)command.ExecuteScalar();
                    return count == 1;
                }
                catch (Exception e) {
                    Console.WriteLine($"Error checking user existence: {e.Message}");
                    return false;
                }
            }
        }
    }
}
