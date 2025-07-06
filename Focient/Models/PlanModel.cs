using System;

public class PlanModel
{
    public enum IntensityLevel
    {
        Low = 0,
        Medium = 1,
        High = 2
    }

    public int Id { get; set; }
    public int UserId { get; set; } // Foreign key linking to Users table
    public string PlanName { get; set; }
    public DateTime DateOfPlan { get; set; }
    public IntensityLevel Intensity { get; set; }
    public string Description { get; set; }
}