using FileSorter9000.Core.Helpers;

using System;
using System.Threading.Tasks;

namespace FileSorter9000.Core.Services
{
	// Added IdentityService for future use, faked out for now
	public class FakeIdentityService : IIdentityService
	{
		public event EventHandler LoggedIn;
		public event EventHandler LoggedOut;

		public Task<bool> AcquireTokenSilentAsync()
		{
			return Task.FromResult<bool>(true);
		}

		public Task<string> GetAccessTokenAsync(string[] scopes)
		{
			return Task.FromResult<string>("");
		}

		public Task<string> GetAccessTokenForGraphAsync()
		{
			return Task.FromResult<string>("");
		}

		public string GetAccountUserName()
		{
			return "";
		}

		public void InitializeWithAadAndPersonalMsAccounts()
		{
		}

		public void InitializeWithAadMultipleOrgs(bool integratedAuth = false)
		{
		}

		public void InitializeWithAadSingleOrg(string tenant, bool integratedAuth = false)
		{
		}

		public void InitializeWithPersonalMsAccount()
		{
		}

		public bool IsAuthorized()
		{
			return true;
		}

		public bool IsLoggedIn()
		{
			return true;
		}

		public Task<LoginResultType> LoginAsync()
		{
			return Task.FromResult<LoginResultType>(LoginResultType.Success);
		}

		public Task LogoutAsync()
		{
			return Task.FromResult(true);
		}
	}
}
