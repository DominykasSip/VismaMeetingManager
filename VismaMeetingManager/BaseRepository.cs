using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace VismaMeetingManager
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
    {

        private readonly string _pathToFile;

        public BaseRepository(string pathToFile)
        {
            _pathToFile = pathToFile;
        }

        public void Add(T entity)
        {
            List<T> all = GetAll().ToList();
            all.Add(entity);
            WriteAllToFile(all);
        }

        public void Delete(string id)
        {
            List<T> all = GetAll().ToList();
            all.Remove(all.Find(x => x.Id == id));
            WriteAllToFile(all);
        }

        public IEnumerable<T> GetAll()
        {
            string jsonAsString = System.IO.File.ReadAllText(_pathToFile);
            List<T> all = new List<T>();
            try
            {
                all = JsonSerializer.Deserialize<List<T>>(jsonAsString);
            }
            catch (JsonException e)
            {

            }
            return all;
        }

        public T GetById(string id)
        {
            IEnumerable<T> all = GetAll();
            T entity = all.FirstOrDefault(x => x.Id == id);
            return entity;
        }

        public void Update(T entity)
        {
            List<T> all = GetAll().ToList();

            int indexOfEntity = all.FindIndex(0, t => t.Id == entity.Id);
            all[indexOfEntity] = entity;

            WriteAllToFile(all);
        }

        private void WriteAllToFile(IEnumerable<T> all)
        {
            string jsonString = JsonSerializer.Serialize(all);
            System.IO.File.WriteAllText(_pathToFile, jsonString);
        }
    }
}
