using System;

namespace Unterrichtsbewertungstool
{
    [Serializable]
    public class Bewertung
    {
        public int Punkte { get; private set; }
        public long TimeStampTicks { get; private set; }

        public Bewertung(int punkte, long timeStampTicks)
        {
            this.Punkte = punkte;
            this.TimeStampTicks = timeStampTicks;
        }
    }
}