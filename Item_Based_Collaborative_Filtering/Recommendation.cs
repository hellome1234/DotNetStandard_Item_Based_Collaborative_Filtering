using System; //system namespace
using System.Collections.Generic; //List<>
using System.Linq;
namespace Item_Based_Collaborative_Filtering
{
    public static class Recommendation<T> where T : ViewRecommendation //static to call the functions and works with a type<T>
    {
        private static List<T> TypeCollection;
        private static int TypeId;

        private static T Type;


        private static double SimilarityDistance(T type)
        {
            Double Sum = 0;
            foreach (KeyValuePair<int, double> dictionary in Type.Item_Rating)
            {
                if (type.Item_Rating.ContainsKey(dictionary.Key))
                {
                    Sum = Sum + Math.Pow(dictionary.Value - type.Item_Rating[dictionary.Key], 2.0);
                }
            }
            if (Sum != 0)
            {
                return 1.0 / (1.0 + Math.Sqrt(Sum));
            }

            return 0;
        }

        public static Dictionary<int, double> GetRecommendation(List<T> typeCollection, int typeId)
        {
            double similarityDistance;
            Dictionary<int, double> productTotal = new Dictionary<int, double>();
            Dictionary<int, double> similaritySum = new Dictionary<int, double>();
            Dictionary<int, double> ranks = new Dictionary<int, double>();

            TypeId = typeId; //assign the primary key to the TypeId field
            TypeCollection = typeCollection; //assign the List that consists of user with products and it's rating to the  TypeColection field

            Type = typeCollection.Find(delegate (T type) { return type.Id == TypeId; });  //fetch the user instance from the list through TypeId field


            foreach (var type in TypeCollection) //iterate the List of users
            {
                if (TypeId != type.Id) //makes sure that the user in collection is not the requested user we need to find the similarity with 
                {
                    similarityDistance = SimilarityDistance(type); //calls the simalirity distance function
                    if (similarityDistance > 0) //if the similarity between two user is greater than 0
                    {
                        foreach (KeyValuePair<int, double> dictionary in type.Item_Rating) //iterates through all the item that the user in the iteration have raterd
                        {
                            if (!Type.Item_Rating.ContainsKey(dictionary.Key))  //checks if the current user hasen't already rated the item
                            {
                                if (!productTotal.ContainsKey(dictionary.Key))  //checks if the key is already placed inside the total dictionary
                                {
                                    productTotal[dictionary.Key] = 0; //intializes the product total 
                                }

                                //adds the product's total till now with rating given by the user multiply it's similarity distance with the requested user we need to find similarity for
                                productTotal[dictionary.Key] += dictionary.Value * similarityDistance;

                                if (!similaritySum.ContainsKey(dictionary.Key))
                                {
                                    similaritySum[dictionary.Key] = 0;
                                }

                                similaritySum[dictionary.Key] += similarityDistance;
                            }
                        }
                    }
                }
            }

            foreach (KeyValuePair<int, double> total in productTotal)
            {
                ranks[total.Key] = total.Value / similaritySum[total.Key];
            }
            // ranks.OrderByDescending(Key); ....create the rank dictionary into sorted dictionary then it will be sorted without any worries
            ranks.OrderByDescending(x => x.Value);
            return ranks;
        }
    }


}
