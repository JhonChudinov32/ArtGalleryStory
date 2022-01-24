using ArtGalleryStore.Domain.Abstract;
using ArtGalleryStore.Domain.Entities;
using ArtGalleryStore.WebUI.Controllers;
using ArtGalleryStore.WebUI.HtmlHelpers;
using ArtGalleryStore.WebUI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArtGalleryStore.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            // Организация (arrange)
            Mock<IArtGalleryRepository> mock = new Mock<IArtGalleryRepository>();
            mock.Setup(m => m.ArtGallerys).Returns(new List<ArtGallery>
            {
                new ArtGallery { Articl_Id = 1, Name = "Картина1"},
                new ArtGallery { Articl_Id = 2, Name = "Картина2"},
                new ArtGallery { Articl_Id = 3, Name = "Картина3"},
                new ArtGallery { Articl_Id = 4, Name = "Картина4"},
                new ArtGallery { Articl_Id = 5, Name = "Картина5"},

            });
            ArtGalleryController controller = new ArtGalleryController(mock.Object);
            controller.pageSize = 3;

            // Действие (act)
            ArtGallerysListViewModel result = ((ArtGallerysListViewModel)controller.List(null, 2).Model);

            // Утверждение (assert)
            List<ArtGallery> ArtGallerys = result.ArtGallerys.ToList();
            Assert.IsTrue(ArtGallerys.Count == 2);
            Assert.AreEqual(ArtGallerys[0].Name, "Картина4");
            Assert.AreEqual(ArtGallerys[1].Name, "Картина5");
        }
        [TestMethod]
        public void Can_Generate_Page_Links()
        {

            // Организация - определение вспомогательного метода HTML - это необходимо
            // для применения расширяющего метода
            System.Web.Mvc.HtmlHelper myHelper = null;

            // Организация - создание объекта PagingInfo
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            // Организация - настройка делегата с помощью лямбда-выражения
            Func<int, string> pageUrlDelegate = i => "Page" + i;

            // Действие
            System.Web.Mvc.MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            // Утверждение
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
                + @"<a class=""btn btn-default"" href=""Page3"">3</a>",
                result.ToString());
        }
        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            // Организация (arrange)
            Mock<IArtGalleryRepository> mock = new Mock<IArtGalleryRepository>();
            mock.Setup(m => m.ArtGallerys).Returns(new List<ArtGallery>
            {
                 new ArtGallery { Articl_Id = 1, Name = "Картина1"},
                 new ArtGallery { Articl_Id = 2, Name = "Картина2"},
                  new ArtGallery { Articl_Id = 3, Name = "Картина3"},
                   new ArtGallery { Articl_Id = 4, Name = "Картина4"},
                    new ArtGallery { Articl_Id = 5, Name = "Картина5"},
            });
            ArtGalleryController controller = new ArtGalleryController(mock.Object);
            controller.pageSize = 3;

            // Act
            ArtGallerysListViewModel result
                = (ArtGallerysListViewModel)controller.List(null, 2).Model;

            // Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }
        [TestMethod]
        public void Can_Filter_ArtGallery()
        {
            // Организация (arrange)
            Mock<IArtGalleryRepository> mock = new Mock<IArtGalleryRepository>();
            mock.Setup(m => m.ArtGallerys).Returns(new List<ArtGallery>
            {
                 new ArtGallery { Articl_Id = 1, Name = "Картина1", Category = "Категория1"},
                 new ArtGallery { Articl_Id = 2, Name = "Картина2", Category = "Категория2"},
                 new ArtGallery { Articl_Id = 3, Name = "Картина3", Category = "Категория1"},
                 new ArtGallery { Articl_Id = 4, Name = "Картина4", Category = "Категория2"},
                 new ArtGallery { Articl_Id = 5, Name = "Картина5", Category = "Категория3"},
            });
            ArtGalleryController controller = new ArtGalleryController(mock.Object);
            controller.pageSize = 3;
            // Action
            List<ArtGallery> result = ((ArtGallerysListViewModel)controller.List("Категория2", 1).Model)
                .ArtGallerys.ToList();

            // Assert
            Assert.AreEqual(result.Count(), 2);
            Assert.IsTrue(result[0].Name == "Картина2" && result[0].Category == "Категория2");
            Assert.IsTrue(result[1].Name == "Картина4" && result[1].Category == "Категория2");

        }
        [TestMethod]
        public void Can_Create_Categories()
        {
            Mock<IArtGalleryRepository> mock = new Mock<IArtGalleryRepository>();
            mock.Setup(m => m.ArtGallerys).Returns(new List<ArtGallery>
            {
                 new ArtGallery { Articl_Id = 1, Name = "Картина1", Category = "Категория1"},
                 new ArtGallery { Articl_Id = 2, Name = "Картина2", Category = "Категория1"},
                 new ArtGallery { Articl_Id = 3, Name = "Картина3", Category = "Подкат"},
                 new ArtGallery { Articl_Id = 4, Name = "Картина4", Category = "арт"},

            });

            // Организация - создание контроллера
            NavController target = new NavController(mock.Object);

            // Действие - получение набора категорий
            List<string> results = ((IEnumerable<string>)target.Menu().Model).ToList();

            // Утверждение
            Assert.AreEqual(results.Count(), 3);
            Assert.AreEqual(results[0], "арт");
            Assert.AreEqual(results[1], "Категория1");
            Assert.AreEqual(results[2], "Подкат");

        }
        [TestMethod]
        public void Indicates_Selected_Category()
        {
            Mock<IArtGalleryRepository> mock = new Mock<IArtGalleryRepository>();
            mock.Setup(m => m.ArtGallerys).Returns(new List<ArtGallery>
            {
                 new ArtGallery { Articl_Id = 1, Name = "Картина1", Category = "Категория1"},
                 new ArtGallery { Articl_Id = 2, Name = "Картина2", Category = "Категория2"},

            });

            // Организация - создание контроллера
            NavController target = new NavController(mock.Object);

            // Организация - определение выбранной категории
            string categoryToSelect = "Категория2";

            // Действие
            string result = target.Menu(categoryToSelect).ViewBag.SelectedCategory;

            // Утверждение
            Assert.AreEqual(categoryToSelect, result);
        }
        [TestMethod]
        public void Generate_Category_Specific_Game_Count()
        {
            /// Организация (arrange)

            Mock<IArtGalleryRepository> mock = new Mock<IArtGalleryRepository>();
            mock.Setup(m => m.ArtGallerys).Returns(new List<ArtGallery>
            {
                 new ArtGallery { Articl_Id = 1, Name = "Картина1", Category = "Категория1"},
                 new ArtGallery { Articl_Id = 2, Name = "Картина2", Category = "Категория2"},
                 new ArtGallery { Articl_Id = 3, Name = "Картина3", Category = "Категория3"},
                 new ArtGallery { Articl_Id = 4, Name = "Картина4", Category = "Категория4"},
                 new ArtGallery { Articl_Id = 5, Name = "Картина5", Category = "Категория5"},
            });
            ArtGalleryController controller = new ArtGalleryController(mock.Object);
            controller.pageSize = 3;

            // Действие - тестирование счетчиков товаров для различных категорий
            int res1 = ((ArtGallerysListViewModel)controller.List("Категория1").Model).PagingInfo.TotalItems;
            int res2 = ((ArtGallerysListViewModel)controller.List("Категория2").Model).PagingInfo.TotalItems;
            int res3 = ((ArtGallerysListViewModel)controller.List("Категория3").Model).PagingInfo.TotalItems;
            int resAll = ((ArtGallerysListViewModel)controller.List(null).Model).PagingInfo.TotalItems;

            // Утверждение
            Assert.AreEqual(res1, 1);
            Assert.AreEqual(res2, 1);
            Assert.AreEqual(res3, 1);
            Assert.AreEqual(resAll, 5);
        }
    }
}
