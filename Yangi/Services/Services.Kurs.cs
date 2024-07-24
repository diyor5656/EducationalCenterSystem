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
        public static string GetKursPAth()
        {
            string currentPath = Directory.GetCurrentDirectory();
            currentPath += @"\kurslar.json";
            return currentPath;
        }

        public void ClearKFile()
        {
            File.WriteAllText(GetKursPAth(), string.Empty);
            Console.WriteLine("Malumotlar tozalandi! ");
        }

        public void SaveKurs(List<Kurs> kurss)
        {
            string serialized = JsonSerializer.Serialize(kurss);
            File.WriteAllText(GetKursPAth(), serialized);
        }

        public void AddKurs()
        {
            List<Kurs> kurss = GetKurs();

            Console.Write("Kurs nomini Kiriting: ");
            string kursName = Console.ReadLine();

            int newId = kurss.Count > 0 ? kurss.Max(t => t.Id) + 1 : 1;

            Kurs newKurs = new Kurs
            {
                Id = newId,
                Name = kursName,
            };

            kurss.Add(newKurs);
            SaveKurs(kurss);

            Console.WriteLine("Kurs muvaffaqiyatli qushildi.");
        }

        public List<Kurs> GetKurs()
        {
            if (!File.Exists(GetKursPAth()))
            {
                Console.WriteLine("Hozircha Kurslar yo'q");
            }
            if (!File.Exists(GetKursPAth()))
            {
                return new List<Kurs>();
            }

            string jsonFromFile = File.ReadAllText(GetKursPAth());
            var kurss = string.IsNullOrEmpty(jsonFromFile) ? new List<Kurs>() : JsonSerializer.Deserialize<List<Kurs>>(jsonFromFile);

            Console.WriteLine("Kurslar Jadvali:");
            foreach (var kurs in kurss)
            {
                Console.WriteLine($"Id: {kurs.Id}, Name: {kurs.Name}");
            }

            return kurss;
        }

        public void DeleteKurs()
        {
            List<Kurs> kurss = GetKurs();

            Console.Write("O'chirish uchun Kurs Id sini kiriting: ");
            int KursId;
            while (!int.TryParse(Console.ReadLine(), out KursId))
            {
                Console.Write("Iltimos Id ni kiriting: ");
            }

            Kurs kursToDelete = kurss.FirstOrDefault(t => t.Id == KursId);
            if (kursToDelete == null)
            {
                Console.WriteLine("Kurs topilmadi.");
                return;
            }

            kurss.Remove(kursToDelete);
            SaveKurs(kurss);

            Console.WriteLine("Kurs muvaffaqiyatli o'chirildi.");
        }

        public void UpdateKurs()
        {
            List<Kurs> kurss = GetKurs();

            Console.Write("Yangilash uchun Kursning Id sini kiriting : ");
            int KursId;
            while (!int.TryParse(Console.ReadLine(), out KursId))
            {
                Console.Write("Iltimos Id ni kiriting: ");
            }

            Kurs kursToUpdate = kurss.FirstOrDefault(t => t.Id == KursId);
            if (kursToUpdate == null)
            {
                Console.WriteLine("Kurs topilmadi!");
                return;
            }

            Console.Write("Yangi nom kiriting: ");
            string newName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName))
            {
                kursToUpdate.Name = newName;
            }

            SaveKurs(kurss);
            Console.WriteLine("Kurs muvaffaqiyatli yangilandi! ");
        }
    }
}
