using Application;
using Application.Guest;
using Application.Guest.DTO;
using Application.Guest.Requests;
using Domain.Entities;
using Domain.Ports;
using Moq;

namespace ApplicationTests
{
    public class GuestManagerTests
    {
        GuestManager _guestManager;

        [Fact]
        public async Task CreateGuest_HappyPath()
        {
            int expectedId = 111; 

            var guest = new GuestDto
            {
                Name = "Fulano",
                Surname = "Ciclano",
                Email = "fulano@gmail.com",
                IdNumber = "123456789",
                IdTypeCode = 1,
            };

            var request = new CreateGuestRequest
            {
                Data = guest
            };

            var fakeRepo = new Mock<IGuestRepository>();

            fakeRepo.Setup(x => x.Create(It.IsAny<Guest>())).Returns(Task.FromResult(expectedId));

            _guestManager = new GuestManager(fakeRepo.Object);

            var res = await _guestManager.CreateGuest(request);

            Assert.NotNull(res);
            Assert.True(res.Success);
            Assert.Equal(expectedId, res.Data.Id);
            Assert.Equal(guest.Name, res.Data.Name);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("1")]
        [InlineData("12")]
        [InlineData("123")]
        public async Task CreateGuest_InvalidIdPersonDocumentException(string docNumber)
        {
            var guest = new GuestDto
            {
                Name = "Fulano",
                Surname = "Ciclano",
                Email = "fulano@gmail.com",
                IdNumber = docNumber,
                IdTypeCode = 1,
            };

            var request = new CreateGuestRequest
            {
                Data = guest
            };

            var fakeRepo = new Mock<IGuestRepository>();

            fakeRepo.Setup(x => x.Create(It.IsAny<Guest>())).Returns(Task.FromResult(222));

            _guestManager = new GuestManager(fakeRepo.Object);

            var res = await _guestManager.CreateGuest(request);

            Assert.NotNull(res);
            Assert.False(res.Success);
            Assert.Equal(ErrorCodes.INVALID_PERSON_ID, res.ErrorCode);
            Assert.Equal("The ID passed is not valid", res.Message);
        }

        [Theory]
        [InlineData("Beltrano", "ciclano", "")]
        [InlineData("Beltrano", "ciclano", null)]
        [InlineData("Beltrano", "", "ciclano@gmail.com")]
        [InlineData("Beltrano", null, "ciclano@gmail.com")]
        [InlineData("", "ciclano", "ciclano@gmail.com")]
        [InlineData(null, "ciclano", "ciclano@gmail.com")]
        public async Task CreateGuest_MissingRequiredInformationException(string name, string surname, string email)
        {
            var guest = new GuestDto
            {
                Name = name,
                Surname = surname,
                Email = email,
                IdNumber = "123456789",
                IdTypeCode = 1,
            };

            var request = new CreateGuestRequest
            {
                Data = guest
            };

            var fakeRepo = new Mock<IGuestRepository>();

            fakeRepo.Setup(x => x.Create(It.IsAny<Guest>())).Returns(Task.FromResult(222));

            _guestManager = new GuestManager(fakeRepo.Object);

            var res = await _guestManager.CreateGuest(request);

            Assert.NotNull(res);
            Assert.False(res.Success);
            Assert.Equal(ErrorCodes.MISSING_REQUIRED_INFORMATION, res.ErrorCode);
            Assert.Equal("Missing required information passed", res.Message);
        }

        [Theory]
        [InlineData("teste")]
        public async Task CreateGuest_InvalidEmailException(string email)
        {
            var guest = new GuestDto
            {
                Name = "Fulano",
                Surname = "Ciclano",
                Email = email,
                IdNumber = "123456789",
                IdTypeCode = 1,
            };

            var request = new CreateGuestRequest
            {
                Data = guest
            };

            var fakeRepo = new Mock<IGuestRepository>();

            fakeRepo.Setup(x => x.Create(It.IsAny<Guest>())).Returns(Task.FromResult(222));

            _guestManager = new GuestManager(fakeRepo.Object);

            var res = await _guestManager.CreateGuest(request);

            Assert.NotNull(res);
            Assert.False(res.Success);
            Assert.Equal(ErrorCodes.INVALID_EMAIL, res.ErrorCode);
            Assert.Equal("The given email is not valid", res.Message);
        }

        [Fact]
        public async Task Should_Return_GuestNotFound_When_GuestDoesntExist()
        {
            var fakeRepo = new Mock<IGuestRepository>();

            var fakeGuest = new Guest
            {
                Id = 333,
                Name = "Test"
            };

            fakeRepo.Setup(x => x.Get(333)).Returns(Task.FromResult<Guest?>(null));

            _guestManager = new GuestManager(fakeRepo.Object);

            var res = await _guestManager.GetGuest(333);

            Assert.NotNull(res);
            Assert.False(res.Success);
            Assert.Equal(ErrorCodes.GUEST_NOT_FOUND, res.ErrorCode);
            Assert.Equal("No Guest record was found with the given Id", res.Message);
        }

        [Fact]
        public async Task Should_Return_Guest_Success()
        {
            var fakeRepo = new Mock<IGuestRepository>();

            var fakeGuest = new Guest
            {
                Id = 333,
                Name = "Test",
                DocumentId = new Domain.ValueObjects.PersonId
                {
                    DocumentType = Domain.Enums.DocumentTypes.DriverLicense,
                    IdNumber = "1234"
                }
            };

            fakeRepo.Setup(x => x.Get(333)).Returns(Task.FromResult((Guest?)fakeGuest));

            _guestManager = new GuestManager(fakeRepo.Object);

            var res = await _guestManager.GetGuest(333);

            Assert.NotNull(res);
            Assert.True(res.Success);
            Assert.Equal(fakeGuest.Id, res.Data.Id);
            Assert.Equal(fakeGuest.Name, res.Data.Name);
        }
    }
}