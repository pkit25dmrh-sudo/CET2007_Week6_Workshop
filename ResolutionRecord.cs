/// <summary>
/// Represents confirmation that a ticket has been resolved.
/// </summary>
public class ResolutionRecord
{
    /// <summary>Gets the ticket ID.</summary>
    public string TicketId { get; set; }

    /// <summary>Gets the name of the IT agent who resolved the ticket.</summary>
    public string ITAgentName { get; set; }

    /// <summary>Gets the resolution date and time.</summary>
    public DateTime Date { get; set; }

    /// <summary>Initialises a resolution record.</summary>
    public ResolutionRecord(string ticketId, string itAgentName, DateTime date)
    {
        TicketId = ticketId;
        ITAgentName = itAgentName;
        Date = date;
    }

    /// <summary>Returns the resolution confirmation in a readable format.</summary>
    public override string ToString()
    {
        return $"RESOLUTION RECORD – Ticket {TicketId} resolved by {ITAgentName} on {Date}";
    }
}
