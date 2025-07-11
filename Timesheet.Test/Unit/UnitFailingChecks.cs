namespace Timesheet.Test.Unit;

using Moq;
using Timesheet.DB;
using Timesheet.Service;
using Timesheet.Models.Auth;

public class UnitFailingChecks
{

    // Create the mocked AuthDB
    private readonly Mock<IAuthDB> _authDBMock = new Mock<IAuthDB>();

    // Create an instances of AuthService
    private AuthService? _authService;

    // This is a test method named testIncorrectLogin that checks the behavior of AuthService for an incorrect login.
    [Test]
    public void testIncorrectLogin()
    {
        // Mocks the behavior of authDB.checkLogin() method for a fake login attempt.
        _authDBMock.Setup(_authDBMock => _authDBMock.CheckLogin(It.IsAny<string>(), It.IsAny<string>())).Returns(new LoginResult(false, null, 0));

        // Calls the login method of authService with fake credentials.
        _authService = new AuthService(_authDBMock.Object);
        (int code, Credentials credentials) = _authService.Login("fake@test.com", "password");

        // Verifies that the authDB.checkLogin() method is called exactly once with specified arguments.
        _authDBMock.Verify(_authDBMock => _authDBMock.CheckLogin("fake@test.com", "password"), Times.Once);
    }

    // This is another test method named testValidTokenReturnsOk to validate a session token.
    [Test]
    public void testValidTokenReturnsOk()
    {
        // Mocks the behavior of authDB.checkSession() method for a valid session token.
        _authDBMock.Setup(_authDBMock => _authDBMock.CheckSession("abc123", DateTime.Parse("3001-01-01"))).Returns(true);

        // Calls the validate method of authService with a token and date.
        _authService = new AuthService(_authDBMock.Object);
        bool response = _authService.CheckSession("abc", DateTime.Parse("3001-01-01"));

        // Asserts that the response from authService.validate() is false.
        Assert.That(response, Is.False);
    }
}
