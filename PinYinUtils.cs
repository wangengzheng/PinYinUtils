using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using System.Web.Caching;
using System.Xml.Linq;

namespace China.PinYin
{
	public class PinYinUtils
	{
		private static string FilePath;
		const string CacheKey = "PinYinCache";
		
		static PinYinUtils()
		{
			FilePath = AppDomain.CurrentDomain.BaseDirectory + "\\PINYINS.xml";			
		}

		public static string FirstPinYin(string @string)
		{

			if (string.IsNullOrEmpty(@string))
				return "Z";

			char ch = @string.TrimStart()[0];
			string key = string.Format("{0:X}", (int)ch).ToUpper() ?? "";

			var dict = PinYinUtils.GetCache();

			string pinyin = null;
			//http://referencesource.microsoft.com/#mscorlib/system/collections/generic/dictionary.cs,bcd13bb775d408f1
			if (dict.TryGetValue(key, out pinyin))
			{
				return pinyin.Length > 0 ? pinyin.Substring(0, 1).ToUpper() : "";
			}


			return "Z";
		}



		/// <summary>
		/// web
		/// </summary>
		/// <returns></returns>
		public static Dictionary<string, string> GetCache()
		{
			if (HttpContext.Current.Cache[CacheKey] != null)
			{
				return (Dictionary<string, string>)HttpContext.Current.Cache[CacheKey];
			}
			else
			{
				XDocument xml = XDocument.Load(PinYinUtils.FilePath);

				var PinYins = from item in xml.Element("PINYINS").Elements()
							select new
							{
								hexStr = item.Attribute("hexStr").Value,
								firstPinYin = item.Attribute("firstPinYin").Value,
								utf8Str = item.Attribute("utf8Str").Value,
								allPinYin = item.Attribute("allPinYin").Value
							};

				if (PinYins == null)
				{
					throw new NullReferenceException("创建 pinyin 实例 失败！");
				}
				//PinYins.ToDictionary(a => a.HexStr,a=>a.PinYinStr);
				//http://referencesource.microsoft.com/#System.Core/System/Linq/Enumerable.cs,a6091311eadfdd8a
				var dictLength = PinYins.Count();
				Dictionary<string, string> dict = new Dictionary<string, string>(dictLength);
				foreach (var item in PinYins)
				{
					dict.Add(item.hexStr, item.firstPinYin);
				}
				CacheDependency dep = new CacheDependency(PinYinUtils.FilePath, DateTime.Now);
				HttpContext.Current.Cache.Insert(CacheKey, dict, dep, DateTime.MaxValue, Cache.NoSlidingExpiration, CacheItemPriority.High, null);
				return dict;
			}
		}

	}
}
