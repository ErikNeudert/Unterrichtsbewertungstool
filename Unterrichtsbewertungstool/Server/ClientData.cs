using System;
using System.Collections.Generic;

namespace Unterrichtsbewertungstool
{
    /// <summary>
    /// Wrappt die Bewertungen eines einzelnen Clients und speichert die letzte Datenaktualisierung dessen
    /// </summary>
    internal class ClientData
    {
        /// <summary>
        /// Die Bewertungen des Clients
        /// </summary>
        public List<Bewertung> bewertungen { get; set; }

        /// <summary>
        /// Die Ticks des Zeitpunktes der die letzte Abfrage der Daten markiert.
        /// Dieser wird verwendet um zu bestimmen welche Daten der Client schon hat.
        /// Dementsprechend werden die fehlenden Daten gesendet.
        /// Dies bringt eine reale Dateneinspaarung von 5000%.
        /// </summary>
        public long LastRequestedTimestampTicks { get; set; } = 0;

        public ClientData()
        {
            bewertungen = new List<Bewertung>();
        }

        /// <summary>
        /// Filtert die Bewertungen nach gegebenem Zeitpunkt, 
        /// so, dass nur ältere zurück gegeben werden.
        /// </summary>
        /// <param name="requestTicks"></param>
        internal List<Bewertung> getBewertungen(long requestTicks)
        {
            return bewertungen.FindAll(b => b.TimeStampTicks >= requestTicks);
        }
    }
}