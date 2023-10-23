using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.Tracing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace App {
    internal class Program {
        static void Main(string[] args) {
            Output.Line("Enter Input: ");
            var inp = Console.ReadLine();
			Console.SetIn ( new StreamReader ( Console.OpenStandardInput ( 2048 ) ) );
            int WordCnt, CharCnt, NumCnt, SpecCnt;
            /********************** DO NOT EDIT ABOVE THIS LINE **********************************/

            // Declare bonus variables

            int SentenceCnt;

            decimal WPerSent, charsPerSent;

            // declare unnecessary variables

            int SpecialCCWSpaces;

            int spaceCnt;

            // Call metric functions
            WordCnt = WordCount(inp);

            CharCnt = CharacterCount(inp);

            NumCnt = NumberCount(inp);

            SpecCnt = SpecialCharacterCount(inp);
            
            SentenceCnt = SentenceCount(inp);

            WPerSent = WordsPerSentence(WordCnt, SentenceCnt);

            charsPerSent = CharactersPerSentence(CharCnt, SentenceCnt);

            SpecialCCWSpaces = SpecialCharCntWithSpaces(inp);

            spaceCnt = SpaceCnt(inp);

            // convert metrics to strings for Output

            string NCnt = Convert.ToString(NumCnt);

            string wCnt = Convert.ToString(WordCnt);

            string CCwithSpaces = Convert.ToString(CharCnt);

            string SpecialChars = Convert.ToString(SpecCnt);

            string sentenceC = Convert.ToString(SentenceCnt);

            string wPerSent = Convert.ToString(WPerSent);

            string charPerS = Convert.ToString(charsPerSent);

            string SpecCCNoS = Convert.ToString(SpecialCCWSpaces);

            string SpaceCount = Convert.ToString(spaceCnt);

            // Output metrics
            
            Output.Line($"1. Word Count: {wCnt}\n\tA. Sentence Count: {sentenceC}\n\tB. Words Per Sentence: {Math.Round(WPerSent, 4)}");

            Output.Line($"2. Character Count: {CCwithSpaces}\n\tA. Special Character Count: {SpecialChars}\n\tB. Characters Per Sentence: {Math.Round(charsPerSent, 4)}");

            Output.Line($"3. Number Count: {NCnt}");

            Output.Line($"Special Character Count (Including Spaces): {SpecCCNoS}\nSpace Count: {SpaceCount}");

            // THIS SHOULD BE THE LAST STATEMENT FOR MAIN
            Console.Read();
        }

        // method finds the word count of the given input
        static int WordCount(string stringInput)
        {
            string[] Words = stringInput.Split(' ');

            int wordCount = 0;

            // count number of words
            foreach(string i in Words)
            {
                wordCount++;
            }

            return wordCount;
        }

        // method finds the character count of the given input
        static int CharacterCount(string stringInput)
        {

            int CCWithSpaces = 0;

            char[] chars1 = stringInput.ToCharArray();

            // count number of characters (including spaces)
            foreach (char i in chars1)
            {
                CCWithSpaces++;
            }

            return CCWithSpaces;
        }

        // method finds the number count of the given input
        static int NumberCount(string stringInput)
        {
            int NCount = 0;

            string stringInput2 = stringInput.Replace(". ", " ").Replace("? ", " ").Replace("! ", " ");

            int lastCharIndex = stringInput2.Length - 1;

            // check if ends with punctuaion, if so, remove punctuation
            if (stringInput2.EndsWith(".") || stringInput2.EndsWith("?") || stringInput2.EndsWith("!"))
            {
                stringInput2 = stringInput2.Remove(lastCharIndex);
            }

            string[] Words = stringInput2.Split(' ');

            float N;

            int index1 = 0;

            // check if each string ends with punctuation, if so, remove punctuation
            foreach(string i in Words)
            {
                int lastCharIndex2 = Words[index1].Length - 1;

                char[] chars = Words[index1].ToCharArray();

                char value = '0';

                // check if string has a length greater than or equal to 0
                if(lastCharIndex2 >= 0)
                {
                    value = chars[lastCharIndex2];
                }
                
                // check if string ends in punctuation
                if (!char.IsDigit(value))
                {
                    Words[index1] = Words[index1].Remove(lastCharIndex2);
                }

                index1++;
            }

            // count number of words
            foreach (string k in Words)
            {
                if (float.TryParse(k, out N))
                {
                        NCount++;
                }
            }

            return NCount;
        }

        // method finds the special character count of the given input
        static int SpecialCharacterCount(string stringInput)
        {
            int specialC = 0;
            
            char[] chars1 = stringInput.ToCharArray();

            // count special characters (excluding spaces)
            foreach(char i in chars1)
            {
                // check if character is a space or alphanumeric
                if (i != ' ' && !char.IsLetterOrDigit(i))
                {
                    specialC++; 
                }
            }

            return specialC;
        }

        // method finds the sentence count of the given input
        static int SentenceCount(string stringInput)
        {
            int sentenceC = 0;

            string newstring = stringInput.Replace(". ", ".|").Replace("? ", "?|").Replace("! ","!|");

            int lastCharIndex = newstring.Length - 1;

            // check if string ends with a pipe, if so, remove it
            if (newstring.EndsWith("|"))
            {
                newstring = newstring.Remove(lastCharIndex);
            }
            
            string[] Sentences = newstring.Split('|');

            // count number of sentences
            foreach (string i in Sentences)
            {
                sentenceC++;  
            }

            return sentenceC;
        }

        // method finds the words per sentence of the given input
        static decimal WordsPerSentence(int Words, int Sentences)
        {
            decimal WperS = (decimal)Words / Sentences;

            return WperS;
        }

        // method finds the characters per sentence of the given input
        static decimal CharactersPerSentence(int characters, int sentence)
        {
            decimal CharactersPerSentence = (decimal)characters / sentence;

            return CharactersPerSentence;
        }

        // method finds the special characters of the given input (including spaces)
        static int SpecialCharCntWithSpaces(string stringInput)
        {
            char[] chars = stringInput.ToCharArray();

            int SpecialCCWSpaces = 0;

            // count special characters (excluding spaces)
            foreach(char i in chars)
            {
                // check if character is a space or alphanumeric
                if (!char.IsLetterOrDigit(i))
                {
                    SpecialCCWSpaces++;
                }
            }

            return SpecialCCWSpaces;
        }

        // method finds the number of space characters in a given input
        static int SpaceCnt(string stringInput)
        {
            char[] chars = stringInput.ToCharArray();

            int spaceCnt = 0;

            // count spaces
            foreach(char i in chars)
            {              
                // check if character is space
                if (i == ' ')
                {
                    spaceCnt++;
                }
            }

            return spaceCnt;
        }
    }
}
