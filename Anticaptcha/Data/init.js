var antcptAccountKeyDiv = document.getElementById('anticaptcha-imacros-account-key');
if (!antcptAccountKeyDiv) {
    antcptAccountKeyDiv = document.createElement('div');
    // ��������� ���� ��� Anti-Captcha API ����
    antcptAccountKeyDiv.innerHTML ='YOUR-ANTI-CAPTCHA-API-KEY';
    antcptAccountKeyDiv.style.display = "none";
    antcptAccountKeyDiv.id = 'anticaptcha-imacros-account-key';
    document.body.appendChild(antcptAccountKeyDiv);
}