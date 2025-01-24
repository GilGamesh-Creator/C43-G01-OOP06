namespace OOP06
{
    #region FirstProject
           public class Point3D : ICloneable, IComparable<Point3D>
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public Point3D(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Point3D() : this(0, 0, 0) { }

        public override string ToString()
        {
            return $"Point Coordinates: ({X}, {Y}, {Z})";
        }

        public override bool Equals(object obj)
        {
            if (obj is Point3D point)
                return X == point.X && Y == point.Y && Z == point.Z;
            return false;
        }

        public override int GetHashCode()
        {
            return (X, Y, Z).GetHashCode();
        }

        public static bool operator ==(Point3D p1, Point3D p2)
        {
            return p1.Equals(p2);
        }

        public static bool operator !=(Point3D p1, Point3D p2)
        {
            return !p1.Equals(p2);
        }

        public object Clone()
        {
            return new Point3D(X, Y, Z);
        }

        public int CompareTo(Point3D other)
        {
            if (X == other.X) return Y.CompareTo(other.Y);
            return X.CompareTo(other.X);
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {
                Console.WriteLine("Enter coordinates for Point 1 (x y z): ");
                var coords1 = Console.ReadLine().Split().Select(int.Parse).ToArray();
                var P1 = new Point3D(coords1[0], coords1[1], coords1[2]);

                Console.WriteLine("Enter coordinates for Point 2 (x y z): ");
                var coords2 = Console.ReadLine().Split().Select(int.Parse).ToArray();
                var P2 = new Point3D(coords2[0], coords2[1], coords2[2]);

                Console.WriteLine(P1.ToString());
                Console.WriteLine(P2.ToString());

                Console.WriteLine(P1 == P2 ? "Points are equal" : "Points are not equal");

                Point3D[] points = { P1, P2, new Point3D(5, 3, 8), new Point3D(1, 4, 2) };
                Array.Sort(points);
                Console.WriteLine("Sorted Points:");
                foreach (var point in points)
                    Console.WriteLine(point.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Input: " + ex.Message);
            }
        }
    }


    #endregion
    #region SecondProject
public static class Maths
    {
        public static int Add(int a, int b) => a + b;
        public static int Subtract(int a, int b) => a - b;
        public static int Multiply(int a, int b) => a * b;
        public static double Divide(double a, double b)
        {
            if (b == 0)
                throw new DivideByZeroException("Cannot divide by zero.");
            return a / b;
        }
    }

    class Program2
    {
        static void Main()
        {
            Console.WriteLine("Addition: " + Maths.Add(10, 20));
            Console.WriteLine("Subtraction: " + Maths.Subtract(30, 15));
            Console.WriteLine("Multiplication: " + Maths.Multiply(6, 7));
            Console.WriteLine("Division: " + Maths.Divide(50, 5));
        }
    }

    #endregion
    #region ThirdProject
    // Part 1: Abstract Discount Class
    public abstract class Discount
    {
        public string Name { get; set; }
        public abstract decimal CalculateDiscount(decimal price, int quantity);
    }

    // Part 2: Specific Discounts
    public class PercentageDiscount : Discount
    {
        public decimal Percentage { get; set; }
        public PercentageDiscount(decimal percentage) { Name = "Percentage Discount"; Percentage = percentage; }
        public override decimal CalculateDiscount(decimal price, int quantity)
        {
            return price * quantity * (Percentage / 100);
        }
    }

    public class FlatDiscount : Discount
    {
        public decimal FlatAmount { get; set; }
        public FlatDiscount(decimal flatAmount) { Name = "Flat Discount"; FlatAmount = flatAmount; }
        public override decimal CalculateDiscount(decimal price, int quantity)
        {
            return FlatAmount * Math.Min(quantity, 1);
        }
    }

    public class BuyOneGetOneDiscount : Discount
    {
        public BuyOneGetOneDiscount() { Name = "Buy One Get One Discount"; }
        public override decimal CalculateDiscount(decimal price, int quantity)
        {
            return (price / 2) * (quantity / 2);
        }
    }

    // Part 3: Discount Applicability
    public abstract class User
    {
        public string Name { get; set; }
        public abstract Discount GetDiscount();
    }

    public class RegularUser : User
    {
        public RegularUser(string name) { Name = name; }
        public override Discount GetDiscount()
        {
            return new PercentageDiscount(5);
        }
    }

    public class PremiumUser : User
    {
        public PremiumUser(string name) { Name = name; }
        public override Discount GetDiscount()
        {
            return new FlatDiscount(100);
        }
    }

    public class GuestUser : User
    {
        public GuestUser() { Name = "Guest"; }
        public override Discount GetDiscount()
        {
            return null; // No discount
        }
    }
    //part 04 integration
    public class Program3
    {
        public static void Main(string[] args)
        {

            Console.WriteLine("Enter user type (Regular, Premium, or Guest):");
            string userType = Console.ReadLine();

            User user;
            switch (userType)
            {
                case "Regular":
                    user = new RegularUser("Regular User");
                    break;
                case "Premium":
                    user = new PremiumUser("Premium User");
                    break;
                default:
                    user = new GuestUser();
                    break;
            }

            Console.WriteLine("Enter product price:");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Enter product quantity:");
            int quantity = int.Parse(Console.ReadLine());

            Discount discount = user.GetDiscount();
            decimal discountAmount = 0;
            if (discount != null)
            {
                discountAmount = discount.CalculateDiscount(price, quantity);
            }

            decimal finalPrice = price * quantity - discountAmount;

            Console.WriteLine($"Original Price: {price * quantity}");
            Console.WriteLine($"Discount: {discountAmount}");
            Console.WriteLine($"Final Price: {finalPrice}");
        }
    }
    #endregion

}
