//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Inha.Commons.OTP
//{
//    public static class TCISOTP
//    {
//        private static int decKey = int.Parse("AAA", System.Globalization.NumberStyles.HexNumber);
//        public static int OTP(int id,
//                              OTPMinuteTypes type)
//        {
//            var dt = DateTime.Now;
//            var result = (id * decKey) ^ ((dt.Day * 100 + dt.Month) * 100 + dt.Hour) * 10 + FindNumberKey(dt.Minute, (int)type);
//            string s = $"Input: {id} - Type: {type} - Output: {result}";
//            return result;
//        }

//        /// <summary>
//        ///     Chuyển kiểu long sang otp. Giả sử input có dạng 12345678901234. Khi đó sẽ lấy 3 ký tự 1 tính từ trái sang phải để
//        ///     chuyển, rồi ghép kết quả với nhau cách nhau bằng dấu -
//        /// </summary>
//        /// <param name="id"></param>
//        /// <param name="type"></param>
//        /// <returns></returns>
//        public static string OtpString(long id,
//                                       OTPMinuteTypes type)
//        {
//            var source = id.ToString();
//            List<int> ids = new List<int>();
//            while (true)
//            {
//                if (source.Length >= 3)
//                {
//                    ids.Add(Convert.ToInt32(source.Substring(0, 3)));
//                    source = source.Remove(0, 3);
//                }
//                else
//                {
//                    if (!string.IsNullOrEmpty(source))
//                    {
//                        ids.Add(Convert.ToInt32(source));
//                    }
//                    break;
//                }
//            }

//            StringBuilder builder = new StringBuilder();
//            string condition = string.Empty;
//            foreach (var i in ids)
//            {
//                var otp = OTP(i, type);
//                builder.AppendFormat("{0}{1}", condition, otp);
//                condition = "-";
//            }

//            return builder.ToString();
//        }

//        public static int RetrievalOTP(int otp,
//                                       OTPMinuteTypes type)
//        {
//            var dt = DateTime.Now;
//            var result = (int)Math.Ceiling((otp ^ (((dt.Day * 100 + dt.Month) * 100 + dt.Hour) * 10 + FindNumberKey(dt.Minute, (int)type))) / (decKey * 1.0));
//            string s = $"Input: {otp} - Type: {type} - Output: {result}";
//            return result;
//        }

//        public static long RetrievalLongOtp(string longOtp,
//                                            OTPMinuteTypes type)
//        {
//            StringBuilder builder = new StringBuilder();
//            var otps = longOtp.Split("-");
//            foreach (var otp in otps)
//            {
//                builder.AppendFormat(RetrievalOTP(Convert.ToInt32(otp), type)
//                                             .ToString());
//            }

//            return Convert.ToInt64(builder.ToString());
//        }

//        private static int FindNumberKey(int currentMinutes,
//                                         int percent)
//        {
//            const int TOTAL_SENCOND_ON_MINUTE = 60;
//            int ratio = TOTAL_SENCOND_ON_MINUTE / percent;
//            int i = 1;
//            while (i * ratio <= TOTAL_SENCOND_ON_MINUTE)
//            {
//                if (currentMinutes <= (i * ratio))
//                    return i;
//                i++;
//            }

//            return 0;
//        }
//    }

//    public enum OTPMinuteTypes
//    {
//        /// <summary>
//        /// Mã OTP sẽ thay đổi sau 15 phút
//        /// </summary>
//        EXTRA_SMALL = 4,

//        /// <summary>
//        /// Mã OTP sẽ thay đổi sau 10 phút
//        /// </summary>
//        SMALL = 6,

//        /// <summary>
//        /// Mã OTP sẽ thay đổi sau 6 phút
//        /// </summary>
//        ELONGATE_SMALL = 10,

//        /// <summary>
//        /// Mã OTP sẽ thay đổi sau 5 phút
//        /// </summary>
//        MEDIUM = 12,

//        /// <summary>
//        /// Mã OTP sẽ thay đổi sau 3 phút
//        /// </summary>
//        LARGE = 20,

//        /// <summary>
//        /// Mã OTP sẽ thay đổi sau 2 phút
//        /// </summary>
//        MEDIUM_PLUS = 30,

//        /// <summary>
//        /// Mã OTP sẽ thay đổi sau 1 phút
//        /// </summary>
//        MAXIMUM = 60

//    }
//}
