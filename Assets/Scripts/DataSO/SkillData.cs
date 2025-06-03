using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewSkillData", menuName = "Skills/Skill Data")]
public class SkillData : ScriptableObject
{
    public int skillId; // ID duy nhất cho mỗi kỹ năng
    public string skillName;
    [TextArea(3, 5)]
    public string description;
    public float cooldownTime;
    public Sprite icon;
    // Thêm các trường khác cần thiết cho UI hoặc logic chung
}
