using System;
using System.Collections.Generic;

public class HealthcareApp
{
    private IRepository<Patient> patientRepository = new Repository<Patient>();
    private IRepository<Prescription> prescriptionRepository = new Repository<Prescription>();
    private Dictionary<int, List<Prescription>> prescriptionMap = new Dictionary<int, List<Prescription>>();

    public void Run()
    {
        bool running = true;
        while (running)
        {
            Console.WriteLine("\n--- Healthcare System Menu ---");
            Console.WriteLine("1. Add Patient");
            Console.WriteLine("2. Add Prescription");
            Console.WriteLine("3. View All Patients");
            Console.WriteLine("4. View All Prescriptions");
            Console.WriteLine("5. View Prescriptions for a Patient");
            Console.WriteLine("6. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    AddPatient();
                    break;
                case "2":
                    AddPrescription();
                    break;
                case "3":
                    ViewPatients();
                    break;
                case "4":
                    ViewPrescriptions();
                    break;
                case "5":
                    ViewPrescriptionsForPatient();
                    break;
                case "6":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }

    private void AddPatient()
    {
        Console.Write("Enter Patient ID: ");
        int id = int.Parse(Console.ReadLine());
        Console.Write("Enter Patient Name: ");
        string name = Console.ReadLine();

        patientRepository.Add(new Patient(id, name));
        Console.WriteLine("Patient added successfully.");
    }

    private void AddPrescription()
    {
        Console.Write("Enter Prescription ID: ");
        int id = int.Parse(Console.ReadLine());
        Console.Write("Enter Medication Name: ");
        string medication = Console.ReadLine();
        Console.Write("Enter Dosage: ");
        string dosage = Console.ReadLine();
        Console.Write("Enter Patient ID: ");
        int patientId = int.Parse(Console.ReadLine());

        Prescription prescription = new Prescription(id, medication, dosage);
        prescriptionRepository.Add(prescription);

        if (!prescriptionMap.ContainsKey(patientId))
        {
            prescriptionMap[patientId] = new List<Prescription>();
        }
        prescriptionMap[patientId].Add(prescription);

        Console.WriteLine("Prescription added successfully.");
    }

    private void ViewPatients()
    {
        var patients = patientRepository.GetAll();
        if (patients.Count == 0)
        {
            Console.WriteLine("No patients found.");
            return;
        }
        Console.WriteLine("\n--- Patients List ---");
        foreach (var patient in patients)
        {
            Console.WriteLine(patient);
        }
    }

    private void ViewPrescriptions()
    {
        var prescriptions = prescriptionRepository.GetAll();
        if (prescriptions.Count == 0)
        {
            Console.WriteLine("No prescriptions found.");
            return;
        }
        Console.WriteLine("\n--- Prescriptions List ---");
        foreach (var prescription in prescriptions)
        {
            Console.WriteLine(prescription);
        }
    }

    private void ViewPrescriptionsForPatient()
    {
        Console.Write("Enter Patient ID: ");
        int patientId = int.Parse(Console.ReadLine());

        if (!prescriptionMap.ContainsKey(patientId))
        {
            Console.WriteLine("No prescriptions found for this patient.");
            return;
        }

        Console.WriteLine($"\nPrescriptions for Patient ID {patientId}:");
        foreach (var prescription in prescriptionMap[patientId])
        {
            Console.WriteLine(prescription);
        }
    }
}
