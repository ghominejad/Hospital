using Hospital.Models;

namespace Hospital.Data
{
    public interface IHospitalRepository
    {
        Patient InsertPatient(Patient patient);
        Room InsertRoom(Room room);
        Room AssignPatientToRoom(Room room, Patient patient);
        Room ReleasePatientFromRoom(Room room, Patient patient);
    }
}