using Hospital.Models;

namespace Hospital.Services
{
    public interface IHospitalService
    {
        Room AdmitPatient(Room room, Patient patient);
        Room RegisterRoom(Room room);
        Patient RegisterPatient(Patient patient);
        Room ReleasePatient(Room room, Patient patient);
    }
}