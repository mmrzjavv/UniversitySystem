namespace Identity.Domain.Users.ValueObject;

public class MobileNumber
{
    public string Value { get; } 

    public MobileNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length != 11)
            throw new ArgumentException("Invalid mobile number format.");
        Value = value;
    }

    public override bool Equals(object? obj) =>
        obj is MobileNumber other && Value == other.Value;

    public override int GetHashCode() => Value.GetHashCode();
}
