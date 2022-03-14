using System.Collections.Generic;

namespace IncomingMailWebApp.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string TagName { get; set; }

        public List<Mail> Mails { get; set; }
    }
}
