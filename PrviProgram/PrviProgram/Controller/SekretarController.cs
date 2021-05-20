using Model;
using Service;

namespace Controller
{
    public class SekretarController
    {
        private TerminiService terminiService = new TerminiService();
        private GradoviService gradoviService = new GradoviService();
        private DrzaveService drzaveService = new DrzaveService();
        private PacijentiService pacijentiService = new PacijentiService();
        private AlergeniService alergeniService = new AlergeniService();
        private VestiService vestiService = new VestiService();
        public LekariService lekariService = new LekariService();

        public bool DodavanjePacijenta(Pacijent pacijent)
        {
            return pacijentiService.DodavanjePacijenta(pacijent);
        }

        public bool BrisanjePacijenta(Pacijent pacijent)
        {
            return pacijentiService.BrisanjePacijenta(pacijent);
        }

        public bool IzmenaPacijenta(Pacijent stariPacijent, Pacijent noviPacijent)
        {
            return pacijentiService.IzmenaPacijenta(stariPacijent, noviPacijent);
        }

        public bool DodavanjeAlergena(Alergen alergen)
        {
            return alergeniService.DodavanjeAlergena(alergen);
        }

        public bool BrisanjeAlergena(Alergen alergen)
        {
            return alergeniService.BrisanjeAlergena(alergen);
        }

        public bool IzmenaAlergena(Alergen stariAlergen, Alergen noviAlergen)
        {
            return alergeniService.IzmenaAlergena(stariAlergen, noviAlergen);
        }

        public bool DodavanjeVesti(Vest vest)
        {
            return vestiService.DodavanjeVesti(vest);
        }

        public bool BrisanjeVesti(Vest vest)
        {
            return vestiService.BrisanjeVesti(vest);
        }

        public bool IzmenaVesti(Vest vest)
        {
            return vestiService.IzmenaVesti(vest);
        }

        public bool DodavanjeGrada(Grad grad)
        {
            return gradoviService.DodavanjeGrada(grad);
        }

        public bool DodavanjeDrzave(Drzava drzava)
        {
            return drzaveService.DodavanjeDrzave(drzava);
        }

        public bool DodavanjeLekara(Lekar lekar)
        {
            return lekariService.DodavanjeLekara(lekar);
        }

        public bool BrisanjeLekara(Lekar lekar)
        {
            return lekariService.BrisanjeLekara(lekar);
        }

        public bool IzmenaLekara(Lekar stariLekar, Lekar noviLekar)
        {
            return lekariService.IzmenaLekara(stariLekar, noviLekar);
        }

    }
}