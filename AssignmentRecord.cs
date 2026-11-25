/// <summary>
/// Represents confirmation that a ticket has been assigned to an IT agent.
/// </summary>
public class AssignmentRecord
{
    /// <summary>Gets the ticket ID.</summary>
    public string TicketId { get; set; }

    /// <summary>Gets the name of the assigned IT agent.</summary>
    public string ITAgentName { get; set; }

    /// <summary>Gets the assignment date and time.</summary>
    public DateTime Date { get; set; }

    /// <summary>Initialises an assignment record.</summary>
    public AssignmentRecord(string ticketId, string itAgentName, DateTime date)
    {
        TicketId = ticketId;
        ITAgentName = itAgentName;
        Date = date;
    }

    /// <summary>Returns the assignment confirmation in a readable format.</summary>
    public override string ToString()
    {
        return $"ASSIGNMENT RECORD – Ticket {TicketId} assigned to {ITAgentName} on {Date}";
    }
}
