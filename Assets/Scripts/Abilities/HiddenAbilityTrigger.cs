using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class HiddenAbilityTrigger : MonoBehaviour
{
    [SerializeField] private string abilityid;
    [SerializeField, TextArea(1, 3)] private string obtainAnnouncement = "You found a hidden ability!";
    [SerializeField] private GameObject visualCue;
    
    private static Dictionary<string, bool> obtainedAbilities = new Dictionary<string, bool>();
    
    private AbilitySelectIndividualAbilities _ability;
    private bool _inTrigger = false;
    
    void Awake()
    {
        try
        {
            _ability = GameObject.FindWithTag("UICanvas").GetComponentInChildren<AbilitySelectManager>().abilitiesList.ToList().Find(x => x.getAbilityID() == abilityid);
            _ability.setLocked(true);
            if (obtainedAbilities.Count > 0 && obtainedAbilities[abilityid] == true)
            {
                _ability.setLocked(false);
                Destroy(gameObject);
                return;
            }
        }
        catch
        {
            Debug.LogError("HiddenAbilityTrigger couldn't find associated ability");
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (GathererAbilityManager.Controls == null) return;
        GathererAbilityManager.Controls.Gameplay_Gatherer.Interact.performed += OnGathererInteract;
    }

    void OnDisable()
    {
        if (GathererAbilityManager.Controls == null) return;
        GathererAbilityManager.Controls.Gameplay_Gatherer.Interact.performed -= OnGathererInteract;
    }

    private void OnGathererInteract(InputAction.CallbackContext context)
    {
        if (_inTrigger)
        {
            _ability.setLocked(false);
            obtainedAbilities[abilityid] = true;
            AnnouncementManager.Instance.AddAnnouncementToQueue(obtainAnnouncement);
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_inTrigger && collision.gameObject.CompareTag("Player"))
        {
            _inTrigger = true;
            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.bubbleDeactivate, this.transform.position);
            visualCue.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _inTrigger = false;
        visualCue.SetActive(false);
    }
}
