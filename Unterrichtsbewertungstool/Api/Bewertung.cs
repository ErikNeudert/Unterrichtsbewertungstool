namespace Unterrichtsbewertungstool
{
    public class Bewertung
    {
        public int punkte { get; set; }
        public long timeStampMillis { get; set; }

        public Bewertung(int punkte, long timeStampMillis)
        {
            this.punkte = punkte;
            this.timeStampMillis = timeStampMillis;
        }
    }
}