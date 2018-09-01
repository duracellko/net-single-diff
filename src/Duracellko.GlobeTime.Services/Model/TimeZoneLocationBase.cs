namespace Duracellko.GlobeTime.Domain.Model
{
    public abstract class TimeZoneLocationBase
    {
        public int Id { get; set; }

        public Coordinates Location { get; set; }
    }
}
