using TMPro;
using UnityEngine;

public class NoticeUI : MonoBehaviour
{
    public static NoticeUI Instance;
    [SerializeField] TMP_Text Notice;

    private void Awake()
    {
        Instance = this;
    }

    public void SetNotice(string content)
    {
        Notice.text = content;
    }
}
