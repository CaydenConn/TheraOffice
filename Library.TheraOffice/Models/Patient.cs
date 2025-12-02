namespace Library.TheraOffice.Models
{
    public class Patient
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public DateTime Birthday { get; set; } = DateTime.Today;
        public string? Race { get; set; }
        public string? Gender { get; set; }
        public string? Diagnoses { get; set; }
        public string? Perscriptions { get; set; }
        public int Id { get; set; }

        public override string ToString()
        {
            return $"{Id}. {Name} ({Gender}, {Race}), Birthday: {DateOnly.FromDateTime(Birthday)}";
        }
        public string Display
        {
            get
            {
                return ToString();
            }
        }
    }
}
