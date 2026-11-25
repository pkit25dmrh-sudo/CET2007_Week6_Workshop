/// <summary>
/// Represents an IT helpdesk support ticket.
/// </summary>
public class Ticket
{
    /// <summary>Gets the ticket ID.</summary>
    public string Id { get; set; }

    /// <summary>Gets the ticket title.</summary>
    public string Title { get; set; }

    /// <summary>Gets or sets whether the ticket is open.</summary>
    public bool IsOpen { get; set; } = true;

    /// <summary>Gets or sets whether the ticket is assigned.</summary>
    public bool IsAssigned { get; set; } = false;

    /// <summary>Gets or sets the assigned IT agent's name.</summary>
    public string ITAgentName { get; set; } = "";

    /// <summary>Initialises a ticket with its ID and title.</summary>
    public Ticket(string id, string title)
    {
        Id = id;
        Title = title;
    }

    /// <summary>Returns the ticket details in a readable format.</summary>
    public override string ToString()
    {
        string status = IsOpen ? "Open" : "Closed";
        string assignedTo = string.IsNullOrWhiteSpace(ITAgentName) ? "Unassigned" : ITAgentName;
        return $"Ticket {Id} – {Title} | Status: {status} | Assigned To: {assignedTo}";
    }
}
