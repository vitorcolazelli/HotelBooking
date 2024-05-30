﻿using Application.Room.DTO;
using Application.Room.Responses;
using Domain.Ports;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Room.Queries
{
    public class GetRoomQueryHandler : IRequestHandler<GetRoomQuery, RoomResponse>
    {
        private readonly IRoomRepository _roomRepository;

        public GetRoomQueryHandler(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<RoomResponse> Handle(GetRoomQuery request, CancellationToken cancellationToken)
        {
            var room = await _roomRepository.Get(request.Id);

            if (room == null)
            {
                return new RoomResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.ROOM_NOT_FOUND,
                    Message = "Could not find a Room with the given Id"
                };
            }

            return new RoomResponse
            {
                Data = RoomDto.MapToDto(room),
                Success = true
            };
        }
    }
}
