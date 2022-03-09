
namespace NexosTest
{
    using ApiNexos.Controllers;
    using DataAccess.DB;
    using DataAccess.Dto;
    using EntityLayer.Entities.InputData;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using System;
    using System.Collections.Generic;
    using Xunit;

    public class BookTests
    {
        private readonly BookController _controller;
        private readonly DbContextOptions<ConnectionContext> options;
        public BookTests()
        {
            options = new DbContextOptionsBuilder<ConnectionContext>()
                .UseInMemoryDatabase(databaseName: "Nexos")
                .Options;
            _controller = new BookController(options);
            PrepareEnvironment();
            Insertar2Registros();
        }

        public void PrepareEnvironment()
        {
            // Insert seed data into the database using one instance of the context
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
                context.Author.Add(new Author
                {
                    Birthdate = DateTime.Now,
                    Email = "Test",
                    FullName = "Test",
                    OriginCity = "Test"
                });
                context.Author.Add(new Author
                {
                    Birthdate = DateTime.Now,
                    Email = "Test2",
                    FullName = "Test2",
                    OriginCity = "Test2"
                });
                context.SaveChanges();
            }

        }

        public void Insertar2Registros()
        {
            // Insert seed data into the database using one instance of the context
            using (var context = new ConnectionContext(options))
            {
                context.Book.Add(new Book
                {
                    AuthorID = 1,
                    EditorialID = 1,
                    Genre = "Test",
                    PagesNumber = 1,
                    Title = "Test",
                    Year = "2022"
                });
                context.Book.Add(new Book
                {
                    AuthorID = 2,
                    EditorialID = 1,
                    Genre = "Test2",
                    PagesNumber = 11,
                    Title = "Test2",
                    Year = "2023"
                });
                context.SaveChanges();
            }
        }

        [Fact]
        public void LibroConAutorYEntidadCorrectosCreaElRegistro()
        {
            // Arrange
            BookEntity entidad = new BookEntity()
            {
                AuthorID = 1,
                EditorialID = 1,
                Genre = "Test",
                PagesNumber = 1,
                Title = "Test",
                Year = "2022"
            };
            // Act
            var response = _controller.CreateNewBook(entidad);
            // Assert
            Assert.Equal(1, response.Data);
        }

        [Fact]
        public void LibroConAutorIncorrectoNoCreaElRegistro()
        {
            // Arrange
            BookEntity entidad = new BookEntity()
            {
                AuthorID = 3,
                EditorialID = 1,
                Genre = "Test",
                PagesNumber = 1,
                Title = "Test",
                Year = "2022"
            };
            // Act
            var response = _controller.CreateNewBook(entidad);
            // Assert
            Assert.Equal("El autor no está registrado.", response.Message);
        }

        [Fact]
        public void LibroConEditorialIncorrectaNoCreaElRegistro()
        {
            // Arrange
            BookEntity entidad = new BookEntity()
            {
                AuthorID = 1,
                EditorialID = 2,
                Genre = "Test",
                PagesNumber = 1,
                Title = "Test",
                Year = "2022"
            };
            // Act
            var response = _controller.CreateNewBook(entidad);
            // Assert
            Assert.Equal("La editorial no está registrada.", response.Message);
        }

        [Fact]
        public void LibroConLimiteDeRegistro_1_Solo_Permite_Crear_1_Registro()
        {
            // Arrange
            BookEntity entidad = new BookEntity()
            {
                AuthorID = 1,
                EditorialID = 1,
                Genre = "Test",
                PagesNumber = 1,
                Title = "Test",
                Year = "2022"
            };
            // Act1
            var response = _controller.CreateNewBook(entidad);
            // Assert1
            Assert.Equal(1, response.Data);

            // Act2
            var response2 = _controller.CreateNewBook(entidad);
            // Assert2
            Assert.Equal("No es posible registrar el libro, se alcanzó el máximo permitido.", response2.Message);
        }

        [Fact]
        public void LibroRetornaLosRegistrosGuardadosEnLaBaseDeDatos()
        {
            
            // Act1
            var response = _controller.GetListBooks();
            // Assert1
            Assert.NotEqual(1, (response.Data as List<Book>).Count);
        }

        [Fact]
        public void LibroConFiltroDeAutor_2_RetornaLibroConAutor_2()
        {
            // Act1
            var response = _controller.GetBookByAuthor(2);
            // Assert1
            Assert.Equal(2, (response.Data as List<Book>)[0].AuthorID);
        }

        [Fact]
        public void LibroConFiltroDeLibro_Test2_RetornaLibro_Test2()
        {
            // Act1
            var response = _controller.GetBookByTitle("Test2");
            // Assert1
            Assert.Equal("Test2", (response.Data as List<Book>)[0].Title);
        }

        [Fact]
        public void LibroConFiltroDeAnio_2023_RetornaLibroConAnio2023()
        {
            // Act1
            var response = _controller.GetBookByYear("2023");
            // Assert1
            Assert.Equal("2023", (response.Data as List<Book>)[0].Year);
        }
    }
}
