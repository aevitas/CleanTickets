using CleanTickets.Domain.Entities;

namespace CleanTickets.Application.Features.Tickets.Book;

public record BookingResult(bool IsSuccessful, IReadOnlyList<Ticket> Tickets);