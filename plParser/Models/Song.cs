using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace plParser.Models
{
    public class Song
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        //[DataType(DataType.Time)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{HH:mm}")]
        //public DateTime? Duration { get; set; }
        public TimeSpan? Duration { get; set; }
        public string Genre { get; set; }

        public override string ToString () {
            return Name+" "+Artist+" "+Duration;
        }    
  
    }
}