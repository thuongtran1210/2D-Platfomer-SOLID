
public interface ISkill 
{
    // ID của kỹ năng, có thể trùng với SkillData.Id
    int SkillId { get; }

    // Tham chiếu đến dữ liệu kỹ năng (từ ScriptableObject)
    SkillData SkillData { get; set; }

    /// <summary>
    /// Kiểm tra xem kỹ năng có thể được sử dụng ngay bây giờ không.
    /// </summary>
    bool CanUseSkill();

    /// <summary>
    /// Thực hiện hành động của kỹ năng.
    /// </summary>
    /// <param name="enity">Thực thể sử dụng kỹ năng.</param>
    void PerformSkill(IEntity entity);

    /// <summary>
    /// Cập nhật trạng thái của kỹ năng (ví dụ: cooldown).
    /// </summary>
    void UpdateSkill();

    /// <summary>
    /// Lấy thời gian cooldown hiện tại còn lại.
    /// </summary>
    float GetCurrentCooldown();
}
