public class Student
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public int Score { get; set; }

    public string GetGrade()
    {
        if (Score >= 80 && Score <= 100) return "A";
        else if (Score >= 70) return "B";
        else if (Score >= 60) return "C";
        else if (Score >= 50) return "D";
        else return "F";
    }
}
