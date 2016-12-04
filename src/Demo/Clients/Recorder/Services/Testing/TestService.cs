using System.IO;
using Common.Testing;
using Newtonsoft.Json;
using Saturn72.Extensions;

namespace Recorder.Services.Testing
{
    public class TestService : ITestService
    {
        public void Create(TestModel testModel)
        {
            //TODO: implement using repository pattern
            Guard.NotNull(testModel);

            var json = JsonConvert.SerializeObject(testModel);

            //save
            var path = Path.GetTempFileName();
            //add json ext
            path = path.Substring(0, path.LastIndexOf(".")) + ".json";

            //add json ext
            File.WriteAllText(path, json);
        }
    }
}