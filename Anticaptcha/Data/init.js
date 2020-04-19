var antcptAccountKeyDiv = document.getElementById('anticaptcha-imacros-account-key');
if (!antcptAccountKeyDiv) {
    antcptAccountKeyDiv = document.createElement('div');
    // Поместите сюда ваш Anti-Captcha API ключ
    antcptAccountKeyDiv.innerHTML ='YOUR-ANTI-CAPTCHA-API-KEY';
    antcptAccountKeyDiv.style.display = "none";
    antcptAccountKeyDiv.id = 'anticaptcha-imacros-account-key';
    document.body.appendChild(antcptAccountKeyDiv);
}