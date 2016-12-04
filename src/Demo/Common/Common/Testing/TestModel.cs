using System.Collections;
using System.Collections.Generic;

namespace Common.Testing
{
    public class TestModel
    {
        private ICollection<TestStepModel> _testSteps;

        public ICollection<TestStepModel> TestSteps { get
        {
            return _testSteps ?? (_testSteps = new List<TestStepModel>());
        }
            protected set { _testSteps = value; }
        }

        public string Name { get; set; }
    }
}