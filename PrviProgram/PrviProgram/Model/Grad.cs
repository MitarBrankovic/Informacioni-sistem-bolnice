namespace Model
{
    public class Grad
    {
        public string Ime { get; set; }
        public Drzava drzava { get; set; }
        public override string ToString()
        {
            return Ime;
        }
    }
}