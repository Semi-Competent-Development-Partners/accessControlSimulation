using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace EntryWebFormsApplication1 {
    [Table(Name = "Employees1")]
    public class Employee {
        private int _id;
        private string _fullName;
        private string _position;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, Storage = "_id", DbType = "Int NOT NULL IDENTITY")]
        public int Id
        {
            get { return this._id; }
            set { if (_id != value) { this._id = value; } }
        }

        [Column(Storage = "_fullName", DbType = "NVarChar(100)")]
        public string FullName
        {
            get { return this._fullName; }
            set { if (_fullName != value) { this._fullName = value; } }
        }

        [Column(Storage = "_position", DbType = "NVarChar(50)")]
        public string Position
        {
            get { return this._position; }
            set { if (_position != value) { this._position = value; } }
        }
    }
}