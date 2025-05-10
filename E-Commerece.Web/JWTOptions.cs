namespace E_Commerece.Web
{
    public class JWTOptions
    {
        public string Issuer { get; internal set; }
        public string Audience { get; internal set; }
        public char[] SecretKey { get; internal set; }
    }
}