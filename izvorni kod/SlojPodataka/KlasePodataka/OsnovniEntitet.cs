using System;
using System.Collections.Generic;
using System.Text;

namespace SlojPodataka.KlasePodataka
{
    public abstract class OsnovniEntitet
    {
        public DateTime DatumKreiranja { get; set; } = DateTime.Now;
    }
}
