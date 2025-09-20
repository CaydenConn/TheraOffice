namespace Library.TheraOffice.Models
{
    public class Patient
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public DateOnly Birthday { get; set; }
        public string? Race { get; set; }
        public string? Gender { get; set; }
        public List<string?>? Diagnoses { get; set; }
        public List<string?>? Perscriptions { get; set; }
        public int Id { get; set; }

        public override string ToString()
        {
            return $"{Id}. Name: {Name} ({Gender}, {Race})\n   Address: {Address}\n   Date of Birth: {Birthday}\n   Diagnoses: {Diagnoses}\n   Perscriptions: {Perscriptions}";
        }
    }
}
