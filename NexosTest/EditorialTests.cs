namespace NexosTest
{
    using ApiNexos.Controllers;
    using DataAccess.DB;
    using DataAccess.Dto;
    using EntityLayer.Entities.InputData;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using Xunit;

    public class EditorialTests
    {
        private readonly EditorialController _controller;
        private readonly DbContextOptions<ConnectionContext> options;
        public EditorialTests()
        {
            options = new DbContextOptionsBuilder<ConnectionContext>()
                .UseInMemoryDatabase(databaseName: "EditorialNexos")
                .Options;
            _controller = new EditorialController(options);

        }


        [Fact]
        public void EditorialCreaElRegistro()
        {
            using (var context = new ConnectionContext(options))
            {
                context.Editorial.Add(new Editorial
                {
                    CorrespondenceAddress = "Test",
                    Email = "Test",
                    MaxBooks = 1,
                    Name = "Test",
                    Phone = "Test"
                });
                context.SaveChanges();
            }
            // Arrange
            EditorialEntity entidad = new EditorialEntity()
            {
                CorrespondenceAddress = "Test",
                Email = "Test",
                MaxBooks = 1,
                Name = "Test",
                Phone = "Test"
            };
            // Act
            var response = _controller.CreateEditorial(entidad);
            // Assert
            Assert.Equal(1, response.Data);
        }


        [Fact]
        public void EditorialRetornaLosRegistrosGuardadosEnLaBaseDeDatos()
        {
            using (var context = new ConnectionContext(options))
            {
                context.Editorial.Add(new Editorial
                {
                    CorrespondenceAddress = "Test",
                    Email = "Test",
                    MaxBooks = 1,
                    Name = "Test",
                    Phone = "Test"
                });
                context.SaveChanges();
            }
            // Act1
            var response = _controller.GetTotalEditorials();
            // Assert1
            Assert.NotEqual(0, (response.Data as List<Editorial>).Count);
        }
    }
}
