using CleanTickets.Application.Abstractions.Messaging;

namespace CleanTickets.Application.Features.Tickets.Book;

public record BookTicketsCommand(long CustomerId, long EventId, int TicketCount) : ICommand<BookingResult>;