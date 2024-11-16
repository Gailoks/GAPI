namespace GAPI.Domain
{
	public interface IPasswordService
    {
        public byte[] ComputeHash(string password);

        public bool Verify(byte[] digest, string password);
    }
}
