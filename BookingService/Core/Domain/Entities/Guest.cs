﻿using Domain.Enums;
using Domain.Exceptions;
using Domain.Ports;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Guest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public PersonId DocumentId { get; set; }

        private void ValidateState()
        {
            if (DocumentId == null || DocumentId.IdNumber.Length <= 3 || !Enum.IsDefined(typeof(DocumentTypes), DocumentId.DocumentType))
                throw new InvalidPersonDocumentIdException();

            if (Name == null || Surname == null || Email == null)
                throw new MissingRequiredInformation();

            if (Utils.ValidateEmail(this.Email) == false)
                throw new InvalidEmailException();
        }

        public async Task Save(IGuestRepository guestRepository)
        {
            this.ValidateState();

            if (this.Id == 0)
            {
                this.Id = await guestRepository.Create(this);
            }
            else
            {
                //await guestRepository.Update(this);
            }
        }
    }
}
