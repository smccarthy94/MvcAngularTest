using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgrammingTest.BusinessLogic;
using ProgrammingTest.DataObjects.DataModels;
using ProgrammingTest.WebClient.Controllers;

namespace ProgrammingTest.WebClient.Tests.Controllers
{
    [TestClass]
    public class MembersControllerTest
    {
        private void _ensureDummyData()
        {
            var _mm = new MemberManager();

            var list = _mm.List();

            if (list.Count > 0)
            {
                for (var i = list.Count - 1; i >= 0; i--)
                {
                    _mm.Delete(list[i].Id);
                }
            }

            list = _mm.List();

            if (list.Count > 0)
            {
                Assert.Fail("Dummy data cleanse failed. Could not clear data.");
            }

            _mm.Create(new Member()
            {
                CreatedDate     = DateTime.Now,
                Email           = "seanmccarthy1994@gmail.com",
                FirstName       = "Sean",
                LastName        = "McCarthy",
                Id              = 998,
                IsAdministrator = true,
                Phone           = "123456789",
                WebSite         = "http://www.google.com/"
            });

            _mm.Create(new Member()
            {
                CreatedDate     = DateTime.Now,
                Email           = "test@gmail.com",
                FirstName       = "Bob",
                LastName        = "Fredson",
                Id              = 1965,
                IsAdministrator = false,
                Phone           = "9865",
                WebSite         = "http://www.google.com.au/"
            });
        }

        [TestMethod]
        public void Get()
        {
            // Arrange
            MembersController controller = new MembersController();
            _ensureDummyData();

            // Act
            IHttpActionResult result = controller.Get();
            // should return 2 results.
            // although we expect the ID to be clensed by the data layer

            // Assert
            var resolvedResult = result as OkNegotiatedContentResult<List<Member>>;
            Assert.IsNotNull(resolvedResult.Content);
            Assert.AreEqual(2, resolvedResult.Content.Count());

            // cleansed Id's
            Assert.AreEqual(0, resolvedResult.Content.ElementAt(0).Id);
            Assert.AreEqual(1, resolvedResult.Content.ElementAt(1).Id);

            // user data as we expect it
            Assert.AreEqual("seanmccarthy1994@gmail.com", resolvedResult.Content.ElementAt(0).Email);
            Assert.AreEqual("test@gmail.com",             resolvedResult.Content.ElementAt(1).Email);
        }

        [TestMethod]
        public void GetById()
        {
            // Arrange
            MembersController controller = new MembersController();
            _ensureDummyData();

            // Act
            IHttpActionResult result = controller.Get(1);
            // should return one result that matches above,

            // Assert
            var resolvedResult = result as OkNegotiatedContentResult<Member>;

            // user data as we expect it
            Assert.IsNotNull(resolvedResult.Content);
            Assert.AreEqual("test@gmail.com", resolvedResult.Content.Email);
        }

        [TestMethod]
        public void Post()
        {
            // Arrange
            MembersController controller = new MembersController();
            _ensureDummyData();

            // Act
            controller.Post(new Member()
            {
                Email           = "test2@gmail.com",
                FirstName       = "Test",
                LastName        = "Post",
                IsAdministrator = false,
                Phone           = "1234",
                WebSite         = "http://site.com/"
            });

            IHttpActionResult result = controller.Get();
            // should return 3 results.
            // although we expect the ID to be clensed by the data layer

            // Assert
            var resolvedResult = result as OkNegotiatedContentResult<List<Member>>;
            Assert.IsNotNull(resolvedResult.Content);
            Assert.AreEqual(3, resolvedResult.Content.Count());

            // user data as we expect it
            Assert.IsNotNull(resolvedResult.Content);
            Assert.AreEqual(2, resolvedResult.Content.ElementAt(2).Id);
            Assert.AreEqual("test2@gmail.com", resolvedResult.Content.ElementAt(2).Email);
        }

        [TestMethod]
        public void Put()
        {
            // Arrange
            MembersController controller = new MembersController();
            _ensureDummyData();

            // Act
            controller.Put(0, new Member()
            {
                Email           = "test34@gmail.com",
                FirstName       = "Test",
                LastName        = "Put",
                IsAdministrator = false,
                Phone           = "1234",
                WebSite         = "http://site.com/"
            });

            IHttpActionResult result = controller.Get();
            // should return 2 results.

            // Assert
            var resolvedResult = result as OkNegotiatedContentResult<List<Member>>;
            Assert.IsNotNull(resolvedResult.Content);
            Assert.AreEqual(2, resolvedResult.Content.Count());

            // user data as we expect it
            Assert.IsNotNull(resolvedResult.Content);
            Assert.AreEqual(0, resolvedResult.Content.ElementAt(0).Id);
            Assert.AreEqual("test34@gmail.com", resolvedResult.Content.ElementAt(0).Email);
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            MembersController controller = new MembersController();
            _ensureDummyData();

            // Act
            controller.Delete(0);
            IHttpActionResult result = controller.Get();
            // should return 1 result.

            // Assert
            var resolvedResult = result as OkNegotiatedContentResult<List<Member>>;
            Assert.IsNotNull(resolvedResult.Content);
            Assert.AreEqual(1, resolvedResult.Content.Count());

            // user data as we expect it
            Assert.IsNotNull(resolvedResult.Content);
            Assert.AreEqual(1, resolvedResult.Content.ElementAt(0).Id);
            Assert.AreEqual("test@gmail.com", resolvedResult.Content.ElementAt(0).Email);
        }
    }
}
