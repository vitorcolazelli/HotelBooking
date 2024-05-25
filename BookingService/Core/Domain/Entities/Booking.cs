using Action = Domain.Enums.Action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entities
{
    public class Booking
    {
        public Booking()
        {
            this.Status = Status.Created;
        }

        public int Id { get; set; }
        public DateTime PlacedAt { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Room Room { get; set; }
        public Guest Guest { get; set; }
        private Status Status { get; set; }
        public Status CurrentStatus { get { return Status; } }

        public void ChangeState(Action action)
        {
            this.Status = (this.Status, action) switch
            {
                (Status.Created,  Action.Pay)     => Status.Paid,
                (Status.Created,  Action.Cancel)  => Status.Canceled,
                (Status.Paid,     Action.Finish)  => Status.Finished,
                (Status.Paid,     Action.Refound) => Status.Refunded,
                (Status.Canceled, Action.Reopen)  => Status.Created,
                _ => this.Status
            };
        }
    }
}
