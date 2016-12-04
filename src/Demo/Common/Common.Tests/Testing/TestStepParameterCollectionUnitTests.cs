using System;
using System.Collections.Generic;
using System.Linq;
using Common.Testing;
using NUnit.Framework;
using Saturn72.UnitTesting.Framework;

namespace Common.Tests.Testing
{
    internal class TestStepParameterCollectionTestObject : TestStepParameterCollection
    {
        public IDictionary<string, TestStepParameterModel> TestStepParameters
        {
            get { return Parameters; }
        }
    }

    public class TestStepParameterCollectionUnitTests
    {
        [Test]
        public void TestStepParameterCollection_GetEnumerator()
        {
            var tsc = new TestStepParameterCollection();
            const int totalItems = 3;
            for (var i = 0; i < totalItems; i++)
                tsc.Add(new TestStepParameterModel(i.ToString(), i));

            var enumerator = tsc.GetEnumerator();
            enumerator.ShouldNotBeNull();

            var t = 0;
            while (enumerator.MoveNext())
                t++;

            t.ShouldEqual(totalItems);
        }

        [Test]
        public void TestStepParameterCollection_Add_Throws()
        {
            //null
            var tspc = new TestStepParameterCollection();
            typeof(NullReferenceException).ShouldBeThrownBy(() => tspc.Add(null));

            //key not exists
            typeof(ArgumentNullException).ShouldBeThrownBy(() => tspc.Add(new TestStepParameterModel(null, null)));

            //On duplicate key
            var tsp = new TestStepParameterModel("key", "value");
            tspc.Add(tsp);
            typeof(InvalidOperationException).ShouldBeThrownBy(() => tspc.Add(tsp));
        }

        [Test]
        public void TestStepParameterCollection_Add_AddsTestStepParameter()
        {
            var tspKey = "Key";
            var tspValue = "Value";

            //null
            var tspc = new TestStepParameterCollectionTestObject();
            var tsp = new TestStepParameterModel(tspKey, tspValue);

            tspc.Add(tsp);

            tspc.TestStepParameters.Count.ShouldEqual(1);
            var prm = tspc.TestStepParameters.First();
            prm.Key.ShouldEqual(tspKey);
            prm.Value.ShouldEqual(tsp);
            prm.Value.PropertyValuesAreEquals(tsp);
        }


        [Test]
        public void TestStepParameterCollection_Clear_ClearsAllParameter()
        {
            var tspc = new TestStepParameterCollectionTestObject();
            const int totalItems = 3;
            for (var i = 0; i < totalItems; i++)
            {
                var key = i.ToString();
                tspc.TestStepParameters.Add(key, new TestStepParameterModel(key, i));
            }
            tspc.Clear();

            tspc.TestStepParameters.Count.ShouldEqual(0);
        }

        [Test]
        public void TestStepParameterCollection_Contains()
        {
            var tspc = new TestStepParameterCollectionTestObject();
            var key = "Key";
            var value = "value";

            var tsp = new TestStepParameterModel(key, value);
            tspc.TestStepParameters.Add(tsp.Key, tsp);

            //return true by specifiv key
            tspc.Contains(tsp).ShouldBeTrue();

            tspc.TestStepParameters.Add("dummyKey", tsp);

            //returns true on specific item
            tspc.Contains(tsp).ShouldBeTrue();
        }


        [Test]
        public void TestStepParameterCollection_CopyTo_Throws()
        {
            var tspc = new TestStepParameterCollectionTestObject();
            var maxElements = 10;
            for (var i = 0; i < maxElements; i++)
            {
                var tsp = new TestStepParameterModel(i.ToString(), i);
                tspc.TestStepParameters.Add(tsp.Key, tsp);
            }

            //on array smaller than content
            typeof(InvalidOperationException).ShouldBeThrownBy(
                () => tspc.CopyTo(new TestStepParameterModel[maxElements/2], 0));

            //on out of boud indexing
            typeof(InvalidOperationException).ShouldBeThrownBy(
                () => tspc.CopyTo(new TestStepParameterModel[maxElements], 3));
        }


        [Test]
        public void TestStepParameterCollection_CopyTo_Copies()
        {
            var tspc = new TestStepParameterCollectionTestObject();
            const int maxElements = 10;
            for (var i = 0; i < maxElements; i++)
            {
                var tsp = new TestStepParameterModel(i.ToString(), i);
                tspc.TestStepParameters.Add(tsp.Key, tsp);
            }

            //copy starts at 0
            var destArray1 = new TestStepParameterModel[maxElements];
            //on array smaller than content
            tspc.CopyTo(destArray1, 0);
            foreach (var da in destArray1)
                da.ShouldNotBeNull();

            var destArray2 = new TestStepParameterModel[maxElements*2];
            //on array smaller than content
            tspc.CopyTo(destArray2, maxElements);
            for (var i = maxElements; i < destArray2.Length; i++)

                destArray2[i].ShouldNotBeNull();
        }

        [Test]
        public void TestStepParameterCollection_Remove()
        {
            var tspc = new TestStepParameterCollectionTestObject();
            const int maxElements = 1;
            for (var i = 0; i < maxElements; i++)
            {
                var tsp = new TestStepParameterModel(i.ToString(), i);
                tspc.TestStepParameters.Add(tsp.Key, tsp);
            }

            //returns false when item not exists
            tspc.Remove(new TestStepParameterModel("eee", "fff")).ShouldBeFalse();

            //returns true when item exists
            var item = tspc.TestStepParameters.First().Value;
            tspc.Remove(item).ShouldBeTrue();
        }

        [Test]
        public void TestSteParameterCollection_Count()
        {
            var tspc = new TestStepParameterCollectionTestObject();
            const int maxElements = 236;
            for (var i = 0; i < maxElements; i++)
            {
                var tsp = new TestStepParameterModel(i.ToString(), i);
                tspc.TestStepParameters.Add(tsp.Key, tsp);
            }

            tspc.Count.ShouldEqual(maxElements);
        }

        [Test]
        public void TestSteParameterCollection_IsReadOnly()
        {
            var tspc = new TestStepParameterCollectionTestObject();
            const int maxElements = 2;
            for (var i = 0; i < maxElements; i++)
            {
                var tsp = new TestStepParameterModel(i.ToString(), i);
                tspc.TestStepParameters.Add(tsp.Key, tsp);
            }

            tspc.IsReadOnly.ShouldBeFalse();
        }
    }
}