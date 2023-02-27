using System.Net.Mail;

namespace TMS.Helper
{
    internal class MailUsername : MailAddress
    {
        public MailUsername(string address) : base(address)
        {
        }
    }
}