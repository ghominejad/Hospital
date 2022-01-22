using Hospital.Models;

namespace Hospital.Data
{
    public class HospitalInMemoryRepository : IHospitalRepository
    {
        IList<Patient> _patients { get; set; } = new List<Patient>();
        IList<Room> _rooms { get; set; } = new List<Room>();
        int _idCounter { get; set; } = 0;



      

        public Patient InsertPatient(Patient patient)
        {
            
            patient.Id = ++_idCounter;
            _patients.Add(patient);

            return patient;
        }

        public Room InsertRoom(Room room)
        {
            room.Id = ++_idCounter;

            _rooms.Add(room);

            return room;
        }
        public Room AssignPatientToRoom(Room room, Patient patient)
        {
            room.Patients.Add(patient);

            return room;
        }

        public Room ReleasePatientFromRoom(Room room, Patient patient)
        {
            var found = room.Patients.Where(x => x.Id == patient.Id).First();
            room.Patients.Remove(found);

            return room;
        }
    }
}