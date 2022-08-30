using CleanTickets.Domain;
using CleanTickets.Domain.Abstractions;
using CleanTickets.Domain.Entities;
using FluentValidation;

namespace CleanTickets.Application.Features.Tickets.Book;

internal class BookTicketsValidator : AbstractValidator<BookTicketsCommand>
{
    public BookTicketsValidator(ICustomerRepository customerRepository, IEventRepository eventRepository)
    {
        RuleFor(c => c.CustomerId).NotEmpty();
        RuleFor(c => c.EventId).NotEmpty();
        RuleFor(c => c.TicketCount).NotEmpty();

        RuleFor(c => c.CustomerId).MustAsync(async (id, _) =>
        {
            Maybe<Customer> existing = await customerRepository.FindAsync(id);

            return existing.HasValue;
        }).WithMessage("The specified customer could not be found");

        RuleFor(c => c.EventId).MustAsync(async (id, _) =>
        {
            Maybe<Event> existing = await eventRepository.FindAsync(id);

            return existing.HasValue;
        }).WithMessage("The specified event could not be found");
    }
}
