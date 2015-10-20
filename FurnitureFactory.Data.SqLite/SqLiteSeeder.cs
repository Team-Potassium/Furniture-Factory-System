namespace FurnitureFactory.Data.SqLite
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class SqLiteSeeder
    {
        private static Random random = new Random();

        public static void Seed(SqLiteDbContext sqlLiteContex)
        {
            var clients = new List<string>();

            using (StreamReader reader = new StreamReader("../../../Resources/SqLiteClients.txt"))
            {
                string line = reader.ReadLine();

                while (line != null)
                {
                    clients.Add(line.Trim());
                    line = reader.ReadLine();
                }
            }
            
            foreach (var client in clients)
            {
                sqlLiteContex.Clients.Add(new Client
                {
                    Name = client,
                    Address = "Sofia, Alexander Malinov blvd " + random.Next(10, 100),
                    Email = client.ToLower() + "@telerik.com",
                    Iban = "BG" + random.Next(10000, 99999) + random.Next(10000, 99999)
                });
                Console.Write(".");
            }

            sqlLiteContex.SaveChanges();
        }
    }
}
