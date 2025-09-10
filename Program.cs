using System;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;
namespace lab1
{
    class Program
    {
        public class ListOfMarks{
            public struct NewDictionary
            {
                public string Key;
                public int Value;
            }
            public NewDictionary[] List = new NewDictionary[5];

            public ListOfMarks(){
                List[0].Key = "дуже погано";
                List[1].Key = "погано";
                List[2].Key = "посередньо";
                List[3].Key = "добре";
                List[4].Key = "дуже добре";
                for(int i = 0; i < 5; ++i){
                    List[i].Value = 0;
                }
            }
        }
        static void Main(string[] args)
        {
            try
            {
                ListOfMarks marks = new ListOfMarks();
                for (int i = 0; i < 5; ++i)
                {
                    Console.Write("Enter numeric mark number for {0}: ", marks.List[i].Key);
                    int tmp;
                    if (int.TryParse(Console.ReadLine(), out tmp))
                    {
                        if (i != 0 && tmp < marks.List[i - 1].Value)
                        {
                            Console.WriteLine("Oh, i see, interesting choice, but it would be difficult to calculate");
                            --i;
                            continue;
                        }
                        marks.List[i].Value = tmp;
                    }
                    else
                    {
                        --i;
                        Console.WriteLine("No no no, program not an AI to understand what you would like to do :)");
                    }
                }
                
                float chanceOfRain;
                while (true)
                {
                    Console.Write("Enter chance of rain: ");
                    if (float.TryParse(Console.ReadLine(), out chanceOfRain) && chanceOfRain <= 1 && chanceOfRain >= 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Well, idk what you have typed, wish you good luck, because you cannot escape >:)");
                    }
                }
                float chanceOfNoRain = 1 - chanceOfRain;
                
                int[][] arrayOfMarks;
                
                Console.WriteLine("Enter locations of entertainment(locations have to be written using spaces):");
                Console.Write("Locations: ");
                string locationString = Console.ReadLine() ?? String.Empty;
                string[] locations = locationString.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim()).ToArray();
                
                int countOfLocations = locations.Length;
                if (countOfLocations == 0)
                {
                    Console.WriteLine("Expecto PATRONUM");
                    return;
                }

                float[] usefulness = new float[countOfLocations];
                int highest = marks.List[4].Value, lowest = marks.List[0].Value;
                for (int i = 0; i < countOfLocations; ++i)
                {
                    string tmpLocation = locations[i];
                    Console.Write("Enter mark if it will be rainy and you are in/at " + tmpLocation + ": ");
                    int rainy, sunny;
                    if (!int.TryParse(Console.ReadLine(), out rainy) || !(rainy <= highest  && rainy >= lowest))
                    {
                        --i;  Console.WriteLine("Nah, it is not working here");
                        continue;
                    }
                    Console.Write("Enter mark if it will be sunny and you are in/at " + tmpLocation + ": ");
                    if (!int.TryParse(Console.ReadLine(), out sunny) || !(sunny <= highest  && sunny >= lowest))
                    {
                        --i;  Console.WriteLine("Nah, it is not working here");
                        continue;
                    }
                    usefulness[i] = rainy * chanceOfRain + sunny * chanceOfNoRain;
                }

                int maxIndex = 0;
                float maxValue =  usefulness[0];
                
                for (int i = 0; i < countOfLocations; ++i)
                {
                    Console.WriteLine("Location: {0}, usefulness: {1};", locations[i], usefulness[i]);
                    if (maxValue < usefulness[i])
                    {
                        maxValue = usefulness[i];
                        maxIndex = i;
                    }
                }

                Console.WriteLine("The best option is to go to: " + locations[maxIndex]);
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine("Key is not found.");
            }
            catch (Exception)
            {
                Console.WriteLine("Wrong data type");
            }
        }
    }
}