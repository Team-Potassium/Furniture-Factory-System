namespace FurnitureFactory.Data.MongoDb
{
    using System.Collections.Generic;

    public class MongoDataRaw
    {
        #region Series list
        private readonly List<string> series = new List<string>()
        {
            // Scandinavian names
            "Alvis",
            "Balder",
            "Borghild",
            "Embla",
            "Erna",
            "Frea",
            "Frigg",
            "Gandalf",
            "Groa",
            "Gudrun",
            "Jarl",
            "Sigurd",
            "Sindri",
            "Skuld",
            "Verdandi",
            "Vidar",
            "Yngvi",

            // Indian Cherooke names
            "Adsila",
            "Amadahy",
            "Awenasa",
            "Awinita",
            "Ayita",
            "Ahyoka",
            "Ama",
            "Galilahi",
            "Leotie",
            "Usdi",
            "Immookalee",
            "Salali",
            "Sequoia",
            "Inola",
            "Knasgowa",
            "Tsula"
         };
        #endregion

        #region Types list
        private readonly List<string> bedroomTypes = new List<string>()
        {
            "Single bed",
            "Double bed"
        };

        private readonly List<string> livingroomTypes = new List<string>()
        {
            "Two-seat sofa",
            "Three-seat sofa",
            "Corner sofa",
            "Armchair",
            "Pouffe",
            "TV bench",
            "Wall shelf",
            "Bookcase",
            "Bookcase with glass doors",
            "Coffee table"
        };
        #endregion

        private readonly List<string> rooms = new List<string>()
        {
            "Bedroom",
            "Living room"
        };

        public IList<string> GetSeriesNames()
        {
            return this.series;
        }

        public List<string> GetBedroomTypeNames()
        {
            return this.bedroomTypes;
        }

        public IList<string> GetLivingRoomTypeNames()
        {
            return this.livingroomTypes;
        }

        public IList<string> GetAllTypeNames()
        {
            var allTypes = new List<string>();

            allTypes.AddRange(this.bedroomTypes);
            allTypes.AddRange(this.livingroomTypes);

            return allTypes;
        }

        public IList<string> GetRoomNames()
        {
            return this.rooms;
        }
    }
}
