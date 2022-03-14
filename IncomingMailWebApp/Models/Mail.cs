using System.Collections.Generic;

namespace IncomingMailWebApp.Models
{
    public class Mail
    {
        public int Id { get; set; }
        public string LetterTitle { get; set; }
        public string DateOfRegistration { get; set; }    
        public string ContentLetter { get; set; }

        public List<Tag> Tags { get; set; }

        public int SenderId { get; set; }
        public Sender Sender { get; set; }

        public int AddresseeId { get; set; }
        public Addressee Addressee { get; set; }
    }
}
