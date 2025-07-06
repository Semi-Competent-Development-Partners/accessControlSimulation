using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.AspNet.SignalR;
using System.Net;
using Microsoft.AspNet.SignalR.Client;
using EntryWebFormsApplication1;

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

            int id = int.Parse(message);

            // Check if employee exists by ISIC ID
            Employee employee = GetEmployeeByIsic(id);
            if (employee == null) {
                Console.WriteLine($"Employee with ISIC ID {id} does not exist.");
                return -1;
            }

            string action = NextValidEmployeeAction(employee.Id);

            // Create Insert query which will insert the userId, the action and the current date into the database
            string query = $"INSERT INTO Entries1 (EmployeeId, EventType, Timestamp) VALUES (@id, @action, GETDATE())";

            // Create a command to execute the query
            using (DbCommand command = _dbConnection.CreateCommand()) {
                command.CommandText = query;
                command.Parameters.Add(new SqlParameter("@id", employee.Id));
                command.Parameters.Add(new SqlParameter("@action", action));
                // Execute the command
                try {
                    result = command.ExecuteNonQuery();
                }
                catch (Exception e) {
                    Console.WriteLine($"Error executing query: {e.Message}");
                    return -1;
                }
            }

            var timestamp = DateTime.Now;
            // send web request to https://localhost:44380/api/notify
            WebRequest request = WebRequest.Create($"https://localhost:44380/api/notify?message={id.ToString() + '+' + action + '+' + timestamp.ToString("dd-MMM-yy HH:mm:ss")}");
            Console.WriteLine($"web request to: {request.RequestUri}");
            // Get the response.
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Console.WriteLine($"response from web api: ({response.StatusCode})");
            return result;
        }

        private bool ValidEmployeeId(int id) {
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

        private string NextValidEmployeeAction(int employeeId) {
            // Create a command to check the last action of the employee
            string query = $"SELECT TOP 1 EventType FROM Entries1 WHERE EmployeeId = {employeeId} ORDER BY Timestamp DESC";
            using (DbCommand command = _dbConnection.CreateCommand()) {
                command.CommandText = query;
                try {
                    string lastAction = (string)command.ExecuteScalar();
                    lastAction = lastAction == null ? null : lastAction.Trim();

                    // If the last action is null and the current action is "in", return true
                    return (lastAction == "out" || lastAction == null) ? "in" : "out";
                }
                catch (Exception e) {
                    Console.WriteLine($"Error checking last action: {e.Message}");
                    return String.Empty;
                }
            }
        }

        private Employee GetEmployeeByIsic(int isicId) {
            // Create a command to get the employee by ISIC ID
            string query = $"SELECT * FROM Employees1 WHERE ISIC_ID = {isicId}";
            using (DbCommand command = _dbConnection.CreateCommand()) {
                command.CommandText = query;
                try {
                    using (DbDataReader reader = command.ExecuteReader()) {
                        if (reader.Read()) {
                            // Return new Employee object if found
                            return new Employee(
                                reader.GetInt32(reader.GetOrdinal("Id")),
                                reader.GetString(reader.GetOrdinal("FullName")),
                                reader.GetString(reader.GetOrdinal("Position")),
                                isicId
                            );
                        }
                    }
                }
                catch (Exception e) {
                    Console.WriteLine($"Error getting employee by ISIC ID: {e.Message}");
                }
            }
            return null; // Return null if no employee found
        }
    }
}
