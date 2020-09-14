using System;
using System.Collections.Generic;

namespace Item_Based_Collaborative_Filtering
{
    public class ViewRecommendation
    {
        //user that provides the rating
        public int Id { get; set; }

        //List of dictionary that constitutes of item and rating given by user
        public Dictionary<int, double> Item_Rating { get; set; }

    }

}