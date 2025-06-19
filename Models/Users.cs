using System.ComponentModel.DataAnnotations;
using System.IO.Pipelines;
using System.Transactions;

namespace Expence_Managment_Core_WebApplication.Models
{
    public class Users
    {
        [Key]
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
