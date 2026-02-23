namespace MOM_PROJECT.Models
{
    public class DashboardViewModel
    {
        // ===== COUNTS =====
        public int TotalMeetings { get; set; }
        public int UpcomingMeetings { get; set; }
        public int CompletedMeetings { get; set; }
        public int CancelledMeetings { get; set; }

        // ===== CHARTS =====
        public List<string> MeetingTypeLabels { get; set; } = new();
        public List<int> MeetingTypeCounts { get; set; } = new();

        public List<string> DepartmentLabels { get; set; } = new();
        public List<int> DepartmentCounts { get; set; } = new();
    }
}