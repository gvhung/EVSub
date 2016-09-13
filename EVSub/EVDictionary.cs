using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;
using System.Text.RegularExpressions;

namespace EVSub
{
    class EVDictionary
    {
        public string GoogleTranslate(string text)
        {
            string url = string.Format(@"http://translate.google.com/translate_a/t?client=j&text={0}&hl=en&sl=en&tl=vi", text);
            string html = null;
            string mean = null;
            try
            {
                WebClient web = new WebClient();
                web.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0");
                web.Headers.Add(HttpRequestHeader.AcceptCharset, "UTF-8");
                web.Encoding = System.Text.Encoding.UTF8;
                html = web.DownloadString(url);
                mean = html.Substring(1, html.Length - 2);
            }
            catch (WebException wex)
            {
                Debug.WriteLine("Translate error = " + wex);
                return wex.Message;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Translate error = " + ex);
                return null;
            }
            return mean;
        }
        public string GoogleTranslate2(string text)
        {
            string mean = GoogleTranslate(text);
            if (string.IsNullOrEmpty(mean))
            {
                return null;
            }
            else
            {
                return "Google Translate" + Environment.NewLine + mean;
            }
        }
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
                Debug.WriteLine("findWordInEvDic KeyNotFoundException = " + e);
                return null;
            }
            catch (Exception e)
            {
                Debug.WriteLine("findWordInEvDic = " + e);
                return null;
            }
        }
        public string translateWithRemoveEnding(string word)
        {
            string mean = findWordInEvDic(word);
            if (string.IsNullOrEmpty(mean))
            {
                mean = removeEdEnding(word);
                if (string.IsNullOrEmpty(mean))
                {
                    mean = removeSEnding(word);
                    if (string.IsNullOrEmpty(mean))
                    {
                        return null;
                    }
                }
            }
            return mean;
        }

        private string removeSEnding(string word)
        {
            PluralizationService ps = PluralizationService.CreateService(CultureInfo.GetCultureInfo("en-us"));
            if (ps.IsPlural(word))
            {
                return findWordInEvDic(ps.Singularize(word));
            }
            else
            {
                return null;
            }
        }

        private string removeEdEnding(string word)
        {
            string mean = null;
            if (word.EndsWith("ed"))
            {
                //Want --> Wanted
                mean = findWordInEvDic(word.Substring(0, word.Length - 2));
                if (mean != null)
                {
                    return mean;
                }
                //Live --> Lived
                mean = findWordInEvDic(word.Substring(0, word.Length - 1));
                if (mean != null)
                {
                    return mean;
                }
                //Stop --> Stopped
                mean = findWordInEvDic(word.Substring(0, word.Length - 3));
                if (mean != null)
                {
                    return mean;
                }
                //Study --> Studied
                mean = findWordInEvDic(word.Substring(0, word.Length - 3) + "y");
                if (mean != null)
                {
                    return mean;
                }
            }
            return null;
        }

        public EVDictionary(string fileName = null)
        {
            if (fileName == null)
            {
                string[] result = Regex.Split(EVSub.Properties.Resources.anhviet109K, "\r\n|\r|\n");
                string word = "@";
                string mean = "@";
                foreach (string s in result)
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
            else
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
}
