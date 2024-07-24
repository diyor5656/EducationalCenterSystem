using Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Yangi.Services
{
    public partial class Services
    {
        public static string GetAboutPAth()
        {
            string currentPath = Directory.GetCurrentDirectory();
            currentPath += @"\malumotlar.json";
            return currentPath;
        }


        public void SaveAbout(List<About> abouts)
        {
            string serialized = JsonSerializer.Serialize(abouts);
            File.WriteAllText(GetAboutPAth(), serialized);
        }

        public void AddAbout()
        {
            List<About> abouts = GetAbout();

            if (abouts.Count > 0)
            {
                Console.WriteLine("Malumot allaqachon mavjud. Yangi ma'lumot qo'shilmaydi.");
                return;
            }

            Console.Write("Markaz haqida ma'lumot kiriting va kiritib bo'lgach 'Enter' tugmasini bosing: ");
            string aboutName = Console.ReadLine();

            int newId = 1; 

            About newabout = new About
            {
                Id = newId,
                Name = aboutName,
            };

            abouts.Add(newabout);
            SaveAbout(abouts);

            Console.WriteLine("Malumot muvaffaqiyatli qo'shildi.");
        }

        public List<About> GetAbout()
        {
            if (File.Exists(GetAboutPAth()))
            {
                Console.WriteLine("Malumotlar yo'q");
            }
            if (!File.Exists(GetAboutPAth()))
            {
                return new List<About>();
            }
            
           
           
                string jsonFromFile = File.ReadAllText(GetAboutPAth());
                var abouts = string.IsNullOrEmpty(jsonFromFile) ? new List<About>() : JsonSerializer.Deserialize<List<About>>(jsonFromFile);
                foreach (var about in abouts)
                {
                    Console.WriteLine($" Malumot: {about.Name}");
                }
                return abouts;
            
        }

        public void DeleteAbout()
        {
            List<About> abouts = GetAbout();

            if (abouts.Count == 0)
            {
                Console.WriteLine("Malumot topilmadi.");
                return;
            }

            abouts.Clear();
            SaveAbout(abouts);

            Console.WriteLine("Malumot muvaffaqiyatli o'chirildi.");
        }

        public void UpdateAbout()
        {
            List<About> abouts = GetAbout();

            if (abouts.Count == 0)
            {
                Console.WriteLine("Malumot topilmadi!");
                return;
            }

            About aboutToUpdate = abouts[0];

            Console.Write("Yangi malumot kiriting: ");
            string newName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName))
            {
                aboutToUpdate.Name = newName;
            }

            SaveAbout(abouts);
            Console.WriteLine("Malumot muvaffaqiyatli yangilandi! ");
        }
    }
}
