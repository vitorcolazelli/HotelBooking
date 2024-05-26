using Domain.Enums;

namespace Domain.ValueObjects
{
    public class PersonId
    {
        public string IdNumber { get; set; }
        public DocumentTypes DocumentType { get; set; }
    }
}
