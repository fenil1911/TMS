using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity.ModelConfiguration.Conventions;
using System.Runtime.Remoting.Contexts;

namespace TMS.Data.Database
{
   public  class TMSEntities : DbContext
    {

        public TMSEntities() :  base("TMSConnection")
        {

        }


        public DbSet<Users> Users { get; set; }
        public DbSet<FormMst> FormMst { get; set; }
        public DbSet<FormRoleMapping> FormRoleMapping { get; set; }
        public DbSet<EmailHistory> EmailHistory { get; set; }
        public DbSet<Tickets> Tickets { get; set; }
        public DbSet<TicketStatus> TicketStatus { get; set; }
        
        public DbSet<TicketAttachment> TicketAttachment { get; set; }
        public DbSet<TicketComment> TicketComment { get; set; }
        public DbSet<CommonLookup> CommonLookup { get; set; }
        public DbSet<webpages_Membership> webpages_Membership { get; set; }
        public DbSet<webpages_OAuthMembership> webpages_OAuthMembership { get; set; }
        public DbSet<webpages_Roles> webpages_Roles { get; set; }
        public DbSet<webpages_UsersInRoles> webpages_UsersInRoles { get; set; }
        public DbSet<ErrorLog_Mst> ErrorLog_Msts { get; set; }
        public DbSet<ActivityLog> activityLogs { get; set; }



    }
}

