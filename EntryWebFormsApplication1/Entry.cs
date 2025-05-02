using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace EntryWebFormsApplication1 {
    [Table(Name = "Entries1")]
    public class Entry {
        private int _entryId;
        private int _employeeId;
        private string _eventType;
        private DateTime _timestamp;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, Storage = "_entryId", DbType = "Int NOT NULL IDENTITY")]
        public int EntryId
        {
            get { return this._entryId; }
            set { if (_entryId != value) { this._entryId = value; } }
        }

        [Column(Storage = "_employeeId", DbType = "Int NOT NULL")]
        public int EmployeeId
        {
            get { return this._employeeId; }
            set { if (_employeeId != value) { this._employeeId = value; } }
        }

        [Column(Storage = "_eventType", DbType = "VarChar(10)")]
        public string EventType
        {
            get { return this._eventType; }
            set { if (_eventType != value) { this._eventType = value; } }
        }

        [Column(Storage = "_timestamp", DbType = "DateTime NOT NULL")]
        public DateTime Timestamp
        {
            get { return this._timestamp; }
            set { if (_timestamp != value) { this._timestamp = value; } }
        }
    }
}