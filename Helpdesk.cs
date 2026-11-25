using System;
using System.Collections.Generic;

/// <summary>
/// Manages helpdesk tickets, assignments, waiting requests, and undo operations.
/// </summary>
public class Helpdesk
{
    private readonly List<Ticket> tickets = new();
    private readonly Dictionary<string, Ticket> byId = new();
    private readonly Queue<string> waitingQueue = new();
    private readonly Stack<Ticket> assignmentStack = new();

    private readonly HelpdeskMsgHandler<Notification> notificationHandler =
        new("Notification Message Handler");

    private readonly HelpdeskMsgHandler<AssignmentRecord> assignmentHandler =
        new("Assignment Message Handler");

    private readonly HelpdeskMsgHandler<ResolutionRecord> resolutionHandler =
        new("Resolution Message Handler");

    /// <summary>Adds a ticket if its ID is not already present.</summary>
    /// <param name="ticket">The ticket to add.</param>
    public void AddTicket(Ticket ticket)
    {
        if (byId.ContainsKey(ticket.Id))
        {
            notificationHandler.Handle(new Notification(
                "Duplicate Ticket",
                $"Ticket ID: {ticket.Id} already exists. Duplicate ID not allowed."));
            return;
        }

        tickets.Add(ticket);
        byId.Add(ticket.Id, ticket);

        notificationHandler.Handle(new Notification(
            "Ticket Added",
            $"Ticket {ticket.Id} – Added successfully."));
    }

    /// <summary>Displays all tickets in the helpdesk.</summary>
    public void ShowAllTickets()
    {
        Console.WriteLine("=== ALL CURRENT TICKETS ===");

        foreach (Ticket ticket in tickets)
        {
            Console.WriteLine(ticket.ToString());
        }

        Console.WriteLine("==============================");
        Console.WriteLine();
    }

    /// <summary>Assigns a ticket to an IT agent or adds its ID to the waiting queue.</summary>
    /// <param name="id">The ticket ID.</param>
    /// <param name="itAgentName">The IT agent's name.</param>
    public void AssignTicket(string id, string itAgentName)
    {
        if (!byId.TryGetValue(id, out Ticket? ticket))
        {
            notificationHandler.Handle(new Notification(
                "Assignment Failed", $"Ticket {id} not found."));
            return;
        }

        if (!ticket.IsOpen)
        {
            notificationHandler.Handle(new Notification(
                "Assignment Failed", $"Ticket {id} is already closed."));
            return;
        }

        if (ticket.IsAssigned)
        {
            waitingQueue.Enqueue(id);
            notificationHandler.Handle(new Notification(
                "Waitlist",
                $"Ticket {id} is currently assigned. Your request has been added to the waiting list."));
            return;
        }

        ticket.IsAssigned = true;
        ticket.ITAgentName = itAgentName;
        assignmentStack.Push(ticket);

        assignmentHandler.Handle(new AssignmentRecord(id, itAgentName, DateTime.Now));
    }

    /// <summary>Displays all ticket IDs currently in the waiting queue.</summary>
    public void ShowWaitingQueue()
    {
        Console.WriteLine("=== WAITING QUEUE ===");

        if (waitingQueue.Count == 0)
        {
            Console.WriteLine("No pending Ticket assignment requests.");
        }
        else
        {
            foreach (string id in waitingQueue)
            {
                Console.WriteLine(id);
            }
        }

        Console.WriteLine("==============================");
        Console.WriteLine();
    }

    /// <summary>Undoes the most recent successful ticket assignment.</summary>
    public void UndoLastAssignment()
    {
        if (assignmentStack.Count == 0)
        {
            notificationHandler.Handle(new Notification("Undo", "Nothing to undo."));
            return;
        }

        Ticket ticket = assignmentStack.Pop();
        ticket.IsAssigned = false;
        ticket.ITAgentName = "";

        notificationHandler.Handle(new Notification(
            "Undo Assignment",
            $"Assignment of \"{ticket.Title}\" has been undone. Ticket is now unassigned again."));
    }

    /// <summary>Closes a ticket and notifies the next ticket ID waiting for assignment.</summary>
    /// <param name="id">The ticket ID to resolve.</param>
    /// <param name="itAgentName">The name of the person resolving the ticket.</param>
    public void ResolveTicket(string id, string itAgentName)
    {
        if (!byId.TryGetValue(id, out Ticket? ticket))
        {
            notificationHandler.Handle(new Notification(
                "Resolution Failed", $"Ticket {id} not found."));
            return;
        }

        if (!ticket.IsOpen)
        {
            notificationHandler.Handle(new Notification(
                "Resolution Skipped", $"Ticket {id} is already closed."));
            return;
        }

        ticket.IsOpen = false;
        ticket.IsAssigned = false;
        ticket.ITAgentName = "";

        resolutionHandler.Handle(new ResolutionRecord(id, itAgentName, DateTime.Now));

        if (waitingQueue.Count > 0)
        {
            string nextId = waitingQueue.Dequeue();
            notificationHandler.Handle(new Notification(
                "Agent Available",
                $"Ticket {nextId} can now be assigned to the next available IT agent."));
        }
    }
}
