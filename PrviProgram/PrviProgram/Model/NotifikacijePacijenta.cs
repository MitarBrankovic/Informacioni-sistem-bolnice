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
        public Pacijent pacijent;
        public DateTime PocetakDatuma;
        public DateTime KrajDatuma;
        public Recept recept;
        public string vremeObavestenja;
        public string opisNotifikacije;

    }
}