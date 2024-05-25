using Domain.Enums;

namespace Domain.ValueObjects
{
    public class PersonId
    {
        public int IdNumber { get; set; }
        public DocumentTypes DocumentType { get; set; }
    }
}
