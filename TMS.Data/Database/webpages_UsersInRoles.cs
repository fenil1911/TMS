using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMS.Data.Database
{
    [Table("webpages_UsersInRoles")]
    public class webpages_UsersInRoles
    {
        [Key]
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}