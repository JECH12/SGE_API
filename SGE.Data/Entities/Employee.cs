using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Data.Entities
{
    public class Employee
    {
        public int Id { get; set; }

        public string FullName { get; set; } = string.Empty;

        public DateTime HireDate { get; set; }

        public decimal Salary { get; set; }   

        public int DepartmentId { get; set; }
        public Department? Department { get; set; }

        public int PositionId { get; set; }
        public Position? Position { get; set; }
    }
}
