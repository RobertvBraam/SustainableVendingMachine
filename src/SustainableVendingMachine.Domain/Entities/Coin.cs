using System;

namespace SustainableVendingMachine.Domain.Entities
{
    public class Coin : IEquatable<Coin>
    {
        private readonly decimal _value;

        public static Coin TenCents => new Coin(0.10m);
        public static Coin TwentyCents => new Coin(0.20m);
        public static Coin FiftyCents => new Coin(0.50m);
        public static Coin OneEuro => new Coin(1.00m);

        private Coin(decimal value)
        {
            _value = value;
        }

        #region Helper Methods
        public static implicit operator decimal(Coin coin)
        {
            return coin._value;
        }

        public bool Equals(Coin other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _value == other._value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Coin)obj);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
        #endregion
    }
}