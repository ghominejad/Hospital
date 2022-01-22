using Hospital.Data;
using Hospital.Models;

namespace Hospital.Services
{
    public class HospitalService : IHospitalService
    {
        private readonly IHospitalRepository repo;

        public HospitalService(IHospitalRepository repository )
        {
            this.repo = repository;
        }

        public Room AdmitPatient(Room room, Patient patient)
        {
            if(room.Capacity == room.Reserved)
            {
                throw new TheRoomIsFullException();
            }


            room =  repo.AssignPatientToRoom(room, patient);
            
            room.Reserved++;

            return room;

        }
        public Room ReleasePatient(Room room, Patient patient)
        {
            room = repo.ReleasePatientFromRoom(room, patient);
            room.Reserved--;

            return room;
        }

        public Patient RegisterPatient(Patient patient)
        {
            return repo.InsertPatient(patient);
        }

        public Room RegisterRoom(Room room)
        {
            return repo.InsertRoom(room);
        }

  
    }
}