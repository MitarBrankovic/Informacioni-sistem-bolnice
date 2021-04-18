using System;
using System.Collections.Generic;
using Model;

namespace Repository
{
    public class GuestPacijentRepository
    {
        private string path;

        public GuestPacijentRepository()
        {
            path = @"..\..\..\data\guestPacijenti.json";
        }

        public void UpisivanjeUFajl(GuestPacijent guestPacijent)
        {
            // TODO: implement
        }

        public List<GuestPacijent> CitanjeIzFajla()
        {
            // TODO: implement
            return null;
        }

        public GuestPacijent PregledGuestPacijenta(String id)
        {
            // TODO: implement
            return null;
        }

        public List<GuestPacijent> PregledSvihGuestPacijenata()
        {
            // TODO: implement
            return null;
        }

    }
}