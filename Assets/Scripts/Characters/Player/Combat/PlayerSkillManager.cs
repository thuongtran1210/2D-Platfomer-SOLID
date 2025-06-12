using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{
    [Header("Skill Configuration")]
    [SerializeField] private List<SkillData> skillDataList = new List<SkillData>();

    private Dictionary<int, ISkill> _activeSkills; 
    private IEntity _playerEntity; 
    // Start is called before the first frame update
    void Awake()
    {
        _activeSkills = new Dictionary<int, ISkill>();
        _playerEntity = GetComponent<IEntity>(); 

        if (_playerEntity == null)
        {
            Debug.LogError("PlayerSkillManager requires an IEntity component on the same GameObject!");
            enabled = false;
            return;
        }

        InitializeSkills();
    }

    private void InitializeSkills()
    {
        for (int i = 0; i < skillDataList.Count; i++)
        {
            var skillData = skillDataList[i];
            if (skillData != null)
            {
                var skill = SkillFactory.CreateSkill(skillData);
                if (skill != null)
                {
                    InitializeSkill(i, skillData, skill);
                }
            }
        }
    }

    private void InitializeSkill(int index, SkillData data, ISkill skillInstance)
    {
        if (data == null)
        {
            Debug.LogWarning($"Skill {index} data is missing. Skill will not be initialized.");
            return;
        }

        skillInstance.SkillData = data; 
        _activeSkills.Add(index, skillInstance);
        Debug.Log($"Skill {data.skillName} (ID: {data.skillId}) initialized for index {index}.");
    }
    /// <summary>
    /// Kiểm tra xem một kỹ năng có sẵn sàng không.
    /// </summary>
    /// <param name="skillIndex">Chỉ số của kỹ năng.</param>
    /// <returns>True nếu kỹ năng sẵn sàng, ngược lại False.</returns>
    public bool IsSkillReady(int skillIndex)
    {
        if (_activeSkills.TryGetValue(skillIndex, out ISkill skill))
        {
            return skill.CanUseSkill();
        }
        return false;
    }
    public void PerformSkill(int skillIndex)
    {
        if (_activeSkills.TryGetValue(skillIndex, out ISkill skill))
        {
            if (skill.CanUseSkill())
            {
                skill.PerformSkill(_playerEntity);
            }
            else
            {
                Debug.LogWarning($"Skill {skillIndex} is not ready or cannot be used.");
            }
        }
        else
        {
            Debug.LogWarning($"Skill with index {skillIndex} not found.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var skill in _activeSkills.Values)
        {
            skill.UpdateSkill();
        }
    }
}
