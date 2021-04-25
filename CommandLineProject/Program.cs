using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace CommandLineProject
{
    public static class Program
    {
        private static readonly Dictionary<string, List<string>> multiValueDic = new Dictionary<string, List<string>>();

        private static void Main(string[] args)
        {
            Console.WriteLine("Enter Command To Procced");
            Console.WriteLine("Enter EXIT To Stop");
            string input = Console.ReadLine();


            while (input.Trim().ToUpper() != "EXIT")
            {
                //Split string by space
                string[] inputSplit = input.Split(" ");

                //Set Command - Key - Value from input arguments
                string command = inputSplit.Length >= 1 ? inputSplit[0] : string.Empty;
                string key = inputSplit.Length >= 2 ? inputSplit[1] : string.Empty;
                string value = inputSplit.Length >= 3 ? inputSplit[2] : string.Empty;
                try
                {
                    switch (GetCommandDescription(command))
                    {
                        case CommandLineEnum.KEYS:
                            //foreach (string item in multiValueDic.Keys)
                            foreach (var item in multiValueDic.Keys.Select((value, index) => new { value, index }))
                            {
                                Console.WriteLine($"{item.index + 1}) {item.value}");
                            }

                            if (multiValueDic.Keys.Count == 0)
                            {
                                Console.WriteLine("empty set");
                            }
                            break;

                        case CommandLineEnum.ADD:
                            AddToMultiValueDictionaryBykeyValue(key, value);
                            break;

                        case CommandLineEnum.MEMBERS:
                            DisplayMembersFromMultiValueDictionary(key);
                            break;

                        case CommandLineEnum.REMOVE:
                            RemoveFromMultiValueDictionaryByKeyValue(key, value);
                            break;

                        case CommandLineEnum.REMOVEALL:
                            RemoveAllValuesFromMultiValueDictionaryBykey(key);
                            break;

                        case CommandLineEnum.KEYEXISTS:
                            Console.WriteLine(multiValueDic.ContainsKey(key));
                            break;

                        case CommandLineEnum.ALLMEMBERS:
                            DisplayMembersFromMultiValueDictionary();
                            break;

                        case CommandLineEnum.VALUEEXISTS:
                            Console.WriteLine(CheckIfValueExistsForGivenKey(key, value));
                            break;

                        case CommandLineEnum.ITEMS:
                            DisplayAllItmesFromMultiValueDictionary();
                            break;

                        case CommandLineEnum.CLEAR:
                            multiValueDic.Clear();
                            Console.WriteLine("Cleared");
                            break;

                        default:
                            Console.WriteLine("Command Not found");
                            break;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Please use proper command");
                }
                input = Console.ReadLine();
            }
        }

        public static CommandLineEnum GetCommandDescription(string input)
        {
            try
            {
                return (CommandLineEnum)Enum.Parse(typeof(CommandLineEnum), input);
            }
            catch (InvalidEnumArgumentException ex)
            {
                throw ex;
            }
        }

        public static void DisplayMembersFromMultiValueDictionary(string key = "")
        {

            if (!string.IsNullOrEmpty(key.Trim()))
            {
                if (!multiValueDic.ContainsKey(key))
                {
                    Console.WriteLine("ERROR, key does not exist.");
                }
                else
                {
                    foreach (KeyValuePair<string, List<string>> kvp in multiValueDic)
                    {
                        if (key == kvp.Key)
                        {
                            foreach (var item in kvp.Value.Select((value, index) => new { value, index }))
                            {
                                Console.WriteLine($"{item.index + 1}) {item.value}");
                            }
                        }
                    }
                }
            }
            else
            {
                if (multiValueDic.Keys.Count <= 0)
                {
                    Console.WriteLine("(empty set)");
                }
                int index = 1;
                foreach (KeyValuePair<string, List<string>> kvp in multiValueDic)
                {
                    if (kvp.Value.Count <= 0)
                    {
                        Console.WriteLine("(empty set)");
                    }
                    else
                    {
                        foreach (string value in kvp.Value)
                        {
                            Console.WriteLine($"{index}) {value}");
                            index++;
                        }
                    }
                }
            }
        }

        public static void DisplayAllItmesFromMultiValueDictionary()
        {
            int index = 1;
            if (multiValueDic.Count <= 0)
            {
                Console.WriteLine("(empty set)");
            }
            foreach (KeyValuePair<string, List<string>> kvp in multiValueDic)
            {
                foreach (string value in kvp.Value)

                {
                    Console.WriteLine($"{index}) {kvp.Key} : {value}");
                    index++;
                }
            }
        }

        public static void AddToMultiValueDictionaryBykeyValue(string key, string value)
        {
            List<string> item = new List<string> { value };

            if (!multiValueDic.ContainsKey(key))
            {
                multiValueDic.Add(key, item);
                Console.WriteLine("Added");
            }
            else
            {
                if (!CheckIfValueExistsForGivenKey(key, value))
                {
                    multiValueDic[key].Add(value);
                    Console.WriteLine("Added");
                }
                else
                {
                    Console.WriteLine("ERROR, value already exists");
                }
            }
        }

        public static void RemoveFromMultiValueDictionaryByKeyValue(string key, string value)
        {
            if (!multiValueDic.ContainsKey(key))
            {
                Console.WriteLine("ERROR, Key doesn't exist");
            }
            else if (CheckIfValueExistsForGivenKey(key, value))
            {
                foreach (KeyValuePair<string, List<string>> kvp in multiValueDic)
                {
                    if (key == kvp.Key)
                    {
                        foreach (var v in kvp.Value.ToArray())
                        {
                            if (v == value)
                            {
                                kvp.Value.Remove(v);
                                Console.WriteLine("Removed");
                            }
                        }
                    }

                    if (kvp.Value.Count == 0)
                    {
                        multiValueDic.Remove(kvp.Key);
                    }
                }
            }
            else
            {
                Console.WriteLine("ERROR, Value doesn't exist");
            }
        }

        public static void RemoveAllValuesFromMultiValueDictionaryBykey(string key)
        {
            if (!multiValueDic.ContainsKey(key))
            {
                Console.WriteLine("ERROR, Key doesn't exist");
            }

            foreach (KeyValuePair<string, List<string>> kvp in multiValueDic)
            {
                if (key == kvp.Key)
                {
                    foreach (var v in kvp.Value.ToArray())
                    {
                        kvp.Value.Remove(v);
                    }
                    Console.WriteLine("Removed");
                }

                if (kvp.Value.Count == 0)
                {
                    multiValueDic.Remove(kvp.Key);
                }
            }
        }

        private static bool CheckIfValueExistsForGivenKey(string key, string value)
        {
            bool isExists = false;
            foreach (KeyValuePair<string, List<string>> kvp in multiValueDic)
            {
                if (key == kvp.Key)
                {
                    foreach (string v in kvp.Value)
                    {
                        if (v == value)
                            isExists = true;
                    }
                }
            }
            return isExists;
        }
    }
}