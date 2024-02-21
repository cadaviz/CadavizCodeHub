namespace CadavizCodeHub.Framework.ValueObjects
{
    public class Email : ValueObject<Email>
    {
        public Email(string email)
        {
            Value = email;
            //ValidationContract.For(this).IsEmail(x => x.Valor, nameof(email));
        }

        public string Value { get; }

        public static implicit operator Email(string value)
        {
            return new Email(value);
        }
    }
}
