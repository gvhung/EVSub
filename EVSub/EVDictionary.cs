using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace EVSub
{
    class EVDictionary
    {
        private Dictionary<string, string> evdic = new Dictionary<string, string>();
        private string getWord(string line)
        {
            if (line[0] != '@')
            {
                return null;
            }
            int lengthOfWord = line.IndexOf('/') - 2;
            if (lengthOfWord > 0)
            {
                return line.Substring(1, lengthOfWord);
            }
            else
            {
                return null;
            }
        }
        private string getPronunciation(string line)
        {
            if (line[0] != '@')
            {
                return null;
            }
            int startOfPronunciation = line.IndexOf('/');
            if (startOfPronunciation > 0)
            {
                return line.Substring(startOfPronunciation);
            }
            else
            {
                return null;
            }
        }
        public string findWordInEvDic(string word)
        {
            try
            {
                return evdic[word];
            }
            catch (KeyNotFoundException e)
            {
                return null;
            }
            catch (Exception e)
            {
                Debug.WriteLine("findWordInEvDic = " + e);
                return null;
            }
        }
        public EVDictionary(string fileName)
        {
            using (StreamReader sr = File.OpenText(fileName))
            {
                string s = String.Empty;
                string word = "@";
                string mean = "@";
                while ((s = sr.ReadLine()) != null)
                {
                    if (s.Length != 0)
                    {
                        string wordtmp = getWord(s);
                        if (wordtmp != null)
                        {
                            mean = mean.Replace("=", "   ").Replace("+", ":").Replace("!", "   ");//TODO Làm cho đẹp hơn
                            evdic.Add(word, mean);
                            word = wordtmp;
                            mean = s.Substring(1);
                        }
                        else
                        {                            
                            mean += s + Environment.NewLine;
                        }
                    }
                }
                evdic.Add(word, mean);
            }
        }
    }
}
