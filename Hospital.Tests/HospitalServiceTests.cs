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
    public class HospitalServiceTests 
    {
        readonly IHospitalService _service;
        public HospitalServiceTests()
        {

            _service = new HospitalService(new HospitalInMemoryRepository());
        }

        [Fact]

        public void Admision_Should_Decrease_The_Room_Capacity_When_A_New_Patient_Is_Admitted()
        {
            var patient = _service.RegisterPatient(new Patient()
            {
                FirstName = "name",
                LastName = "family"
            });

            var room = _service.RegisterRoom(new Room()
            {
                Capacity = 3,
                Reserved = 2
            });


            room = _service.AdmitPatient(room, patient);

            var existsInTheRoom = room.Patients.Where(p => p.Id == patient.Id).Any();

            Assert.NotNull(room);
            Assert.Equal(3, room.Reserved);
            Assert.True(existsInTheRoom);
        }

        [Fact]

        public void A_Patient_Should_not_Be_Addmitted_If_The_Room_Is_Full()
        {
            var patient = _service.RegisterPatient(new Patient()
            {
                FirstName = "name",
                LastName = "family"
            });

            var room = _service.RegisterRoom(new Room()
            {
                Capacity = 3,
                Reserved = 3
            });

            Assert.Throws<TheRoomIsFullException>(() => { _service.AdmitPatient(room, patient); });
        }

        [Fact]
        public void Admision_Should_Increase_The_Room_Capacity_When_A_New_Patient_Is_Released()
        {
            var patient = _service.RegisterPatient(new Patient()
            {
                FirstName = "name",
                LastName = "family"
            });

            var room = _service.RegisterRoom(new Room()
            {
                Capacity = 3,
                Reserved = 2
            });

            room = _service.AdmitPatient(room, patient);
            Assert.Equal(3, room.Reserved);

            room = _service.ReleasePatient(room, patient);

            var existsInTheRoom = room.Patients.Where(p => p.Id == patient.Id).Any();

            Assert.NotNull(room);
            Assert.Equal(2, room.Reserved);
            Assert.False(existsInTheRoom);
        }

        [Fact]
        public void RegisteringPatientTests()
        {
            var patient = _service.RegisterPatient(new Patient()
            {
                FirstName = "name",
                LastName = "family"
            });

            Assert.NotNull(patient);
            Assert.NotEqual(0, patient.Id);

        }
        [Fact]
        public void RegisteringRoomTests()
        {
            var room = _service.RegisterRoom(new Room()
            {
                Capacity = 3
            });

            Assert.NotNull(room);
            Assert.NotEqual(0, room.Id);

        }
        [Fact]
        public void AdmitionTests()
        {
            var patient = _service.RegisterPatient(new Patient()
            {
                FirstName = "name",
                LastName = "family"
            });

            var room = _service.RegisterRoom(new Room()
            {
                Capacity = 3,
                Reserved = 0
            });


            room = _service.AdmitPatient(room, patient);

            var existsInTheRoom = room.Patients.Where(p => p.Id == patient.Id).Any();

            Assert.NotNull(room);
            Assert.True(existsInTheRoom);

        }


        [Fact]
        public void ReleasePatientTests()
        {
            var patient = _service.RegisterPatient(new Patient()
            {
                FirstName = "name",
                LastName = "family"
            });

            var room = _service.RegisterRoom(new Room()
            {
                Capacity = 3,
                Reserved = 2
            });


            room = _service.AdmitPatient(room, patient);

            var existsInTheRoom = room.Patients.Where(p => p.Id == patient.Id).Any();

            Assert.NotNull(room);
            Assert.True(existsInTheRoom);


            room = _service.ReleasePatient(room, patient);
            existsInTheRoom = room.Patients.Where(p => p.Id == patient.Id).Any();

            Assert.NotNull(room);
            Assert.False(existsInTheRoom);

        }
     
    }
}
