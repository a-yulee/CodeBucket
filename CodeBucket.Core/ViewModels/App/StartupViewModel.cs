using System;
using CodeBucket.Core.Data;
using CodeBucket.Core.Services;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CodeBucket.Core.ViewModels.Accounts;
using BitbucketSharp;
using CodeBucket.Core.Utils;
using System.Reactive.Threading.Tasks;
using ReactiveUI;
using System.Reactive;
using Splat;

namespace CodeBucket.Core.ViewModels.App
{
    public class StartupViewModel : ReactiveObject
    {
        private readonly IApplicationService _applicationService;
        private readonly IAccountsService _accountsService;
        private readonly IAlertDialogService _alertDialogService;

        private bool _isLoggingIn;
        public bool IsLoggingIn
        {
            get { return _isLoggingIn; }
            private set { this.RaiseAndSetIfChanged(ref _isLoggingIn, value); }
        }

        private string _status;
        public string Status
        {
            get { return _status; }
            private set { this.RaiseAndSetIfChanged(ref _status, value); }
        }

        private Avatar _avatar;
        public Avatar Avatar
        {
            get { return _avatar; }
            private set { this.RaiseAndSetIfChanged(ref _avatar, value); }
        }

        public ReactiveCommand<object> GoToMenuCommand { get; } = ReactiveCommand.Create();

        public ReactiveCommand<object> GoToAccountsCommand { get; } = ReactiveCommand.Create();

        public ReactiveCommand<object> GoToLoginCommand { get; } = ReactiveCommand.Create();

        public ReactiveCommand<Unit> StartupCommand { get; }

        /// <summary>
        /// Gets the default account. If there is not one assigned it will pick the first in the account list.
        /// If there isn't one, it'll just return null.
        /// </summary>
        /// <returns>The default account.</returns>
        protected BitbucketAccount GetDefaultAccount()
        {
            return _accountsService.GetDefault();
        }

        public StartupViewModel(
            IAccountsService accountsService = null, 
            IApplicationService applicationService = null, 
            IAlertDialogService alertDialogService = null)
		{
            _accountsService = accountsService ?? Locator.Current.GetService<IAccountsService>();
			_applicationService = applicationService ?? Locator.Current.GetService<IApplicationService>();
            _alertDialogService = alertDialogService ?? Locator.Current.GetService<IAlertDialogService>();

            StartupCommand = ReactiveCommand.CreateAsyncTask(_ => Startup());
		}

        protected async Task Startup()
		{
			if (!_applicationService.Accounts.Any())
			{
                GoToLoginCommand.Execute(null);
				return;
			}

			var account = GetDefaultAccount();
			if (account == null)
			{
                GoToAccountsCommand.Execute(null);
				return;
			}

            if (string.IsNullOrEmpty(account.Token) || string.IsNullOrEmpty(account.RefreshToken))
            {
                await _alertDialogService.Alert("Welcome!", "CodeBucket is now OAuth compliant!\n\nFor your security, " +
                "you will now be prompted to login to Bitbucket via their OAuth portal. This will swap out your credentials" +
                " for an OAuth token you may revoke at any time!");
                GoToLoginCommand.Execute(null);
                return;
            }

            try
            {
                IsLoggingIn = true;
                Status = "Logging in as " + account.Username;

                var ret = await Client.GetRefreshToken(LoginViewModel.ClientId, LoginViewModel.ClientSecret, account.RefreshToken);
                if (ret == null)
                {
                    await _alertDialogService.Alert("Error!", "Unable to refresh OAuth token. Please login again.");
                    GoToLoginCommand.Execute(null);
                    return;
                }

                account.RefreshToken = ret.RefreshToken;
                account.Token = ret.AccessToken;
                _accountsService.Update(account);

                await AttemptLogin(account);

                GoToMenuCommand.Execute(null);
            }
            catch (Exception e)
            {
                _alertDialogService.Alert("Error!", "Unable to login successfully: " + e.Message)
                    .ToObservable()
                    .Subscribe(_ => GoToAccountsCommand.ExecuteIfCan());
            }
            finally
            {
                IsLoggingIn = false;
            }
		}

        private async Task AttemptLogin(BitbucketAccount account)
        {
            Uri accountAvatarUri = null;
            var avatarUrl = account.AvatarUrl;
            if (!string.IsNullOrEmpty(avatarUrl))
            {
                var match = Regex.Match(avatarUrl, @"&s=(\d+)", RegexOptions.IgnoreCase);
                if (match.Success && match.Groups.Count > 1)
                    avatarUrl = avatarUrl.Replace(match.Groups[0].Value, "&s=128");
            }

            if (Uri.TryCreate(avatarUrl, UriKind.Absolute, out accountAvatarUri))
                Avatar = new Avatar(accountAvatarUri.AbsoluteUri);

            var client = await LoginAccount(account);
            _applicationService.ActivateUser(account, client);
        }

        public async Task<Client> LoginAccount(BitbucketAccount account)
        {
            //Create the client
            var client = Client.WithBearerAuthentication(account.Token);
            var user = await client.Users.GetUser();
            account.Username = user.Username;
            account.AvatarUrl = user.Links.Avatar.Href.Replace("/avatar/32", "/avatar/64");
            _accountsService.Update(account);
            return client;
        }
    }
}

