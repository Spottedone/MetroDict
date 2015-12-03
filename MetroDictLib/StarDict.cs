using MetroDictLib.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroDictLib
{
	public class StarDict : IDisposable
	{
		private readonly string _workDir;
		private readonly string _name, _indexFileName, _mainFileName;
		private bool _disposed = false;
		private FileStream _index;
		private MemoryStream _mainStream;
		private BinaryReader _reader;
		private readonly Dictionary<string, Article> _articles = new Dictionary<string, Article>();

		/// <summary>
		/// Constructs an instance of this class.
		/// </summary>
		/// <param name="name">Dictionary name without extension!</param>
		public StarDict(string workDir, string name)
		{
			this._workDir = workDir;
			this._name = name;
			this._indexFileName = Path.Combine(_workDir, string.Format("{0}.idx", _name));
			this._mainFileName = Path.Combine(_workDir, string.Format("{0}.dict.dz", _name));
		}

		public async Task Init()
		{
			await ReadIndex();
			using (var compressedStream = new GZipStream(File.OpenRead(_mainFileName), CompressionMode.Decompress))
			{
				this._mainStream = new MemoryStream();
				await compressedStream.CopyToAsync(this._mainStream);
				_reader = new BinaryReader(_mainStream);
			}
			Console.WriteLine(string.Format("{0} articles total.", _articles.Keys.Count));
		}

		public async Task<string> GetArticle(string word)
		{
			if (!_articles.ContainsKey(word))
			{
				return "";
			}
			var article = _articles[word];
			return GetArticleBody(article);
		}

		public async Task<List<string>> GetArticlesContaining(string word)
		{
			var keys = from k in _articles.Keys
					   where k.ToLower().Contains(word.ToLower())
					   select k;
			return (from k in keys
					select GetArticleBody(_articles[k])).ToList();
		}

		public void Dispose()
		{
			Dispose(false);
			GC.SuppressFinalize(this);
		}

		private async Task ReadIndex()
		{
			byte[] buf = new byte[256];
			byte b;
			try
			{
				using (_index = File.OpenRead(_indexFileName))
				{
					_index.Seek(0, SeekOrigin.Begin);
					using (var br = new BinaryReader(_index))
					{
						while (br.BaseStream.Position != br.BaseStream.Length - 1)
						{
							// zero buffer
							buf.Fill<byte>(0x20);

							// read word
							int idx = 0;
							while (true)
							{
								b = br.ReadByte();
								if (b == 0)
								{
									break;
								}
								buf[idx] = b;
								idx++;
								if (idx >= buf.Length)
								{
									Array.Resize<byte>(ref buf, idx + 1);
								}
							}
							// convert to string
							string word = System.Text.Encoding.UTF8.GetString(buf).Trim();
							// article start
							uint start = br.ReadUInt32BE();
							// article length
							uint length = br.ReadUInt32BE();
							_articles.Add(word, new Article(word, start, length));
						}
					}
				}
			}
			catch (Exception)
			{
				//TODO: log, report error
			}
		}

		private string GetArticleBody(Article article)
		{
			_mainStream.Seek(article.Start, SeekOrigin.Begin);
			uint length = article.Length;
			byte[] buf = _reader.ReadBytes((int)length);
			return Encoding.UTF8.GetString(buf);
		}

		protected void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (!_disposed)
				{
					_mainStream.Close();
					_mainStream.Dispose();
				}
				_disposed = true;
			}
		}

		~StarDict()
		{
			Dispose(true);
		}
	}
}