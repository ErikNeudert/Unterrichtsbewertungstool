namespace Unterrichtsbewertungstool
{
    public class Bewertung
    {
        public int Punkte { get; private set; }
        public long TimeStampMillis { get; private set; }

        public Bewertung(int punkte, long timeStampMillis)
        {
            this.Punkte = punkte;
            this.TimeStampMillis = timeStampMillis;
        }
    }
}