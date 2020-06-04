namespace Inha.Commons.Types
{
    public class KeyVal
    {
        public string Key { get; private set; }
        public object Val { get; private set; }
        public KeyVal(string key, object val)
        {
            this.Key = key;
            this.Val = val;
        }
    }
}
