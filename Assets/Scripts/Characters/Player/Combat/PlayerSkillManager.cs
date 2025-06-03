using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{
    [Header("Skill Configuration")]
    [SerializeField] private SkillData healSkillData;
    [SerializeField] private SkillData aoeSkillData;
    [SerializeField] private SkillData invisibilitySkillData;

    private Dictionary<int, ISkill> _activeSkills; 
    private IEntity _playerEntity; 
    // Start is called before the first frame update
    void Awake()
    {
            _activeSkills = new Dictionary<int, ISkill>();
            _playerEntity = GetComponent<IEntity>(); // L?y tham chi?u ??n IEntity c?a PlayerController

            if (_playerEntity == null)
            {
                Debug.LogError("PlayerSkillManager requires an IEntity component on the same GameObject!");
                enabled = false;
                return;
            }

            // Kh?i t?o các k? n?ng v?i SkillData t??ng ?ng
            // Skill Index (key trong Dictionary) có th? trùng v?i SkillId ho?c là m?t ch? s? slot

            InitializeSkill(0, healSkillData, new HealSkill(healSkillData));
            InitializeSkill(1, aoeSkillData, new AoESkill(aoeSkillData));
            InitializeSkill(2, invisibilitySkillData, new InvisibilitySkill(invisibilitySkillData));
    }
    private void InitializeSkill(int index, SkillData data, ISkill skillInstance)
    {
        if (data == null)
        {
            Debug.LogWarning($"Skill {index} data is missing. Skill will not be initialized.");
            return;
        }

        skillInstance.SkillData = data; // Gán SkillData cho instance c?a skill
        _activeSkills.Add(index, skillInstance);
        Debug.Log($"Skill {data.skillName} (ID: {data.skillId}) initialized for index {index}.");
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
