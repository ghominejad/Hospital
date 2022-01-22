namespace Hospital.Models
{
    public class Room
    {
        public int Id { get; set; }
        public int Capacity { get; set; }
        public int Reserved { get; set; }

        public List<Patient> Patients = new List<Patient>(); 
    }
}