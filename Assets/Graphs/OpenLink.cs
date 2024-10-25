using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using UnityEngine;
using UnityEngine.EventSystems;

public class OpenLink : MonoBehaviour, IPointerClickHandler {
    public string url = "";
    public bool sendMail = false;
    private MailMessage mail = new MailMessage();

    private void Start() {
    }
    public void OnPointerClick(PointerEventData eventData) {
        Tween.Bounce(transform);
        if (sendMail) {
            SendEmail();
            return;
        }
        Application.OpenURL(url);
    }
    public void SendEmail() {
        string email = MissionIntroDisplay.Instance.mail_adress;
        string subject = MyEscapeURL(MissionIntroDisplay.Instance.mail_subject);
        string body = MyEscapeURL("");
        Application.OpenURL("mailto:" + email + "?subject=" + subject + "&amp;body=" + body);
    }
    string MyEscapeURL(string URL) {
        return WWW.EscapeURL(URL).Replace("+", "%20");
    }
}
