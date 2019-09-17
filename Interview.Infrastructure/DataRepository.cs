using System;
using System.Collections.Generic;
using System.Linq;
using Interview.Domain;
using System.IO;
using Newtonsoft.Json;

namespace Interview.Infrastructure
{
    public class FileSystemDataRepository : IDataRepository
    {
        private static readonly object _syncObject = new object();

        private const string DataFilePath = @"path\goes\here\data.json";

        public void Delete(Guid id)
        {
            var newColletion = GetAll().Where(x => x.Id != id).ToList();

            lock (_syncObject)
            {
                File.WriteAllText(DataFilePath, JsonConvert.SerializeObject(newColletion, Formatting.Indented));
            }
        }

        public IEnumerable<Data> GetAll()
        {
            var result = new List<Data>();

            string json;

            lock (_syncObject)
            {
                json = File.ReadAllText(DataFilePath);
            }

            JsonConvert.PopulateObject(json, result);

            return result;
        }

        public Data GetOne(Guid id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public void Add(Data data)
        {
            var newCollection = GetAll().ToList();
            newCollection.Add(data);

            lock (_syncObject)
            {
                File.WriteAllText(DataFilePath, JsonConvert.SerializeObject(newCollection, Formatting.Indented));
            }
        }
    }
}
