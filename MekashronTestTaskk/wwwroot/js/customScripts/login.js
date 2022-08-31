(function () {

    login();
})();

function login() {

    document.getElementById("submitBtn").addEventListener("click", function () {

        var userName = document.getElementById("userNameInput").value;
        var password = document.getElementById("passwordInput").value;

        sendLoginAjax(userName, password);
    });
};

function sendLoginAjax(userName, password) {

    var params = "UserName=" + userName + "&Password=" + password + "&IPs=";

    // Bind the FormData object and the form element
    const formData = new FormData();

    formData.append("func", "Login");
    formData.append("params", params);

    fetch("http://isapi.mekashron.com/soapclient/soapclient.php?URL=http://isapi.icu-tech.com/icutech-test.dll%2Fwsdl%2FIICUTech", {
        method: 'POST',
        body: formData
    }).then(result => result.json()).then(
        (result) => {
            console.log(result);
        }
    );
}