using Domain.Entities;
using Domain.Enums;
using Action = Domain.Enums.Action;

namespace DomainTests.Bookings
{
    public class StateMachineTests
    {
        [Fact]
        public void ShouldAlwaysStartWithCreatedStatus()
        {
            var booking = new Booking();
            Assert.Equal(Status.Created, booking.Status);
        }

        [Fact]
        public void ShouldSetStatusToPaidWhenPayingForABookingWithCreatedStatus()
        {
            var booking = new Booking();

            booking.ChangeState(Action.Pay);

            Assert.Equal(Status.Paid, booking.Status);
        }

        [Fact]
        public void ShouldSetStatusToCancelWhenCancelingForABookingWithCreatedStatus()
        {
            var booking = new Booking();

            booking.ChangeState(Action.Cancel);

            Assert.Equal(Status.Canceled, booking.Status);
        }

        [Fact]
        public void ShouldSetStatusToFinishedWhenFinishingPaidBooking()
        {
            var booking = new Booking();

            booking.ChangeState(Action.Pay);
            booking.ChangeState(Action.Finish);

            Assert.Equal(Status.Finished, booking.Status);
        }

        [Fact]
        public void ShouldSetStatusToRefoundedWhenRefoundingPaidBooking()
        {
            var booking = new Booking();

            booking.ChangeState(Action.Pay);
            booking.ChangeState(Action.Refound);

            Assert.Equal(Status.Refunded, booking.Status);
        }

        [Fact]
        public void ShouldSetStatusToCreatedWhenReopeningCanceledBooking()
        {
            var booking = new Booking();

            booking.ChangeState(Action.Cancel);
            booking.ChangeState(Action.Reopen);

            Assert.Equal(Status.Created, booking.Status);
        }

        [Fact]
        public void ShouldNotChangeStatusWhenRefoundingABookingWithCreatedStatus()
        {
            var booking = new Booking();

            booking.ChangeState(Action.Refound);

            Assert.Equal(Status.Created, booking.Status);
        }

        [Fact]
        public void ShouldNotChangeStatusWhenRefoundingAFinishedBooking()
        {
            var booking = new Booking();

            booking.ChangeState(Action.Pay);
            booking.ChangeState(Action.Finish);
            booking.ChangeState(Action.Refound);

            Assert.Equal(Status.Finished, booking.Status);
        }
    }
}