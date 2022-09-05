using CleanTickets.Application.Contracts;

namespace CleanTickets.Application.Features.Tickets.Book;

public record BookingResult(bool IsSuccessful, IReadOnlyList<TicketModel> Tickets);
