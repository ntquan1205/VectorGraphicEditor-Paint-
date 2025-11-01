using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace VectorGraphicEditor__Paint_
{
    public class FileIOService
    {
        private readonly string _filePath;

        public FileIOService(string filePath)
        {
            _filePath = filePath;
        }

        public List<Shape> LoadData()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Shape>();
            }

            string json = File.ReadAllText(_filePath);

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented
            };

            return JsonConvert.DeserializeObject<List<Shape>>(json, settings);
        }

        public void SaveData(List<Shape> shapes)
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented
            };

            string json = JsonConvert.SerializeObject(shapes, settings);
            File.WriteAllText(_filePath, json);
        }
    }
}