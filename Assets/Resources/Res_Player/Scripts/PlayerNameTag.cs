using UnityEngine;
using TMPro;
using Photon.Pun;

public class PlayerNameTag : MonoBehaviourPun
{
    [Header("UI Reference")]
    public TMP_Text nameText;
    
    private Camera _localCamera;

    void Start()
    {
        if (photonView.Owner != null)
        {
            nameText.text = photonView.Owner.NickName;
            
            if (photonView.IsMine)
            {
                nameText.color = Color.green;
                // Optional: Hide your own name tag from your own view
                // nameText.gameObject.SetActive(false); 
            }
        }
    }

    void LateUpdate()
    {
        // 1. Find the local camera if we don't have it yet
        if (_localCamera == null)
        {
            _localCamera = Camera.main;
            if (_localCamera == null) return;
        }

        // 2. Make ONLY the name tag rotate, not the whole player
        // We use the camera's rotation so the text stays flat relative to the screen
        if (nameText != null)
        {
            nameText.transform.rotation = _localCamera.transform.rotation;
        }
    }
}
