using System;

namespace Unterrichtsbewertungstool
{
    /// <summary>
    /// Representiert eine Nutzerbewertung.
    /// Wird verwendet um den Zeitpunkt und die Punkte der Bewertung festhalten
    /// </summary>
    [Serializable]
    public class Bewertung
    {
        public int Punkte { get; private set; }
        /// <summary>
        /// Der Zeitpunkt der Bewertung, angegeben in Ticks (DateTime.Now.Ticks)
        /// </summary>
        public long TimeStampTicks { get; private set; }

        public Bewertung(int Punkte, long TimeStampTicks)
        {
            this.Punkte = Punkte;
            this.TimeStampTicks = TimeStampTicks;
        }

        public override string ToString()
        {
            return Punkte + " - " + TimeStampTicks;
        }
    }
}