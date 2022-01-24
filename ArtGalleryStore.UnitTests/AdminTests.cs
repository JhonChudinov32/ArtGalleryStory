using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ArtGalleryStore.Domain.Abstract;
using ArtGalleryStore.Domain.Entities;
using ArtGalleryStore.WebUI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ArtGalleryStore.UnitTests
{
    [TestClass]
    public class AdminTests
    {
        [TestMethod]
        public void Index_Contains_All_ArtGallerys()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IArtGalleryRepository> mock = new Mock<IArtGalleryRepository>();
            mock.Setup(m => m.ArtGallerys).Returns(new List<ArtGallery>
            {
                new ArtGallery { Articl_Id = 1, Name = "Картина1"},
                new ArtGallery { Articl_Id = 2, Name = "Картина2"},
                new ArtGallery { Articl_Id = 3, Name = "Картина3"},
                new ArtGallery { Articl_Id = 4, Name = "Картина4"},
                new ArtGallery { Articl_Id = 5, Name = "Картина5"}
            });

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Действие
            List<ArtGallery> result = ((IEnumerable<ArtGallery>)controller.Index().
                ViewData.Model).ToList();

            // Утверждение
            Assert.AreEqual(result.Count(), 5);
            Assert.AreEqual("Картина1", result[0].Name);
            Assert.AreEqual("Картина2", result[1].Name);
            Assert.AreEqual("Картина3", result[2].Name);
        }
        [TestMethod]
        public void Can_Edit_ArtGallery()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IArtGalleryRepository> mock = new Mock<IArtGalleryRepository>();
            mock.Setup(m => m.ArtGallerys).Returns(new List<ArtGallery>
           {
                new ArtGallery { Articl_Id = 1, Name = "Картина1"},
                new ArtGallery { Articl_Id = 2, Name = "Картина2"},
                new ArtGallery { Articl_Id = 3, Name = "Картина3"},
                new ArtGallery { Articl_Id = 4, Name = "Картина4"},
                new ArtGallery { Articl_Id = 5, Name = "Картина5"}
            });

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Действие
            ArtGallery art1 = controller.Edit(1).ViewData.Model as ArtGallery;
            ArtGallery art2 = controller.Edit(2).ViewData.Model as ArtGallery;
            ArtGallery art3 = controller.Edit(3).ViewData.Model as ArtGallery;

            // Assert
            Assert.AreEqual(1, art1.Articl_Id);
            Assert.AreEqual(2, art2.Articl_Id);
            Assert.AreEqual(3, art3.Articl_Id);
        }

        [TestMethod]
        public void Cannot_Edit_Nonexistent_ArtGallery()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IArtGalleryRepository> mock = new Mock<IArtGalleryRepository>();
            mock.Setup(m => m.ArtGallerys).Returns(new List<ArtGallery>
            {
                new ArtGallery { Articl_Id = 1, Name = "Картина1"},
                new ArtGallery { Articl_Id = 2, Name = "Картина2"},
                new ArtGallery { Articl_Id = 3, Name = "Картина3"},
                new ArtGallery { Articl_Id = 4, Name = "Картина4"},
                new ArtGallery { Articl_Id = 5, Name = "Картина5"}
            });

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Действие
            ArtGallery result = controller.Edit(6).ViewData.Model as ArtGallery;

            // Assert
        }
        [TestMethod]
        public void Can_Save_Valid_Changes()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IArtGalleryRepository> mock = new Mock<IArtGalleryRepository>();

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Организация - создание объекта ArtGallery
            ArtGallery art = new ArtGallery { Name = "Test" };

            // Действие - попытка сохранения товара
            ActionResult result = controller.Edit(art);

            // Утверждение - проверка того, что к хранилищу производится обращение
            mock.Verify(m => m.SaveArt(art));

            // Утверждение - проверка типа результата метода
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Cannot_Save_Invalid_Changes()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IArtGalleryRepository> mock = new Mock<IArtGalleryRepository>();

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Организация - создание объекта ArtGallery
            ArtGallery art = new ArtGallery { Name = "Test" };

            // Организация - добавление ошибки в состояние модели
            controller.ModelState.AddModelError("error", "error");

            // Действие - попытка сохранения товара
            ActionResult result = controller.Edit(art);

            // Утверждение - проверка того, что обращение к хранилищу НЕ производится 
            mock.Verify(m => m.SaveArt(It.IsAny<ArtGallery>()), Times.Never());

            // Утверждение - проверка типа результата метода
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Can_Delete_Valid_ArtGallerys()
        {
            // Организация - создание объекта ArtGallery
            ArtGallery art = new ArtGallery { Articl_Id = 2, Name = "Картина2" };

            // Организация - создание имитированного хранилища данных
            Mock<IArtGalleryRepository> mock = new Mock<IArtGalleryRepository>();
            mock.Setup(m => m.ArtGallerys).Returns(new List<ArtGallery>
            {
                new ArtGallery { Articl_Id = 1, Name = "Картина1"},
                new ArtGallery { Articl_Id = 2, Name = "Картина2"},
                new ArtGallery { Articl_Id = 3, Name = "Картина3"},
                new ArtGallery { Articl_Id = 4, Name = "Картина4"},
                new ArtGallery { Articl_Id = 5, Name = "Картина5"}
            });

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Действие - удаление ArtGallery
            controller.Delete(art.Articl_Id);

            // Утверждение - проверка того, что метод удаления в хранилище
            // вызывается для корректного объекта ArtGallery
            mock.Verify(m => m.DeleteArt(art.Articl_Id));
        }
    }
}
