/// <summary>
/// Handles and displays any supplied helpdesk message type.
/// </summary>
/// <typeparam name="T">The type of message to handle.</typeparam>
public class HelpdeskMsgHandler<T>
{
    /// <summary>Gets the name of this message handler.</summary>
    public string HandlerName { get; set; }

    /// <summary>Initialises the handler with a name.</summary>
    public HelpdeskMsgHandler(string handlerName)
    {
        HandlerName = handlerName;
    }

    /// <summary>Displays the handler name, message, and footer separator.</summary>
    /// <param name="message">The message to display.</param>
    public void Handle(T message)
    {
        Console.WriteLine($"=== {HandlerName} ===");
        Console.WriteLine(message is null ? string.Empty : message.ToString());
        Console.WriteLine("==============================");
        Console.WriteLine();
    }
}
