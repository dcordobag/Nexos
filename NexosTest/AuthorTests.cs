namespace NexosTest
{
    using ApiNexos.Controllers;
    using DataAccess.DB;
    using DataAccess.Dto;
    using EntityLayer.Entities.InputData;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using Xunit;

    public class AuthorTests
    {
        private readonly AuthorController _controller;
        private readonly DbContextOptions<ConnectionContext> options;
        public AuthorTests()
        {
            options = new DbContextOptionsBuilder<ConnectionContext>()
                .UseInMemoryDatabase(databaseName: "AuthorNexos")
                .Options;
            _controller = new AuthorController(options);

        }


        [Fact]
        public void AutorCreaElRegistro()
        {
            using (var context = new ConnectionContext(options))
            {
                context.Author.Add(new Author
                {
                    Birthdate = DateTime.Now,
                    Email = "Test",
                    FullName = "Test",
                    OriginCity = "Test"
                });
                context.SaveChanges();
            }
            // Arrange
            AuthorEntity entidad = new AuthorEntity()
            {
                Birthdate = DateTime.Now,
                Email = "Test",
                FullName = "Test",
                OriginCity = "Test"
            };
            // Act
            var response = _controller.CreateAuthor(entidad);
            // Assert
            Assert.Equal(1, response.Data);
        }


        [Fact]
        public void AutorRetornaLosRegistrosGuardadosEnLaBaseDeDatos()
        {
            int idAuthor = 0;
            using (var context = new ConnectionContext(options))
            {
                idAuthor = context.Author.Add(new Author
                {
                    Birthdate = DateTime.Now,
                    Email = "Test",
                    FullName = "Test",
                    OriginCity = "Test"
                }).Entity.ID;
                context.SaveChanges();
            }
            // Act1
            var response = _controller.GetListAuthors();
            // Assert1
            Assert.NotEqual(0, (response.Data as List<Author>).Count);
        }
    }
}
