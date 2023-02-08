using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMS.Data.Database
{
    [Table("webpages_OAuthMembership")]
    public class webpages_OAuthMembership
    {
        [Key]
        public string Provider { get; set; }
        public string ProviderUserId { get; set; }
   
        public int UserId { get; set; }
    }
}