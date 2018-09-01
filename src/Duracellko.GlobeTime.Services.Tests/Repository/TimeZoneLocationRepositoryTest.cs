using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Duracellko.GlobeTime.Domain.Repository;
using Duracellko.GlobeTime.Services.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Duracellko.GlobeTime.Services.Tests.Repository
{
    [TestClass]
    public class TimeZoneLocationRepositoryTest
    {
        [TestMethod]
        public async Task GetTimeZones_FileExists_LoadsTimeZones()
        {
            var repositoryFiles = new Mock<IRepositoryFiles>();
            var timeZonesStream = CreateTimeZonesStream();
            repositoryFiles.Setup(o => o.GetTimeZonesDataStream()).Returns(timeZonesStream);
            var target = new TimeZoneLocationRepository(repositoryFiles.Object);

            var result = await target.GetTimeZones();

            var resultList = result.ToList();
            Assert.AreEqual(39, resultList.Count);

            var timeZone = resultList[0];
            Assert.AreEqual(-1, timeZone.Id);
            Assert.AreEqual(0, timeZone.Location.Latitude);
            Assert.AreEqual(0, timeZone.Location.Longitude);
            Assert.AreEqual(0, timeZone.TimeOffset);
            Assert.AreEqual(1024, timeZone.Boundaries.Count);
            Assert.AreEqual(1, timeZone.Boundaries[0].Count());
            Assert.AreEqual(-0.130899698f, timeZone.Boundaries[0].First().Start);
            Assert.AreEqual(0.130899698f, timeZone.Boundaries[0].First().End);
            Assert.AreEqual(1, timeZone.Boundaries[200].Count());
            Assert.AreEqual(-0.148337349f, timeZone.Boundaries[200].First().Start);
            Assert.AreEqual(0.130899698f, timeZone.Boundaries[200].First().End);
            Assert.AreEqual(1, timeZone.Boundaries[512].Count());
            Assert.AreEqual(-0.130899698f, timeZone.Boundaries[512].First().Start);
            Assert.AreEqual(0.130899698f, timeZone.Boundaries[512].First().End);
            Assert.AreEqual(1, timeZone.Boundaries[654].Count());
            Assert.AreEqual(-0.130899698f, timeZone.Boundaries[654].First().Start);
            Assert.AreEqual(0.130899698f, timeZone.Boundaries[654].First().End);
            Assert.AreEqual(1, timeZone.Boundaries[1023].Count());
            Assert.AreEqual(-0.130899698f, timeZone.Boundaries[1023].First().Start);
            Assert.AreEqual(0.130899698f, timeZone.Boundaries[1023].First().End);

            timeZone = resultList[2];
            Assert.AreEqual(-3, timeZone.Id);
            Assert.AreEqual(0, timeZone.Location.Latitude);
            Assert.AreEqual(0.52359877559829882, timeZone.Location.Longitude);
            Assert.AreEqual(2, timeZone.TimeOffset);
            Assert.AreEqual(1024, timeZone.Boundaries.Count);
            Assert.AreEqual(1, timeZone.Boundaries[0].Count());
            Assert.AreEqual(0.392699093f, timeZone.Boundaries[0].First().Start);
            Assert.AreEqual(0.654498518f, timeZone.Boundaries[0].First().End);
            Assert.AreEqual(1, timeZone.Boundaries[222].Count());
            Assert.AreEqual(0.418863177f, timeZone.Boundaries[222].First().Start);
            Assert.AreEqual(0.617620528f, timeZone.Boundaries[222].First().End);
            Assert.AreEqual(1, timeZone.Boundaries[511].Count());
            Assert.AreEqual(0.404951066f, timeZone.Boundaries[511].First().Start);
            Assert.AreEqual(0.518901587f, timeZone.Boundaries[511].First().End);
            Assert.AreEqual(1, timeZone.Boundaries[700].Count());
            Assert.AreEqual(0.314739674f, timeZone.Boundaries[700].First().Start);
            Assert.AreEqual(0.654498518f, timeZone.Boundaries[700].First().End);
            Assert.AreEqual(1, timeZone.Boundaries[1020].Count());
            Assert.AreEqual(0.392699093f, timeZone.Boundaries[1020].First().Start);
            Assert.AreEqual(0.654498518f, timeZone.Boundaries[1020].First().End);

            timeZone = resultList[22];
            Assert.AreEqual(-23, timeZone.Id);
            Assert.AreEqual(0, timeZone.Location.Latitude);
            Assert.AreEqual(-0.78539816339744828, timeZone.Location.Longitude);
            Assert.AreEqual(-3, timeZone.TimeOffset);
            Assert.AreEqual(1024, timeZone.Boundaries.Count);
            Assert.AreEqual(1, timeZone.Boundaries[0].Count());
            Assert.AreEqual(-0.916297913f, timeZone.Boundaries[0].First().Start);
            Assert.AreEqual(-0.654498518f, timeZone.Boundaries[0].First().End);
            Assert.AreEqual(1, timeZone.Boundaries[300].Count());
            Assert.AreEqual(-0.916297913f, timeZone.Boundaries[300].First().Start);
            Assert.AreEqual(-0.654498518f, timeZone.Boundaries[300].First().End);
            Assert.AreEqual(1, timeZone.Boundaries[500].Count());
            Assert.AreEqual(-0.98953402f, timeZone.Boundaries[500].First().Start);
            Assert.AreEqual(-0.654498518f, timeZone.Boundaries[500].First().End);
            Assert.AreEqual(3, timeZone.Boundaries[708].Count());
            Assert.AreEqual(-1.22496021f, timeZone.Boundaries[708].First().Start);
            Assert.AreEqual(-1.01975918f, timeZone.Boundaries[708].First().End);
            Assert.AreEqual(-0.994668722f, timeZone.Boundaries[708].Skip(1).First().Start);
            Assert.AreEqual(-0.943626821f, timeZone.Boundaries[708].Skip(1).First().End);
            Assert.AreEqual(-0.916297913f, timeZone.Boundaries[708].Skip(2).First().Start);
            Assert.AreEqual(-0.654498518f, timeZone.Boundaries[708].Skip(2).First().End);
            Assert.AreEqual(1, timeZone.Boundaries[1020].Count());
            Assert.AreEqual(-0.916297913f, timeZone.Boundaries[1020].First().Start);
            Assert.AreEqual(-0.654498518f, timeZone.Boundaries[1020].First().End);

            timeZone = resultList[38];
            Assert.AreEqual(-39, timeZone.Id);
            Assert.AreEqual(0, timeZone.Location.Latitude);
            Assert.AreEqual(1.0471975511965976, timeZone.Location.Longitude);
            Assert.AreEqual(4.5f, timeZone.TimeOffset);
            Assert.AreEqual(1024, timeZone.Boundaries.Count);
            Assert.IsNull(timeZone.Boundaries[0]);
            Assert.AreEqual(2, timeZone.Boundaries[300].Count());
            Assert.AreEqual(1.13426638f, timeZone.Boundaries[300].First().Start);
            Assert.AreEqual(1.24725294f, timeZone.Boundaries[300].First().End);
            Assert.AreEqual(1.27048576f, timeZone.Boundaries[300].Skip(1).First().Start);
            Assert.AreEqual(1.29996777f, timeZone.Boundaries[300].Skip(1).First().End);
            Assert.IsNull(timeZone.Boundaries[500]);
            Assert.IsNull(timeZone.Boundaries[800]);
            Assert.IsNull(timeZone.Boundaries[1020]);
        }

        private static Stream CreateTimeZonesStream()
        {
            return typeof(TimeZoneLocationRepositoryTest).Assembly.GetManifestResourceStream("Duracellko.GlobeTime.Services.Tests.Repository.TimeZones.dat");
        }
    }
}
