namespace SayyadCo.Application.Common.Templates
{
    public static class EmailTemplates
    {
        public static string VerifyEmailOtpTemplate(string otpCode, string firstName) => $"""
            <!DOCTYPE html>
            <html>
            <body style="margin:0; padding:0; background-color:#f4f6f9; font-family: 'Segoe UI', Arial, sans-serif;">
              <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                  <td align="center" style="padding: 40px 0;">
                    <table width="500" cellpadding="0" cellspacing="0" style="background:#ffffff; border-radius:16px; overflow:hidden; box-shadow: 0 4px 24px rgba(0,0,0,0.08);">
                      
                      <!-- Header -->
                      <tr>
                        <td style="background: linear-gradient(135deg, #667eea 0%, #764ba2 100%); padding: 40px; text-align:center;">
                          <h1 style="color:#ffffff; margin:0; font-size:28px; font-weight:700;">SayyadCo</h1>
                          <p style="color:rgba(255,255,255,0.85); margin:8px 0 0; font-size:15px;">Email Verification</p>
                        </td>
                      </tr>

                      <!-- Body -->
                      <tr>
                        <td style="padding: 40px 48px;">
                          <p style="color:#374151; font-size:16px; margin:0 0 8px;">Hi <strong>{firstName}</strong> 👋</p>
                          <p style="color:#6b7280; font-size:15px; line-height:1.6; margin:0 0 32px;">
                            Thanks for signing up! Use the verification code below to confirm your email address.
                          </p>

                          <!-- OTP Box -->
                          <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                              <td align="center">
                                <div style="background:#f3f0ff; border: 2px dashed #764ba2; border-radius:12px; padding:24px 40px; display:inline-block;">
                                  <p style="margin:0 0 6px; color:#6b7280; font-size:13px; text-transform:uppercase; letter-spacing:2px;">Verification Code</p>
                                  <p style="margin:0; color:#4c1d95; font-size:42px; font-weight:800; letter-spacing:12px;">{otpCode}</p>
                                </div>
                              </td>
                            </tr>
                          </table>

                          <p style="color:#9ca3af; font-size:13px; text-align:center; margin:24px 0 0;">
                            ⏳ This code expires in <strong>10 minutes</strong>
                          </p>

                          <hr style="border:none; border-top:1px solid #f3f4f6; margin:32px 0;">

                          <p style="color:#9ca3af; font-size:13px; text-align:center; margin:0;">
                            If you didn't create an account, you can safely ignore this email.
                          </p>
                        </td>
                      </tr>

                      <!-- Footer -->
                      <tr>
                        <td style="background:#f9fafb; padding:20px; text-align:center;">
                          <p style="color:#d1d5db; font-size:12px; margin:0;">© 2025 SayyadCo. All rights reserved.</p>
                        </td>
                      </tr>

                    </table>
                  </td>
                </tr>
              </table>
            </body>
            </html>
            """;

        public static string ForgotPasswordOtpTemplate(string otpCode, string firstName) => $"""
            <!DOCTYPE html>
            <html>
            <body style="margin:0; padding:0; background-color:#f4f6f9; font-family: 'Segoe UI', Arial, sans-serif;">
              <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                  <td align="center" style="padding: 40px 0;">
                    <table width="500" cellpadding="0" cellspacing="0" style="background:#ffffff; border-radius:16px; overflow:hidden; box-shadow: 0 4px 24px rgba(0,0,0,0.08);">
                      
                      <!-- Header -->
                      <tr>
                        <td style="background: linear-gradient(135deg, #f97316 0%, #ea580c 100%); padding: 40px; text-align:center;">
                          <h1 style="color:#ffffff; margin:0; font-size:28px; font-weight:700;">SayyadCo</h1>
                          <p style="color:rgba(255,255,255,0.85); margin:8px 0 0; font-size:15px;">Reset Your Password</p>
                        </td>
                      </tr>

                      <!-- Body -->
                      <tr>
                        <td style="padding: 40px 48px;">
                          <p style="color:#374151; font-size:16px; margin:0 0 8px;">Hi <strong>{firstName}</strong> 👋</p>
                          <p style="color:#6b7280; font-size:15px; line-height:1.6; margin:0 0 32px;">
                            We received a request to reset your password. Use the verification code below to continue.
                          </p>

                          <!-- OTP Box -->
                          <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                              <td align="center">
                                <div style="background:#fff7ed; border: 2px dashed #ea580c; border-radius:12px; padding:24px 40px; display:inline-block;">
                                  <p style="margin:0 0 6px; color:#9a3412; font-size:13px; text-transform:uppercase; letter-spacing:2px;">Reset Code</p>
                                  <p style="margin:0; color:#c2410c; font-size:42px; font-weight:800; letter-spacing:12px;">{otpCode}</p>
                                </div>
                              </td>
                            </tr>
                          </table>

                          <p style="color:#9ca3af; font-size:13px; text-align:center; margin:24px 0 0;">
                            ⏳ This code expires in <strong>10 minutes</strong>
                          </p>

                          <hr style="border:none; border-top:1px solid #f3f4f6; margin:32px 0;">

                          <p style="color:#9ca3af; font-size:13px; text-align:center; margin:0;">
                            If you didn’t request a password reset, you can safely ignore this email.
                          </p>
                        </td>
                      </tr>

                      <!-- Footer -->
                      <tr>
                        <td style="background:#f9fafb; padding:20px; text-align:center;">
                          <p style="color:#d1d5db; font-size:12px; margin:0;">© 2025 SayyadCo. All rights reserved.</p>
                        </td>
                      </tr>

                    </table>
                  </td>
                </tr>
              </table>
            </body>
            </html>
            """;
    }
}
