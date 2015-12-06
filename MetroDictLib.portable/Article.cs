namespace MetroDictLib
{
    public class Article
    {
        public Article(string word, uint start, uint length)
        {
            Word = word;
            Start = start;
            Length = length;
        }

        public string Word { get; private set; }
        public uint Start { get; private set; }
        public uint Length { get; private set; }
    }
}