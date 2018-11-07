using System;
using System.Collections.Generic;

namespace MovieRentalKata
{
    enum PriceCode
    {
        REGULAR,
        NEW_RELEASE,
        CHILDRENS
    }   

    class Movie {
        private String title;
        private PriceCode priceCode;

        public Movie(String title, PriceCode priceCode) {
            this.title = title;
            this.priceCode = priceCode;
        }

        public PriceCode PriceCode {
            get => priceCode;
            set => priceCode = value;
        }

        public String Title {
            get => title;
        }
    }

    class Rental {
        private Movie movie;
        private int daysRented;

        public Rental(Movie movie, int daysRented) {
            this.movie = movie;
            this.daysRented = daysRented;
        }

        public int DaysRented {
            get => daysRented;
        }

        public Movie Movie {
            get => movie;
        }
    }

    class Customer {
        private String name;
        private List<Rental> rentals = new List<Rental>();

        public Customer(String name) {
            this.name = name;
        }

        public void AddRental(Rental rental) {
            rentals.Add(rental);
        }

        public String Name {
            get => name;
        }

        public String Statement() {
            double totalAmount = 0;
            int frequentRenterPoints = 0;

            String result = "Rental Record for " + Name + "\n";

            foreach (var rental in rentals) {
                double thisAmount = 0;

                // Determine amounts for each line
                switch (rental.Movie.PriceCode) {
                    case PriceCode.REGULAR:
                        thisAmount += 2;
                        if (rental.DaysRented > 2)
                            thisAmount += (rental.DaysRented - 2) * 1.5;
                        break;

                    case PriceCode.NEW_RELEASE:
                        thisAmount += rental.DaysRented * 3;
                        break;

                    case PriceCode.CHILDRENS:
                        thisAmount += 1.5;
                        if (rental.DaysRented > 3)
                            thisAmount += (rental.DaysRented - 3) * 1.5;
                        break;
                }

                // Add frequent renter points
                frequentRenterPoints++;

                // Add bonus for a two day new release rental
                if ((rental.Movie.PriceCode == PriceCode.NEW_RELEASE) && rental.DaysRented > 1)
                    frequentRenterPoints++;

                // Show figures for this rental
                result += "\t" + rental.Movie.Title + "\t" + thisAmount.ToString() + "\n";

                totalAmount += thisAmount;
            }

            // Add footer lines
            result += "Amount owed is " + totalAmount.ToString() + "\n";
            result += "You earned " + frequentRenterPoints.ToString() + " frequent renter points";

            return result;
        }
    }


    class MainClass
    {
        public static void Main(string[] args)
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

            Console.WriteLine(customer.Statement());

        }
    }
}
