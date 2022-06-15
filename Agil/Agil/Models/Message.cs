using System.ComponentModel.DataAnnotations.Schema;

namespace Agil.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        [InverseProperty("ReceivedMessages")]
        public virtual User ToUser { get; set; }

        [InverseProperty("SendedMessages")]
        public virtual User FromUser { get; set; }
        public virtual Item ItemInQuestion { get; set; }
        public string Body { get; set; }
    }
}
