using System;
using System.IO;
using Moq;
using Xunit;
using Hospital.Tests.Helpers;
using Hospital.Services;
using Hospital.Data;
using Hospital.Models;

using System.Linq;

namespace Hospital.Tests
{
    public class HospitalInMemoryRepositoryTests 
    {
        readonly IHospitalRepository _repo;
        public HospitalInMemoryRepositoryTests()
        {

            _repo = new HospitalInMemoryRepository();
        }

        [Fact]
        public void CanAddPatient()
        {
            var patient = _repo.InsertPatient(new Patient()
            {
                FirstName = "name",
                LastName = "family"
            });

            Assert.NotNull(patient);
            Assert.NotEqual(0, patient.Id);

        }
        [Fact]
        public void CanAddRoom()
        {
            var room = _repo.InsertRoom(new Room()
            {
                Capacity = 3
            });

            Assert.NotNull(room);
            Assert.NotEqual(0, room.Id);

        }
        [Fact]
        public void CanAssignPatientToRoom()
        {
            var patient = _repo.InsertPatient(new Patient()
            {
                FirstName = "name",
                LastName = "family"
            });

            var room = _repo.InsertRoom(new Room()
            {
                Capacity = 3
            });


            room = _repo.AssignPatientToRoom(room, patient);
            var existsInTheRoom = room.Patients.Where(p => p.Id == patient.Id).Any();

            Assert.NotNull(room);
            Assert.True(existsInTheRoom);



        }

        [Fact]
        public void CanReleasePatientFromRoom()
        {
            var patient = _repo.InsertPatient(new Patient()
            {
                FirstName = "name",
                LastName = "family"
            });

            var room = _repo.InsertRoom(new Room()
            {
                Capacity = 3
            });


            room = _repo.AssignPatientToRoom(room, patient);
            var existsInTheRoom = room.Patients.Where(p => p.Id == patient.Id).Any();

            Assert.NotNull(room);
            Assert.True(existsInTheRoom);


            room = _repo.ReleasePatientFromRoom(room, patient);
            existsInTheRoom = room.Patients.Where(p => p.Id == patient.Id).Any();

            Assert.NotNull(room);
            Assert.False(existsInTheRoom);

        }
    }
}
