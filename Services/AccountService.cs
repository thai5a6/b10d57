using System.Security.Cryptography;
using System.Text;
using TaiSanHV.Models;

namespace TaiSanHV.Services;

/// <summary>Dịch vụ tài khoản lưu trong bộ nhớ để demo. Thay bằng DbContext khi có CSDL.</summary>
public class AccountService
{
    public Account Current { get; private set; }

    public AccountService()
    {
        Current = new Account
        {
            Id = 1,
            Username = "nguyenvana",
            PasswordHash = Hash("Admin@123"),
            DisplayName = "Nguyễn Văn A",
            Tagline = "K45",
            FullName = "Nguyễn Văn A",
            Email = "nguyenvana@hvannd.edu.vn",
            Phone = "0912345678",
            StaffCode = "HV0001",
            Rank = "Thiếu úy",
            Unit = "Khoa Công nghệ thông tin",
            DateOfBirth = new DateTime(2000, 5, 12),
            Sessions =
            {
                new LoginSession { Device = "Chrome · Windows 11", IpAddress = "10.0.0.15", LastActive = DateTime.Now, IsCurrent = true },
                new LoginSession { Device = "Safari · iPhone", IpAddress = "10.0.2.41", LastActive = DateTime.Now.AddDays(-1) },
                new LoginSession { Device = "Edge · Windows 10", IpAddress = "10.0.1.7", LastActive = DateTime.Now.AddDays(-5) },
            }
        };
    }

    public static string Hash(string input) =>
        Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(input)));

    public void UpdateProfile(string displayName, string tagline)
    {
        Current.DisplayName = displayName.Trim();
        Current.Tagline = tagline.Trim().TrimStart('#');
    }

    public void UpdatePersonal(string fullName, string email, string phone, string unit, string rank, DateTime? dob)
    {
        Current.FullName = fullName.Trim();
        Current.Email = email.Trim();
        Current.Phone = phone.Trim();
        Current.Unit = unit.Trim();
        Current.Rank = rank.Trim();
        Current.DateOfBirth = dob;
    }

    /// <returns>null nếu thành công, ngược lại là thông báo lỗi.</returns>
    public string? ChangePassword(string oldPassword, string newPassword)
    {
        if (Hash(oldPassword) != Current.PasswordHash) return "Mật khẩu hiện tại không đúng.";
        if (newPassword.Length < 8) return "Mật khẩu mới phải có ít nhất 8 ký tự.";
        if (!newPassword.Any(char.IsDigit) || !newPassword.Any(char.IsLetter))
            return "Mật khẩu mới phải gồm cả chữ và số.";
        Current.PasswordHash = Hash(newPassword);
        return null;
    }

    public void SetMfa(bool enabled) => Current.MfaEnabled = enabled;

    public void SignOutSession(Guid id) => Current.Sessions.RemoveAll(s => s.Id == id && !s.IsCurrent);

    public void SignOutOthers() => Current.Sessions.RemoveAll(s => !s.IsCurrent);
}
