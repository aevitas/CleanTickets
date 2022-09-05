using CleanTickets.Domain.Entities;

namespace CleanTickets.Application.Contracts;

public record TicketModel(Customer Customer, Event Event, DateTimeOffset PurchasedAt);
