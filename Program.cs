using System;

/// <summary>
/// Contains the application entry point and console menu.
/// </summary>
public class Program
{
    /// <summary>Runs the message-handler demonstration and helpdesk menu.</summary>
    public static void Main()
    {
        // Exercise 1: test the generic message handlers.
        Notification notification = new(
            "System Update",
            "Scheduled maintenance will occur at 8 PM.");

        AssignmentRecord assignment = new(
            "IT-1001", "Alice Johnson", DateTime.Now);

        ResolutionRecord resolution = new(
            "IT-1001", "Alice Johnson", DateTime.Now);

        HelpdeskMsgHandler<Notification> notificationDemoHandler =
            new("Notification Handler");

        HelpdeskMsgHandler<AssignmentRecord> assignmentDemoHandler =
            new("Assignment Handler");

        HelpdeskMsgHandler<ResolutionRecord> resolutionDemoHandler =
            new("Resolution Handler");

        notificationDemoHandler.Handle(notification);
        assignmentDemoHandler.Handle(assignment);
        resolutionDemoHandler.Handle(resolution);

        // Exercises 2–4: create the helpdesk and its initial tickets.
        Helpdesk helpdesk = new();

        helpdesk.AddTicket(new Ticket("IT-1001", "Wi-Fi not connecting in Lab A"));
        helpdesk.AddTicket(new Ticket("IT-1002", "Password reset required"));
        helpdesk.AddTicket(new Ticket("IT-1003", "Printer jam in Room 204"));

        bool running = true;

        while (running)
        {
            Console.WriteLine("=== IT HELP DESK MENU ===");
            Console.WriteLine("1) Add ticket");
            Console.WriteLine("2) Assign ticket");
            Console.WriteLine("3) Resolve ticket");
            Console.WriteLine("4) Undo last assignment");
            Console.WriteLine("5) Show all tickets");
            Console.WriteLine("6) Show waiting queue");
            Console.WriteLine("0) Exit");
            Console.Write("Choose: ");

            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    Console.Write("Enter Ticket ID: ");
                    string newId = Console.ReadLine() ?? "";

                    Console.Write("Enter Title: ");
                    string title = Console.ReadLine() ?? "";

                    helpdesk.AddTicket(new Ticket(newId, title));
                    break;

                case "2":
                    Console.Write("Enter Ticket ID: ");
                    string assignmentId = Console.ReadLine() ?? "";

                    Console.Write("Enter IT agent name: ");
                    string agentName = Console.ReadLine() ?? "";

                    helpdesk.AssignTicket(assignmentId, agentName);
                    break;

                case "3":
                    Console.Write("Enter Ticket ID: ");
                    string resolutionId = Console.ReadLine() ?? "";

                    Console.Write("Enter resolver name: ");
                    string resolverName = Console.ReadLine() ?? "";

                    helpdesk.ResolveTicket(resolutionId, resolverName);
                    break;

                case "4":
                    helpdesk.UndoLastAssignment();
                    break;

                case "5":
                    helpdesk.ShowAllTickets();
                    break;

                case "6":
                    helpdesk.ShowWaitingQueue();
                    break;

                case "0":
                    running = false;
                    Console.WriteLine("Program ended.");
                    break;

                default:
                    Console.WriteLine("Invalid Option");
                    break;
            }
        }
    }
}
