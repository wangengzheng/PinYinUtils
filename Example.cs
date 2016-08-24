using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinYinUtils
{
	public class Examples
	{
		/// <summary>
		/// get frist Pinyin
		/// </summary>
		public void Method1()
		{
			China.PinYin.PinYinUtils.FirstPinYin("中");
			//return : Z (upper)
		}

		/// <summary>
		/// get hexstr
		/// </summary>
		public void GetHexStr()
		{
			char ch = "拼音"[0];

			string key = string.Format("{0:X}", (int)ch).ToUpper() ?? "";

			//reuslt :62FC
		}

		/// <summary>
		///HanZi_UTF8_Range
		/// </summary>
		public void HanZi_UTF8_Range()
		{
			//pinyin  //[\u4E00-\u9FA5];
			// int value range   19968  40865

			for (int begin = 19968; begin <= 40895; begin++)
			{
				//hanzi
				string utf8Str = Char.ConvertFromUtf32(begin);
				//hexstr
				string hexStr = begin.ToString("X8").Substring(4);

				Console.WriteLine(hexStr + "，" + utf8Str);


			}

		}

		/// <summary>
		/// Create_Xml_File
		/// </summary>
		public void Create_Xml_File()
		{
			StringBuilder sb = new StringBuilder();
			for (int begin = 19968; begin <= 40895; begin++)
			{
				string utf8Str = Char.ConvertFromUtf32(begin);

				string hexStr = begin.ToString("X8").Substring(4);

				Console.WriteLine(hexStr + "_____________" + utf8Str);

				sb.AppendFormat("<item hexStr=\"{0}\" pinyinStr=\"{1}\" utf8Str=\"{2}\" /> \n", hexStr, ConvertToPinYin(utf8Str), utf8Str);

				//sb.AppendLine("<item hexStr=\"" + hexStr + "\" pinyinStr=\"" + ConvertToPinYin(utf8Str) + "\" utf8Str=\"" + utf8Str + "\" />");

				//sb.AppendLine("<p>hexStr_________" + hexStr + "______________pinyinStr_____________" + ConvertToPinYin(utf8Str) + "_________________utf8Str_______________" + utf8Str + "</p>");
			}


			System.IO.File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "pinyin.xml", sb.ToString(), Encoding.UTF8);
		
		}

		/// <summary>
		/// ConvertToPinYin
		/// <remarks>Idea source Idea source Idea source</remarks>
		/// </summary>
		/// <param name="utf8Str"></param>
		/// <returns></returns>
		public string ConvertToPinYin(string utf8Str)
		{
			//https://www.nuget.org/packages?q=pinyin

			//method 1 Install-Package Microsoft.International.Converters.PinYinConverter			

			//method 2 Install-Package pinyin4net


			return string.Empty;
		}
	}
}
