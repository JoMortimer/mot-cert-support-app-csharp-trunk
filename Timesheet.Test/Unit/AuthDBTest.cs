namespace Timesheet.Test.Unit;
using Timesheet.Models.Auth;
using Timesheet.DB;

public class AuthDBTest
{

   private AuthDB _authDB = new AuthDB();

   [SetUp]
   public void Setup()
   {
       PrepareDB prepareDB = new PrepareDB();
       prepareDB.SeedUsers();
   }

   [Test]
   public void CheckLoginReturnsPostiveResult()
   {
       LoginResult loginResult = _authDB.CheckLogin("admin@test.com", "password123");

       Assert.That(loginResult.isUser, Is.True);
   }

}