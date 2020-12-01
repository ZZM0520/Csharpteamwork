using JiebaNet.Segmenter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace 图云词频计算
{
    class Program
    {
        static void Main(string[] args)
        {
            method2();
        }
        public static void method1()
        {
            List<string> AllWords = new List<string>();
            List<int> AllFrequencies = new List<int>();
            var file_words = File.ReadAllText(@"D:\c#\图云词频计算\words.txt");
            var words = file_words.Split('\n');
            foreach (var word in words)
            {
                if (AllWords.Contains(word))
                {
                    //如果已经存在就+1
                    AllFrequencies[AllWords.IndexOf(word)]++;
                }
                else
                {
                    //如果不存在就添加
                    AllWords.Add(word);
                    AllFrequencies.Add(1);
                }
            }
            foreach (var temp in AllFrequencies)
            {
                if(temp>200)
                Console.WriteLine(temp);
            }
        }
        public static void method2()
        {
            string path = @"D:\c#\stopwords-master\baidu_stopwords.txt";   //路径
            string str = File.ReadAllText(path);
            var stop_words = str.Split('\n');

            path = @"D:\c#\图云词频计算\files";
            DirectoryInfo root = new DirectoryInfo(path);
            foreach (FileInfo f in root.GetFiles())
            {
                string fullName = f.FullName;
                var text = File.ReadAllText(fullName);
                string pattern = @"abstract:[\S\s]+";
                Regex regex = new Regex(pattern);
                Match match = regex.Match(text);
                if (match.Groups.Count != 0)
                {
                    text = match.Groups[0].ToString().Substring(9);
                }
                else
                {
                    text = "abstract"; //防止出现“”
                }
                if (text == "") text = "abstract";
                var segmenter = new JiebaSegmenter();
                var segments = segmenter.Cut(text, cutAll: true);  // 默认为精确模式

                System.IO.StreamWriter file = new System.IO.StreamWriter(@"D:\c#\图云词频计算\words.txt", true);  //写入到文件末尾 不覆盖
                foreach (var temp in segments)
                {
                    if (!stop_words.Contains(temp))
                    {
                        file.WriteLine(temp);
                    }
                }
                file.Close();
            }
        }
    }
}
