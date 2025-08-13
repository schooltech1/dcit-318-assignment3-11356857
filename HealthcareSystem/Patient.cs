using System;

public class Patient
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Patient(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public override string ToString()
    {
        return $"Patient ID: {Id}, Name: {Name}";
    }
}
