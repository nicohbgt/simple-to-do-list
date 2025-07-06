public class ActivityModel
{
    public int Id { get; set; }
    public int PlanId { get; set; }
    public string ActivityName { get; set; }
    public DateTimeOffset StartTime { get; set; } // Use DateTimeOffset for precise time scheduling
    public DateTimeOffset EndTime { get; set; }
    public IntensityLevel Intensity { get; set; }
}