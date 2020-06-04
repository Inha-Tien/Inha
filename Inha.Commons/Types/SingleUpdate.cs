namespace Inha.Commons.Types
{
    using global::Dapper;
    using System.Collections.Generic;
    public class SingleUpdate
    {
        public IEnumerable<string> ColumnUpdates { get; set; }
        public IEnumerable<string> ConditionEquals { get; set; }
        public IEnumerable<string> ConditionINs { get; set; }
        public IEnumerable<KeyVal> Parameters
        {
            get { return this._params; }
            set { this._params.AddRange(value); }
        }

        private List<KeyVal> _params = new List<KeyVal>();
        public SingleUpdate()
        {
        }
        public void AddParameter(string parameterName, object val)
        {
            this._params.Add(new KeyVal(parameterName, val));
        }
        public DynamicParameters GetDynamicParameters()
        {
            var parameters = new DynamicParameters();
            foreach (var p in this._params)
            {
                if (p.Val.GetType().Name.Equals("JArray"))
                {
                    parameters.Add(p.Key, Newtonsoft.Json.JsonConvert.DeserializeObject<List<object>>(p.Val.ToString()));
                }
                else
                {
                    parameters.Add(p.Key, p.Val);
                }


            }
            return parameters;
        }
    }
}
