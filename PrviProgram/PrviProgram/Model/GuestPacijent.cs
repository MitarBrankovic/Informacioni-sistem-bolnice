namespace Model
{
    public class GuestPacijent
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Jmbg { get; set; }
        public string KontaktTelefon { get; set; }
        public Pol Pol { get; set; }

        public override string ToString()
        {
            return "Guest " + Ime + " " + Prezime + " ";
        }

    }
}