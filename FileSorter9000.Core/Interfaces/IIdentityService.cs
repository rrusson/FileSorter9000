using FileSorter9000.Core.Helpers;

using System;
using System.Threading.Tasks;

namespace FileSorter9000.Core.Services
{
	public interface IIdentityService
	{
		event EventHandler LoggedIn;
		event EventHandler LoggedOut;

		Task<bool> AcquireTokenSilentAsync();
		Task<string> GetAccessTokenAsync(string[] scopes);
		Task<string> GetAccessTokenForGraphAsync();
		string GetAccountUserName();
		void InitializeWithAadAndPersonalMsAccounts();
		void InitializeWithAadMultipleOrgs(bool integratedAuth = false);
		void InitializeWithAadSingleOrg(string tenant, bool integratedAuth = false);
		void InitializeWithPersonalMsAccount();
		bool IsAuthorized();
		bool IsLoggedIn();
		Task<LoginResultType> LoginAsync();
		Task LogoutAsync();
	}
}