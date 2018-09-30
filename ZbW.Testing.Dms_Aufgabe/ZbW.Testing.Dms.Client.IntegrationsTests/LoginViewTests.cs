using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using NUnit.Framework;
using Prism.Commands;
using Prism.Mvvm;
using ZbW.Testing.Dms.Client.ViewModels;
using ZbW.Testing.Dms.Client.Views;

namespace ZbW.Testing.Dms.Client.IntegrationsTests
{
    internal class LoginViewTests
    {
        private const string Username = "simo";
        [Test]
        public void Login_Username_isDisabled()
        {
            // arrange
            var loginViewModel = new LoginViewModel(null);

            // act
            var result = loginViewModel.CmdLogin.CanExecute();

            // assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void Login_Username_isEnabled()
        {
            // arrange
            var loginViewModel = new LoginViewModel(null)
            {

                // act
                Benutzername = Username
            };
            var result = loginViewModel.CmdLogin.CanExecute();

            // assert
            Assert.That(result, Is.True);
        }
    }
}
