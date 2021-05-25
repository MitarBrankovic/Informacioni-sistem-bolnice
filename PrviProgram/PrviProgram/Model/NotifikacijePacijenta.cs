/***********************************************************************
 * Module:  NotifikacijePacijenta.cs
 * Author:  Saska WorkPC
 * Purpose: Definition of the Class Model.NotifikacijePacijenta
 ***********************************************************************/

using System;
using System.Collections.Generic;

namespace Model
{
   public class NotifikacijePacijenta
   {
        public Pacijent Pacijent;
        public DateTime PocetakDatuma;
        public DateTime KrajDatuma;
        public Recept Recept;
        public string VremeObavestenja;
        public string OpisNotifikacije;

    }
}