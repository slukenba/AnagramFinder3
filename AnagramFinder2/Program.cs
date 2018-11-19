using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;

namespace AnagramFinder2
{
    public class Program
    {
        /// <summary>
        /// Console driven application that finds the anagrams from the user inputted string
        /// </summary>
        /// <param name="args">The file name for the dictionary</param>
        static void Main(string[] args)
        {
            try
            {
                Program p = new Program();

                //Print Title to screen
                Console.WriteLine("Welcome to the Anagram Finder");
                Console.WriteLine("-----------------------------");

                //path to the dictionary file
                string path = Path.Combine(Environment.CurrentDirectory, args[0]);

                //Timer for measuring how long it takes to load the dictionary
                Stopwatch aTimer = new Stopwatch();
                aTimer.Start();
                //Create a dictionary from the file
                Dictionary<string, string> anagramDict = p.CreateDictionary(path);

                aTimer.Stop();

                //Show the user the time it took to load the dictionary
                Console.WriteLine("Dictionary loaded in " + aTimer.ElapsedMilliseconds + " ms");

                Console.Write("AnagramFinder>");
                //Record the user input
                string userInput = Console.ReadLine();

                while (userInput != "exit")
                {
                    List<string> resultList = new List<string>();

                    //Timer for measuring how long it takes to find anagrams from the dictionary
                    Stopwatch aResultTimer = new Stopwatch();
                    aResultTimer.Start();

                    var orderAnagramToFind = p.SortAnagramCharacters(userInput);

                    //Finds the anagram in the dictionary for the user input
                    if (anagramDict.ContainsKey(orderAnagramToFind))
                    {
                        resultList = anagramDict[orderAnagramToFind].Split(',').ToList<string>();
                    }

                    aResultTimer.Stop();

                    //Shows the results to the user
                    p.ResultOutput(resultList, userInput, aResultTimer);

                    Console.Write("AnagramFinder>");
                    userInput = Console.ReadLine();
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("ERROR: Could not find the dictionary file!");
                Console.ReadLine();
            }
            catch (Exception)
            {
                Console.WriteLine("ERROR: Unexpected Error!");
                Console.ReadLine();
            }
        }

        /// <summary>
        /// Prints to the console the results of your anagram search
        /// </summary>
        /// <param name="resultListToPrint"></param>
        /// <param name="userInput"></param>
        /// <param name="timeToGetResult"></param>
        public void ResultOutput(List<string> resultListToPrint, string userInput, Stopwatch timeToGetResult)
        {
            if (resultListToPrint != null && resultListToPrint.Count > 0)
            {
                Console.WriteLine(resultListToPrint.Count + " Anagrams found for " + userInput + " in " + timeToGetResult.ElapsedMilliseconds + "ms");
                Console.WriteLine(string.Join(",", resultListToPrint));
            }
            else
                Console.WriteLine("No anagrams found for " + userInput + " " + timeToGetResult.ElapsedMilliseconds + "ms");
        }

        /// <summary>
        /// Creates a Dictionary from the text file with all the anagrams
        /// </summary>
        /// <param name="path"></param>
        /// <returns>A Dictionary with the sorted anagram as the key and a list of the anagrams as the value</returns>
        public Dictionary<string, string> CreateDictionary(string path)
        {
            List<string> dictFromFile = File.ReadAllLines(path).ToList<string>();
            var anagramDict = new Dictionary<string, string>();

            foreach (var i in dictFromFile)
            {
                var orderAnagramToFind = SortAnagramCharacters(i);

                if (anagramDict.ContainsKey(orderAnagramToFind))
                {
                    anagramDict[orderAnagramToFind] = CreateDictValue(anagramDict[orderAnagramToFind], i);
                }
                else
                {
                    anagramDict.Add(orderAnagramToFind, i);
                }
            }

            return anagramDict;
        }

        /// <summary>
        /// Sorts the provided string
        /// </summary>
        /// <param name="anagramToSort"></param>
        /// <returns></returns>
        public string SortAnagramCharacters(string anagramToSort)
        {
            return String.Concat(anagramToSort.OrderBy(c => c));
        }

        /// <summary>
        /// Creates Dict value
        /// </summary>
        /// <param name="currentValue"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        public string CreateDictValue(string currentValue, string newValue)
        {
            return currentValue + "," + newValue;
        }
    }
}