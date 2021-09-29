using LinenAndBird.Controllers;
using LinenAndBird.DataAccess;
using LinenAndBird.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Xunit;

namespace LinenAndBird.Tests
{
    public class HatsControllerTests
    {
        [Fact]
        public void requesting_all_hats_returns_all_hats()
        {
            // Arrange
            var controller = new HatsController(new FakeHatRepository());

            // Act
            var result = controller.GetAllHats();
            var resultObj = result as OkObjectResult;

            // Assert
            
            Assert.NotNull(resultObj);
            Assert.True(resultObj is OkObjectResult);
            Assert.IsType<List<Hat>>(resultObj.Value);
            var model = Assert.IsAssignableFrom<List<Hat>>(resultObj.Value);
            Assert.Equal(StatusCodes.Status200OK, resultObj.StatusCode);
            Assert.Equal(3, model.Count);


        }
    }
    public class FakeHatRepository : IHatRepository
    {
        public void Add(Hat newHat)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Hat> GetAll()
        {
           var list = new List<Hat> { new Hat(), new Hat(), new Hat() };
            return (IEnumerable<Hat>)list;
        }

        public Hat GetById(Guid hatId)
        {
            throw new NotImplementedException();
        }

        public System.Collections.Generic.IEnumerable<Hat> GetByStyle(HatStyle style)
        {
            throw new NotImplementedException();
        }
    }
}
