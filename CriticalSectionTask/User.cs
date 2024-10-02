namespace CriticalSectionTask;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public DateTime BirthDay { get; set; }
    public object DateOfBirth { get; internal set; }
}
