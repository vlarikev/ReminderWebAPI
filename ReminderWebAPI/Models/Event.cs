namespace ReminderWebAPI.Models
{
    public class Event
    {
        public int eventId { get; set; }

        public string? eventDescription { get; set; }

        public DateTime eventDateTimer { get; set; }
    }
}
