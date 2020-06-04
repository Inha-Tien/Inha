namespace Inha.Commons.Types
{
    public class HATEOAS
    {
        public string href { get; set; }
        public string rel { get; set; }
        public string type { get; set; }

        public HATEOAS(string href, string rel, string type)
        {
            this.href = href;
            this.rel = rel;
            this.type = type;
        }
        public HATEOAS()
        {

        }
    }
}
