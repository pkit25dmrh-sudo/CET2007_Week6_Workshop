/// <summary>
/// Represents a general helpdesk notification message.
/// </summary>
public class Notification
{
    /// <summary>Gets the notification title.</summary>
    public string Title { get; set; }

    /// <summary>Gets the notification message.</summary>
    public string Message { get; set; }

    /// <summary>Initialises a notification with a title and message.</summary>
    public Notification(string title, string message)
    {
        Title = title;
        Message = message;
    }

    /// <summary>Returns the notification in a readable format.</summary>
    public override string ToString()
    {
        return $"NOTIFICATION – {Title}: {Message}";
    }
}
