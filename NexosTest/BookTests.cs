
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

        }


        [Fact]
        public void LibroConAutorYEntidadCorrectosCreaElRegistro()
        {
            int idEditorial = 0;
            int idActor = 0;
            using (var context = new ConnectionContext(options))
            {
                idEditorial = context.Editorial.Add(new Editorial
                {
                    CorrespondenceAddress = "Test",
                    Email = "Test",
                    MaxBooks = 1,
                    Name = "Test",
                    Phone = "Test"
                }).Entity.ID;
                idActor = context.Author.Add(new Author
                {
                    Birthdate = DateTime.Now,
                    Email = "Test",
                    FullName = "Test",
                    OriginCity = "Test"
                }).Entity.ID;
                context.SaveChanges();
            }
            // Arrange
            BookEntity entidad = new BookEntity()
            {
                AuthorID = idActor,
                EditorialID = idEditorial,
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
            int idEditorial = 0;
            using (var context = new ConnectionContext(options))
            {
                idEditorial = context.Editorial.Add(new Editorial
                {
                    CorrespondenceAddress = "Test",
                    Email = "Test",
                    MaxBooks = 1,
                    Name = "Test",
                    Phone = "Test"
                }).Entity.ID;
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
            // Arrange
            BookEntity entidad = new BookEntity()
            {
                AuthorID = 100,
                EditorialID = idEditorial,
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
            int idAuthor = 0;
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
                idAuthor = context.Author.Add(new Author
                {
                    Birthdate = DateTime.Now,
                    Email = "Test",
                    FullName = "Test",
                    OriginCity = "Test"
                }).Entity.ID;
                context.SaveChanges();
            }
            BookEntity entidad = new BookEntity()
            {
                AuthorID = idAuthor,
                EditorialID = 100,
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
            int idEditorial = 0;
            int idAuthor = 0;
            using (var context = new ConnectionContext(options))
            {
                idEditorial = context.Editorial.Add(new Editorial
                {
                    CorrespondenceAddress = "Test",
                    Email = "Test",
                    MaxBooks = 1,
                    Name = "Test2",
                    Phone = "Test"
                }).Entity.ID;
                idAuthor = context.Author.Add(new Author
                {
                    Birthdate = DateTime.Now,
                    Email = "Test",
                    FullName = "Test",
                    OriginCity = "Test"
                }).Entity.ID;
                context.SaveChanges();
            }
            BookEntity entidad = new BookEntity()
            {
                AuthorID = idAuthor,
                EditorialID = idEditorial,
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
            int idEditorial = 0;
            int idAuthor = 0;
            int idBook = 0;
            using (var context = new ConnectionContext(options))
            {
                idEditorial = context.Editorial.Add(new Editorial
                {
                    CorrespondenceAddress = "Test",
                    Email = "Test",
                    MaxBooks = 1,
                    Name = "Test",
                    Phone = "Test"
                }).Entity.ID;
                idAuthor = context.Author.Add(new Author
                {
                    Birthdate = DateTime.Now,
                    Email = "Test",
                    FullName = "Test",
                    OriginCity = "Test"
                }).Entity.ID;
                context.Book.Add(new Book
                {
                    AuthorID = idAuthor,
                    EditorialID = idEditorial,
                    Genre = "Test",
                    PagesNumber = 1,
                    Title = "Test",
                    Year = "2022"
                });
                idBook = context.Book.Add(new Book
                {
                    AuthorID = idAuthor,
                    EditorialID = idEditorial,
                    Genre = "Test2",
                    PagesNumber = 11,
                    Title = "Test2",
                    Year = "2023"
                }).Entity.ID;
                context.SaveChanges();
            }
            // Act1
            var response = _controller.GetListBooks();
            // Assert1
            Assert.Equal(idBook, (response.Data as List<Book>).Count);
        }

        [Fact]
        public void LibroConFiltroDeAutorRetornaLibroConAutor()
        {
            int idAuthor = 0;
            int idEditorial = 0;
            using (var context = new ConnectionContext(options))
            {
                idEditorial = context.Editorial.Add(new Editorial
                {
                    CorrespondenceAddress = "Test",
                    Email = "Test",
                    MaxBooks = 1,
                    Name = "Test",
                    Phone = "Test"
                }).Entity.ID;
                context.Author.Add(new Author
                {
                    Birthdate = DateTime.Now,
                    Email = "Test",
                    FullName = "Test",
                    OriginCity = "Test"
                });
                idAuthor = context.Author.Add(new Author
                {
                    Birthdate = DateTime.Now,
                    Email = "Test",
                    FullName = "Test",
                    OriginCity = "Test"
                }).Entity.ID;
                context.Book.Add(new Book
                {
                    AuthorID = idAuthor,
                    EditorialID = idEditorial,
                    Genre = "Test",
                    PagesNumber = 1,
                    Title = "Test",
                    Year = "2022"
                });
                context.Book.Add(new Book
                {
                    AuthorID = idAuthor,
                    EditorialID = idEditorial,
                    Genre = "Test2",
                    PagesNumber = 11,
                    Title = "Test2",
                    Year = "2023"
                });
                context.SaveChanges();
            }
            // Act1
            var response = _controller.GetBookByAuthor(idAuthor);
            // Assert1
            Assert.Equal(idAuthor, (response.Data as List<Book>)[0].AuthorID);
        }

        [Fact]
        public void LibroConFiltroDeLibro_Test2_RetornaLibro_Test2()
        {
            int idAuthor = 0;
            int idEditorial = 0;
            using (var context = new ConnectionContext(options))
            {
                idEditorial = context.Editorial.Add(new Editorial
                {
                    CorrespondenceAddress = "Test",
                    Email = "Test",
                    MaxBooks = 1,
                    Name = "Test",
                    Phone = "Test"
                }).Entity.ID;
                idAuthor = context.Author.Add(new Author
                {
                    Birthdate = DateTime.Now,
                    Email = "Test",
                    FullName = "Test",
                    OriginCity = "Test"
                }).Entity.ID;
                context.Book.Add(new Book
                {
                    AuthorID = idAuthor,
                    EditorialID = idEditorial,
                    Genre = "Test",
                    PagesNumber = 1,
                    Title = "Test",
                    Year = "2022"
                });
                context.Book.Add(new Book
                {
                    AuthorID = idAuthor,
                    EditorialID = idEditorial,
                    Genre = "Test2",
                    PagesNumber = 11,
                    Title = "Test2",
                    Year = "2023"
                });
                context.SaveChanges();
            }
            // Act1
            var response = _controller.GetBookByTitle("Test2");
            // Assert1
            Assert.Equal("Test2", (response.Data as List<Book>)[0].Title);
        }

        [Fact]
        public void LibroConFiltroDeAnio_2023_RetornaLibroConAnio2023()
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
                context.Author.Add(new Author
                {
                    Birthdate = DateTime.Now,
                    Email = "Test",
                    FullName = "Test",
                    OriginCity = "Test"
                });
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
                    AuthorID = 1,
                    EditorialID = 1,
                    Genre = "Test2",
                    PagesNumber = 11,
                    Title = "Test2",
                    Year = "2023"
                });
                context.SaveChanges();
            }
            // Act1
            var response = _controller.GetBookByYear("2023");
            // Assert1
            Assert.Equal("2023", (response.Data as List<Book>)[0].Year);
        }
    }
}
