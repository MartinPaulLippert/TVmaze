using System;
using System.Collections.Generic;
using System.Text;

namespace TVmaze.Domain.Models
{
    //For the sake of simplicity all the models are in one file but should be in separate files
    public class Schedule
    {
        public string time { get; set; }
        public List<string> days { get; set; }
    }

    public class Rating
    {
        public double? average { get; set; }
    }

    public class Country
    {
        public string name { get; set; }
        public string code { get; set; }
        public string timezone { get; set; }
    }

    public class Network
    {
        public int id { get; set; }
        public string name { get; set; }
        public Country country { get; set; }
    }

    public class Externals
    {
        public int tvrage { get; set; }
        public int thetvdb { get; set; }
        public string imdb { get; set; }
    }

    public class Image
    {
        public string medium { get; set; }
        public string original { get; set; }
    }

    public class Self
    {
        public string href { get; set; }
    }

    public class Episode
    {
        public string href { get; set; }
    }

    public class Links
    {
        public Self self { get; set; }
        public Episode previousepisode { get; set; }
        public Episode nextepisode { get; set; }
    }

    public class Country2
    {
        public string name { get; set; }
        public string code { get; set; }
        public string timezone { get; set; }
    }

    public class Image2
    {
        public string medium { get; set; }
        public string original { get; set; }
    }

    public class Self2
    {
        public string href { get; set; }
    }

    public class Links2
    {
        public Self2 self { get; set; }
    }

    public class Person
    {
        public int id { get; set; }
        public string url { get; set; }
        public string name { get; set; }
        public Country2 country { get; set; }
        public string birthday { get; set; }
        public string deathday { get; set; }
        public string gender { get; set; }
        public Image2 image { get; set; }
        public Links2 _links { get; set; }
    }

    public class Image3
    {
        public string medium { get; set; }
        public string original { get; set; }
    }

    public class Self3
    {
        public string href { get; set; }
    }

    public class Links3
    {
        public Self3 self { get; set; }
    }

    public class Character
    {
        public int id { get; set; }
        public string url { get; set; }
        public string name { get; set; }
        public Image3 image { get; set; }
        public Links3 _links { get; set; }
    }

    public class Cast
    {
        public Person person { get; set; }
        public Character character { get; set; }
        public bool self { get; set; }
        public bool voice { get; set; }
    }

    public class Embedded
    {
        public List<Cast> cast { get; set; }
    }

    public class Show
    {
        public int id { get; set; }
        public string url { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string language { get; set; }
        public List<string> genres { get; set; }
        public string status { get; set; }
        public int runtime { get; set; }
        public string premiered { get; set; }
        public string officialSite { get; set; }
        public Schedule schedule { get; set; }
        public Rating rating { get; set; }
        public int weight { get; set; }
        public Network network { get; set; }
        public Network webChannel { get; set; }
        public Externals externals { get; set; }
        public Image image { get; set; }
        public string summary { get; set; }
        public int updated { get; set; }
        public Links _links { get; set; }
        public Embedded _embedded { get; set; }
    }
}



