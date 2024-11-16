namespace GAPI.Domain
{
	public class User
	{
		public User(string email, byte[] passwordDigest, string name)
		{
			Email = email;
			PasswordDigest = passwordDigest;
			Name = name;
		}

		
		public string Email { get; private set; }

		public byte[] PasswordDigest { get; set; }

		public string Name { get; private set; }

		public DateTimeOffset? SubscriptionUntil { get; private set; }

		public bool IsSubscribed => DateTimeOffset.UtcNow <= SubscriptionUntil;


		public void Rename(string newName)
		{
			Name = newName;
		}

		public void AddSubscription(TimeSpan time)
		{
			var now = DateTimeOffset.UtcNow;
			SubscriptionUntil ??= now;
			SubscriptionUntil = SubscriptionUntil.Value.Add(time);
		}
	}
}
