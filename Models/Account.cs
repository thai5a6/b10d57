namespace TaiSanHV.Models;

public class Account
{
    public int Id { get; set; }
    public string Username { get; set; } = "";
    public string PasswordHash { get; set; } = "";

    // Hồ sơ hiển thị (giống Riot ID: Tên + Tagline)
    public string DisplayName { get; set; } = "";
    public string Tagline { get; set; } = "";

    // Thông tin cá nhân
    public string FullName { get; set; } = "";
    public string Email { get; set; } = "";
    public string Phone { get; set; } = "";
    public string StaffCode { get; set; } = "";   // Mã cán bộ / học viên
    public string Rank { get; set; } = "";        // Cấp bậc
    public string Unit { get; set; } = "";        // Đơn vị
    public DateTime? DateOfBirth { get; set; }

    // Bảo mật
    public bool MfaEnabled { get; set; }
    public List<LoginSession> Sessions { get; set; } = new();

    // Tùy chọn thông báo
    public bool NotifyEmail { get; set; } = true;
    public bool NotifyRecovery { get; set; } = true;   // Có đợt thu hồi mới
    public bool NotifyOverdue { get; set; } = true;    // Quá hạn bàn giao
}

public class LoginSession
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Device { get; set; } = "";
    public string IpAddress { get; set; } = "";
    public DateTime LastActive { get; set; }
    public bool IsCurrent { get; set; }
}
