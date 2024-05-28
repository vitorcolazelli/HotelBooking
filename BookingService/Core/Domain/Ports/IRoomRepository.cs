﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports
{
    public interface IRoomRepository
    {
        Task<Entities.Room> Get(int Id);
        Task<int> Create(Entities.Room room);
    }
}
