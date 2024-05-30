﻿using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Room.DTO
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public bool InMaintenance { get; set; }
        public decimal Price { get; set; }
        public AcceptedCurrencies Currency { get; set; }

        public static Domain.Entities.Room MapToEntity(RoomDto dto)
        {
            return new Domain.Entities.Room
            {
                Id = dto.Id,
                Name = dto.Name,
                InMaintenance = dto.InMaintenance,
                Level = dto.Level,
                Price = new Domain.ValueObjects.Price
                {
                    Currency = dto.Currency,
                    Value = dto.Price
                }
            };
        }

        public static RoomDto MapToDto(Domain.Entities.Room room)
        {
            return new RoomDto
            {
                Id = room.Id,
                Name = room.Name,
                Level = room.Level,
                InMaintenance = room.InMaintenance,
            };
        }
    }
}
