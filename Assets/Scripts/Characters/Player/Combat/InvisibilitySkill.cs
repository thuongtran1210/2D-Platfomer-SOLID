using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibilitySkill : BaseSkill
{
    [SerializeField] private float invisibilityDuration = 5f;

    // Constructor
    public InvisibilitySkill(SkillData data) { SkillData = data; }
    public InvisibilitySkill(float duration, SkillData data)
    {
        this.invisibilityDuration = duration;
        SkillData = data;
    }
    public override void PerformSkill(IEntity entity)
    {
        if (!CanUseSkill())
        {
            Debug.Log($"Invisibility Skill ({_skillData.skillName}) is on cooldown.");
            return;
        }

        Debug.Log($"Player ({entity.GetType().Name}) used Invisibility Skill for {invisibilityDuration} seconds!");

        // Logic để thực hiện tàng hình
        MonoBehaviour userMono = entity as MonoBehaviour;
        if (userMono != null)
        {
            // Kích hoạt tàng hình thông qua Coroutine
            userMono.StartCoroutine(HandleInvisibility(userMono.gameObject.GetComponent<SpriteRenderer>(),
                invisibilityDuration));
        }
        else
        {
            Debug.LogWarning("User is not a MonoBehaviour for InvisibilitySkill.");
        }
        ResetCooldown();
    }
    private IEnumerator HandleInvisibility(SpriteRenderer renderer, float duration)
    {
        if (renderer != null)
        {
            Color originalColor = renderer.color;
            renderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.3f); // Làm mờ
            // Hoặc renderer.enabled = false; để ẩn hoàn toàn
        }
        yield return new WaitForSeconds(duration);
        if (renderer != null)
        {
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 1f); // Trở lại bình thường
            // Hoặc renderer.enabled = true;
        }
        Debug.Log("Invisibility ended.");
    }
}
