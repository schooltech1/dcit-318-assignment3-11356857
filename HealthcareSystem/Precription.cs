using System;

public class Prescription
{
    public int Id { get; set; }
    public string Medication { get; set; }
    public string Dosage { get; set; }

    public Prescription(int id, string medication, string dosage)
    {
        Id = id;
        Medication = medication;
        Dosage = dosage;
    }

    public override string ToString()
    {
        return $"Prescription ID: {Id}, Medication: {Medication}, Dosage: {Dosage}";
    }
}
