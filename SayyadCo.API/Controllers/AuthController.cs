using Microsoft.AspNetCore.Mvc;
using SayyadCo.Application.Common.Models;
using SayyadCo.Application.Features.Auth.Commands.FacebookLogin;
using SayyadCo.Application.Features.Auth.Commands.ForgetPassword;
using SayyadCo.Application.Features.Auth.Commands.GoogleLogin;
using SayyadCo.Application.Features.Auth.Commands.Login;
using SayyadCo.Application.Features.Auth.Commands.RefreshToken;
using SayyadCo.Application.Features.Auth.Commands.Register;
using SayyadCo.Application.Features.Auth.Commands.ResendOtp;
using SayyadCo.Application.Features.Auth.Commands.ResendResetPasswordOtp;
using SayyadCo.Application.Features.Auth.Commands.ResetPassword;
using SayyadCo.Application.Features.Auth.Commands.VerifyEmail;
using SayyadCo.Application.Features.Auth.Commands.VerifyResetOtp;

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
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
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
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
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
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
            => HandleResult(await Mediator.Send(command));

        /// <summary>
        /// Refresh access token
        /// </summary>
        /// <remarks>
        /// Generates new Access Token and Refresh Token using an expired Access Token and valid Refresh Token.
        /// The old Refresh Token is invalidated automatically.
        ///
        /// Sample request:
        ///
        ///     POST /api/auth/refresh-token
        ///     {
        ///         "accessToken": "eyJhbGci...",
        ///         "refreshToken": "base64-refresh-token"
        ///     }
        /// </remarks>
        /// <response code="200">Returns new tokens</response>
        /// <response code="400">Invalid or expired tokens</response>
        /// <response code="404">Refresh token not found</response>
        [HttpPost("refresh-token")]
        [ProducesResponseType(typeof(RefreshTokenResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
            => HandleResult(await Mediator.Send(command));

        /// <summary>Google OAuth Login</summary>
        /// <remarks>
        /// Authenticates user with Google ID Token received from the frontend Google SDK.
        ///
        /// Sample request:
        ///
        ///     POST /api/auth/google-login
        ///     {
        ///         "idToken": "eyJhbGci..."
        ///     }
        /// </remarks>
        /// <response code="200">Login successful, returns tokens</response>
        /// <response code="400">Invalid Google token</response>
        [HttpPost("google-login")]
        [ProducesResponseType(typeof(GoogleLoginResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginCommand command)
            => HandleResult(await Mediator.Send(command));

        /// <summary>Facebook OAuth Login</summary>
        /// <remarks>
        /// Authenticates user with Facebook Access Token received from the frontend Facebook SDK.
        ///
        /// Sample request:
        ///
        ///     POST /api/auth/facebook-login
        ///     {
        ///         "accessToken": "EAAxxxxxxx..."
        ///     }
        /// </remarks>
        /// <response code="200">Login successful, returns tokens</response>
        /// <response code="400">Invalid Facebook token or no email on account</response>
        [HttpPost("facebook-login")]
        [ProducesResponseType(typeof(FacebookLoginResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> FacebookLogin([FromBody] FacebookLoginCommand command)
            => HandleResult(await Mediator.Send(command));

        /// <summary>Forgot password</summary>
        /// <remarks>
        /// Sends a 6-digit OTP to the user's email to reset their password.
        /// The returned `resetToken` is required for the reset password step.
        ///
        /// Sample request:
        ///
        ///     POST /api/auth/forgot-password
        ///     {
        ///         "email": "ahmed@example.com"
        ///     }
        /// </remarks>
        /// <response code="200">OTP sent successfully</response>
        /// <response code="400">Validation error</response>
        /// <response code="404">Email not found</response>
        [HttpPost("forgot-password")]
        [ProducesResponseType(typeof(ForgotPasswordResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand command)
            => HandleResult(await Mediator.Send(command));

        /// <summary>Verify reset OTP</summary>
        /// <remarks>
        /// Verifies the OTP sent to the user's email.
        /// On success, returns a passwordResetToken to be used in the reset password step.
        ///
        /// Sample request:
        ///
        ///     POST /api/auth/verify-reset-otp
        ///     {
        ///         "resetToken": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "otpCode": "123456"
        ///     }
        /// </remarks>
        /// <response code="200">OTP verified, returns passwordResetToken</response>
        /// <response code="400">Invalid or expired OTP</response>
        /// <response code="404">Reset token not found</response>
        [HttpPost("verify-reset-otp")]
        [ProducesResponseType(typeof(VerifyResetOtpResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> VerifyResetOtp([FromBody] VerifyResetOtpCommand command)
            => HandleResult(await Mediator.Send(command));


        /// <summary>Reset password</summary>
        /// <remarks>
        /// Resets the user's password using the passwordResetToken received after OTP verification.
        ///
        /// Sample request:
        ///
        ///     POST /api/auth/reset-password
        ///     {
        ///         "passwordResetToken": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "newPassword": "NewPassword@123",
        ///         "confirmPassword": "NewPassword@123"
        ///     }
        /// </remarks>
        /// <response code="200">Password reset successfully</response>
        /// <response code="400">Passwords don't match</response>
        /// <response code="404">Invalid or expired reset token</response>
        [HttpPost("reset-password")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
            => HandleResult(await Mediator.Send(command));

        /// <summary>Resend reset password OTP</summary>
        /// <remarks>
        /// Resends a new OTP to the user's email for password reset.
        ///
        /// - Can only be requested **once per minute**
        /// - New OTP expires after **10 minutes**
        ///
        /// Sample request:
        ///
        ///     POST /api/auth/resend-reset-otp
        ///     {
        ///         "resetToken": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
        ///     }
        /// </remarks>
        /// <response code="200">New OTP sent successfully</response>
        /// <response code="400">Resend cooldown active or account locked</response>
        /// <response code="404">Reset token not found</response>
        [HttpPost("resend-reset-otp")]
        [ProducesResponseType(typeof(ResendResetPasswordOtpResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ResendResetOtp([FromBody] ResendResetPasswordOtpCommand command)
            => HandleResult(await Mediator.Send(command));
    }
}
