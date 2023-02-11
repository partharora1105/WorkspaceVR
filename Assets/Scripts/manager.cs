using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class manager : MonoBehaviour
{
    [SerializeField] private GameObject customer;
    [SerializeField] private GameObject managerAvatar;
    [SerializeField] private GameObject uiCanvas;
    [SerializeField] private TextMeshProUGUI txt;
    [SerializeField] private GameObject yellowCover;
    [SerializeField] private GameObject latte;
    public InputActionReference trigger = null;
    public InputActionReference primary = null;
    public InputActionReference secondary = null;
    public InputActionReference grip = null;
    private Vector3 startPos;
    [Header("Audio Clips")]
    [SerializeField] private AudioClip managerInstructs;
    [SerializeField] private AudioClip customerThanks;
    [SerializeField] private AudioClip customerAppropriate1;
    [SerializeField] private AudioClip customerAppropriate2;
    [SerializeField] private AudioClip customerInappropriate;
    [SerializeField] private AudioClip customerYouToo;
    [SerializeField] private AudioClip managerMixer;
    [SerializeField] private AudioClip customerObjects;
    [Header("Development")]
    [SerializeField] private bool appropriate;
    [SerializeField] private bool inappropriate;
    [SerializeField] private bool notAGoodResponse;
    [SerializeField] private bool noResponse;
    public bool next;

    private  Animator customerAnimator;
    private bool isWalking;
    
    private int count;

    private void Awake()
    {
        trigger.action.started += setNext;
        primary.action.started += setAppropriate;
        secondary.action.started += setInappropriate;
        grip.action.started += setNoResponse;
    }

    // Start is called before the first frame update
    void Start()
    {
        customerAnimator = customer.GetComponent<Animator>();
        isWalking = false;
        next = false;
        notAGoodResponse = true;
        count = 1;
        uiCanvas.SetActive(false);
        startPos = customer.transform.position;
        latte.SetActive(false);
        yellowCover.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (isWalking)
        {
            customer.transform.position += new Vector3(0, 0, -0.01f);
        }
        if (next)
        {
            performAction();
            count++;
            next = false;
        }
    }

    private void setNext(InputAction.CallbackContext context)
    {
        next = true;
    }

    private void setAppropriate(InputAction.CallbackContext context) {
        appropriate = true;
        notAGoodResponse = false;
        next = true;
    }
    private void setInappropriate(InputAction.CallbackContext context)
    {
        inappropriate = true;
        notAGoodResponse = false;
        next = true;
    }
    private void setNoResponse(InputAction.CallbackContext context)
    {
        noResponse = true;
        notAGoodResponse = false;
        next = true;
    }

    private void performAction()
    {
        if (count == 1 || count == 11 || count == 6 || count == 16)
        {
            uiCanvas.SetActive(true);
            customer.transform.position = startPos;
        }
        else if (count == 2 || count == 7 || count == 12 || count == 17)
        {
            uiCanvas.SetActive(false);
            managerAvatar.GetComponent<ReadyPlayerMe.VoiceHandler>().PlayAudioClip(managerInstructs);
            managerAvatar.GetComponent<Animator>().SetTrigger("startTalking");
        }
        else if (count == 3 || count == 8 || count == 13 || count == 18)
        {
            startWalking();
        }
        else if (count == 4 || count == 9)
        {
            customer.GetComponent<ReadyPlayerMe.VoiceHandler>().PlayAudioClip(customerThanks);
        }
        else if (count == 5 || count == 10)
        {
            customer.GetComponent<ReadyPlayerMe.VoiceHandler>().PlayAudioClip(customerYouToo); 
        }
        else if (count == 14 || count == 19)
        {
            customer.GetComponent<ReadyPlayerMe.VoiceHandler>().PlayAudioClip(customerObjects);
            customer.GetComponent<Animator>().SetTrigger("startTalking");
            notAGoodResponse = true;
            noResponse = false;
            appropriate = false;
            inappropriate = false;
        }
        else if (count == 15 || count == 20)
        {
            ReadyPlayerMe.VoiceHandler voice = customer.GetComponent<ReadyPlayerMe.VoiceHandler>();
            if (notAGoodResponse) managerAvatar.GetComponent<ReadyPlayerMe.VoiceHandler>().PlayAudioClip(managerMixer);
            else if (appropriate) voice.PlayAudioClip(customerAppropriate1);
            else if (inappropriate) voice.PlayAudioClip(customerInappropriate);
            else if (noResponse) voice.PlayAudioClip(customerInappropriate);
            
        }

        if (count == 1) txt.text = "Scene 1";
        else if (count == 6) txt.text = "Scene 2";
        else if (count == 11) txt.text = "Scene 3";
        else if (count == 16) txt.text = "Scene 4";

        if (count == 6)
        {
            latte.SetActive(true);
            yellowCover.SetActive(false);
        }

    }

    private void startWalking()
    {
        customerAnimator.SetTrigger("startWalking");
        isWalking = true;
        
    }

    private void startTalking()
    {
        customerAnimator.SetTrigger("startWalking");
        isWalking = true;

    }

    public void stopWalking()
    {
        customerAnimator.SetTrigger("stop");
        isWalking = false;
    }

    private void OnDestroy()
    {
        trigger.action.started -= setNext;
        primary.action.started -= setAppropriate;
        secondary.action.started -= setInappropriate;
        grip.action.started -= setNoResponse;
    }
}
