using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace cd_db
{
    class CD
    {
        [PrimaryKey]
        public int ID { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }

        public override string ToString()
        {
            return "Artist: " + Artist + " Album: " + Album + " Year: " + Year;
        }
    }
}
