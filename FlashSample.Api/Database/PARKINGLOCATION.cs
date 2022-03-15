using SQLite;

namespace FlashSample.Api.Database
{
    public class PARKINGLOCATION
    {
        [AutoIncrement]
        [PrimaryKey]
        public int ID { get; set; }

        public string NAME { get; set; }

        public int MAXCAPACITY { get; set; }
    }
}
