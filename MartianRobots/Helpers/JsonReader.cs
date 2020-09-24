using MartianRobots.Classes;
using MartianRobots.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MartianRobots.Helpers
{
    public class JsonReader<T> where T:class
    {
        private readonly string _path;

        public JsonReader(string path)
        {
            _path = path;
        }

        public IEnumerable<T> ReadListObjects()
        {
            using (StreamReader jsonStream = File.OpenText(_path))
            {
                var json = jsonStream.ReadToEnd();
                IEnumerable<T> des = JsonConvert.DeserializeObject<IEnumerable<T>>(json);

                return des;
            }
        }

        public T ReadObjects(string path)
        {
            using (StreamReader jsonStream = File.OpenText(path))
            {
                var json = jsonStream.ReadToEnd();
                T des = JsonConvert.DeserializeObject<T>(json);

                return des;
            }
        }
    }
}
