using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Shared.DTOs
{
    public class CreateEmployeeDto
    {
        public string FullName { get; set; } = default!;
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }
        public int DepartmentId { get; set; }
        public int PositionId { get; set; }
    }
}
