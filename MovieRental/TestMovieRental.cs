using NUnit.Framework;
using System;
namespace MovieRentalKata
{
    public class TestDifferentCombinations
    {
        [Test]
        public void TestCaseThreeRentals()
        {
            var movie01 = new Movie("Captain Marvel", PriceCode.NEW_RELEASE);
            var movie02 = new Movie("Aladdin", PriceCode.CHILDRENS);
            var movie03 = new Movie("Back To The Future", PriceCode.REGULAR);

            var customer = new Customer("Roger Ebert");

            var rental01 = new Rental(movie01, 2);
            var rental02 = new Rental(movie02, 3);
            var rental03 = new Rental(movie03, 2);

            customer.AddRental(rental01);
            customer.AddRental(rental02);
            customer.AddRental(rental03);

            string expected = "Rental Record for Roger Ebert\n\tCaptain Marvel\t6\n\tAladdin\t1.5\n\tBack To The Future\t2\nAmount owed is 9.5\nYou earned 4 frequent renter points";
            Assert.AreEqual(expected, customer.Statement());
        }

        [Test]
        public void TestNoRentals()
        {
            var customer = new Customer("Roger Ebert");
            string expected = "Rental Record for Roger Ebert\nAmount owed is 0\nYou earned 0 frequent renter points";
            Assert.AreEqual(expected, customer.Statement());
        }

    }

    public class TestDifferentDaysMovies
    {
        Customer customer;

        [SetUp]
        public void Setup()
        {
            customer = new Customer("John Chu");
        }

        [TestCase(1, "2", 1)]
        [TestCase(2, "2", 1)]
        [TestCase(3, "3.5", 1)]
        [TestCase(4, "5", 1)]
        public void TestRegular(int days, string price, int points)
        {
            string movieName = "Back to the Future";
            Movie movie = new Movie(movieName, PriceCode.REGULAR);
            var rental = new Rental(movie, days);
            customer.AddRental(rental);
            string expected = $"Rental Record for John Chu\n\t{movieName}\t{price}\nAmount owed is {price}\nYou earned {points} frequent renter points";
            Assert.AreEqual(expected, customer.Statement());
        }

        [TestCase(1, "3", 1)]
        [TestCase(2, "6", 2)]
        [TestCase(3, "9", 2)]
        [TestCase(4, "12", 2)]
        public void TestNewRelease(int days, string price, int points)
        {
            string movieName = "Captain Marvel";
            Movie movie = new Movie(movieName, PriceCode.NEW_RELEASE);
            var rental = new Rental(movie, days);
            customer.AddRental(rental);
            string expected = $"Rental Record for John Chu\n\t{movieName}\t{price}\nAmount owed is {price}\nYou earned {points} frequent renter points";
            Assert.AreEqual(expected, customer.Statement());
        }

        [TestCase(1, "1.5", 1)]
        [TestCase(2, "1.5", 1)]
        [TestCase(3, "1.5", 1)]
        [TestCase(4, "3", 1)]
        public void TestChildrens(int days, string price, int points)
        {
            string movieName = "Aladdin";
            Movie movie = new Movie(movieName, PriceCode.CHILDRENS);
            var rental = new Rental(movie, days);
            customer.AddRental(rental);
            string expected = $"Rental Record for John Chu\n\t{movieName}\t{price}\nAmount owed is {price}\nYou earned {points} frequent renter points";
            Assert.AreEqual(expected, customer.Statement());
        }
    }

}
