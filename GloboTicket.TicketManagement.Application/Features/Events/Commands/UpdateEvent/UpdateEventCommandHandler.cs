﻿using AutoMapper;
using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Application.Exceptions;
using GloboTicket.TicketManagement.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GloboTicket.TicketManagement.Application.Features.Events.Commands.UpdateEvent
{
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand>
    {
        private readonly IAsyncRepository<Event> _eventRepository;
        private readonly IMapper _mapper;

        public UpdateEventCommandHandler(IMapper mapper, IAsyncRepository<Event> eventRepository) 
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
        }

        public async Task Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var eventToUpdate = await _eventRepository.GetByIdAsync(request.EventId);
            if(eventToUpdate == null)
            {
                throw new NotFoundException(nameof(Event), request.EventId);
            }

            var validator = new UpdateEventCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if(validationResult.Errors.Count > 0)
            {
                throw new ValidatorException(validationResult);
            }
            _mapper.Map(request, eventToUpdate, typeof(UpdateEventCommand),typeof(Event));
            await _eventRepository.UpdateAsync(eventToUpdate);
            return;
        }
    }
}
