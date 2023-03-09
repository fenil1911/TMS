using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data.SqlClient;
using TMS.Model.General;
using static TMS.Model.AccountModel;

namespace TMS.Data
{
    public class AccountProvider
    {
        public static List<SelectListItem> GetAllRoles(int roleId)
        {
            List<SelectListItem> roles = new List<SelectListItem>();
            using (SqlConnection conn = new SqlConnection(AppSetting.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("usp_RolesGetRolesByRoleId", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();
                    cmd.Parameters.AddWithValue("@RoleId", roleId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        SelectListItem item = new SelectListItem();
                        item.Value = reader["RoleName"].ToString();
                        item.Text = reader["RoleName"].ToString();

                        roles.Add(item);
                    }
                }
            }
            return roles;
        }
    }
}
