using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InMemoryDb
{
    [TestClass]
    public class TestByteComparisson
    {
        [TestMethod]
        public void Test_UsingEquals()
        {
            Guid someId = Guid.NewGuid();
            byte[] randomBytes = someId.ToByteArray();
            byte[] someIdBytes = someId.ToByteArray();
            
            // Assure the references are different
            Assert.AreNotSame(randomBytes, someIdBytes);
            // Assure the content is the same
            Assert.IsTrue(randomBytes.SequenceEqual(someIdBytes));

            DbContextOptionsBuilder dbContextOptionsBuilder = new DbContextOptionsBuilder();
            dbContextOptionsBuilder.UseInMemoryDatabase("Test_UsingEquals");

            using (var context = new DatabaseContext(dbContextOptionsBuilder.Options))
            {
                context.Tables.Add(new Table() { Field = randomBytes });
                context.SaveChanges();
            }

            using (var context = new DatabaseContext(dbContextOptionsBuilder.Options))
            {
                var item = context.Tables.SingleOrDefault(t => t.Field == someIdBytes);
                Assert.IsNotNull(item, "Equals == failed");
            }

        }

        [TestMethod]
        public void Test_UsingSequenceEquals ()
        {
            Guid someId = Guid.NewGuid();
            byte[] randomBytes = someId.ToByteArray();
            byte[] someIdBytes = someId.ToByteArray();

            // Assure the references are different
            Assert.AreNotSame(randomBytes, someIdBytes);
            // Assure the content is the same
            Assert.IsTrue(randomBytes.SequenceEqual(someIdBytes));

            DbContextOptionsBuilder dbContextOptionsBuilder = new DbContextOptionsBuilder();
            dbContextOptionsBuilder.UseInMemoryDatabase("Test_UsingSequenceEquals");

            // Add randomBytes
            using (var context = new DatabaseContext(dbContextOptionsBuilder.Options))
            {
                context.Tables.Add(new Table() { Field = randomBytes });
                context.SaveChanges();
            }

            // Query someIdBytes
            using (var context = new DatabaseContext(dbContextOptionsBuilder.Options))
            {
                var item = context.Tables.SingleOrDefault(t => t.Field.SequenceEqual(someIdBytes));
                Assert.IsNotNull(item, "SequenceEqual failed");
            }

        }
    }
}
