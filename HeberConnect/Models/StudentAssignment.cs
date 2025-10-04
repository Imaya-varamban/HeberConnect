public class StudentAssignment
{
    public int Id { get; set; }
    public int AssignmentId { get; set; }
    public string StudentId { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public DateTime SubmittedOn { get; set; }
}
