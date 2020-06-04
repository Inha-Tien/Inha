using Inha.Commons.OTP.OtpLibrary;
using System;
using System.Text;

namespace Inha.Commons.OTP
{
    /// <summary>
    /// Cung cấp các function phục vụ các phương thức tạo otp riêng cho IDS
    /// </summary>
    public static class IDSOtp
    {
        /// <summary>
        /// Tạo OTP
        /// </summary>
        /// <param name="key">Key-Mã object (id,guid)</param>
        /// <param name="step">Mã OTP sẽ được tạo mới sau [step] giây </param>
        /// <param name="mode">Các kiểu mã hóa</param>
        /// <param name="totpSize">Chiều dài mã OTP</param>
        /// <param name="timeCorrection">Thời gian</param>
        /// <returns></returns>
        public static string GenerateOtpCode(string key, int step = 30, OtpHashMode mode = OtpHashMode.Sha1, int totpSize = 8, TimeCorrection timeCorrection = null)
        {
            Totp otpCalc = new Totp(Encoding.UTF8.GetBytes(key), step, OtpHashMode.Sha1, totpSize, timeCorrection);
            return otpCalc.ComputeTotp(DateTime.Now);
        }

        /// <summary>
        /// Verify OTP
        /// </summary>
        /// <param name="key"></param>
        /// <param name="otp"></param>
        /// <param name="step"></param>
        /// <param name="mode"></param>
        /// <param name="totpSize"></param>
        /// <param name="timeCorrection"></param>
        /// <returns></returns>
        public static bool IsVerify(string key, string otp, int step = 30, OtpHashMode mode = OtpHashMode.Sha1, int totpSize = 8, TimeCorrection timeCorrection = null)
        {
            Totp otpCalc = new Totp(Encoding.UTF8.GetBytes(key), step, OtpHashMode.Sha1, totpSize, timeCorrection);
            return otpCalc.VerifyTotp(DateTime.Now, otp, out long timestemp);
        }
    }
}
