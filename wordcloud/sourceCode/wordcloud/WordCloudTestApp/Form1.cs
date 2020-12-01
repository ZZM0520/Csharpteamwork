using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WordCloudGen = WordCloud.WordCloud;
using JiebaNet.Analyser;
using JiebaNet.Segmenter;
using JiebaNet.Segmenter.PosSeg;
using NUnit.Framework;
namespace WordCloudTestApp
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			var lines = File.ReadLines("../../content/counts.csv");
			Words = new List<string>(100);
			Frequencies = new List<int>(100);
			foreach (var line in lines)
			{
				var textValue = line.Split(new char[] {','});
				Words.Add(textValue[0]);
				Frequencies.Add(int.Parse(textValue[1]));
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			var s = new Stopwatch();
			s.Start();
			var wc = new WordCloudGen(1000, 600);
			if(resultPictureBox.Image != null) resultPictureBox.Image.Dispose();
			Words.Clear();
			Frequencies.Clear();

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
					bool result = false;
					for (int j = 0; j < word.Length; j++)
					{
						if (Char.IsNumber(word, j))
						{
							result = true;
						}
					}
					if (!result)
					{
						//如果不存在 且不为数字就添加
						AllWords.Add(word);
						AllFrequencies.Add(1);
					}
				}
			}
			int index = 0;
			foreach (var temp in AllFrequencies)
			{
				if (temp > 200)
				{
					Words.Add(AllWords[index]);
					Frequencies.Add(temp);
				}
				index++;
			}

			Image i = wc.Draw(Words, Frequencies);
			s.Stop();
			elapsedLabel.Text = s.Elapsed.TotalMilliseconds.ToString();
			resultPictureBox.Image = i;
		}

		List<string> Words { get; set; }

		List<int> Frequencies { get; set; } 
	}
}
