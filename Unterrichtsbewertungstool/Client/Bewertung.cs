using System;

namespace Unterrichtsbewertungstool
{
    [Serializable]
    public class Bewertung
    {
        public int Punkte { get; private set; }
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