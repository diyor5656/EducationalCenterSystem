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
        public static string GetArizaPAth()
        {
            string currentPath = Directory.GetCurrentDirectory();
            currentPath += @"\arizalar.json";
            return currentPath;
        }

        public void ClearArFile()
        {
            File.WriteAllText(GetKursPAth(), string.Empty);
            Console.WriteLine("Malumotlar tozalandi! ");
        }

        public void SaveAriza(List<Ariza> arizas)
        {
            string serialized = JsonSerializer.Serialize(arizas);
            File.WriteAllText(GetArizaPAth(), serialized);
        }

        public void AddAriza()
        {
            List<Ariza> arizas = GetAriza();

            Console.Write("Ariza kiriting: ");
            Console.Write("Arizada Ism Familyangiz ,Qaysi kursda Uqimoqchiligingiz");
            Console.Write("yoki sizni qiziqtirgan savol bilan raqamingizni qoldiring!");
            Console.Write("Bizning o'zimiz siz bilan bog'lanamiz!");
            string arizaName = Console.ReadLine();

            int newId = arizas.Count > 0 ? arizas.Max(t => t.Id) + 1 : 1;

            Ariza newAriza = new Ariza
            {
                Id = newId,
                Name = arizaName,
            };

            arizas.Add(newAriza);
            SaveAriza(arizas);

            Console.WriteLine("Ariza muvaffaqiyatli joylandi Javobimizni kuting!");
        }

        public List<Ariza> GetAriza()
        {
            if (!File.Exists(GetArizaPAth()))
            {
                Console.WriteLine("Arizalar yo'q");
            }
            if (!File.Exists(GetArizaPAth()))
            {
                return new List<Ariza>();
            }

            string jsonFromFile = File.ReadAllText(GetArizaPAth());
            var arizas = string.IsNullOrEmpty(jsonFromFile) ? new List<Ariza>() : JsonSerializer.Deserialize<List<Ariza>>(jsonFromFile);

            Console.WriteLine("Arizalar: ");
            foreach (var ariza in arizas)
            {
                Console.WriteLine($"Id: {ariza.Id}, Ariza: {ariza.Name}");
            }

            return arizas;
        }

        public void DeleteAriza()
        {
            List<Ariza> arizas = GetAriza();

            Console.Write("O'chirish uchun Ariza Id sini kiriting: ");
            int ArizaId;
            while (!int.TryParse(Console.ReadLine(), out ArizaId))
            {
                Console.Write("Iltimos Id ni kiriting: ");
            }

            Ariza arizaToDelete = arizas.FirstOrDefault(t => t.Id == ArizaId);
            if (arizaToDelete == null)
            {
                Console.WriteLine("Ariza topilmadi.");
                return;
            }

            arizas.Remove(arizaToDelete);
            SaveAriza(arizas);

            Console.WriteLine("Ariza muvaffaqiyatli o'chirildi.");
        }

        public void UpdateAriza()
        {
            List<Ariza> arizas = GetAriza();

            Console.Write("Yangilash uchun Ariza Id sini kiriting : ");
            int ArizaId;
            while (!int.TryParse(Console.ReadLine(), out ArizaId))
            {
                Console.Write("Iltimos Id ni kiriting: ");
            }

            Ariza arizaToUpdate = arizas.FirstOrDefault(t => t.Id == ArizaId);
            if (arizaToUpdate == null)
            {
                Console.WriteLine("Ariza topilmadi!");
                return;
            }

            Console.Write("Arizani Yangilang: ");
            string newName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName))
            {
                arizaToUpdate.Name = newName;
            }

            SaveAriza(arizas);
            Console.WriteLine("Ariza muvaffaqiyatli yangilandi! ");
        }
    }
}
