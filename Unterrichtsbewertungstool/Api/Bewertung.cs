namespace Unterrichtsbewertungstool
{
    public class Bewertung
    {
        public int punkte;
        public long timeStampMillis;

        public Bewertung(int punkte, long timeStampMillis)
        {
            this.punkte = punkte;
            this.timeStampMillis = timeStampMillis;
        }
    }
}