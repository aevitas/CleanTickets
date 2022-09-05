using CleanTickets.Application.Abstractions.Messaging;
using CleanTickets.Application.Contracts;
using CleanTickets.Application.Exceptions;
using CleanTickets.Domain;
using CleanTickets.Domain.Abstractions;
using CleanTickets.Domain.Entities;
using Mapster;

namespace CleanTickets.Application.Features.Tickets.Book;

public class BookTicketsCommandHandler : ICommandHandler<BookTicketsCommand, BookingResult>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IEventRepository _eventRepository;
    private readonly ITicketRepository _ticketRepository;


    public BookTicketsCommandHandler(ICustomerRepository customerRepository, IEventRepository eventRepository,
        ITicketRepository ticketRepository)
    {
        _customerRepository = customerRepository;
        _eventRepository = eventRepository;
        _ticketRepository = ticketRepository;
    }

    public async Task<BookingResult> Handle(BookTicketsCommand request, CancellationToken cancellationToken)
    {
        Maybe<Event> bookingEvent = await _eventRepository.FindAsync(request.EventId);
        IReadOnlyList<Ticket> existingTickets = await _ticketRepository.GetTicketsForEventAsync(request.EventId);

        if (existingTickets.Count >= bookingEvent.Value.TicketsAvailable)
        {
            throw new InvalidRequestException("The specified event is at capacity");
        }

        if (bookingEvent.Value.TicketsAvailable - existingTickets.Count < request.TicketCount)
        {
            throw new InvalidRequestException("Insufficient tickets available");
        }

        Maybe<Customer> customer = await _customerRepository.FindAsync(request.CustomerId);
        List<Ticket> tickets = new();

        for (int i = 0; i < request.TicketCount; i++)
        {
            tickets.Add(await _ticketRepository.AddAsync(new Ticket
            {
                Customer = customer.Value, Event = bookingEvent.Value, PurchasedAt = DateTimeOffset.UtcNow
            }));
        }

        return new BookingResult(tickets.Any(), tickets.Adapt<List<TicketModel>>());
    }
}
