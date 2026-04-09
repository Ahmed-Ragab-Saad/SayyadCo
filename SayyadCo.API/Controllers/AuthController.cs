using Microsoft.AspNetCore.Mvc;
using SayyadCo.Application.Features.Auth.Commands.Login;
using SayyadCo.Application.Features.Auth.Commands.Register;
using SayyadCo.Application.Features.Auth.Commands.ResendOtp;
using SayyadCo.Application.Features.Auth.Commands.VerifyEmail;

namespace SayyadCo.API.Controllers
{
    public class AuthController : CustomBaseController
    {
        /// <summary>
        /// Register a new user account
        /// </summary>
        /// <remarks>
        /// Creates a new user account and sends a 6-digit OTP to the provided email for verification.
        /// The returned `verificationToken` is required for email verification and OTP resend.
        ///
        /// **Flow:** Register → Receive OTP on email → Verify Email
        ///
        /// Sample request:
        ///
        ///     POST /api/auth/register
        ///     {
        ///         "firstName": "Ahmed",
        ///         "lastName": "Mohamed",
        ///         "email": "ahmed@example.com",
        ///         "password": "Password@123",
        ///         "confirmPassword": "Password@123"
        ///     }
        /// </remarks>
        /// <response code="200">Registration successful, OTP sent to email</response>
        /// <response code="400">Validation error or email already exists</response>
        [HttpPost("register")]
        [ProducesResponseType(typeof(RegisterResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
            => HandleResult(await Mediator.Send(command));


        /// <summary>
        /// Verify email using OTP code
        /// </summary>
        /// <remarks>
        /// Verifies the user's email using the OTP sent during registration or login.
        /// On success, returns JWT Access Token and Refresh Token.
        ///
        /// - OTP expires after **10 minutes**
        /// - Account locks for **5 minutes** after **5 failed attempts**
        ///
        /// Sample request:
        ///
        ///     POST /api/auth/verify-email
        ///     {
        ///         "verificationToken": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "otpCode": "123456"
        ///     }
        /// </remarks>
        /// <response code="200">Email verified successfully, returns tokens</response>
        /// <response code="400">Invalid or expired OTP</response>
        /// <response code="404">Verification token not found</response>
        [HttpPost("verify-email")]
        [ProducesResponseType(typeof(VerifyEmailResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailCommand command)
            => HandleResult(await Mediator.Send(command));


        /// <summary>
        /// Resend OTP verification code
        /// </summary>
        /// <remarks>
        /// Resends a new OTP to the user's email.
        ///
        /// - Can only be requested **once per minute**
        /// - New OTP expires after **10 minutes**
        /// - Previous OTPs are invalidated automatically
        ///
        /// Sample request:
        ///
        ///     POST /api/auth/resend-otp
        ///     {
        ///         "verificationToken": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
        ///     }
        /// </remarks>
        /// <response code="200">New OTP sent successfully</response>
        /// <response code="400">Resend cooldown active or account locked</response>
        /// <response code="404">Verification token not found</response>
        [HttpPost("resend-otp")]
        [ProducesResponseType(typeof(ResendOtpResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ResendOtp([FromBody] ResendOtpCommand command)
            => HandleResult(await Mediator.Send(command));


        /// <summary>
        /// Login to existing account
        /// </summary>
        /// <remarks>
        /// Authenticates the user and returns JWT Access Token and Refresh Token.
        ///
        /// If email is not verified, a new OTP is sent automatically and a `verificationToken`
        /// is returned with **403** status to redirect the user to the verification flow.
        ///
        /// Sample request:
        ///
        ///     POST /api/auth/login
        ///     {
        ///         "email": "ahmed@example.com",
        ///         "password": "Password@123"
        ///     }
        ///
        /// Sample response (unverified email - 403):
        ///
        ///     {
        ///         "error": "Email is not verified",
        ///         "verificationToken": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
        ///     }
        /// </remarks>
        /// <response code="200">Login successful, returns tokens</response>
        /// <response code="400">Invalid email or password</response>
        /// <response code="403">Email not verified, OTP sent to email</response>
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
            => HandleResult(await Mediator.Send(command));
    }
}
