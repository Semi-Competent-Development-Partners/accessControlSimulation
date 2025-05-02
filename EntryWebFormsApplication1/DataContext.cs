using EntryWebFormsApplication1;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace EntryWebFormsApplication1 {
    public class DataContext : System.Data.Linq.DataContext {
        public Table<Employee> Employees;
        public Table<Entry> Entries;

        // Constructor that accepts a connection string
        public DataContext(string connectionString) : base(connectionString) {
            InitializeTables();
        }

        // Constructor that reads the connection string from Web.config
        public DataContext() : base(ConfigurationManager.ConnectionStrings["impPrepDbConnectionString"].ConnectionString) {
            InitializeTables();
        }

        // Initialize the tables
        private void InitializeTables() {
            Employees = GetTable<Employee>();
            Entries = GetTable<Entry>();
        }

        // Retrieve all employees
        public IEnumerable<Employee> GetAllEmployees() {
            return Employees.ToList();
        }

        // Get all entries for a specific employee
        public IEnumerable<Entry> GetEntriesByEmployeeId(int employeeId) {
            return Entries.Where(e => e.EmployeeId == employeeId).ToList();
        }


        public static void AddEntry(Entry entry) {
            // how can I call this from the server thread/db handler?

        }
    }
}
