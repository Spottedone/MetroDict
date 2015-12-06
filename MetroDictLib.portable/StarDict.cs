using MetroDictLib.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace MetroDictLib
{
	public class StarDict : IDisposable
	{
		private bool _disposed = false;
		private MemoryStream _mainStream;
		private BinaryReader _reader;
		private readonly Dictionary<string, Article> _articles = new Dictionary<string, Article>();

	    private readonly StorageFile _main, _index;
        public bool IsInitialised { get; set; }
	    public string Name { get; private set; }

	    public StarDict(StorageFile main, StorageFile index)
	    {
	        _main = main;
	        _index = index;
            IsInitialised = false;
	        Name = main.Name;
	    }

	    public async Task Init()
	    {
            await ReadIndex();
            using (var compressedStream = new GZipStream(
                (await _main.OpenReadAsync()).AsStreamForRead(), CompressionMode.Decompress))
            {
                this._mainStream = new MemoryStream();
                await compressedStream.CopyToAsync(this._mainStream);
                _reader = new BinaryReader(_mainStream);
            }
	        IsInitialised = true;
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

		public List<string> GetArticlesContaining(string word)
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
				using (Stream stream = (await _index.OpenReadAsync()).AsStreamForRead())
				{
					using (var br = new BinaryReader(stream))
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
						    string word = Encoding.UTF8.GetString(buf, 0, buf.Length);
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
            return Encoding.UTF8.GetString(buf, 0, buf.Length);
		}

		protected void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (!_disposed)
				{
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