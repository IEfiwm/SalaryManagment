using Common.Helpers;
using Infrastructure.DbContexts;
using Infrastructure.Map;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace MyUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            const string dataAccessLayerAssemblyNamePattern = @"(Infrastructure)";

            var dalAssembly = TypeFinderHelper.GetTest(
                typeToSearch: typeof(IEntityTypeConfiguration<>),
                excludeTypes: new[]
                {
                    //typeof(BaseEntityMap<>),
                    //typeof(IdentityBaseEntityMap<>),
                    typeof(AuditBaseEntityMap<>)
                },
                assemblyNamePattern: dataAccessLayerAssemblyNamePattern);

            var test = "test => ";

            foreach (var item in dalAssembly)
            {
                test += item.FullName + "\n";
            }

            Console.WriteLine(test);
        }
    }
}
