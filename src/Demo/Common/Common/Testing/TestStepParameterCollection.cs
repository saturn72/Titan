using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Saturn72.Extensions;

namespace Common.Testing
{
    public class TestStepParameterCollection : ICollection<TestStepParameterModel>
    {
        private IDictionary<string, TestStepParameterModel> _parametersHolder;

        protected IDictionary<string, TestStepParameterModel> Parameters
        {
            get { return _parametersHolder ?? (_parametersHolder = new Dictionary<string, TestStepParameterModel>()); }
            set { _parametersHolder = value; }
        }

        public IEnumerator<TestStepParameterModel> GetEnumerator()
        {
            return ToEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(TestStepParameterModel item)
        {
            Guard.MustFollow(!Contains(item));

            Parameters.Add(item.Key, item);
        }

        public void Clear()
        {
            Parameters.Clear();
        }

        public bool Contains(TestStepParameterModel item)
        {
            return Parameters.ContainsKey(item.Key) || ToEnumerable().Contains(item);
        }

        public void CopyTo(TestStepParameterModel[] array, int arrayIndex)
        {
            Guard.GreaterThanOrEqualsTo(array.Length-arrayIndex, _parametersHolder.Count(), "Request causes out-of-bound operation");

            var itemsToCopy = ToEnumerable().ToArray();
            for (var i = 0; i < itemsToCopy.Length; i++)
                array[i + arrayIndex] = itemsToCopy[i];
        }

        public bool Remove(TestStepParameterModel item)
        {
            return Parameters.Remove(item.Key);
        }

        public int Count
        {
            get { return Parameters.Count(); }
        }

        public bool IsReadOnly => false;

        protected IEnumerable<TestStepParameterModel> ToEnumerable()
        {
            return Parameters.Select(x => x.Value);
        }
    }
}