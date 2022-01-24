using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ArtGalleryStore.Domain.Abstract;
using ArtGalleryStore.Domain.Entities;
using ArtGalleryStore.WebUI.Controllers;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;

namespace ArtGalleryStore.UnitTests
{
    [TestClass]
    public class ImageTests
    {
        [TestMethod]
        public void Can_Retrieve_Image_Data()
        {
            // Организация - создание объекта ArtGallery с данными изображения
            ArtGallery art = new ArtGallery
            {
                Articl_Id = 2,
                Name = "Картина2",
                Photo = new byte[] { },
                ImageMimeType = "image/png"
            };

            // Организация - создание имитированного хранилища
            Mock<IArtGalleryRepository> mock = new Mock<IArtGalleryRepository>();
            mock.Setup(m => m.ArtGallerys).Returns(new List<ArtGallery> {
                new ArtGallery {Articl_Id = 1, Name = "Картина1"},
                art,
                new ArtGallery {Articl_Id = 3, Name = "Картина3"}
            }.AsQueryable());

            // Организация - создание контроллера
            ArtGalleryController controller = new ArtGalleryController(mock.Object);

            // Действие - вызов метода действия GetImage()
            ActionResult result = controller.GetImage(2);

            // Утверждение
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FileResult));
            Assert.AreEqual(art.ImageMimeType, ((FileResult)result).ContentType);
        }

        [TestMethod]
        public void Cannot_Retrieve_Image_Data_For_Invalid_ID()
        {
            // Организация - создание имитированного хранилища
            // Организация - создание имитированного хранилища
            Mock<IArtGalleryRepository> mock = new Mock<IArtGalleryRepository>();
            mock.Setup(m => m.ArtGallerys).Returns(new List<ArtGallery> {
                new ArtGallery {Articl_Id = 1, Name = "Картина1"},
                new ArtGallery {Articl_Id = 2, Name = "Картина2"}
            }.AsQueryable());

            // Организация - создание контроллера
            ArtGalleryController controller = new ArtGalleryController(mock.Object);

            // Действие - вызов метода действия GetImage()
            ActionResult result = controller.GetImage(10);

            // Утверждение
            Assert.IsNull(result);
        }
    }
}
