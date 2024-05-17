using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LoginScene : MonoBehaviour
{
    [SerializeField]private UIDocument document;
    private TextField mobileNumberTextField;
    private TextField otpTextField;
    private Button loginButton;
    private Label messageLabel;
    void Start()
    {
        var rootElement = document.rootVisualElement;

        mobileNumberTextField = rootElement.Q<TextField>("MobileNumberTextField");

        otpTextField = rootElement.Q<TextField>("OTPTextField");

        loginButton = rootElement.Q<Button>("LoginButton");

        loginButton.clickable.clicked += OnLoginButtonClicked;

        messageLabel = rootElement.Q<Label>("MessageLabel");

        messageLabel.style.visibility = Visibility.Hidden;
    }

    private void OnLoginButtonClicked()
    {
        Debug.Log(mobileNumberTextField.value + ","+ otpTextField.value);
        string message = "";
        if(mobileNumberTextField.value.Length != 10)
        {
            message += "Enter ten digit mobile number.";
        }
        if(otpTextField.value.Length != 4)
        {
            message += "Enter four digit OTP";
        }
        else if (otpTextField.value != "1234")
        {
            message += "Enter correct OTP";
        }
            if (!string.IsNullOrEmpty(message))
        {
            messageLabel.text = message;
            StartCoroutine(ShowMessage());
        }
        else
        {
            if (otpTextField.value == "1234")
            {
                PlayerPrefs.SetString("UserLogin",mobileNumberTextField.value + "," + otpTextField.value);
                SceneManager.LoadScene((int)Scenes.Dashboard);
            }
        }
    }

    private void OnDestroy()
    {
        loginButton.clickable.clicked -= OnLoginButtonClicked;
    }

    private IEnumerator ShowMessage()
    {
        messageLabel.style.visibility = Visibility.Visible;
        yield return new WaitForSeconds(10f);
        messageLabel.style.visibility = Visibility.Hidden;
    }
}

public enum Scenes
{
    LoginScene,Dashboard,GameScene
}