namespace Appos.Lib.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public override bool Equals(object? obj)
        {
            if(obj == null) return false;
            if(obj.GetType() != typeof(Customer)) return false;

            Customer customer = (Customer)obj;

            return  this.Name.Equals(customer.Name) &&
                    this.Email.Equals(customer) &&
                    this.Phone.Equals(customer.Phone);
                    
        }
    }
}