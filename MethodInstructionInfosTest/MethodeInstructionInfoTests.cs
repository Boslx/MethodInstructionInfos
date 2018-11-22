using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MethodInstructionInfos;

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

namespace MethodInstructionInfosTest
{
    [TestClass]
    public class MethodeInstructionInfoTests
    {
        [TestMethod]
        public void staticMethode()
        {
            Action barActionA = StaticMethode.bar;
            var variables = barActionA.Method.GetHandledFields();
            Assert.IsFalse(variables.Any(x => x.Name=="x"));
            Assert.IsTrue(variables.Any(x => x.Name == "i"));
        }

        [TestMethod]
        public void publicMethodeStaticField()
        {
            var testB = new PublicMethodeStaticField();
            Action barActionB = testB.bar;
            var variables = barActionB.Method.GetHandledFields();
            Assert.IsFalse(variables.Any(x => x.Name == "x"));
            Assert.IsTrue(variables.Any(x => x.Name == "i"));
        }

        [TestMethod]
        public void publicMethodePrivateField()
        {
            var testC = new PublicMethodePrivateField();
            Action barActionC = testC.bar;
            var variables = barActionC.Method.GetHandledFields();
            Assert.IsFalse(variables.Any(x => x.Name == "x"));
            Assert.IsTrue(variables.Any(x => x.Name == "i"));
        }

        [TestMethod]
        public void publicMethodeBaseStaticField()
        {
            var testD = new publicMethodeBaseStaticField();
            Action barActionD = testD.bar;
            var variables = barActionD.Method.GetHandledFields();
            Assert.IsFalse(variables.Any(x => x.Name == "x"));
            Assert.IsTrue(variables.Any(x => x.Name == "i"));
        }

        [TestMethod]
        public void publicMethodeBasePrivateField()
        {
            var testE = new publicMethodeBasePrivateField();
            Action barActionE = testE.bar;
            var variables = barActionE.Method.GetHandledFields();
            Assert.IsFalse(variables.Any(x => x.Name == "x"));
            Assert.IsFalse(variables.Any(x => x.Name == "y"));
            Assert.IsTrue(variables.Any(x => x.Name == "i"));
        }

        [TestMethod]
        public void nestedClassStatic()
        {
            var testF = new nestedClassStatic();
            Action barActionE = testF.bar;
            var variables = barActionE.Method.GetHandledFields();
            Assert.IsFalse(variables.Any(x => x.Name == "x"));
            Assert.IsFalse(variables.Any(x => x.Name == "y"));
            Assert.IsTrue(variables.Any(x => x.Name == "i"));
        }

        [TestMethod]
        public void nestedClassPublic()
        {
            var testG = new nestedClassPublic();
            Action barActionE = testG.bar;
            var variables = barActionE.Method.GetHandledFields();
            Assert.IsFalse(variables.Any(x => x.Name == "x"));
            Assert.IsFalse(variables.Any(x => x.Name == "y"));
            Assert.IsTrue(variables.Any(x => x.Name == "i"));
        }

        [TestMethod]
        public void lambdaTestStatic()
        {
            var testH = new lambdaTestStatic();
            var variables = testH.bar.Method.GetHandledFields();
            Assert.IsFalse(variables.Any(x => x.Name == "x"));
            Assert.IsTrue(variables.Any(x => x.Name == "i"));
        }

        [TestMethod]
        public void lambdaTestPublic()
        {
            var testI = new lambdaTestPublic();
            var variables = testI.bar.Method.GetHandledFields();
            Assert.IsFalse(variables.Any(x => x.Name == "x"));
            Assert.IsTrue(variables.Any(x => x.Name == "i"));
        }

        [TestMethod]
        public void lambdaTestLocal()
        {
            var testJ = new lambdaTestLocal();
            var variables = testJ.bar.Method.GetHandledFields();
            Assert.IsFalse(variables.Any(x => x.Name == "x"));
            Assert.IsTrue(variables.Any(x => x.Name == "i"));
        }

        [TestMethod]
        public void methodeCallinMethode()
        {
            var testK = new methodeCallingMethode();
            Action barActionC = testK.bar;
            var variables = barActionC.Method.GetRecursiveHandledFields();
            Assert.IsFalse(variables.Any(x => x.Name == "x"));
            Assert.IsTrue(variables.Any(x => x.Name == "i"));
        }

        delegate void Del();
        [TestMethod]
        public void anonymousMethod()
        {
            int i;
            int x;
            Del testK = delegate () { i = 45; };
            var variables = testK.Method.GetHandledFields();
            Assert.IsFalse(variables.Any(xx => xx.Name == "x"));
            Assert.IsTrue(variables.Any(xx => xx.Name == "i"));
        }
    }

    public class StaticMethode
    {
        static int i;
        static int x;
        public static void bar() { i = 45; }
    }

    public class PublicMethodeStaticField
    {
        static int i;
        static int x;
        public void bar() { i = 45; }
    }

    public class PublicMethodePrivateField
    {
        int i;
        int x;
        public void bar() { i = 45; }
    }

    public class publicMethodeBaseStaticFieldBase
    {
        public static int i;
    }

    public class publicMethodeBaseStaticField : publicMethodeBaseStaticFieldBase
    {
        public void bar() { i = 45; }
    }

    public class publicMethodeBasePrivateFieldBase
    {
        public int i;
        public int y;
    }

    public class publicMethodeBasePrivateField : publicMethodeBasePrivateFieldBase
    {
        int x;
        public void bar() { i = 45; }
    }

    public class nestedClassStatic
    {
        public class nestedClass
        {
            public static int i;
            public static int x;
        }

        int y;
        public void bar() { nestedClass.i = 45; }
    }

    public class nestedClassPublic
    {
        public class nestedClass
        {
            public int i;
            public int x;
        }

        int y;
        nestedClass nesty = new nestedClass();
        public void bar() { nesty.i = 45; }
    }

    public class lambdaTestStatic
    {
        static int i = 0;
        static int x = 0;
        public Func<int> bar = new Func<int>(() => i);
    }

    public class lambdaTestPublic
    {
        public int i = 0;
        int x = 0;
        public Func<int> bar;
        public lambdaTestPublic()
        {
            bar = new Func<int>(() => i);
        }
    }

    public class lambdaTestLocal
    {
        public Func<int> bar;
        public lambdaTestLocal()
        {
            int i = 0;
            int x = 0;
            bar = new Func<int>(() => i);
        }
    }

    public class methodeCallingMethode
    {
        public void bar()
        {
            var testC = new PublicMethodePrivateField();
            Console.WriteLine("Test");
            testC.bar();
        }
    }
}
