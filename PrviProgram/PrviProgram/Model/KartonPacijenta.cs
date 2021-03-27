namespace Model
{
    public class KartonPacijenta
    {
        public string Sifra { get; set; }
        public string Terapija { get; set; }
        public string Alergeni { get; set; }
        public string IstorijaBolesti { get; set; }

        public Pacijent pacijent { get; set; }

    }
}