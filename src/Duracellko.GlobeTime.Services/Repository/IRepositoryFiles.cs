using System.IO;

namespace Duracellko.GlobeTime.Domain.Repository
{
    public interface IRepositoryFiles
    {
        string SelectedCitiesFile { get; }

        Stream GetCitiesXmlStream();

        Stream GetTimeZonesDataStream();

        string GetTextureUri(string imageName);

        Stream GetTextureStream(string imageName);
    }
}
