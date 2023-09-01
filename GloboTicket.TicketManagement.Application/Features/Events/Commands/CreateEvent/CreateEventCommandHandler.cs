using AutoMapper;
using GloboTicket.TicketManagement.Application.Contracts.Infrastructure;
using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Application.Models.Mail;
using GloboTicket.TicketManagement.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GloboTicket.TicketManagement.Application.Features.Events.Commands.CreateEvent
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Guid>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        public CreateEventCommandHandler(IEventRepository eventRepository, IMapper mapper, IEmailService emailService)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var @event = _mapper.Map<Event>(request);

            //validator
            var validator = new CreateEventCommandValidator(_eventRepository);
            var validationResult = await validator.ValidateAsync(request);
            if(validationResult.Errors.Any()) 
            {
                throw new Exceptions.ValidatorException(validationResult);
            }
            //endvalidator

            var email = new Email() { To = "josue.molloja", Subject = "a new event was created", Body = $"A new event was created: {request}" };
            try
            {
                await _emailService.SendEmail(email);
            }
            catch (Exception) 
            {
                //esto no deberia detener a la api de hacer algo mas asi que puede ser logeado
            }
            
            @event = await _eventRepository.AddAsync(@event);
            return @event.EventId;
        }
    }
}
