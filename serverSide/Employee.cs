using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace serverSide {
    internal class Employee {
        private int _id;
        private string _fullName;
        private string _position;
        private int _isic_id;

        public Employee(int id, string fullName, string position, int isic_id) {
            Id = id;
            FullName = fullName;
            Position = position;
            Isic_id = isic_id;
        }

        public int Id { get => _id; set => _id = value; }
        public string FullName { get => _fullName; set => _fullName = value; }
        public string Position { get => _position; set => _position = value; }
        public int Isic_id { get => _isic_id; set => _isic_id = value; }
    }
}
