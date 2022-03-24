using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{

    public class ImdbAPIResponse
    {
        public Results results { get; set; }
    }

    public class Results
    {
        public string id { get; set; }
        public ImdbRatingssummary ratingsSummary { get; set; }
        public ImdbEpisodes episodes { get; set; }
        public ImdbPrimaryimage primaryImage { get; set; }
        public ImdbTitletype titleType { get; set; }
        public ImdbGenres genres { get; set; }
        public Titletext titleText { get; set; }
        public Releasedate releaseDate { get; set; }
        public Runtime runtime { get; set; }
        public ImdbMeterranking meterRanking { get; set; }
        public Plot plot { get; set; }
    }

    public class ImdbRatingssummary
    {
        public float aggregateRating { get; set; }
        public int voteCount { get; set; }
        public string __typename { get; set; }
    }

    public class ImdbEpisodes
    {
        public Season[] seasons { get; set; }
        public Year[] years { get; set; }
        public Totalepisodes totalEpisodes { get; set; }
        public string __typename { get; set; }
    }


    public class Totalepisodes
    {
        public int total { get; set; }
        public string __typename { get; set; }
    }

    public class Season
    {
        public int number { get; set; }
        public string __typename { get; set; }
    }

    public class Year
    {
        public int year { get; set; }
        public string __typename { get; set; }
    }

    public class ImdbPrimaryimage
    {
        public string id { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string url { get; set; }
        public Caption caption { get; set; }
        public string __typename { get; set; }
    }

    public class Caption
    {
        public string plainText { get; set; }
        public string __typename { get; set; }
    }

    public class ImdbTitletype
    {
        public string text { get; set; }
        public string id { get; set; }
        public bool isSeries { get; set; }
        public bool isEpisode { get; set; }
        public string __typename { get; set; }
    }

    public class ImdbGenres
    {
        public ImdbGenre[] genres { get; set; }
        public string __typename { get; set; }
    }

    public class ImdbGenre
    {
        public string text { get; set; }
        public string id { get; set; }
        public string __typename { get; set; }
    }

    public class Titletext
    {
        public string text { get; set; }
        public string __typename { get; set; }
    }

    public class Releasedate
    {
        public int day { get; set; }
        public int month { get; set; }
        public int year { get; set; }
        public string __typename { get; set; }
    }

    public class Runtime
    {
        public int seconds { get; set; }
        public string __typename { get; set; }
    }

    public class ImdbMeterranking
    {
        public int currentRank { get; set; }
        public Rankchange rankChange { get; set; }
        public string __typename { get; set; }
    }

    public class Rankchange
    {
        public string changeDirection { get; set; }
        public int difference { get; set; }
        public string __typename { get; set; }
    }

    public class Plot
    {
        public Plottext plotText { get; set; }
        public Language language { get; set; }
        public string __typename { get; set; }
    }

    public class Plottext
    {
        public string plainText { get; set; }
        public string __typename { get; set; }
    }

    public class Language
    {
        public string id { get; set; }
        public string __typename { get; set; }
    }


}
