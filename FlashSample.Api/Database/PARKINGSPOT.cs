using System;
using SQLite;

namespace FlashSample.Api.Database
{
    public class PARKINGSPOT
    {
        [AutoIncrement]
        [PrimaryKey]
        public int ID { get; set; }

        public int PARKINGLOCATIONID { get; set; }

        public int SPOTNUMBER { get; set; }

        public bool OCCUPIED { get; set; }
    }
}
