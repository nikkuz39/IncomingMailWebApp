using System.Collections.Generic;

namespace IncomingMailWebApp.Models
{
    public class ViewModelLetter
    {
        public Addressee Addressee { get; set; }
        public Mail Mail { get; set; }
        public Sender Sender { get; set; }
        public Tag Tag { get; set; }

        public IEnumerable<Mail> Mails { get; set; }
        public IEnumerable<Tag> Tags { get; set; }     
        public int[] TagsId { get; set; }
        public IEnumerable<Addressee> Addressees { get; set; }
        public IEnumerable<Sender> Senders { get; set; }
    }
}
