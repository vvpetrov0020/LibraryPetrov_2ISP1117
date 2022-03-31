using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Library_Petrov_TestMethod_Debt
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TrueRecord()
        {
            //arrange
            DateTime dateStart = new DateTime(2021, 03, 29);
            double price = 1000;
            double ex = 3350;
            //act
            double res = LibraryPetrov_2ISP1117.ClassHelper.LessIssue.Debt(price, dateStart);
            //assert
            Assert.AreEqual(ex, res);
        }
        [TestMethod]
        public void FutureData()
        {
            //arrange
            DateTime dateStart = new DateTime(2022, 04, 29);
            double price = 1000;
            double ex = 0;
            //act
            double res = LibraryPetrov_2ISP1117.ClassHelper.LessIssue.Debt(price, dateStart);
            //assert
            Assert.AreEqual(ex, res);
        }

        [TestMethod]
        public void InThisMonth()
        {
            //arrange
            DateTime dateStart = new DateTime(2022, 03, 22);
            double price = 1000;
            double ex = 0;
            //act
            double res = LibraryPetrov_2ISP1117.ClassHelper.LessIssue.Debt(price, dateStart);
            //assert
            Assert.AreEqual(ex, res);
        }

        [TestMethod]
        public void OneDayAfter()
        {
            //arrange
            DateTime dateStart = new DateTime(2022, 02, 28);
            double price = 1000;
            double ex = 0;
            //act
            double res = LibraryPetrov_2ISP1117.ClassHelper.LessIssue.Debt(price, dateStart);
            //assert
            Assert.AreEqual(ex, res);
        }

        [TestMethod]
        public void ZeroPrice()
        {
            //arrange
            DateTime dateStart = new DateTime(2022, 02, 28);
            double price = 0;
            double ex = 0;
            //act
            double res = LibraryPetrov_2ISP1117.ClassHelper.LessIssue.Debt(price, dateStart);
            //assert
            Assert.AreEqual(ex, res);
        }

        [TestMethod]
        public void MinusPrice()
        {
            //arrange
            DateTime dateStart = new DateTime(2022, 03, 28);
            double price = -100;
            double ex = 0;
            //act
            double res = LibraryPetrov_2ISP1117.ClassHelper.LessIssue.Debt(price, dateStart);
            //assert
            Assert.AreEqual(ex, res);
        }

        [TestMethod]
        public void NextMonth()
        {
            //arrange
            DateTime dateStart = new DateTime(2022, 02, 28);
            double price = 100;
            double ex = 1;
            //act
            double res = LibraryPetrov_2ISP1117.ClassHelper.LessIssue.Debt(price, dateStart);
            //assert
            Assert.AreEqual(ex, res);
        }
        [TestMethod]
        public void DayInDay()
        {
            //arrange
            DateTime dateStart = new DateTime(2022, 03, 31);
            double price = 100;
            double ex = 0;
            //act
            double res = LibraryPetrov_2ISP1117.ClassHelper.LessIssue.Debt(price, dateStart);
            //assert
            Assert.AreEqual(ex, res);
        }
        [TestMethod]
        public void TenYearsDate()
        {
            //arrange
            DateTime dateStart = new DateTime(2012, 03, 31);
            double price = 100;
            double ex = 3622;
            //act
            double res = LibraryPetrov_2ISP1117.ClassHelper.LessIssue.Debt(price, dateStart);
            //assert
            Assert.AreEqual(ex, res);
        }

        [TestMethod]
        public void LastThousand()
        {
            //arrange
            DateTime dateStart = new DateTime(1990, 03, 31);
            double price = 100;
            double ex = 11658;
            //act
            double res = LibraryPetrov_2ISP1117.ClassHelper.LessIssue.Debt(price, dateStart);
            //assert
            Assert.AreEqual(ex, res);
        }
    }
}
