using System;
using System.Collections.Generic;

namespace Unterrichtsbewertungstool
{
    internal class ClientData
    {
        public List<Bewertung> bewertungen { get; set; }
        public long LastRequestedTimestampTicks { get; set; } = 0;

        public ClientData()
        {
            bewertungen = new List<Bewertung>();
        }

        internal void setRequested(long ticks)
        {
            LastRequestedTimestampTicks = ticks;
        }

        /// <summary>
        /// Holt die Bewertungen ab gegebenem Zeitpunkt
        /// </summary>
        /// <param name="ticks"></param>
        internal List<Bewertung> getBewertungen(long ticks)
        {
            return bewertungen.FindAll(b => b.TimeStampTicks >= ticks);
        }
    }
}