using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment2_Durham_Ryan.Entities
{
    [Table("Employees")]
    public class Employee
    {
        public int EmployeeID { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public  string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        [MaxLength(50)]
        public string MaritalStatus { get; set; }
    }
}
