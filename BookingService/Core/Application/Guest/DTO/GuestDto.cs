using Entities = Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Application.Guest.DTO
{
    public class GuestDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string IdNumber { get; set; }
        public int IdTypeCode { get; set; }

        public static Entities.Guest MapToEntity(GuestDto guestDto) 
        {
            return new Entities.Guest
            {
                Id = guestDto.Id,
                Name = guestDto.Name,
                Surname = guestDto.Surname,
                Email = guestDto.Email,
                DocumentId = new Domain.ValueObjects.PersonId
                {
                    IdNumber = guestDto.IdNumber,
                    DocumentType = (DocumentTypes)guestDto.IdTypeCode
                }
            };
        }

        public static GuestDto MapToDto(Entities.Guest guest)
        {
            return new GuestDto
            {
                Id = guest.Id,
                Name = guest.Name,
                Surname = guest.Surname,
                Email = guest.Email,
                IdNumber = guest.DocumentId.IdNumber,
                IdTypeCode = (int)guest.DocumentId.DocumentType
            };
        }
    }
}
