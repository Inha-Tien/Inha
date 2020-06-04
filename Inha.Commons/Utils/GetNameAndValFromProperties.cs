namespace Inha.Commons.Utils
{
    using Inha.Commons.Types;
    using System.Collections.Generic;
    public static class GetNameAndValFromProperties
    {
        public static IEnumerable<KeyVal> Retrieval<T>(T item)
        {
            var properties = item.GetType().GetProperties();
            foreach (var info in properties)
            {
                yield return new KeyVal(info.Name, info.GetValue(item, null));
            }
        }
    }
}
